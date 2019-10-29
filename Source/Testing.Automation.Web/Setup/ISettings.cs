namespace Testing.Automation.Web.Setup
{
    /// <summary>
    /// Nastavenie sessions
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Ms TestContext
        /// </summary>
        //TestContext TestContext { get; }

        /// <summary>
        /// Kde uložiť snimok obrazovky
        /// </summary>
        string ScreenCapturePath { get; }

        /// <summary>
        /// Urobit snimok obrazovky a verifikovat chybu
        /// </summary>
        bool CaptureScreenOnVerificationFailure { get; }

        /// <summary>
        /// Čas spustenia testu
        /// </summary>
        System.DateTime? TestRunStartTime { get; set; }

        /// <summary>
        /// Je test v stave zlyhania?
        /// </summary>
        bool IsTestFailed { get; set; }
     
        /// <summary>
        /// Mal by zabit proces?
        /// </summary>
        bool ShouldKill { get; set; }

        /// <summary>
        /// Prihlasit sa.
        /// </summary>
        bool ShouldLogin { get; set; }

        /// <summary>
        /// Get názov adresára pre PageSource.
        /// </summary>
        string PageSourceFolder { get; }

        /// <summary>
        /// Get Set názov adresára pre ScreenShot.
        /// </summary>
         string ScreenShotFolder { get; }

        /// <summary>
        /// Ukoncenie instancie procesu
        /// </summary>
        void KillProcess();
    }
}