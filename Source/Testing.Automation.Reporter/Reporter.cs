using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter
{
    [Binding]
    public static partial class Reporter
    {
        public static readonly List<ReporterManager> reporters = new List<ReporterManager>();

        public static string GetResult(ReportItem item)
        {
            return string.Format("[{0}] in {1}ms", item.Result, (item.EndTime - item.StartTime).Milliseconds);
        }


        /// <summary>
        ///     Vrati aktualny datum a cas, ktory sa pouzije pocas vykonavania testu. Moze byt nastavelny na pevny cas <see cref="FixedRunTime" />
        /// </summary>
        internal static DateTime CurrentRunTime
        {
            get
            {
                if (FixedRunTime.HasValue)
                {
                    return FixedRunTime.Value;
                }
                return DateTime.Now;
            }
        }

        public static DateTime? FixedRunTime { get; set; }

        internal static Step CreateStep(DateTime starttime, MethodBase method, params object[] args)
        {
            var methodName = method.Name;

            var step = new Step
            {
                Title = method.Name,
                Steps = new List<Step>(),
                StartTime = starttime
            };

            var attr = method.GetCustomAttributes(true).OfType<StepDefinitionBaseAttribute>().FirstOrDefault();
            if (attr != null)
            {
                // Handle regex style
                if (!String.IsNullOrEmpty(attr.Regex))
                {
                    step.Title = attr.Regex;

                    for (var i = 0; i < args.Length; i++)
                    {
                        var arg = args[i];
                        var table = arg as Table;
                        if (table != null)
                        {
                            step.Table = new TableParam
                            {
                                Columns = table.Header.ToList(),
                                Rows = table.Rows.Select(x => x.Keys.ToDictionary(
                                    k => k,
                                    k => x[k]
                                    )).ToList()
                            };
                            

                        }
                        else
                        {
                            var titleRegex = new Regex(step.Title);
                            var match = titleRegex.Match(step.Title);
                            if (match.Groups.Count > 1)
                            {
                                step.Title = step.Title.ReplaceFirst(match.Groups[1].Value, args[i].ToString());
                            }
                            else
                            {
                                step.MultiLineParameter = args[i].ToString();
                            }
                        }
                    }
                }
                else
                {
                    if (methodName.Contains('_'))
                    {
                        // underscore style
                        step.Title = methodName.Replace("_", " ");
                        step.Title = step.Title.Substring(step.Title.IndexOf(' ') + 1);

                        var methodInfo = method as MethodInfo;
                        for (var i = 0; i < args.Length; i++)
                        {
                            var arg = args[i];
                            var table = arg as Table;

                            if (table != null)
                            {
                                step.Table = new TableParam
                                {
                                    Columns = table.Header.ToList(),
                                    Rows = table.Rows.Select(x => x.Keys.ToDictionary(
                                        k => k,
                                        k => x[k]
                                        )).ToList()
                                };
                            }
                            else
                            {
                                var name = methodInfo.GetParamName(i).ToUpper();
                                var value = arg.ToString();
                                if (step.Title.Contains(name + " "))
                                {
                                    step.Title = step.Title.ReplaceFirst(name + " ", value + " ");
                                }
                                else
                                {
                                    step.Title = step.Title.ReplaceFirst(" " + name, " " + value);
                                }
                            }
                        }
                    }
                    else
                    {
                        // pascal naming style
                        throw new NotSupportedException("Pascal naming style not supported yet");
                    }
                }
            }

            return step;
        }

        internal static void ExecuteStep(Action action, params object[] args)
        {
            ExecuteStep(action, null, args);
        }

        internal static void ExecuteStep(Action action, MethodBase methodBase, params object[] args)
        {
            methodBase = methodBase ?? action.Method;

            var currentSteps = new Dictionary<ReporterManager, Step>();

            var starttime = CurrentRunTime;
            foreach (var reporter in GetAll())
            {
                currentSteps.Add(reporter, reporter.CurrentStep);

                var step = CreateStep(starttime, methodBase, args);

                var stepContainer = reporter.CurrentStep ?? reporter.CurrentScenarioBlock;
                stepContainer.Steps.Add(step);
                reporter.CurrentStep = step;

                if (reporter.CurrentScenarioBlock == reporter.CurrentScenario.Given)
                {
                    reporter.MethodStepGiven = reporter.ScenarioTest.CreateNode<AventStack.ExtentReports.Gherkin.Model.Given>(reporter.CurrentStep.Title);
                }
                else if (reporter.CurrentScenarioBlock == reporter.CurrentScenario.When)
                {
                    reporter.MethodStepWhen = reporter.MethodStepGiven.CreateNode<AventStack.ExtentReports.Gherkin.Model.When>(reporter.CurrentStep.Title);
                }
                else if (reporter.CurrentScenarioBlock == reporter.CurrentScenario.Then)
                {
                    reporter.MethodStepThen = reporter.MethodStepWhen.CreateNode<AventStack.ExtentReports.Gherkin.Model.Then>(reporter.CurrentStep.Title);
                }
       
                OnStartedStep(reporter);
            }

            Exception actionException = null;
            try
            {
                if (!action.Method.GetParameters().Any())
                {
                    action.Method.Invoke(action.Target, null);
                }
                else
                {
                    action.Method.Invoke(action.Target, args);
                }
            }
            catch (Exception ex)
            {
                if (ex is TargetInvocationException && ex.InnerException != null)
                {
                    // Exceptions thrown by ReportingMessageSink are wrapped in a TargetInvocationException
                    actionException = ex.InnerException;
                }
                else
                {
                    actionException = ex;
                }
            }
            finally
            {
                var endtime = CurrentRunTime;

                TestResult testResult;
                if (actionException is PendingStepException)
                {
                    testResult = TestResult.Pending;
                }
                else if (actionException != null)
                {
                    testResult = TestResult.Error;
                }
                else
                {
                    testResult = TestResult.OK;
                }

                foreach (var reporter in GetAll())
                {
                    reporter.CurrentStep.EndTime = endtime;
                    reporter.CurrentStep.Result = testResult;
                    reporter.CurrentStep.Exception = actionException.ToExceptionInfo();
                    OnFinishedStep(reporter);
                    //reporter.CurrentStep = currentSteps[reporter];
                }

                if (testResult == TestResult.OK) { Pass("TESTING: SUCCESS", CodeBlockType.Label); }
                else if (testResult == TestResult.Error)
                {
                    Fail(reporters[0].CurrentStep.Exception);
                }
                else if (testResult == TestResult.Pending)
                {
                    Skip("Skip test");
                }
            }
        }


        public static void Info(string message)
        {
            Log(Status.Info, message);
        }


        public static void Info(string message, CodeBlockType codeBlockType = CodeBlockType.None)
        {
            Log(Status.Info, message, codeBlockType, null);
        }


        public static void Pass(string message)
        {
            Log(Status.Pass, message);
        }


        public static void Pass(string message, CodeBlockType codeBlockType = CodeBlockType.None)
        {
            Log(Status.Pass, message, codeBlockType, null);
        }


        public static void Error(string message)
        {
            Log(Status.Error, message);
        }


        public static void Error(string message, ExceptionInfo exception = null)
        {
            Log(Status.Error, message, exception);
        }


        public static void Error(string message, CodeBlockType codeBlockType = CodeBlockType.None, ExceptionInfo exception = null)
        {
            Log(Status.Error, message, codeBlockType, exception);
        }


        public static void Error(ExceptionInfo exception)
        {
            Log(Status.Error, null, exception);
        }


        public static void Fail(string message)
        {
            Log(Status.Fail, message);
        }


        public static void Fail(string message, ExceptionInfo exception = null)
        {
            Log(Status.Fail, message, exception);
        }


        public static void Fail(string message, CodeBlockType codeBlockType = CodeBlockType.None, ExceptionInfo exception = null)
        {
            Log(Status.Fail, message, codeBlockType, exception);
        }


        public static void Fail(ExceptionInfo exception)
        {
            Log(Status.Fail, null, exception);
        }


        public static void Fatal(string message)
        {
            Log(Status.Fatal, message);
        }


        public static void Fatal(ExceptionInfo exception)
        {
            Log(Status.Fatal, null, exception);
        }


        public static void Fatal(string message, ExceptionInfo exception = null)
        {
            Log(Status.Fatal, message, exception);
        }


        public static void Fatal(string message, CodeBlockType codeBlockType = CodeBlockType.None, ExceptionInfo exception = null)
        {
            Log(Status.Fatal, message, codeBlockType, exception);
        }


        public static void Warning(string message)
        {
            Log(Status.Warning, message);
        }


        public static void Warning(string message, ExceptionInfo exception = null)
        {
            Log(Status.Warning, message, exception);
        }


        public static void Warning(string message, CodeBlockType codeBlockType = CodeBlockType.None, ExceptionInfo exception = null)
        {
            Log(Status.Warning, message, codeBlockType, exception);
        }


        public static void Warning(ExceptionInfo exception)
        {
            Log(Status.Warning, null, exception);
        }


        public static void Skip(string message)
        {
            Log(Status.Skip, message);
        }


        public static void AddScreenCapture(string screenCapturePath)
        {
            var logger = Reporter.reporters[0];

            if (logger.CurrentScenarioBlock == logger.CurrentScenario.Given)
            {
                logger.MethodStepGiven.AddScreenCaptureFromPath(screenCapturePath);
            }
            else if (logger.CurrentScenarioBlock == logger.CurrentScenario.When)
            {
                logger.MethodStepWhen.AddScreenCaptureFromPath(screenCapturePath);
            }
            else if (logger.CurrentScenarioBlock == logger.CurrentScenario.Then)
            {
                logger.MethodStepThen.AddScreenCaptureFromPath(screenCapturePath);
            }
        }


        private static void Log(Status status, string message)
        {
            Log(new LogEventInfo
            {
                Status = status,
                Message = message,
                CodeBlockType = CodeBlockType.None
            });
        }


        private static void Log(Status status, string message, ExceptionInfo exception)
        {
            Log(new LogEventInfo
            {
                Status = status,
                Message = message,
                Exception = exception
            });
        }


        private static void Log(Status status, string message, CodeBlockType codeBlockType, ExceptionInfo exception)
        {
            Log(new LogEventInfo
            {
                Status = status,
                Message = message,
                CodeBlockType = codeBlockType,
                Exception = exception
            });
        }


        private static void Log(LogEventInfo eventInfo)
        {
            var logger = Reporter.reporters[0];

            string message = ((eventInfo.Message != null) ? eventInfo.Message : eventInfo.Exception.Message);

            if (eventInfo.CodeBlockType != CodeBlockType.None)
            {
                if (logger.CurrentScenarioBlock == logger.CurrentScenario.Given)
                {
                    if (eventInfo.CodeBlockType == CodeBlockType.Json)
                    {
                        logger.MethodStepGiven.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Json));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Xml)
                    {
                        logger.MethodStepGiven.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Xml));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Label)
                    {
                        ExtentColor color = ((eventInfo.Status == Status.Pass) ? ExtentColor.Green : ExtentColor.Grey);
                        logger.MethodStepGiven.Log(eventInfo.Status, MarkupHelper.CreateLabel(message, color));
                    }
                }
                else if (logger.CurrentScenarioBlock == logger.CurrentScenario.When)
                {
                    if (eventInfo.CodeBlockType == CodeBlockType.Json)
                    {
                        logger.MethodStepWhen.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Json));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Xml)
                    {
                        logger.MethodStepWhen.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Xml));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Label)
                    {
                        ExtentColor color = ((eventInfo.Status == Status.Pass) ? ExtentColor.Green : ExtentColor.Grey);
                        logger.MethodStepWhen.Log(eventInfo.Status, MarkupHelper.CreateLabel(message, color));
                    }
                }
                else if (logger.CurrentScenarioBlock == logger.CurrentScenario.Then)
                {
                    if (eventInfo.CodeBlockType == CodeBlockType.Json)
                    {
                        logger.MethodStepThen.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Json));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Xml)
                    {
                        logger.MethodStepThen.Log(eventInfo.Status, MarkupHelper.CreateCodeBlock(message, CodeLanguage.Xml));
                    }
                    if (eventInfo.CodeBlockType == CodeBlockType.Label)
                    {
                        ExtentColor color = ((eventInfo.Status == Status.Pass) ? ExtentColor.Green : ExtentColor.Grey);
                        logger.MethodStepThen.Log(eventInfo.Status, MarkupHelper.CreateLabel(message, color));
                    }
                }
            }
            else
            {
                if (logger.CurrentScenarioBlock == logger.CurrentScenario.Given)
                {
                    logger.MethodStepGiven.Log(eventInfo.Status, message);
                }
                else if (logger.CurrentScenarioBlock == logger.CurrentScenario.When)
                {
                    logger.MethodStepWhen.Log(eventInfo.Status, message);
                }
                else if (logger.CurrentScenarioBlock == logger.CurrentScenario.Then)
                {
                    logger.MethodStepThen.Log(eventInfo.Status, message);
                }
            }
        }


        public static ReporterManager Add(ReporterManager reporter)
        {
            reporters.Add(reporter);
            return reporter;
        }

        public static void EndReport()
        {
            reporters[0].ExtentReport.Flush();
        }

        public static IEnumerable<ReporterManager> GetAll()
        {
            return reporters;
        }
    }
}
