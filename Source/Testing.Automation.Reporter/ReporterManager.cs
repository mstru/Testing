using AventStack.ExtentReports;
using Testing.Automation.Reporter.Model;

namespace Testing.Automation.Reporter
{
    /// <summary>
    /// Zakladna trieda pre implementaciu Reportu
    /// </summary>
    public abstract class ReporterManager
    {
        /// <summary>
        ///    Aktualne spusteny Feature
        /// </summary>
        public Feature CurrentFeature { get; internal set; }

        /// <summary>
        ///     Aktualne spusteny Scenario
        /// </summary>
        public Scenario CurrentScenario { get; internal set; }

        /// <summary>
        ///     Aktualne spusteny ScenarioBlock
        /// </summary>
        public ScenarioBlock CurrentScenarioBlock { get; internal set; }

        /// <summary>
        ///    Aktualne spusteny test krok
        /// </summary>
        public Step CurrentStep { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny report
        /// </summary>
        public Report Report { get; set; }

        /// <summary>
        ///     Aktualne vytvoreny extent report
        /// </summary>
        public ExtentReports ExtentReport { get; set; }

        /// <summary>
        ///     Aktualne vytvoreny parent test
        /// </summary>
        public ExtentTest ParentTest { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny test Given, When, Then
        /// </summary>
        public ExtentTest MethodStepGiven { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny test Given, When, Then
        /// </summary>
        public ExtentTest MethodStepWhen { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny test Given, When, Then
        /// </summary>
        public ExtentTest MethodStepThen { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny scenario
        /// </summary>
        public ExtentTest ScenarioTest { get; internal set; }

        /// <summary>
        ///     Aktualne vytvoreny node
        /// </summary>
        public ExtentTest Node { get; internal set; }


        /// <summary>
        ///     Nazov vytvorenej instancie reportu
        /// </summary>
        public virtual string Name
        {
            get { return GetType().FullName; }
        }
    }
}
