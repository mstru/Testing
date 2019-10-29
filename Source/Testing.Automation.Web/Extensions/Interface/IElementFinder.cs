using System.Collections.Generic;

namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IElementFinder
    {
        TElement Find<TElement>(By by) where TElement : class, IElement;

        IEnumerable<TElement> FindAll<TElement>(By by) where TElement : class, IElement;
    }
}
