namespace Testing.Automation.Reporter
{
    public enum LogLevel
    {
        None,
        Basic,
        Verbose
    }

    public enum CodeBlockType
    {
        Xml,
        Json,
        Table,
        Label,
        None
    }

 

    public enum LogStatus
    {
        Pass,
        Fail,
        Fatal,
        Error,
        Warning,
        Info,
        Skip,
        Debug
    }
}
