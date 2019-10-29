namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Net.Http;
    using Contracts.HttpResponseMessages;

    /// <summary>
    /// Používa sa na testovanie správy odozvy HTTP s meraním času odozvy.
    /// </summary>
    public class HttpHandlerResponseMessageWithTimeTestBuilder
        : HttpHandlerResponseMessageTestBuilder, IAndHttpHandlerResponseMessageWithTimeTestBuilder
    {
        private readonly TimeSpan responseTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerResponseMessageWithTimeTestBuilder" /> class.
        /// </summary>
        /// <param name="handler">Tested HTTP message handler.</param>
        /// <param name="httpResponseMessage">HTTP response result from the tested handler.</param>
        /// <param name="responseTime">Measured response time from the tested handler.</param>
        public HttpHandlerResponseMessageWithTimeTestBuilder(
            HttpResponseMessage httpResponseMessage,
            TimeSpan responseTime)
            : base(httpResponseMessage)
        {
            this.responseTime = responseTime;
        }

        /// <summary>
        /// Testuje, či meraná doba odozvy prechádza danými tvrdeniami.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Action<TimeSpan> assertions)
        {
            assertions(this.responseTime);
            return this;
        }

        /// <summary>
        /// Testuje, či meraná doba odozvy prechádza daným predikátom.
        /// </summary>
        /// <param name="predicate">Predicate testing the measured response time.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageWithTimeTestBuilder WithResponseTime(Func<TimeSpan, bool> predicate)
        {
            if (!predicate(this.responseTime))
            {
                this.ThrowNewHttpResponseMessageAssertionException("response time " + this.responseTime + " ms", "to pass the given condition", "it failed");
            }

            return this;
        }

        /// <summary>
        /// Získa čas odozvy meraný pri testovaní.
        /// </summary>
        /// <returns>Instance of TimeSpan.</returns>
        public TimeSpan AndProvideTheResponseTime()
        {
            return this.responseTime;
        }

        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťazení správy HTTP odpovede s testami času odozvy.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public new IHttpHandlerResponseMessageWithTimeTestBuilder AndAlso()
        {
            return this;
        }
    }
}
