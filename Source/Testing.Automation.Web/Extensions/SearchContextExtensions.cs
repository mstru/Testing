using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using Testing.Automation.Common;
using Testing.Automation.Web.Extensions;

namespace Testing.Automation.Web.Extensions
{
    /// <summary>
    /// Metódy rozšírenia pre IWebDriver a IWebElement.
    /// </summary>
    public static class SearchContextExtensions
    {
        /// <summary>
        /// Nájde a čaká na prvok, ktorý je viditeľný a zobrazený za dlhý časový limit.
        /// </summary>
        /// <param name="locator">Hľadaný element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Hľadaný element.</returns>
        public static IWebElement GetElement(this ISearchContext searchContext, By locator, [Optional] string customMessage)
        {
            return searchContext.GetElement(locator, BaseConfiguration.LongTimeout, e => e.Displayed & e.Enabled, customMessage);
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý je viditeľný a zobrazený za vlastný časový limit.
        /// </summary>
        /// <param name="element">Hľadaný element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Špeciálny timeout.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Hľadaný element.</returns>
        public static IWebElement GetElement(this ISearchContext searchContext, By locator, double timeout, [Optional] string customMessage)
        {
            return searchContext.GetElement(locator, timeout, e => e.Displayed & e.Enabled, customMessage);
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý spĺňa stanovené podmienky pre dlhý časový limit.
        /// </summary>
        /// <param name="element">Hľadaný element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="condition">Čakanie na splnenie podmienok.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Hľadaný element.</returns>
        public static IWebElement GetElement(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, [Optional] string customMessage)
        {
            return searchContext.GetElement(locator, BaseConfiguration.LongTimeout, condition, customMessage);
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý spĺňa stanovené podmienky v určenom čase.
        /// </summary>
        /// <param name="element">Hľadaný element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Timeout.</param>
        /// <param name="condition">Čakanie na splnenie podmienok.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Hľadaný element.</returns>
        public static IWebElement GetElement(this ISearchContext element, By locator, double timeout, Func<IWebElement, bool> condition, [Optional] string customMessage)
        {
            var driver = element.ToDriver();
            var by = locator.ToSeleniumBy();
       
            var start = System.DateTime.Now;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout)) { Message = @by.ToString() };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException), typeof(NoSuchElementException));
            wait.Until(
                    drv =>
                    {
                        var ele = driver.FindElement(@by);
                        return condition(ele);
                    });

            return driver.FindElement(@by);
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý spĺňa špecifikované podmienky v určenom čase, znova urobí kontrolu stavu za konkrétny čas.
        /// </summary>
        /// <param name="element">Hľadaný element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Timeout.</param>
        /// <param name="timeInterval">Hodnota indikujúca, ako často sa má kontrolovať, či je podmienka pravdivá.</param>
        /// <param name="condition">Podmienak, ktorá sa má splniť.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Hľadaný element.</returns>
        public static IWebElement GetElement(this ISearchContext searchContext, By locator, double timeout, double timeInterval, Func<IWebElement, bool> condition, [Optional] string customMessage)
        {
            var by = locator.ToSeleniumBy();

            var wait = new WebDriverWait(new SystemClock(), searchContext.ToDriver(), TimeSpan.FromSeconds(timeout), TimeSpan.FromSeconds(timeInterval)) { Message = customMessage };
            wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));

            wait.Until(
                    drv =>
                    {
                        var ele = searchContext.FindElement(@by);
                        return condition(ele);
                    });

            return searchContext.FindElement(@by);
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý je viditeľný a zobrazený za dlhý časový limit.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Vyhľadanie umiestnenia elementu.</returns>
        public static T GetElement<T>(this ISearchContext searchContext, By locator, [Optional] string customMessage)
            where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, customMessage);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý je viditeľný a zobrazený za určitý čas.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Vlastný timeout.</param>
        /// <returns>Vyhľadanie umiestnenia elementu.</returns>
        public static T GetElement<T>(this ISearchContext searchContext, By locator, double timeout)
            where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, timeout);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý spĺňa stanovené podmienky pre dlhý časový limit.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="condition">Podmienak, ktorá sa má splniť.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Vyhľadanie umiestnenia elementu.</returns>
        public static T GetElement<T>(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition, [Optional] string customMessage)
            where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, condition, customMessage);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Nájde a čaká na prvok, ktorý spĺňa stanovené podmienky v určenom čase.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="timeout">Vlastný timeout.</param>
        /// <param name="condition">Podmienak, ktorá sa má splniť.</param>
        /// <param name="customMessage">Zobrazenie vlastnej správy ak nie je zobrazený prvok.</param>
        /// <returns>Vyhľadanie umiestnenia elementu.</returns>
        public static T GetElement<T>(this ISearchContext searchContext, By locator, double timeout, Func<IWebElement, bool> condition, [Optional] string customMessage)
            where T : class, IWebElement
        {
            IWebElement webElemement = searchContext.GetElement(locator, timeout, condition, customMessage);
            return webElemement.As<T>();
        }

        /// <summary>
        /// Nájde všetky prvky, ktoré sú viditeľné a zobrazené.
        /// </summary>
        /// <param name="element">Web Element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <returns>Vráť všetky nájdené a zobrazené a povolené prvky.</returns>
        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator)
        {
            return searchContext.GetElements(locator, e => e.Displayed && e.Enabled).ToList();
        }

        /// <summary>
        /// Nájde prvky, ktoré spĺňajú špecifikované podmienky.
        /// </summary>
        /// <param name="element">Web Element.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="condition">Podmienka musí byť splnená.</param>
        /// <returns>Vráte všetky nájdené prvky za špecifikované podmienky.</returns>
        public static IList<IWebElement> GetElements(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition)
        {
            return searchContext.FindElements(locator.ToSeleniumBy()).Where(condition).ToList();
        }

        /// <summary>
        /// Nájde všetky prvky, ktoré sú viditeľné a zobrazené.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <returns>Nájdené prvky.</returns>
        public static IList<T> GetElements<T>(this ISearchContext searchContext, By locator)
            where T : class, IWebElement
        {
            var webElements = searchContext.GetElements(locator);
            return
                new ReadOnlyCollection<T>(
                    webElements.Select(e => e.As<T>()).ToList());
        }

        /// <summary>
        /// Nájde všetky prvky, ktoré spĺňajú špecifikované podmienky.
        /// </summary>
        /// <typeparam name="T">IWebComponent ako IComboBox, ISelect atď.</typeparam>
        /// <param name="searchContext">Search context.</param>
        /// <param name="locator">Lokátor.</param>
        /// <param name="condition">Podmienka musí byť splnená.</param>
        /// <returns>Nájdené prvky.</returns>
        public static IList<T> GetElements<T>(this ISearchContext searchContext, By locator, Func<IWebElement, bool> condition)
            where T : class, IWebElement
        {
            var webElements = searchContext.GetElements(locator, condition);
            return
                new ReadOnlyCollection<T>(
                    webElements.Select(e => e.As<T>()).ToList());
        }

        /// <summary>
        /// IwebDriver pomocná metóda.
        /// </summary>
        /// <param name="webElement">Web prvok.</param>
        /// <returns>Ovládač pre element.</returns>
        public static IWebDriver ToDriver(this ISearchContext webElement)
        {
            var wrappedElement = webElement as IWrapsDriver;
            if (wrappedElement == null)
            {
                return (IWebDriver)webElement;
            }

            return wrappedElement.WrappedDriver;
        }

        /// <summary>
        /// Prevádza generický IWebElement na špecifikovaný webový prvok (Checkbox, Table, atď.).
        /// </summary>
        /// <typeparam name="T">Web element trieda.</typeparam>
        /// <param name="webElement">generický IWebElement.</param>
        /// <returns>špecifikovaný webový prvok (Checkbox, Table, atď.).</returns>
        private static T As<T>(this IWebElement webElement)
            where T : class, IWebElement
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(IWebElement) });

            if (constructor != null)
            {
                return constructor.Invoke(new object[] { webElement }) as T;
            }
            throw new ArgumentNullException(string.Format(CultureInfo.CurrentCulture, "Constructor for type {0} is null.", typeof(T)));
        }
    }
}
