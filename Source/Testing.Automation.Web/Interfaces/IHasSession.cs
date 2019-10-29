namespace Testing.Automation.Web.Interfaces
{
    using Setup;

    public interface IHasSession : IConfigurable
    {
        Session Session { get; }
    }
}