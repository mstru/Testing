using System.Collections.Generic;

namespace Testing.Automation.Web.Interfaces
{
    public interface ISubjectsFilter : IElement
    {
    }

    public interface ISubjectsFilter<out TResult>
        where TResult : IBlock
    {
        IEnumerable<IOption<TResult>> GetSubjectFilter { get; }

         IEnumerable<IOption<TResult>> GetSubjectEicFilter { get; }

        IEnumerable<IOption<TResult>> GetSubject { get; }

        IEnumerable<IOption<TResult>> GetSubjectEic { get; }

        IEnumerable<IOption<TResult>> GetSubjects { get; }
    }
}
