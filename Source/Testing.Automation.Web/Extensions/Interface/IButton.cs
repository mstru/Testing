namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IButton : IContentElement
    {
        /// <summary>
        /// Vykonanie metódy <see cref="Click"/> nad elementom.
        /// </summary>
        void Click();

        /// <summary>
        /// Vykonanie metódy <see cref="ClickAndWait"/> nad elementom.
        /// </summary>
        void ClickAndWait();

        /// <summary>
        /// Vykonanie metódy <see cref="ClickAndEnter"/> nad elementom.
        /// </summary>
        void ClickAndEnter();

        /// <summary>
        /// Vykonanie metódy <see cref="ClickAndDown"/> nad elementom.
        /// </summary>
        void ClickAndDown();

        /// <summary>
        /// Vykonanie metódy <see cref="ClickAndUp"/> nad elementom.
        /// </summary>
        void ClickAndUp();

        /// <summary>
        /// Vykonanie metódy <see cref="Hover(int, int)"/> nad elementom.
        /// </summary>
        void Hover(int offsetX, int offsetY);

        /// <summary>
        /// Vykonanie metódy <see cref="HoverAndClick"/> nad elementom.
        /// </summary>
        void HoverAndClick(int offsetX, int offsetY);

        /// <summary>
        /// Vykonanie metódy <see cref="HoverAndClick"/> nad elementom.
        /// </summary>
        void HoverAndClick();

        /// <summary>
        /// Vykonanie metódy <see cref="DoubleClick"/> nad elementom.
        /// </summary>
        void DoubleClick();

        /// <summary>
        /// Vykonanie metódy <see cref="RightClick"/> nad elementom.
        /// </summary>
        void RightClick();

        /// <summary>
        /// Vykonanie metódy <see cref="ClickAndHold"/> nad elementom.
        /// </summary>
        void ClickAndHold();

    }
}