using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Testing.Automation.Reporter
{
    /// <summary>
    /// http://extentreports.com/docs/versions/4/net/
    /// </summary>
    public class ExtentReporter : ReporterManager
    {
        public ExtentReporter(string featureTitle)
        {
            Start(featureTitle);
        }

        public ExtentHtmlReporter ExtentHtmlReporter { get; private set; }

        private static readonly NameValueCollection ReportSettings = ConfigurationManager.GetSection("reporting") as NameValueCollection;
        private static readonly bool Enabled = ReportSettings != null && bool.Parse(ReportSettings["Enabled"]);
        private static readonly bool ShowSteps = Enabled && bool.Parse(ReportSettings["ShowSteps"]);
        private static readonly bool DarkTheme = Enabled && bool.Parse(ReportSettings["DarkTheme"]);
        private static readonly string Author = ConfigurationManager.AppSettings["Author"];

        public void Start(string featureTitle)
        {
            try
            {
                if (Enabled)
                {
                    string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
                    string actualPath = pth.Substring(0, pth.LastIndexOf("Source"));
                    string projectPath = new Uri(actualPath).LocalPath;
                    string reportPath = projectPath + @"TestResults\Reports\" + featureTitle;

                    if (!Directory.Exists(reportPath)) Directory.CreateDirectory(reportPath);

                    string style = "body {font-family: 'Segoe UI';}";
                    ExtentHtmlReporter = new ExtentHtmlReporter(reportPath + @"\\" + featureTitle + ".html");
                    ExtentHtmlReporter.Config.Theme = DarkTheme ? Theme.Dark : Theme.Standard;
                    ExtentHtmlReporter.Config.CSS = style;
                    ExtentReport = new ExtentReports();
                    ExtentReport.AnalysisStrategy = AnalysisStrategy.BDD;
                    ExtentReport.AddSystemInfo("Host", Environment.MachineName);
                    ExtentReport.AddSystemInfo("Env", ConfigurationManager.AppSettings["Env"]);
                    ExtentReport.AddSystemInfo("User", Environment.UserName);

                    ExtentReport.AttachReporter(ExtentHtmlReporter);
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public void AddMetaData(IEnumerable<object> categories, string author)
        {
            try
            {
                if (categories.Count() > 0 && !string.IsNullOrEmpty(author))
                {
                    ParentTest.AssignCategory(categories.Cast<string>().ToArray());
                    ParentTest.AssignAuthor(author);
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);
            }
        }
    }
}
