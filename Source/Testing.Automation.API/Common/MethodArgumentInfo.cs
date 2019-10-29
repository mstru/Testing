namespace Testing.Automation.API.Common
{
    using System;

    /// <summary>
    /// Informacie o parametroch metodach, obsahujuce meno, typ, hodnotu.
    /// </summary>
    public class MethodArgumentInfo
    {
        /// <summary>
        /// Ziskanie alebo nastavenie nazvu argumentu.
        /// </summary>
        /// <value>String hodnota nazvu argumentu.</value>
        public string Name { get; set; }

        /// <summary>
        /// Ziskanie alebo nastavenie typu argumentu.
        /// </summary>
        /// <value>Typ argumentu.</value>
        public Type Type { get; set; }

        /// <summary>
        /// Ziskanie alebo nastavenie hodnoty argumentu
        /// </summary>
        /// <value>Hodnota argumentu.</value>
        public object Value { get; set; }
    }
}
