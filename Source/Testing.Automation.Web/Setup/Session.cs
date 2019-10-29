using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Testing.Automation.Web.Interfaces;
using Testing.Automation.Web.Extensions;
using Testing.Automation.Common.Helper;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System.Threading;
using Testing.Automation.Common;
using System.Web;
using System.Diagnostics;

namespace Testing.Automation.Web.Setup
{
    public class Session : IConfigurable
    {
        public Session(IDriverEnvironment environment)
            : this(environment, new Settings())
        {
        }

        public Session(IDriverEnvironment environment, ISettings settings)
        {
            Settings = settings;
            Driver = environment?.CreateWebDriver();
            settings.TestRunStartTime = System.DateTime.Now.AddSeconds(-10);
        }

        public virtual ISettings Settings { get; private set; }

        public virtual IWebDriver Driver { get; protected set; }

        public virtual IMonkey Monkey { get; protected set; }

        /// <summary>
        ///     Navigacia na zakladnu adresu webovej aplikacie.
        /// </summary>
        /// <typeparam name="TBlock"></typeparam>
        /// <param name="url"> Url adresa.</param>
        /// <returns></returns>
        public virtual TBlock NavigateTo<TBlock>(string url = null) where TBlock : IBlock
        {
            var baseUrl = string.Empty;

            if (url == null)
            {
                baseUrl = BaseConfiguration.GetUrlValue;
                if(baseUrl == null)
                {
                    throw new ArgumentException("The base URL cannot be null. Please check settings file.");
                }
            }
            else
            {
                baseUrl = url;
            }

            if (Driver.Url == "about:blank")
            {
                Driver.Navigate().GoToUrl(baseUrl);
                Settings.ShouldLogin = true;

                Reporter.Reporter.Info("Url: " + baseUrl, Reporter.CodeBlockType.Label);
            }

            BaseConfiguration.WorkingUrl = baseUrl;

            return CurrentBlock<TBlock>();
        }

        /// <summary>
        ///     Navigacia na stranku webovej aplikacie, ktora sa bude testovat.
        /// </summary>
        /// <typeparam name="TBlock"></typeparam>
        /// <param name="modul">Modul aplikacie</param>
        /// <param name="page">Stranka aplikacie</param>
        /// <param name="useDecodeUrl"></param>
        /// <returns></returns>
        public virtual TBlock NavigateTo<TBlock>(bool useDecodeUrl) where TBlock: IBlock
        {          
            //Zvolim jazykovu mutaciu
            string lang = BaseConfiguration.Culture;   

            var relUrl = RelativeUrl();

            if (relUrl == null)
            {
                throw new ArgumentException("The relative URL cannot be null. Please check settings file.");
            }
            if (Driver.Url != string.Concat(BaseConfiguration.WorkingUrl, "#", relUrl))
            {
                if (lang != "Sk")
                {
                    Driver.SwitchTo().DefaultContent();

                    string langText = lang == "En" ? "English" : "Український";

                    Driver.FindElement(OpenQA.Selenium.By.XPath("//div[@id='cphShellContent_Header1_languages']/div/span[@data-qtip=\"" + langText + "\"]"))
                        .Click();
                    Thread.Sleep(2000);
                }

                var urlNavigateTo = relUrl;
                if (useDecodeUrl)
                {
                    urlNavigateTo = HttpUtility.UrlDecode(string.Concat(BaseConfiguration.WorkingUrl, "#", urlNavigateTo));
                }
                Driver.Navigate().GoToUrl(urlNavigateTo);
                Reporter.Reporter.Info("Navigate: " + urlNavigateTo, Reporter.CodeBlockType.Label);
            }   
            return CurrentBlock<TBlock>();
        }

        public virtual string RelativeUrl(bool useDecodeUrl = true)
        {
            var eleName = $"/configuration/{BaseConfiguration.FeatureName}/{BaseConfiguration.ScenarioName}/Url";
            return XmlHelper.GetElementValue(XDocument.Load(
                BaseConfiguration.PathToSettingsFile),
                eleName,
                "Value");
        }

        public virtual TBlock CurrentBlock<TBlock>(IWebElement tag = null) where TBlock : IBlock
        {
            var type = typeof (TBlock);
            IList<Type> constructorSignature = new List<Type> {typeof (Session)};
            IList<object> constructorArgs = new List<object> {this};

            if (typeof (ISpecificBlock).IsAssignableFrom(typeof (TBlock)))
            {
                constructorSignature.Add(typeof (IWebElement));
                constructorArgs.Add(tag);
            }

            var constructor = type.GetConstructor(constructorSignature.ToArray());

            if (constructor == null)
            {
                throw new ArgumentException(
                    $"The result type specified ({type}) is not a valid block. It must have a constructor that takes only a session.");
            }

            return (TBlock) constructor.Invoke(constructorArgs.ToArray());
        }

        public virtual void EndWebDriverInstance()
        {
            if (Driver != null)
            {
                Driver.Quit();
                Driver.Dispose();
                Driver = null;
            }
        }

        public virtual void CleanWebDriverProfile()
        {
            if (BaseConfiguration.DeleteTempFilesEnabled)
            {
                Process.Start("CMD.exe", "/C RMDIR /Q /S %temp%\"");
            }
        }

        public virtual void FinishHim()
        {
            if (Settings.IsTestFailed)
            {
                Settings.KillProcess();

                Driver.Quit();
                Driver.Dispose();
                Driver = null;
                Settings.IsTestFailed = false;       
            }
        }

        public virtual Session CaptureScreen()
        {
            var filename = $"{CallStack.GetCallingMethod().GetFullName()}.png";
            var path = Path.Combine(Settings.ScreenCapturePath, filename);
            return CaptureScreen(path);
        }

        public virtual Session CaptureScreen(string path)
        {
            var screenshot = Driver.TakeScreenshot();

            var extension = Path.GetExtension(path);

            if (string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(path + "." + ImageFormat.Png);
            }
            else if (string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase)
                     || string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(path + "." + ImageFormat.Jpeg);
            }
            else if (string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(path + "." + ImageFormat.Bmp);
            }
            else if (string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase))
            {
                screenshot.SaveAsFile(path + "." + ImageFormat.Gif);
            }
            else
            {
                throw new ArgumentException("Unable to determine image format. The supported formats are BMP, GIF, JPEG and PNG.", "path");
            }

            return this;
        }

        public virtual T ExecuteJavaScript<T>(string script, params object[] args)
        {
            return Driver.ExecuteJavaScript<T>(script, args);
        }     
    }

    public class Session<TDriverEnvironment> : Session
        where TDriverEnvironment : IDriverEnvironment, new()
    {
        public Session() : base(new TDriverEnvironment())
        {
        }

        public Session(ISettings settings) : base(new TDriverEnvironment(), settings)
        {

        }
    }    
}