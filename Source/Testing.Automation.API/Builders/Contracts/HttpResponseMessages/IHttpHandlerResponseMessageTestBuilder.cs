namespace Testing.Automation.API.Builders.Contracts.HttpResponseMessages
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using Base;
    using Models;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Používa sa na testovanie výsledkov správ HTTP odpovede.
    /// </summary>
    public interface IHttpHandlerResponseMessageTestBuilder : IBaseHandlerTestBuilder
    {
        /// <summary>
        /// Testuje tvrdenie, ze objekt sa rovna inemu objektu pomocou System.Object.Equals
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldEqual<T>(string name, T value);

        /// <summary>
        /// Testuje tvrdenie, ze objekt sa rovna inemu objektu pomocou System.Object.Equals
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldEqual<T>(IDictionary<string, T> values);

        /// <summary>
        /// Testuje tvrdenie, ze aktualny System.DateTime je teraz alebo potom
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOnOrAfter(IDictionary<string, DateTime> values);

        /// <summary>
        /// Testuje tvrdenie, ze aktualny System.DateTime je teraz alebo predtym
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOnOrBefore(IDictionary<string, DateTime> values);

        /// <summary>
        /// Testuje tvrdenie, ze System.String je jednou zo specifikovanych values
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOneOf(string objectPath, IEnumerable<string> values);

        /// <summary>
        /// Testuje tvrdenie, ze System.String je jednou zo specifikovanych values
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeOneOf(IDictionary<string, IEnumerable<string>> values);

        /// <summary>
        /// Testuje tvrdenie, ze pocet poloziek v kolekcii je mensi alebo rovnaky ako ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldHaveCountLessOrEqualTo(int value);

        /// <summary>
        /// Testuje tvrdenie, ze objekt sa nerovna inemu objektu pomocou System.Object.Equals 
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBe<T>(string name, T value);

        /// <summary>
        /// Testuje tvrdenie, ze objekt sa nerovna inemu objektu pomocou System.Object.Equals 
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBe<T>(IDictionary<string, T> values);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je vyssia ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterThan(string name, long value);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je vyssia ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterThan(IDictionary<string, long> values);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je vacsia alebo rovna ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterOrEqualTo(string name, long value);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je vacsia alebo rovna ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeGreaterOrEqualTo(IDictionary<string, long> values);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je nizsia ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessThan(string name, long value);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je nizsia ako zadana ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessThan(IDictionary<string, long> values);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je mensia alebo rovna ako ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessOrEqualTo(string name, long value);

        /// <summary>
        /// Testuje tvrdenie, ze ciselna hodnota je mensia alebo rovna ako ocakavana hodnota
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeLessOrEqualTo(IDictionary<string, long> values);

        /// <summary>
        /// Testuje tvrdenie, ze retazec obsahuje specifikovane ocakavane hodnoty
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldContainEquivalentOf(IEnumerable<string> names, long value);

        /// <summary>
        /// Testuje tvrdenie, ze retazec obsahuje specifikovane ocakavane hodnoty
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldContainEquivalentOf(IDictionary<IEnumerable<string>, long> values);

        /// <summary>
        /// Testuje tvrdenie, ze hodnota je true
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeTrue(string name);

        /// <summary>
        /// Testuje tvrdenie, ze hodnota je false
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldBeFalse(string name);

        /// <summary>
        /// Testuje tvrdenie, ze hodnota nie je null
        /// </summary>
        IAndHttpHandlerResponseMessageTestBuilder CheckShouldNotBeNull();

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu objektu HTTP odpoveď.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu objektu HTTP odpoveď.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModelDeserialize<TResponseModel>();

        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu objektu HTTP odpoveď.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponsemodelDeserialize<TResponseModel>();

        /// <summary>
        /// Testuje, či sa objekt s obsahom odpovedí HTTP vráti hlboko rovnaký objekt ako poskytnutý.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IHttpHandlerModelDetailsTestBuilder<TResponseModel> WithResponseModel<TResponseModel>(TResponseModel expectedModel);

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovede určitého typu.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithContentOfType<TContentType>()
            where TContentType : HttpContent;

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovedí poskytnutým reťazcom.
        /// </summary>
        /// <param name="content">Expected string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(string content);

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Action<JObject> assertions);

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Action<string> assertions);

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá danému predikátu.
        /// </summary>
        /// <param name="predicate">Predicate testing the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithStringContent(Func<string, bool> predicate);

        /// <summary>
        /// Testuje, či má odpoveď HTTP nainštalovaný formátovač médií.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Testuje, či správa HTTP odpovedí má poskytnutý typ formátora typu média.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje predvolený formátovač médií poskytovaný rámcom.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithDefaultMediaTypeFormatter();

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeaderType(string name);


        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s urcitou dlzkou.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingLength();

        /// <summary>
        /// Testuje, či odpoveď HTTP obsahuje hlavičku odpovede s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name, string value);

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičku odpovede s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičky odpovedí poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku obsahu s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="value">Value of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name, string value);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="values">Collection of values in the expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičky obsahu poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing content headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidContainingContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Tests whether HTTP response message status code is the same as the provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Testuje, či je stavový kód správy o odozve HTTP rovnaký ako kód HttpStatusCode.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(string version);

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="major">Major number in the expected version.</param>
        /// <param name="minor">Minor number in the expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(int major, int minor);

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithVersion(Version version);

        /// <summary>
        /// Testuje, či fráza odôvodnenia správy HTTP je rovnaká ako fráza poskytnutá dôvodom ako reťazec.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithReasonPhrase(string reasonPhrase);

        /// <summary>
        /// Testuje, či hlásenie odozvy HTTP vráti stavový kód úspešnosti medzi 200 a 299.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpHandlerResponseMessageTestBuilder ValidWithSuccessStatusCode();

        /// <summary>
        /// Získanie správy o požiadavke HTTP, ktorá sa používa pri testovaní obsluhy.
        /// </summary>
        /// <returns>Instance of HttpRequestMessage.</returns>
        HttpResponseMessage AndProvideTheHttpRequestMessage();

        /// <summary>
        ///  Získanie JSON
        /// </summary>
        /// <returns></returns>
        T JSON<T>();

        string WithStringContent();
    }
}
