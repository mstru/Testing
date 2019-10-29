namespace Testing.Automation.API.Builders.Contracts.HttpResponseMessages
{
    using System;

    /// <summary>
    /// Používa sa na testovanie správy odozvy HTTP s meraním času odozvy.
    /// </summary>
    public interface IHttpHandlerResponseMessageWithTimeTestBuilder : IHttpHandlerResponseMessageTestBuilder
    {
        /// <summary>
        /// Testuje, či meraná doba odozvy prechádza danými tvrdeniami.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions);

        /// <summary>
        /// Testuje, či meraná doba odozvy prechádza daným predikátom.
        /// </summary>
        /// <param name="predicate">Predicate testing the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate);

        /// <summary>
        /// Získa čas odozvy meraný pri testovaní.
        /// </summary>
        /// <returns>Instance of TimeSpan.</returns>
        TimeSpan AndProvideTheResponseTime();
    }
}
