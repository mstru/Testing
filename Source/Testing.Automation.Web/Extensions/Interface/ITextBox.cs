namespace Testing.Automation.Web.Extensions.Interface
{
    public interface ITextBox : IContentElement
    {
        /// <summary>
        /// Vykonanie metódy <see cref="SendKeys(string)"/> nad elementom.
        /// </summary>
        void SendKeys(string text);


        /// <summary>
        /// Vykonanie metódy <see cref="SendKeysAndEnter(string)"/> nad elementom.
        /// </summary>
        void SendKeysAndEnter(string text);


        /// <summary>
        /// Vykonanie metódy <see cref="SendKeysAndWait(string)"/> nad elementom.
        /// </summary>
        void SendKeysAndWait(string text);

        /// <summary>
        /// Vykonanie metódy <see cref="GetAttribute(string)"/> nad elementom.
        /// </summary>
        string Text();
 
        /// <summary>
        /// Vykonanie JavaScript metódy <see cref="SetAttributeWithJS(string, string)"/> nad elementom.
        /// </summary>
        /// <param name="attributeName">Názov atribútu.</param>
        /// <param name="text">Text ktorý sa vkladá.</param>
        void SetAttributeWithJS(string text, string attributeName = "value");
    }
}
