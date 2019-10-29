using Testing.Automation.Web.Extensions.Interface;

namespace Testing.Automation.Web.Extensions
{
    public class By
    {
        public enum SearchTypes
        {
            Id,
            Name,
            Tag,
            CssClass,
            XPath,
            CssSelector,
            LinkText,
            IdEndingWith,
            ValueEndingWith,
            IdContaining,
            CssClassContaining,
            LinkTextContaining,
            XPathContaining,
            InnerTextContains,
            NameEndingWith
        }

        public By(SearchTypes type, string value) : this(type, value, null)
        {
        }

        public By(SearchTypes type, string value, IElement parent)
        {
            this.Type = type;
            this.Value = value;
            this.Parent = parent;
        }

        public SearchTypes Type { get; private set; }

        public string Value { get; private set; }

        public IElement Parent { get; private set; }

        public static By Id(string id)
        {
            return new By(SearchTypes.Id, id);
        }

        public static By Id(string id, IElement parentElement)
        {
            return new By(SearchTypes.Id, id, parentElement);
        }

        public static By LinkText(string linkText)
        {
            return new By(SearchTypes.LinkText, linkText);
        }

        public static By CssClass(string cssClass, IElement parentElement)
        {
            return new By(SearchTypes.CssClass, cssClass, parentElement);
        }

        public static By CssClass(string cssClass)
        {
            return new By(SearchTypes.CssClass, cssClass);
        }

        public static By Tag(string tagName)
        {
            return new By(SearchTypes.Tag, tagName);
        }

        public static By Tag(string tagName, IElement parentElement)
        {
            return new By(SearchTypes.Tag, tagName, parentElement);
        }

        public static By CssSelector(string cssSelector)
        {
            return new By(SearchTypes.CssSelector, cssSelector);
        }

        public static By CssSelector(string cssSelector, IElement parentElement)
        {
            return new By(SearchTypes.CssSelector, cssSelector, parentElement);
        }

        public static By XPath(string xpathSelector)
        {
            return new By(SearchTypes.XPath, xpathSelector);
        }

        public static By XPath(string xpathSelector, IElement parentElement)
        {
            return new By(SearchTypes.XPath, xpathSelector, parentElement);
        }

        public static By Name(string name)
        {
            return new By(SearchTypes.Name, name);
        }

        public static By Name(string name, IElement parentElement)
        {
            return new By(SearchTypes.Name, name, parentElement);
        }
    }
}
