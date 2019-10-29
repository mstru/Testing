using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using Testing.Automation.Common.Helper;
using Testing.Automation.Reporter.Exceptions;
using Testing.Automation.Common;

namespace Testing.Automation.Web.Setup
{
    /// <summary>
    ///     A simple in-memory version of the <see cref="ISettings" /> interface.
    /// </summary>
    public class Settings : ISettings
    {
        public bool CaptureScreenOnVerificationFailure { get; set; }
        public DateTime? TestRunStartTime { get; set; }
        public bool IsTestFailed { get; set; }
        public bool ShouldKill { get; set; }
        public bool ShouldLogin { get; set; }
        public static bool ReinitializeTestDestroy { get; set; }

        private string _screenCapturePath;

        private readonly Collection<ErrorDetail> verifyMessages = new Collection<ErrorDetail>();

        /// <summary>
        ///     Initializes a default instance of the <see cref="Settings" /> 
        /// </summary>
        public Settings()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("Source"));
            string projectPath = new Uri(actualPath).LocalPath;
            BaseConfiguration.WorkingDirectory = projectPath;

            ScreenCapturePath = Environment.CurrentDirectory;
            CaptureScreenOnVerificationFailure = false;
            ShouldKill = false;
            IsTestFailed = false;
        }
 
        /// <summary>
        ///     Gets or sets the screen capture output path.
        /// </summary>
        /// <value>
        ///     The screen capture path.
        /// </value>
        public string ScreenCapturePath
        {
            get { return _screenCapturePath; }
            set
            {
                if (Directory.Exists(value) == false)
                {
                    throw new ArgumentException("Not an existing directory.", "value");
                }

                _screenCapturePath = value;
            }
        }

        private readonly List<string> _processesToCheck = new List<string>
        {
             "opera",
             "chrome",
             "firefox",
             "ie",
             "geckodriver",
             "phantomjs",
             "edge",
             "microsoftwebdriver",
             "webdriver",
             "EXCEL"
        };

        /// <summary>
        /// Ukoncenie instancie procesu
        /// </summary>
        public virtual void KillProcess()
        {
            if (IsTestFailed)
            {
                var processes = Process.GetProcesses();
                foreach (var process in processes)
                {
                    try
                    {
                        Debug.WriteLine(process.ProcessName);

                        if (process.StartTime > TestRunStartTime || process.ProcessName == "geckodriver")
                        {
                            ShouldKill = false;
                            foreach (var processName in _processesToCheck)
                            {
                                if (process.ProcessName.ToLower().Contains(processName))
                                {
                                    ShouldKill = true;
                                    break;
                                }
                            }

                            if (ShouldKill)
                            {
                                process.Kill();
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e);
                    }
                }
            }
        }

        /// <summary>
        /// Get názov adresára pre PageSource.
        /// </summary>
        public string PageSourceFolder
        {
            get
            {
                return FileHelper.GetFolder(string.Empty, BaseConfiguration.PathToPageSourceFolder);
            }
        }

        /// <summary>
        /// Get Set názov adresára pre ScreenShot.
        /// </summary>
        public string ScreenShotFolder
        {
            get
            {
                return FileHelper.GetFolder(string.Empty, BaseConfiguration.PathToScreenShotFolder);
            }
        }

        /// <summary>
        /// Získanie všetkých error správ.
        /// </summary>
        public Collection<ErrorDetail> VerifyMessages
        {
            get
            {
                return verifyMessages;
            }
        }
    }
}