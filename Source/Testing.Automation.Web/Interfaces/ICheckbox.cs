using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface ICheckbox : IElement, ISelectable
    {
        TCustomResult Click<TCustomResult>() where TCustomResult : IBlock;
        //TCustomResult Check<TCustomResult>() where TCustomResult : IBlock;
        //TCustomResult Uncheck<TCustomResult>() where TCustomResult : IBlock;
        //TCustomResult Toggle<TCustomResult>() where TCustomResult : IBlock;
        IEnumerable<IOption> GetOptions();
    }

    public interface ICheckbox<out TResult> : ICheckbox
        where TResult : IBlock
    {
        //TResult Check();
        //TResult Uncheck();
        //TResult Toggle();
        TResult Click();
        IEnumerable<IOption<TResult>> Options { get; }
    }
}