using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using Testing.Automation.Common.Helper;
using Testing.Automation.Reporter;

namespace Testing.Automation.Common
{
    /// <summary>
    /// Pomocná trieda základného nastavenia testov.
    /// </summary>
    public static class BaseConfiguration
    {
        /// <summary>
        /// Verejná vlasnosť pre základný adresár, ktorú testy použijú.
        /// </summary>
        public static string WorkingDirectory { get; set; }

        /// <summary>
        /// Verejná vlasnosť pre url, ktorú testy použijú.
        /// </summary>
        public static string WorkingUrl { get; set; }

        /// <summary>
        /// Vráti názov projektu
        /// </summary>
        public static string ProjectName { get; set; }

        /// <summary>
        /// Získa protokol http alebo https.
        /// </summary>
        public static string Protocol
        {
            get { return ConfigurationManager.AppSettings["protocol"]; }
        }

        /// <summary>
        /// Získanie času čakania [seconds].
        /// </summary>
        public static int MediumTimeout
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["mediumTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Získanie času čakania [seconds].
        /// </summary>
        public static int LongTimeout
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["longTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Získanie času čakania [seconds].
        /// </summary>
        public static int ShortTimeout
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["shortTimeout"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Získanie implicitného čakania [milliseconds].
        /// </summary>
        public static double ImplicitlyWaitMilliseconds
        {
            get { return Convert.ToDouble(ConfigurationManager.AppSettings["ImplicitlyWaitMilliseconds"], CultureInfo.CurrentCulture); }
        }

        /// <summary>
        /// Nazov feature
        /// </summary>
        public static string FeatureName
        {
            get
            {
                try
                {
                    return Reporter.Reporter.reporters[0].CurrentFeature.Title;
                }
                catch
                {
                    return "TESTING";
                }
            }
        }

        /// <summary>
        /// Nazov scenario
        /// </summary>
        public static string ScenarioName
        {
            get
            {
                try
                {
                    return Reporter.Reporter.reporters[0].CurrentScenario.Tags[0];
                }
                catch
                {
                    return "TESTING";
                }
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má zapnúť plný screenShot obrazovky. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool FullDesktopScreenShotEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["FullDesktopScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má zapnúť plný screenShot obrazovky.  Defaultne nastavená hodnota false.
        /// </summary>
        public static bool SeleniumScreenShotEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"]))
                {
                    return true;
                }

                if (ConfigurationManager.AppSettings["SeleniumScreenShotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Ziskanie hodnoty, ktora indikuje, ci sa ma pouzit nastavenie prostredia podla app konfiguracie.
        /// </summary>
        public static bool CustomEnvironment
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["CustomEnvironment"]))
                {
                    return false;
                }
                return true;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má zapnúť v prehliadači JS logovanie. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool JavaScriptErrorLogging
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["JavaScriptErrorLogging"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["JavaScriptErrorLogging"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má vykonať odstránenie starých snapshot súborov. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool DeleteOldSnapshotEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DeleteOldSnapshotEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["DeleteOldSnapshotEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má vykonať odstránenie starých export súborov. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool DeleteOldDownloadFilesEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DeleteOldDownloadFilesEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["DeleteOldDownloadFilesEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má vykonať odstránenie starých Mstest testresult súborov. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool DeleteOldTestResultsEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DeleteOldTestResultsEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["DeleteOldTestResultsEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má vykonať odstránenie temp súborov. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool DeleteTempFilesEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["DeleteTempFilesEnabled"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["DeleteTempFilesEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má vykonať odstránenie temp súborov. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool UseCustomUrlEnvironment
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["Env"]))
                {
                    return false;
                }

                return true;
            }
        }


        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má zalogovať stackTrace do reportu. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool ShowStackTrace
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["ShowStackTrace"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["ShowStackTrace"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie typov chýb jazyka JavaScript z prehliadača. "SyntaxError, EvalError, ReferenceError, RangeError, TypeError, URIError, Refused to display, Internal Server Error, Cannot read property". 
        /// </summary>
        public static Collection<string> JavaScriptErrorTypes
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["JavaScriptErrorTypes"]))
                {
                    return new Collection<string>
                    {
                        "SyntaxError",
                        "EvalError",
                        "ReferenceError",
                        "RangeError",
                        "TypeError",
                        "URIError",
                        "Refused to display",
                        "Internal Server Error",
                        "Cannot read property"
                    };
                }

                return new Collection<string>(ConfigurationManager.AppSettings["JavaScriptErrorTypes"].Split(new char[] { ',' }));
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má použiť základný profil. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool UseCustomProfile
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCustomProfile"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["UseCustomProfile"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnotu, ktorá indikuje, či sa má zapnúť EventFiringWebDriver.
        /// </summary>
        public static bool EnableEventFiringWebDriver
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["EnableEventFiringWebDriver"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["EnableEventFiringWebDriver"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie hodnoty, ktorá indikuje, či sa má použiť Current direcotory. Defaultne nastavená hodnota false.
        /// </summary>
        public static bool UseCurrentDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCurrentDirectory"]))
                {
                    return false;
                }

                if (ConfigurationManager.AppSettings["UseCurrentDirectory"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získate hodnoty označujúcu, či je [povolený zdroj stránky default true].
        /// </summary>
        public static bool GetPageSourceEnabled
        {
            get
            {
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["GetPageSourceEnabled"]))
                {
                    return true;
                }

                if (ConfigurationManager.AppSettings["GetPageSourceEnabled"].ToLower(CultureInfo.CurrentCulture).Equals("true"))
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Získanie cesty pre TestResults folder.
        /// </summary>
        public static string PathToTestResultFolder
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["TestResultFolderPath"]);
                }

                return ConfigurationManager.AppSettings["TestResultFolderPath"];
            }
        }

        /// <summary>
        /// Získanie cesty pre Download folder.
        /// </summary>
        public static string PathToDownloadFolder
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["DownloadFolder"]);
                }

                return ConfigurationManager.AppSettings["DownloadFolder"];
            }
        }

        /// <summary>
        /// Získanie cesty pre ScreenShot folder.
        /// </summary>
        public static string PathToScreenShotFolder
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["ScreenShotFolder"]);
                }

                return ConfigurationManager.AppSettings["ScreenShotFolder"];
            }
        }

        /// <summary>
        /// Získanie cesty pre PageSource folder.
        /// </summary>
        public static string PathToPageSourceFolder
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PageSourceFolder"]);
                }

                return ConfigurationManager.AppSettings["PageSourceFolder"];
            }
        }

        /// <summary>
        /// Získanie cesty pre ImportSource folder.
        /// </summary>
        public static string PathToImportSourceFolder
        {

            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine($@"{WorkingDirectory}\Source\{ProjectName}\{ConfigurationManager.AppSettings["DataDrivenFileImport"]}");
                }

                return ConfigurationManager.AppSettings["DataDrivenFileImport"];
            }
        }

        /// <summary>
        /// Získanie cesty pre ImportSource folder.
        /// </summary>
        public static string PathToImportSourceFolder2
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine($@"{WorkingDirectory}\Source\Testing\bin\Debug\{ConfigurationManager.AppSettings["DataDrivenFileImport"]}\{FeatureName}");
                }

                return ConfigurationManager.AppSettings["DataDrivenFileImport"];
            }
        }

        /// <summary>
        /// Cesta k web profilu.
        /// </summary>
        public static string PathToBrowserProfileFolder
        {
            get
            {
                if (BaseConfiguration.UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PathToBrowserProfile"]);
                }

                return ConfigurationManager.AppSettings["PathToBrowserProfile"];
            }
        }

        /// <summary>
        /// Cesta k report style šablone.
        /// </summary>
        public static string PathToReportStyle
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PathToReportStyle"]);
                }

                return ConfigurationManager.AppSettings["PathToReportStyle"];
            }
        }

        /// <summary>
        /// Cesta do adresára k environment súboru. Obsahujúci výber testovacieho prostredia.
        /// </summary>
        public static string PathToEnvironmentFile
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.GetFullPath(Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["EnvironmentFilePath"]));
                }

                return ConfigurationManager.AppSettings["EnvironmentFilePath"];
            }
        }

        /// <summary>
        /// Cesta do adresára k properties súboru. Obsahujúci výber pre nastavenia databázy vzhľadom na testovacie prostredie.
        /// </summary>
        public static string PathToPropertiesFile
        {
            get
            {
                if (!CustomEnvironment)
                {
                    if (Prefix.Equals("dev"))
                    {
                        return Path.GetFullPath(Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PropertiesFilePath"] + Properties.Settings.Default.PropertiesFileDEV));
                    }

                    if (Prefix.Equals("test"))
                    {
                        return Path.GetFullPath(Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PropertiesFilePath"] + Properties.Settings.Default.PropertiesFileTEST));
                    }
                }

                return Path.GetFullPath(Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["PropertiesFilePath"] + $"/properties_{ ConfigurationManager.AppSettings["CustomEnvironment"]}.json"));
            }
        }

        /// <summary>
        /// Cesta do adresára k navigate súboru. Obsahujúci Url adresy pre prechodov medzi stránkami.
        /// </summary>
        public static string PathToNavigateFile
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["NavigateFilePath"]);
                }

                return ConfigurationManager.AppSettings["NavigateFilePath"];
            }
        }

        /// <summary>
        /// Cesta do adresára k settings súboru. Obsahujúci základné nastavenie testu pre konkrétnu testovanú stránku.
        /// </summary>
        public static string PathToSettingsFile
        {
            get
            {
                if (UseCurrentDirectory)
                {
                    return Path.Combine(WorkingDirectory + ConfigurationManager.AppSettings["SettingsFilePath"]);
                }

                return ConfigurationManager.AppSettings["SettingsFilePath"];
            }
        }

        /// <summary>
        /// Gets the URL value.
        /// </summary>
        public static string GetUrlValue
        {
            get
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "{0}",
                    Url);
            }
        }

        /// <summary>
        /// Gets the URL value with hmtl format.
        /// </summary>
        public static string GetUrlValueWithFormatHtml
        {
            get
            {
                return string.Format(
                    CultureInfo.CurrentCulture,
                    "<a href={0}>{0}</a>",
                    Url);
            }
        }

        /// <summary>
        /// Vráti Url adresa ktorú testy použijú nastaviteĺne v ..\environment.properties.
        /// </summary>
        public static string Url
        {
            get
            {
                if (UseCustomUrlEnvironment)
                {
                    return ConfigurationManager.AppSettings["Env"];
                }

                return PropertyHelper.ReadProperty("environment", Path.Combine(PathToEnvironmentFile));
            }
        }

        /// <summary>
        /// Vráti Prefix ktorú testy použijú napríklad [test alebo dev] nastaviteĺne v ..\environment.properties.
        /// </summary>
        public static string Prefix
        {
            get { return PropertyHelper.ReadProperty("prefix", Path.Combine(PathToEnvironmentFile)); }
        }

        /// <summary>
        /// Prihlasovanie meno
        /// </summary>
        public static string Username
        {
            get
            {
                string key = "UserName";
                string value = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrEmpty(value))
                {
                    return GetSettingsValue(key);
                }

                return value;
            }
        }

        /// <summary>
        /// Prihlasovacie heslo
        /// </summary>
        public static string Password
        {
            get
            {
                string key = "PassWord";
                string value = ConfigurationManager.AppSettings[key];
 
                if (string.IsNullOrEmpty(value))
                {
                    return GetSettingsValue(key);
                }

                return value;
            }
        }

        /// <summary>
        /// Získanie hodnoty, pre zvolenie jazykovej mutácie portalu. Defaultne nastavená hodnota sk.
        /// </summary>
        public static string Culture
        {
            get
            {
                string key = "Culture";
                string value = ConfigurationManager.AppSettings[key];

                if (string.IsNullOrEmpty(value))
                {
                    return GetSettingsValue(key);
                }

                return value;
            }
        }


        public static string GetSettingsValue(string key)
        {
            
            var eleName = $"/configuration/{FeatureName}/{ScenarioName}/{key}";
            return XmlHelper.GetElementValue(
                XDocument.Load(PathToSettingsFile), eleName, "Value");
        }
    }
}
