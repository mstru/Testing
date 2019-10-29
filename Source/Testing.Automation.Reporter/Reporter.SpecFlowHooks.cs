using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Testing.Automation.Reporter.Model;

using ScenarioBlock = Testing.Automation.Reporter.Model.ScenarioBlock;

namespace Testing.Automation.Reporter
{
    public static partial class Reporter
    {
        private static bool _testrunIsFirstFeature;

        [BeforeTestRun]
        internal static void BeforeTestRun()
        {
            _testrunIsFirstFeature = true;
        }


        [BeforeFeature]
        internal static void BeforeFeature()
        {
            var starttime = CurrentRunTime;

            if (_testrunIsFirstFeature)
            {
                reporters.Add(new ExtentReporter(FeatureContext.Current.FeatureInfo.Title));

                foreach (var report in reporters)
                {
                    report.Report = new Report
                    {                       
                        Features = new List<Feature>(),
                        Generator = report.Name,
                        StartTime = starttime
                    };                    

                    OnStartedReport(report);
                }

                _testrunIsFirstFeature = false;
            }

            foreach (var report in reporters)
            {
                var feature = new Feature
                {
                    Tags = new List<string>(FeatureContext.Current.FeatureInfo.Tags),
                    Scenarios = new List<Scenario>(),
                    StartTime = starttime,
                    Title = FeatureContext.Current.FeatureInfo.Title,
                    Description = FeatureContext.Current.FeatureInfo.Description,
                    Nodes = new List<AventStack.ExtentReports.ExtentTest>()
                };

                report.Report.Features.Add(feature);
                report.CurrentFeature = feature;

                if (report.ExtentReport == null)
                {
                    throw new Exception("Potrebne najprv inicializovat ExtenReport nastavitelne v app.config. sekcia reporting");
                }

                report.ParentTest = report.ExtentReport.CreateTest(feature.Title);

                OnStartedFeature(report);
            }
        }


        [BeforeScenario]
        internal static void BeforeScenario()
        {
            var starttime = CurrentRunTime;

            foreach (var report in reporters)
            {
                var scenario = new Scenario
                {
                    Tags = new List<string>(ScenarioContext.Current.ScenarioInfo.Tags),
                    Given = new ScenarioBlock { Steps = new List<Step>() },
                    When = new ScenarioBlock { Steps = new List<Step>() },
                    Then = new ScenarioBlock { Steps = new List<Step>() },
                    StartTime = starttime,
                    Title = ScenarioContext.Current.ScenarioInfo.Title
                };
   
                report.CurrentFeature.Scenarios.Add(scenario);
                report.CurrentScenario = scenario;

                OnStartedScenario(report);

                string tag = report.CurrentScenario.Tags[0];

                bool containsNodeName = report.CurrentFeature.Nodes.Any(item => item.Model.Name == tag);
       
                if (containsNodeName)
                {
                    foreach (var node in report.CurrentFeature.Nodes)
                    {
                        if (node.Model.Name == tag)
                        {
                            report.Node = node;
                            break;
                        }
                    }
                }
                else
                {
                    report.Node =
                        report.ParentTest.CreateNode<AventStack.ExtentReports.Gherkin.Model.Feature>(tag, report.CurrentFeature.Description);
                    report.Node.AssignCategory(tag);

                    report.CurrentFeature.Nodes.Add(report.Node);
                }

                report.ScenarioTest = report.Node.CreateNode<AventStack.ExtentReports.Gherkin.Model.Scenario>(report.CurrentScenario.Title);
            }
        }


        [BeforeScenarioBlock]
        internal static void BeforeScenarioBlock()
        {
            var starttime = CurrentRunTime;

            foreach (var report in reporters)
            {
                switch (ScenarioContext.Current.CurrentScenarioBlock)
                {
                    case TechTalk.SpecFlow.ScenarioBlock.Given:
                        report.CurrentScenarioBlock = report.CurrentScenario.Given;                       
                        break;
                    case TechTalk.SpecFlow.ScenarioBlock.Then:
                        report.CurrentScenarioBlock = report.CurrentScenario.Then;                        
                        break;
                    case TechTalk.SpecFlow.ScenarioBlock.When:
                        report.CurrentScenarioBlock = report.CurrentScenario.When;
                        break;
                }

                report.CurrentScenarioBlock.StartTime = starttime;
                OnStartedScenarioBlock(report);
            }
        }


        [AfterFeature]
        internal static void AfterFeature()
        {
            foreach (var report in reporters)
            {
                var feature = report.CurrentFeature;

                //var scenarioOutlineGroups = feature.Scenarios.GroupBy(scenario => scenario.Title)
                //    .Where((scenarioGrp, key) => scenarioGrp.Count() > 1)
                //    .Select((scenarioGrp, key) => scenarioGrp.ToList());

                //foreach (var scenarioOutlineGroup in scenarioOutlineGroups)
                //{
                //    for (int i = 0; i < scenarioOutlineGroup.Count(); i++)
                //    {
                //        scenarioOutlineGroup[i].Title = string.Format("{0} (example {1})", scenarioOutlineGroup[i].Title, i + 1);
                //    }
                //}

                feature.EndTime = CurrentRunTime;
                OnFinishedFeature(report);
                report.CurrentFeature = null;
            }
        }


        [AfterScenario]
        internal static void AfterScenario()
        {
            foreach(var report in reporters.ToArray())
            {
                var scenario = report.CurrentScenario;
                scenario.EndTime = CurrentRunTime;
                OnFinishedScenario(report);
                report.CurrentScenario = null;
            }
        }


        [AfterScenarioBlock]
        internal static void AfterScenarioBlock()
        {
            var endtime = CurrentRunTime;
            foreach (var report in reporters)
            {
                var scenarioBlock = report.CurrentScenarioBlock;
                scenarioBlock.EndTime = endtime;
                OnFinishedScenario(report);
                report.CurrentScenarioBlock = null;
            }
        }


        [AfterTestRun]
        internal static void AfterTestRun()
        {
            foreach (var reporter in reporters)
            {
                reporter.Report.EndTime = CurrentRunTime;
                OnFinishedReport(reporter);

                reporter.ExtentReport.Flush();
            }
        }
    }
}
