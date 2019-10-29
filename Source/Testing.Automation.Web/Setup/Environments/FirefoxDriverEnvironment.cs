using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.IO;
using Testing.Automation.Common.Helper;
using System.Collections.Specialized;
using Testing.Automation.Common;
using System.Configuration;

namespace Testing.Automation.Web.Setup.Environments
{
    public abstract class FirefoxDriverEnvironment<TWebDriver> : IDriverEnvironment
        where TWebDriver : IWebDriver, new()
    {
        protected FirefoxDriverEnvironment() : this(TimeSpan.FromSeconds(5))
        {
        }

        protected FirefoxDriverEnvironment(TimeSpan timeToWait)
        {
            TimeToWait = timeToWait;
            DriverFactory = () => new FirefoxDriver(FirefoxDriverService, SetDriverOptions(new FirefoxOptions { Profile = FirefoxProfile }), TimeSpan.FromSeconds(BaseConfiguration.LongTimeout));
        }

        protected TimeSpan TimeToWait { get; }

        public virtual IWebDriver CreateWebDriver()
        { 
            var driver = DriverFactory();
            //InitializeDriver(driver);
            return driver;
        }

        public virtual IWebDriver CreateWebDriver(string proxy)
        {
            return CreateWebDriver();
        }
 
        protected Func<IWebDriver> DriverFactory { get; set; }

        protected virtual IWebDriver InitializeDriver(IWebDriver driver)
        {
            driver.Manage().Window.Maximize();
            return driver;
        }

        public FirefoxDriverService FirefoxDriverService
        {
            get
            {
                FirefoxDriverService service;

                if (BaseConfiguration.UseCustomProfile)
                {
                    try
                    {
                        var pathCurrentDriver = BaseConfiguration.PathToBrowserProfileFolder;
                        var pathsToDrivers = Directory.GetDirectories(pathCurrentDriver, "Drivers", SearchOption.TopDirectoryOnly);
                        string pathsToDriversString = Convert.ToString(pathsToDrivers[0]);

                        service = FirefoxDriverService.CreateDefaultService(pathsToDriversString);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        throw new Exception(string.Format("Problem with loading firefox driver {0}", e.Message));
                    }
                }
                else
                {
                    service = null;
                }

                return service;

            }
        }

        private T SetDriverOptions<T>(T options)
           where T : DriverOptions
        {
            this.DriverOptionsSet?.Invoke(this, new DriverContextSetEventArgs(options));
            return options;
        }

        public event EventHandler<DriverContextSetEventArgs> DriverOptionsSet;

        private FirefoxProfile FirefoxProfile
        {
            get
            {
                FirefoxProfile profile;

                if (BaseConfiguration.UseCustomProfile)
                {
                    try
                    {
                        //string firefxProfileName = $"Firefox_{BaseConfiguration.Culture}";

                        string firefxProfileName = "Firefox";

                        var pathCurrentUserProfiles = BaseConfiguration.PathToBrowserProfileFolder;
                        var pathsToProfiles = Directory.GetDirectories(pathCurrentUserProfiles, firefxProfileName, SearchOption.TopDirectoryOnly);

                        profile = new FirefoxProfile(pathsToProfiles[0]);
                    }
                    catch (DirectoryNotFoundException e)
                    {
                        throw new Exception(string.Format("Problem with loading firefox profile {0}", e.Message));
                    }
                }
                else
                {
                    profile = new FirefoxProfile();
                }

                profile.SetPreference("toolkit.startup.max_resumed_crashes", "999999");

                // Načítanie nastavení z konfiguračného súboru.
                var firefoxPreferences = ConfigurationManager.GetSection("FirefoxPreferences") as NameValueCollection;
                var firefoxExtensions = ConfigurationManager.GetSection("FirefoxExtensions") as NameValueCollection;

                // Uprednostňovanie sťáhovania súborov.
                profile.SetPreference("browser.download.dir", FileHelper.GetFolder(string.Empty, BaseConfiguration.PathToDownloadFolder));
                profile.SetPreference("browser.download.folderList", 2);
                profile.SetPreference("browser.download.managershowWhenStarting", false);
                profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/vnd.ms-excel, application/x-msexcel, application/pdf, text/csv, text/html, application/octet-stream");

                // Zakázať vstavaný prehlidač PDF.
                profile.SetPreference("pdfjs.disabled", true);

                // Zakázať doplnok Adobe Acrobat PDF preview plugin.
                profile.SetPreference("plugin.scan.Acrobat", "99.0");
                profile.SetPreference("plugin.scan.plid.all", false);

                // Vlastné nastavenia.
                // Ak existujú nejaké nastavenia.
                if (firefoxPreferences != null)
                {
                    // lopp cez všetky z nich.
                    for (var i = 0; i < firefoxPreferences.Count; i++)
                    {
                        // a overiť všetky z nich.
                        switch (firefoxPreferences[i])
                        {
                            // Ak aktuálna hodnota je "true"
                            case "true":
                                profile.SetPreference(firefoxPreferences.GetKey(i), true);
                                break;

                            // Ak "false"
                            case "false":
                                profile.SetPreference(firefoxPreferences.GetKey(i), false);
                                break;

                            // Inak
                            default:
                                int temp;

                                // Pokus o analýzu hodnoty aktuálneho nastavenia na celé číslo.Metóda TryParse vracia True, ak je pokus úspešný(reťazec je celé číslo) alebo sa vráti False (ak reťazec je iba reťazec a nemôže byť odovzdaný číslu).
                                if (int.TryParse(firefoxPreferences.Get(i), out temp))
                                {
                                    profile.SetPreference(firefoxPreferences.GetKey(i), temp);
                                }
                                else
                                {
                                    profile.SetPreference(firefoxPreferences.GetKey(i), firefoxPreferences[i]);
                                }

                                break;
                        }
                    }
                }

                // Ak existujú rozšírenia.
                if (firefoxExtensions != null)
                {
                    // lopp cez všetky z nich.
                    for (var i = 0; i < firefoxExtensions.Count; i++)
                    {
                        profile.AddExtension(firefoxExtensions.GetKey(i));
                    }
                }

                return profile;
            }
        }
    }
}