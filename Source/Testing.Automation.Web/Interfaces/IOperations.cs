using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface IOperations : IElement
    {
    }

    public interface IOperations<out TResult>
        where TResult : IBlock
    {
        IEnumerable<IOption<TResult>> GetSixthColumn { get; }

        IEnumerable<IOption<TResult>> GetFifthColumn { get; }

        IEnumerable<IOption<TResult>> GetFourthColumn { get; }

        IEnumerable<IOption<TResult>> GetThirthColumn { get; }

        IEnumerable<IOption<TResult>> GetSecondColumn { get; }

        IEnumerable<IOption<TResult>> GetFirsthColumn { get; }
    }
}
