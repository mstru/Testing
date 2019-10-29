namespace Testing.Automation.Common.DataProviders.Sql
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <summary>
    /// Výsledný objekt pre dopyt Oracle Count
    /// </summary>
    public class OracleCountResult : IExternalDataResult
    {
        /// <summary>
        /// Koľko riadkov bolo vrátených
        /// </summary>
        public int ResultCount
        {
            get;
            set;
        }

        /// <summary>
        /// Zobrazená správa o výnimke
        /// </summary>
        public string ExceptionMessage
        {
            get;
            set;
        }

        /// <summary>
        /// Bol dopyt úspešný
        /// </summary>
        public bool Success
        {
            get;
            set;
        }
    }
}
