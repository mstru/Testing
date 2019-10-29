namespace Testing.Automation.Web.Extensions.Interface
{
    public interface ICheckbox : IContentElement
    {
        bool IsChecked { get; }

        void Check();

        void Uncheck();
    }
}
