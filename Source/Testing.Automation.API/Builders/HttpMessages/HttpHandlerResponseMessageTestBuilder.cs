namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using Base;
    using Common.Extensions;
    using Contracts.HttpResponseMessages;
    using Contracts.Models;
    using Models;
    using Utilities.Validators;
    using Newtonsoft.Json.Linq;
    using global::Testing.Automation.Reporter.Exceptions;
    using Newtonsoft.Json;

    /// <summary>
    /// Používa sa na testovanie výsledkov správ HTTP.
    /// </summary>
    public class HttpHandlerResponseMessageTestBuilder
        : BaseHandlerTestBuilder, IAndHttpHandlerResponseMessageTestBuilder
    {
        private readonly HttpResponseMessage httpResponseMessage;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpHandlerResponseMessageTestBuilder" /> class.
        /// </summary>
        /// <param name="httpResponseMessage">HTTP response result from the tested handler.</param>
        public HttpHandlerResponseMessageTestBuilder(
            HttpResponseMessage httpResponseMessage)
            : base()
        {
            CommonValidator.CheckForNullReference(httpResponseMessage, errorMessageName: "HttpResponseMessage");
            this.httpResponseMessage = httpResponseMessage;
        }

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu správy odpovedí HTTP.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>()
        {
            var actualModel = HttpResponseMessageValidator.GetActualContentModel<TResponseModel>(
                this.httpResponseMessage.Content,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this,
                actualModel);
        }

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu správy odpovedí HTTP.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModelDeserialize<TResponseModel>()
        {
            var actualModel = HttpResponseMessageValidator.DeserializeActualContentModel<TResponseModel>(
                this.httpResponseMessage.Content,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this,
                actualModel);
        }

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu správy odpovedí HTTP.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponsemodelDeserialize<TResponseModel>()
        {
            var actualModel = HttpResponseMessageValidator.DeserializeactualContentModel<TResponseModel>(
                this.httpResponseMessage.Content,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this,
                actualModel);
        }

        /// <summary>
        /// Testuje, či sa od spomenutého obsahu správy HTTP odošle rovnaký objekt ako bol poskytnutý.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        public IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel)
        {
            var actualModel = HttpResponseMessageValidator.WithResponseModel(
                this.httpResponseMessage.Content,
                expectedModel,
                this.ThrowNewHttpResponseMessageAssertionException,
                this.ThrowNewResponseModelAssertionException);

            return new HttpHandlerModelDetailsTestBuilder<TResponseModel>(
                this,
                actualModel);
        }

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovede určitého typu.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithContentOfType<TContentType>()
            where TContentType : HttpContent
        {
            HttpResponseMessageValidator.WithContentOfType<TContentType>(
                this.httpResponseMessage.Content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovedí poskytnutým reťazcom.
        /// </summary>
        /// <param name="content">Expected string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(string content)
        {
            HttpResponseMessageValidator.WithStringContent(
                this.httpResponseMessage.Content,
                content,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Action<JObject> assertions)
        {
            HttpResponseMessageValidator.ContainingResponse(
                this.httpResponseMessage.Content, 
                this.ThrowNewHttpResponseMessageAssertionException);
            assertions(JObject.Parse(this.GetJsonContent()));

            return this;
        }

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Action<string> assertions)
        {
            HttpResponseMessageValidator.ContainingResponse(
                this.httpResponseMessage.Content,
                this.ThrowNewHttpResponseMessageAssertionException);

            assertions(this.GetJsonContent());

            return this;
        }

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá danému predikátu.
        /// </summary>
        /// <param name="predicate">Predicate testing the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Func<string, bool> predicate)
        {
            if (!predicate(this.GetJsonContent()))
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter)
        {
            MediaTypeFormatterValidator.ValidateMediaTypeFormatter(
                this.httpResponseMessage.Content,
                mediaTypeFormatter,
                this.ThrowNewHttpResponseMessageAssertionException);

            return this;
        }

        /// <summary>
        /// Testuje, či správa HTTP odpovedí má poskytnutý typ formátora typu média.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new()
        {
            return this.ValidWithMediaTypeFormatter(Activator.CreateInstance<TMediaTypeFormatter>());
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje predvolený formátovač médií poskytovaný rámcom.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithDefaultMediaTypeFormatter()
        {
            return this.ValidWithMediaTypeFormatterOfType<JsonMediaTypeFormatter>();
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeaderType(string name)
        {
            HttpResponseMessageValidator.ContainHeaderType(this.httpResponseMessage.Content, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s urcitou dlzkou.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingLength()
        {
            HttpResponseMessageValidator.ContainLength(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či odpoveď HTTP obsahuje hlavičku odpovede s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name, string value)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičku odpovede s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="validValues">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name, IEnumerable<string> validValues)
        {
            HttpResponseMessageValidator.ContainingHeader(this.httpResponseMessage.Headers, name, validValues, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze objekt sa rovna inemu objektu pomocou System.Object.Equals
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Ocakavana hodnota objektu</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldEqual<T>(string name, T value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldEqual(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze objekt sa rovna inemu objektu pomocou System.Object.Equals
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Ocakavana hodnota objektu</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldEqual<T>(IDictionary<string, T> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldEqual(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdi, ze aktualny System.DateTime je teraz alebo potom
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOnOrAfter(IDictionary<string, DateTime> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(h => HttpResponseMessageValidator.ShouldBeOnOrAfter(this.httpResponseMessage.Content, h.Key, h.Value, this.ThrowNewHttpResponseMessageAssertionException));
            return this;
        }

        /// <summary>
        /// Tvrdi, ze aktualny System.DateTime je teraz alebo predtym
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOnOrBefore(IDictionary<string, DateTime> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(h => HttpResponseMessageValidator.ShouldBeOnOrBefore(this.httpResponseMessage.Content, h.Key, h.Value, this.ThrowNewHttpResponseMessageAssertionException));
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze System.String je jednou zo specifikovanych values
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="values">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOneOf(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeOneOf(this.httpResponseMessage.Content, name, values, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze System.String je jednou zo specifikovanych values
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="values">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOneOf(IDictionary<string, IEnumerable<string>> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldBeOneOf(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze pocet poloziek v kolekcii je mensi alebo rovnaky ako ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="value">Ocakavana hodnota objektu</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldHaveCountLessOrEqualTo(int value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldHaveCountLessOrEqualTo(this.httpResponseMessage.Content, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze objekt sa nerovna inemu objektu pomocou System.Object.Equals 
        /// </summary>
        /// <typeparam name="T">Typ objektu</typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBe<T>(string name, T value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldNotBe(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze objekt sa nerovna inemu objektu pomocou System.Object.Equals 
        /// </summary>
        /// <typeparam name="T">Typ objektu</typeparam>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBe<T>(IDictionary<string, T> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldNotBe(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vyssia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterThan(string name, long value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeGreaterThan(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vyssia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterThan(IDictionary<string, long> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldBeGreaterThan(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vacsia alebo rovna ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterOrEqualTo(string name, long value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeGreaterOrEqualTo(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je vacsia alebo rovna ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterOrEqualTo(IDictionary<string, long> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldBeGreaterOrEqualTo(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je nizsia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessThan(string name, long value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeLessThan(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdenie, ze ciselna hodnota je nizsia ako zadana ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessThan(IDictionary<string, long> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldBeLessThan(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdi, ze ciselna hodnota je mensia alebo rovna ako ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessOrEqualTo(string name, long value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeLessOrEqualTo(this.httpResponseMessage.Content, name, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze ciselna hodnota je mensia alebo rovna ako ocakavana hodnota
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazov property objektu</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessOrEqualTo(IDictionary<string, long> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldBeLessOrEqualTo(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdi, ze retazec obsahuje specifikovane ocakavane hodnoty
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldContainEquivalentOf(IEnumerable<string> names, long value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldContainEquivalentOf(this.httpResponseMessage.Content, names, value, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze retazec obsahuje specifikovane ocakavane hodnoty
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        /// <param name="value">Specifikovane hodnoty</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldContainEquivalentOf(IDictionary<IEnumerable<string>, long> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            values.ForEach(v => this.CheckShouldContainEquivalentOf(v.Key, v.Value));
            return this;
        }

        /// <summary>
        /// Tvrdi, ze hodnota je true
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeTrue(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeTrue(this.httpResponseMessage.Content, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze hodnota je false
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        /// <param name="name">Nazvy property objektov</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeFalse(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldBeFalse(this.httpResponseMessage.Content, name, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Tvrdi, ze hodnota nie je null
        /// </summary>
        /// <param name="actualContent">Aktualny Http content</param>
        public IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBeNull()
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ShouldNotBeNull(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }


        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičky odpovedí poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateHeadersCount(headers, this.httpResponseMessage.Headers, this.ThrowNewHttpResponseMessageAssertionException);
            headers.ForEach(h => this.ValidContainingHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name, string value)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name, IEnumerable<string> values)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ContainingHeader(
                this.httpResponseMessage.Content.Headers,
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeaders(
            IDictionary<string, IEnumerable<string>> headers)
        {
            HttpResponseMessageValidator.ValidateContent(this.httpResponseMessage.Content, this.ThrowNewHttpResponseMessageAssertionException);
            HttpResponseMessageValidator.ValidateHeadersCount(
                headers,
                this.httpResponseMessage.Content.Headers,
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithStatusCode(HttpStatusCode statusCode)
        {
            HttpResponseMessageValidator.ValidWithStatusCode(this.httpResponseMessage, statusCode, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia ako reťazec.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(string version)
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
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(int major, int minor)
        {
            return this.ValidWithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(Version version)
        {
            HttpResponseMessageValidator.WithVersion(this.httpResponseMessage, version, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či fráza odôvodnenia správy HTTP je rovnaká ako fráza poskytnutá dôvodom ako reťazec.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithReasonPhrase(string reasonPhrase)
        {
            HttpResponseMessageValidator.ValidWithReasonPhrase(this.httpResponseMessage, reasonPhrase, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// Testuje, či hlásenie odozvy HTTP vráti stavový kód úspešnosti medzi 200 a 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IAndHttpHandlerResponseMessageTestBuilder ValidWithSuccessStatusCode()
        {
            HttpResponseMessageValidator.WithSuccessStatusCode(this.httpResponseMessage, this.ThrowNewHttpResponseMessageAssertionException);
            return this;
        }

        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťaze testov správ HTTP odpovede.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        public IHttpHandlerResponseMessageTestBuilder AndAlso()
        {
            return this;
        }

        /// <summary>
        /// Získa správu odpovedí HTTP použitú pri testovaní.
        /// </summary>
        /// <returns>List entity collections.</returns>
        public HttpResponseMessage AndProvideTheHttpRequestMessage()
        {
            return this.httpResponseMessage;
        }

        public string WithStringContent()
        {
            return GetJsonContent();
        }

        public T JSON<T>()
        {       
            var actualContentAsString = GetJsonContent();

            var containerToken = JToken.Parse(actualContentAsString);

            if (containerToken.Count() == 0)
                throw new VerificationException("Data not found!");

            if (containerToken.Type == JTokenType.Array)
            {
                var o = JArray.Parse(actualContentAsString);
                try
                {
                    return o["value"].Value<T>();
                }
                catch(Exception ex)
                {
                    return o.Value<T>();
                }      
            }
            else
            {
                var o = JObject.Parse(actualContentAsString);
                try
                {
                    return o["value"].Value<T>();
                }
                catch (Exception ex)
                {
                    return o.Value<T>();
                }
            }
        }

        protected void ThrowNewHttpResponseMessageAssertionException(string propertyName, string expectedValue, string actualValue, string myJSON)
        {
            Reporter.Reporter.Fail(myJSON, Reporter.CodeBlockType.Json);

            throw new HttpResponseMessageAssertionException(string.Format(
                "{4}Testing HTTP response message result, {0}{1}{2}. {4}{3}",
                    propertyName,
                    expectedValue,
                    actualValue,
                    this.FormatHttpResponseMessage(false),
                    Environment.NewLine));
        }


        protected void ThrowNewHttpResponseMessageAssertionException(string propertyName, string expectedValue, string actualValue)
        {
            throw new HttpResponseMessageAssertionException(string.Format(
                "{4}Testing HTTP response message result, {0}{1}{2}. {4}{3}",
                    propertyName,
                    expectedValue,
                    actualValue,
                    this.FormatHttpResponseMessage(true),
                    Environment.NewLine));
        }

        private ResponseModelAssertionException ThrowNewResponseModelAssertionException(string expectedResponseModel, string actualResponseModel)
        {
            return new ResponseModelAssertionException(string.Format(
                "When testing {0} expected HTTP response message model to {1}, but {2}.",
                    this.Handler.GetName(),
                    expectedResponseModel,
                    actualResponseModel));
        }

        private string GetJsonContent()
        {
            return this.httpResponseMessage.Content.ReadAsStringAsync().Result;
        }

        private string FormatHttpResponseMessage(bool isFormatHttpResponseMessage)
        {
            var statusCode = (int)this.httpResponseMessage.StatusCode;
            dynamic parsedJson = JsonConvert.DeserializeObject(this.httpResponseMessage.Content.ReadAsStringAsync().Result);
            var actualResponseFormat = JsonConvert.SerializeObject(parsedJson, Formatting.Indented); ;

            var headers = !this.httpResponseMessage.Headers.Any()
                ? new[] { "None" }
                : this.httpResponseMessage.Headers.Select(h => string.Format("{0} - '{1}'", h.Key, string.Join("; ", h.Value)));

            return string.Format("{3}Uri: '{0}' {3}{3}Status code: '{1}' {2}",
                httpResponseMessage.RequestMessage.RequestUri,
                statusCode,
                (isFormatHttpResponseMessage == false) ? string.Empty : string.Format("{2} <-------------------------------- JSON DATA --------------------------------> {2} Headers: {2}{1}, {2} Actual JSON: {0}", actualResponseFormat, string.Join(Environment.NewLine, headers), Environment.NewLine),
                Environment.NewLine);
        }
    }
}
