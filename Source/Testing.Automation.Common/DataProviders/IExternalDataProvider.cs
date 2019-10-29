namespace Testing.Automation.Common.DataProviders
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <summary>
    /// Rozhranie definujúce objekt, ktorý sa dá načítať
    /// </summary>
    /// <typeparam name="TQuery"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public interface IExternalDataProvider<in TQuery, out TResult>
        where TQuery : IExternalDataQuery
        where TResult : IExternalDataResult
    {
        /// <summary>
        /// Definuje dopyt, ktorý sa má vykonať voči poskytovateľovi
        /// </summary>
        /// <param name="query"></param>
        void DefineQuery(TQuery query);

        /// <summary>
        /// Vykoná dotaz voči poskytovateľovi údajov
        /// </summary>
        /// <returns></returns>
        TResult Execute();
    }
}
