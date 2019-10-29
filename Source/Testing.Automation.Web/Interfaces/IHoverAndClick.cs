namespace Testing.Automation.Web.Interfaces
{

    public interface IHoverAndClick : IClickable, IElement, IHasText
    {
        TResult HoverAndClick<TResult>(int offsetX, int offsetY) where TResult : IBlock;
    }

    public interface IHoverAndClick<out TResult> : IClickable<TResult>, IHoverAndClick
        where TResult : IBlock
    {
        TResult HoverAndClick(int offsetX, int offsetY);
    }
}
