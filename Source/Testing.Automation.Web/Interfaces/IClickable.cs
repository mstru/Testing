namespace Testing.Automation.Web.Interfaces
{
    public interface IClickable : IElement, IHasText
    {
        TResult Click<TResult>() where TResult : IBlock;

        TResult ClickWait<TResult>() where TResult : IBlock;

        TResult ClickJs<TResult>() where TResult : IBlock;
    }

    public interface IClickable<out TResult> : IClickable
        where TResult : IBlock
    {
        TResult Click();

        TResult ClickJs();
    }
}