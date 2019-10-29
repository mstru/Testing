using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using Testing.Automation.Web.Component;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using Testing.Automation.Reporter.Exceptions;
using Testing.Automation.Common;
using Testing.Automation.Common.Helper;

namespace Testing.Automation.Web.Setup
{
    public class FinishStep : WebBlock
    {
        private Session session;

        public FinishStep(Session session) : base(session)
        {
            this.session = session;
        }

        public void Logging()
        {
            var setting = session.Settings;

            if (Reporter.Reporter.reporters[0].CurrentStep.Exception != null)
            {
                setting.IsTestFailed = true;
                Settings.ReinitializeTestDestroy = true;

                var filePaths = SaveTestDetails(setting, session.Driver);
                string urlToHtml = string.Format(CultureInfo.CurrentCulture, "<a href={0}>{0}</a>", session.Driver.Url);
                Reporter.Reporter.Warning(urlToHtml); 

                AddScreenCapture(filePaths);

                Reporter.Reporter.EndReport();

                //session.EndWebDriverInstance();
                session.CleanWebDriverProfile();
            }
            else
            {
                Reporter.Reporter.EndReport();
            }

            // Vycistenie TestResults/Testing*
            if (BaseConfiguration.DeleteOldTestResultsEnabled)
            {
                try { FileHelper.DeleteDirectoriesOfGivenCreationTime(BaseConfiguration.PathToTestResultFolder, "Testing", TimeSpan.FromMinutes(60)); }
                catch (Exception) { }
            }

            // Vycistenie TestResults/Snapshot/*
            if (BaseConfiguration.DeleteOldSnapshotEnabled)
            {
                try { FileHelper.DeleteFilesOfGivenCreationTime(BaseConfiguration.PathToScreenShotFolder, TimeSpan.FromDays(1)); }
                catch (Exception) { }
            }

            // Vycistenie TestResults/ Download/*
            if (BaseConfiguration.DeleteOldDownloadFilesEnabled)
            {
                try { FileHelper.DeleteFiles(BaseConfiguration.PathToDownloadFolder); }
                catch (Exception) { }
            }

            if (ClearVerifyFailed(setting))
            {
                Assert.Fail("Look at extent report stack trace logs for more details.");
            }
            if (LogJavaScriptErrors(session.Driver))
            {
                Assert.Fail("JavaScript errors found. See the logs for details.");
            }
        }

        public bool LogJavaScriptErrors(IWebDriver driver)
        {

            IEnumerable<LogEntry> jsErrors = null;
            bool javScriptErrors = false;

            // Skontroluje protokol prehliadača pre JavaScript chyby.
            if (BaseConfiguration.JavaScriptErrorLogging)
            {
                try
                {
                    jsErrors =
                         driver.Manage()
                            .Logs.GetLog(LogType.Browser)
                            .Where(x => BaseConfiguration.JavaScriptErrorTypes.Any(e => x.Message.Contains(e)));
                }
                catch (NullReferenceException)
                {
                    return false;
                }

                if (jsErrors.Any())
                {
                    // Zobraziť chyby JavaScript, ak existujú.
                    Reporter.Reporter.Fail(string.Format("JavaScript error(s): {0}", Environment.NewLine + jsErrors.Aggregate(string.Empty, (s, entry) => s + entry.Message + Environment.NewLine)));
                    javScriptErrors = true;
                }
            }
            return javScriptErrors;

        }

        public void AddScreenCapture(string[] filePaths)
        {
            foreach (var filePath in filePaths)
            {
                string relPath = FileHelper.MakeRelativePath(filePath, "Snapshot", @"..\..\");
                Reporter.Reporter.AddScreenCapture(relPath);
            }
        }

        public string[] SaveTestDetails(ISettings settings, IWebDriver driver)
        {
            var screenshots = TakeAndSaveScreenshot(settings, driver);
            var pageSource = SavePageSource(settings, driver);

            var returnList = new List<string>();
            returnList.AddRange(screenshots);
            if (pageSource != null)
            {
                returnList.Add(pageSource);
            }

            return returnList.ToArray();
        }

        public string SavePageSource(ISettings settings, IWebDriver driver)
        {
            if (BaseConfiguration.GetPageSourceEnabled)
            {
                var fileNameShort = Regex.Replace(BaseConfiguration.ScenarioName, "[^0-9a-zA-Z._]+", "_");
                fileNameShort = NameHelper.ShortenFileName(settings.PageSourceFolder, fileNameShort, "_", 255);
                var fileNameWithExtension = string.Format(CultureInfo.CurrentCulture, "{0}{1}", fileNameShort, ".html");
                var path = Path.Combine(settings.PageSourceFolder, fileNameWithExtension);
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                var pageSource = driver.PageSource;
                pageSource = pageSource.Replace("<head>", string.Format(CultureInfo.CurrentCulture, "<head><base href=\"{0}\" target=\"_blank\">", BaseConfiguration.GetUrlValue));
                File.WriteAllText(path, pageSource);
                FileHelper.WaitForFileOfGivenName(BaseConfiguration.LongTimeout, fileNameWithExtension, settings.PageSourceFolder);
                return path;
            }

            return null;
        }

        public string[] TakeAndSaveScreenshot(ISettings settings, IWebDriver driver)
        {
            List<string> filePaths = new List<string>();
            if (BaseConfiguration.FullDesktopScreenShotEnabled)
            {
                filePaths.Add(Save(DoIt(), ImageFormat.Png, settings.ScreenShotFolder, BaseConfiguration.ScenarioName));
            }

            if (BaseConfiguration.SeleniumScreenShotEnabled)
            {
                filePaths.Add(SaveScreenshot(new ErrorDetail(TakeScreenshot(driver), System.DateTime.Now, null, null), settings.ScreenShotFolder, BaseConfiguration.ScenarioName));
            }

            return filePaths.ToArray();
        }

        public Screenshot TakeScreenshot(IWebDriver driver)
        {
            try
            {
                var screenshotDriver = (ITakesScreenshot)driver;
                var screenshot = screenshotDriver.GetScreenshot();
                return screenshot;
            }
            catch (NullReferenceException)
            {
                Reporter.Reporter.Warning("Test failed but was unable to get webdriver screenshot.");
            }
            catch (UnhandledAlertException)
            {
                Reporter.Reporter.Warning("Test failed but was unable to get webdriver screenshot.");
            }
            return null;
        }

        public string SaveScreenshot(ErrorDetail errorDetail, string folder, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}_{2}.png", title, errorDetail.DateTime.ToString("yyyy-MM-dd HH-mm-ss-fff", CultureInfo.CurrentCulture), "browser");
            var correctFileName = Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(CultureInfo.CurrentCulture), string.Empty));
            correctFileName = Regex.Replace(correctFileName, "[^0-9a-zA-Z._]+", "_");
            correctFileName = NameHelper.ShortenFileName(folder, correctFileName, "_", 255);

            var filePath = Path.Combine(folder, correctFileName);

            try
            {
                errorDetail.Screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
                FileHelper.WaitForFileOfGivenName(BaseConfiguration.ShortTimeout, correctFileName, folder);
                return filePath;
            }
            catch (NullReferenceException)
            {
                Reporter.Reporter.Warning("Test failed but was unable to get webdriver screenshot.");
            }

            return null;
        }

        public bool ClearVerifyFailed(ISettings settings)
        {
            if (VerificationException.VerifyMessages.Count.Equals(0))
            {
                return false;
            }
            VerificationException.VerifyMessages.Clear();

            return true;
        }

        /// <summary>
        /// Spravý screenshot.
        /// </summary>
        /// <returns></returns>
        public static Bitmap DoIt()
        {
            var screen = Screen.PrimaryScreen;
            using (var bitmap = new Bitmap(screen.Bounds.Width, screen.Bounds.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    try
                    {
                        graphics.CopyFromScreen(0, 0, 0, 0, screen.Bounds.Size);
                    }
                    catch (Win32Exception)
                    {
                        Reporter.Reporter.Warning("Win32Exception Exception, user is locked out with no access to windows desktop");
                        return null;
                    }
                }

                return (Bitmap)bitmap.Clone();
            }
        }

        /// <summary>
        /// Uloženie spec. bitmap
        /// </summary>
        /// <param name="bitmap">Bitmap súbor.</param>
        /// <param name="format">Formát súboru.</param>
        /// <param name="folder">Zložka.</param>
        /// <param name="title">Title.</param>
        /// <returns>Cesta k súboru bitmap, vráti null v prípade neuloženia.</returns>
        public static string Save(Bitmap bitmap, ImageFormat format, string folder, string title)
        {
            var fileName = string.Format(CultureInfo.CurrentCulture, "{0}_{1}_{2}.png", title, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff", CultureInfo.CurrentCulture), "fullscreen");
            fileName = Regex.Replace(fileName, "[^0-9a-zA-Z._]+", "_");
            fileName = NameHelper.ShortenFileName(folder, fileName, "_", 255);
            var filePath = Path.Combine(folder, fileName);

            if (bitmap == null)
            {
                Reporter.Reporter.Warning("Full screenshot is not saved.");
            }
            else
            {
                bitmap.Save(filePath, format);
                bitmap.Dispose();
                FileHelper.WaitForFileOfGivenName(BaseConfiguration.ShortTimeout, fileName, folder);
                return filePath;
            }
            return null;
        }
    }
}
