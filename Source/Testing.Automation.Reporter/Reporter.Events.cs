using System;
using Testing.Automation.Reporter.EventArgs;

namespace Testing.Automation.Reporter
{
    public static partial class Reporter
    {
        public static event EventHandler<ReportEventArgs> StartedReport;

        public static event EventHandler<ReportEventArgs> FinishedReport;

        public static event EventHandler<FeatureEventArgs> StartedFeature;

        public static event EventHandler<FeatureEventArgs> FinishedFeature;

        public static event EventHandler<ScenarioEventArgs> StartedScenario;

        public static event EventHandler<ScenarioEventArgs> FinishedScenario;

        public static event EventHandler<ScenarioBlockEventArgs> StartedScenarioBlock;

        public static event EventHandler<ScenarioBlockEventArgs> FinishedScenarioBlock;

        public static event EventHandler<StepEventArgs> StartedStep;

        public static event EventHandler<StepEventArgs> FinishedStep;

        internal static void OnStartedReport(ReporterManager reporter)
        {
            RaiseEvent(StartedReport, new ReportEventArgs(reporter));
        }

        internal static void OnFinishedReport(ReporterManager reporter)
        {
            RaiseEvent(FinishedReport, new ReportEventArgs(reporter));
        }

        internal static void OnStartedFeature(ReporterManager reporter)
        {
            RaiseEvent(StartedFeature, new FeatureEventArgs(reporter));
        }

        internal static void OnFinishedFeature(ReporterManager reporter)
        {
            RaiseEvent(FinishedFeature, new FeatureEventArgs(reporter));
        }

        internal static void OnStartedScenario(ReporterManager reporter)
        {
            RaiseEvent(StartedScenario, new ScenarioEventArgs(reporter));
        }

        internal static void OnFinishedScenario(ReporterManager reporter)
        {
            RaiseEvent(FinishedScenario, new ScenarioEventArgs(reporter));
        }

        internal static void OnStartedScenarioBlock(ReporterManager reporter)
        {
            RaiseEvent(StartedScenarioBlock, new ScenarioBlockEventArgs(reporter));
        }

        internal static void OnFinishedScenarioBlock(ReporterManager reporter)
        {
            RaiseEvent(FinishedScenarioBlock, new ScenarioBlockEventArgs(reporter));
        }

        internal static void OnStartedStep(ReporterManager reporter)
        {
            RaiseEvent(StartedStep, new StepEventArgs(reporter));
        }

        internal static void OnFinishedStep(ReporterManager reporter)
        {
            RaiseEvent(FinishedStep, new StepEventArgs(reporter));
        }

        private static void RaiseEvent<T>(EventHandler<T> handler, T args)
            where T : ReportEventArgs
        {
            if (handler != null)
            {
                handler(null, args);
            }
        }
    }
}
