using OpenQA.Selenium;

namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IComboBox : IContentElement
    {
        /// <summary>
        /// Vykonanie metódy <see cref="DragAndDrop"/> nad elementom.
        /// </summary>
        /// <param name="targetElement"></param>
        void DragAndDrop(IWebElement targetElement);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownByText(string)"/> nad elementom.
        /// </summary>
        /// <param name="text"></param>
        void SelectFromDropDownByText(string text);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownByTextAndEnter(string)"/> nad elementom.
        /// </summary>
        /// <param name="text"></param>
        void SelectFromDropDownByTextAndEnter(string text);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownByTextAndWait(string)"/> nad elementom.
        /// </summary>
        /// <param name="text"></param>
        void SelectFromDropDownByTextAndWait(string text);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownRandomByText(string, int, int)"/> nad elementom.
        /// </summary>
        /// <param name="text"></param>
        void SelectFromDropDownRandomByText(int start, int end);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownCheckBoxByText(string)"/> nad elementom.
        /// </summary>
        /// <param name="text"></param>
        void SelectFromDropDownCheckBoxByText(string text);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownByIndex(int)"/> nad elementom.
        /// </summary>
        /// <param name="index">Hĺadaný text v combobox.</param>
        void SelectFromDropDownByIndex(int index);

        /// <summary>
        /// Vykonanie metódy <see cref="SelectFromDropDownByValue(string)"/> nad elementom.
        /// </summary>
        /// <param name="value">Hĺadaný item v combobox.</param>
        void SelectFromDropDownByValue(string value);


        void SelectFromDropDownByMultiValue(string[] text);
    }
}
