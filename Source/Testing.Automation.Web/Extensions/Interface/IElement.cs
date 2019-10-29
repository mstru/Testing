using OpenQA.Selenium;

namespace Testing.Automation.Web.Extensions.Interface
{
    public interface IElement : IElementFinder
    {      
        /// <summary>
        /// Vykonanie metódy <see cref="Up"/> nad elementom.
        /// </summary>
        void Up();

        /// <summary>
        /// Vykonanie metódy <see cref="Down"/> nad elementom.
        /// </summary>
        void Down();

        /// <summary>
        /// Vykonanie metódy <see cref="Tab"/> nad elementom.
        /// </summary>
        void Tab();

        /// <summary>
        /// Vykonanie metódy <see cref="Escape"/> nad elementom.
        /// </summary>
        void Escape();

        /// <summary>
        /// Vykonanie metódy <see cref="Enter"/> nad elementom.
        /// </summary>
        void Enter();

        /// <summary>
        /// Vykonanie metódy <see cref="EnterAndWait"/> nad elementom.
        /// </summary>
        void EnterAndWait();

        /// <summary>
        /// Vykonanie metódy <see cref="Clear"/> nad elementom.
        /// </summary>
        void Clear();

        bool Displayed { get; }

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



        IWebDriver GetDriver();

        IJavaScriptExecutor GetJavascriptExecutor();

        /// <summary>
        /// Nastavenie subjektu zo zoznamu. System ponukne zoznam subjektov.
        /// </summary>
        /// <param name="text">testovacie data</param>
        void EnterSubjectOfSettlementList(string text);

        /// <summary>
        /// Nastavenie subjektu zo zoznamu. System ponukne zoznam subjektov.
        /// </summary>
        /// <param name="text">testovacie data</param>
        void EnterSendersOfSettlementList(string text);

        /// <summary>
        /// Nastavenie subjektu cez index. System ponukne zoznam subjektov.
        /// </summary>
        /// <param name="row">riadok</param>
        /// <param name="column">stlpec</param>
        void SelectIndexSubjectsOfSettlementList(int row, int column);

        /// <summary>
        /// Nastavenie subjektu cez index. System ponukne zoznam subjektov.
        /// </summary>
        /// <param name="row">riadok</param>
        /// <param name="column">stlpec</param>
        void SelectIndexSubjectOfSettlementList(int row, int column);
    }
}