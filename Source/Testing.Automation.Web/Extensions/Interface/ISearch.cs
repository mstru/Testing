namespace Testing.Automation.Web.Extensions.Interface
{
    public interface ISearch : IContentElement
    {
        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string ClassName();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Value();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Name();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Id();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Style();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string TypeValue();


        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Title();
    }
}
