using System;

namespace Testing.Automation.Web.Repository
{
    /// <summary>
    /// Zakladna business entita (objekt).
    /// </summary>
    /// <remarks>
    /// Kazda business entita musi byt odvodena od tejto entity.
    /// </remarks>
    [Serializable()]
    public class BaseBusinessEntity
    {
        /// <summary>
        /// Jedinecny identifikator.
        /// </summary>
        /// <remarks>
        /// Sluzi na jednoznacnu identifikaciu business entity (objektu).
        /// </remarks>
        public decimal? ID { get; set; }

        /// <summary>
        /// Uchovava dodatocnu informaciu o business entite.
        /// </summary>
        /// <remarks>
        /// Charakter dodatocnej informacie je specificky pre rozne business entity. Hodnota nemusi byt jedinecna.
        /// </remarks>
        public decimal? Value { get; set; }

        /// <summary>
        /// Nazov business entity (objektu).
        /// </summary>
        public string Name { get; set; }
    }
}
