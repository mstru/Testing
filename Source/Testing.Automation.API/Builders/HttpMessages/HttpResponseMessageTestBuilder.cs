namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using global::Testing.Automation.Reporter.Exceptions;
    using Models;
    using Utilities.Validators;

    /// <summary>
    /// Používa sa na testovanie výsledkov správ odozvy HTTP z akcií.
    /// </summary>
    public class HttpResponseMessageTestBuilder
        : BaseTestBuilderWithActionResult<HttpResponseMessage>, IAndHttpResponseMessageTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="controller">Controller on which the action will be tested.</param>
        /// <param name="actionName">Name of the tested action.</param>
        /// <param name="caughtException">Caught exception during the action execution.</param>
        /// <param name="actionResult">HTTP response result from the tested action.</param>
        public HttpResponseMessageTestBuilder(
            ApiController controller,
            string actionName,
            Exception caughtException,
            HttpResponseMessage actionResult)
            : base(controller, actionName, caughtException, actionResult)
        {
        }

       
        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu objektu HTTP odpoveď.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> ValidWithResponseModelOfType<TResponseModel>()
        {
            var actualModel = HttpResponseMessageValidator.GetActualContentModel<TResponseModel>(
                this.ActionResult.Content,
                this.ThrowNewResponseModelAssertionException);
            
            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        /// <summary>
        /// Testuje, či sa hromadne zhodný objekt s poskytnutým objektom vráti z obsahu objektového hlásenia odozvy HTTP.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IModelDetailsTestBuilder<TResponseModel> ValidWithResponseModel<TResponseModel>(TResponseModel expectedModel)
        {
            var actualModel = HttpResponseMessageValidator.WithResponseModel(
                this.ActionResult.Content,
                expectedModel,
                this.ThrowNewHttpResponseMessageAssertionException,
                this.ThrowNewResponseModelAssertionException);

            return new ModelDetailsTestBuilder<TResponseModel>(
                this.Controller,
                this.ActionName,
                this.CaughtException,
                actualModel);
        }

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovede určitého typu.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithContentOfType<TContentType>()
            where TContentType : HttpContent
        {
            HttpResponseMessageValidator.WithContentOfType<TContentType>(
                this.ActionResult.Content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovedí poskytnutým reťazcom.
        /// </summary>
        /// <param name="content">Expected string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithStringContent(string content)
        {
            HttpResponseMessageValidator.WithStringContent(
                this.ActionResult.Content,
                content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithStringContent(Action<string> assertions)
        {
            assertions(this.GetStringContent());
            return this;
        }

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá danému predikátu.
        /// </summary>
        /// <param name="predicate">Predicate testing the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithStringContent(Func<string, bool> predicate)
        {
            if (!predicate(this.GetStringContent()))
            {
                this.ThrowNewHttpResponseMessageAssertionException(
                    "Content",
                    "to pass the given predicate",
                    "but it failed");
            }

            return this;
        }

        /// <summary>
        /// Testuje, či má odpoveď HTTP nainštalovaný formátovač médií.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.ActionResult.Content,
                mediaTypeFormatter,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či správa HTTP odpovedí má poskytnutý typ formátora typu média.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ValidWithMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje predvolený formátovač médií poskytovaný rámcom.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithDefaultMediaTypeFormatter()
        {
            return this.ValidWithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či odpoveď HTTP obsahuje hlavičku odpovede s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name, string value)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičku odpovede s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ContainingHeader(this.ActionResult.Headers, name, values, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičky odpovedí poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateHeadersCount(headers, this.ActionResult.Headers, this.ThrowNewHttpResponseMessageAssertionException);
            headers.ForEach(h => this.ValidContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
                name,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku obsahu s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="value">Value of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name, string value)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
                name,
                value,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="values">Collection of values in the expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.ActionResult.Content.Headers,
                name,
                values,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeader: true);

            return this;
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičky obsahu poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing content headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidContainingContentHeaders(
            IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateContent(this.ActionResult.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ValidateHeadersCount(
                headers,
                this.ActionResult.Content.Headers,
                this.ThrowNewHttpResponseMessageAssertionException,
                isContentHeaders: true);

            headers.ForEach(h => this.ValidContainingContentHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Testuje, či je stavový kód správy o odozve HTTP rovnaký ako kód HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithStatusCode(HttpStatusCode statusCode)
        {
            HttpResponseMessageValidator.ValidWithStatusCode(this.ActionResult, statusCode, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia ako reťazec.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithVersion(string version)
        {
            var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewHttpResponseMessageAssertionException);
            return this.ValidWithVersion(parsedVersion);
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="major">Major number in the expected version.</param>
        /// <param name="minor">Minor number in the expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithVersion(int major, int minor)
        {
            return this.ValidWithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithVersion(Version version)
        {
            HttpResponseMessageValidator.WithVersion(this.ActionResult, version, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či fráza odôvodnenia správy HTTP je rovnaká ako fráza poskytnutá dôvodom ako reťazec.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithReasonPhrase(string reasonPhrase)
        {
            HttpResponseMessageValidator.ValidWithReasonPhrase(this.ActionResult, reasonPhrase, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či hlásenie odozvy HTTP vráti stavový kód úspešnosti medzi 200 a 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpResponseMessageTestBuilder ValidWithSuccessStatusCode()
        {
            HttpResponseMessageValidator.WithSuccessStatusCode(this.ActionResult, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťaze testov správ HTTP odpovede.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IHttpResponseMessageTestBuilder AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Získanie správy o požiadavke HTTP, ktorá sa použila pri testovaní.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        public HttpResponseMessage AndProvideTheHttpResponseMessage()
        {
            return this.ActionResult;
        }

        private string GetStringContent()
        {
            return this.ActionResult.Content.ReadAsStringAsync().Result;
        }

        private void ThrowNewHttpResponseMessageAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpResponseMessageAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP response message result {2} {3}, but {4}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    propertyName,
                    expectedValue,
                    actualValue));
        }

        private ResponseModelAssertionException ThrowNewResponseModelAssertionException(string expectedResponseModel, string actualResponseModel)
        {
            return new ResponseModelAssertionException(string.Format(
                    "When calling {0} action in {1} expected HTTP response message model to {2}, but {3}.",
                    this.ActionName,
                    this.Controller.GetName(),
                    expectedResponseModel,
                    actualResponseModel));
        }
    }
}
