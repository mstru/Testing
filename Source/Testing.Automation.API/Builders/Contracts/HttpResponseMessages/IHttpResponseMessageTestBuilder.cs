namespace Testing.Automation.API.Builders.Contracts.HttpResponseMessages
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using Base;
    using Models;

    /// <summary>
    /// Používa sa na testovanie výsledkov správ HTTP odpovede.
    /// </summary>
    public interface IHttpResponseMessageTestBuilder : IBaseTestBuilderWithCaughtException
    {
        /// <summary>
        /// Testuje, či sa určitý typ modelu odpovede vráti z obsahu objektu HTTP odpoveď.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> ValidWithResponseModelOfType<TResponseModel>();

        /// <summary>
        /// Testuje, či sa objekt s obsahom odpovedí HTTP vráti hlboko rovnaký objekt ako poskytnutý.
        /// </summary>
        /// <typeparam name="TResponseModel">Type of the response model.</typeparam>
        /// <param name="expectedModel">Expected model to be returned.</param>
        /// <returns>Builder for testing the response model errors.</returns>
        IModelDetailsTestBuilder<TResponseModel> ValidWithResponseModel<TResponseModel>(TResponseModel expectedModel);

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovede určitého typu.
        /// </summary>
        /// <typeparam name="TContentType">Type of expected HTTP content.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithContentOfType<TContentType>()
            where TContentType : HttpContent;

        /// <summary>
        /// Testuje, či je obsah správy HTTP odpovedí poskytnutým reťazcom.
        /// </summary>
        /// <param name="content">Expected string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithStringContent(string content);

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá daným tvrdeniam.
        /// </summary>
        /// <param name="assertions">Action containing all assertions on the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithStringContent(Action<string> assertions);

        /// <summary>
        /// Testuje, či obsah reťazca správ HTTP odpovedá danému predikátu.
        /// </summary>
        /// <param name="predicate">Predicate testing the string content.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithStringContent(Func<string, bool> predicate);

        /// <summary>
        /// Testuje, či má odpoveď HTTP nainštalovaný formátovač médií.
        /// </summary>
        /// <param name="mediaTypeFormatter">Expected media type formatter.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithMediaTypeFormatter(MediaTypeFormatter mediaTypeFormatter);

        /// <summary>
        /// Testuje, či správa HTTP odpovedí má poskytnutý typ formátora typu média.
        /// </summary>
        /// <typeparam name="TMediaTypeFormatter">Type of MediaTypeFormatter.</typeparam>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithMediaTypeFormatterOfType<TMediaTypeFormatter>()
            where TMediaTypeFormatter : MediaTypeFormatter, new();

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje predvolený formátovač médií poskytovaný rámcom.
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithDefaultMediaTypeFormatter();

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku odpovede s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name);

        /// <summary>
        /// Testuje, či odpoveď HTTP obsahuje hlavičku odpovede s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="value">Value of expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name, string value);

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičku odpovede s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected response header.</param>
        /// <param name="values">Collection of values in the expected response header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičky odpovedí poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing response headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje hlavičku obsahu s určitým názvom a hodnotou.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="value">Value of expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name, string value);

        /// <summary>
        /// Testuje, či správa odozvy HTTP obsahuje záhlavie obsahu s určitým názvom a zberom hodnoty.
        /// </summary>
        /// <param name="name">Name of expected content header.</param>
        /// <param name="values">Collection of values in the expected content header.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingContentHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Testuje, či správa o odozve HTTP obsahuje hlavičky obsahu poskytnuté v slovníku.
        /// </summary>
        /// <param name="headers">Dictionary containing content headers.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidContainingContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Testuje, či je stavový kód správy o odozve HTTP rovnaký ako kód HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">Expected status code.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithStatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia ako reťazec.
        /// </summary>
        /// <param name="version">Expected version as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithVersion(string version);

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="major">Major number in the expected version.</param>
        /// <param name="minor">Minor number in the expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithVersion(int major, int minor);

        /// <summary>
        /// Testuje, či verzia správy HTTP odpovede je rovnaká ako poskytnutá verzia.
        /// </summary>
        /// <param name="version">Expected version.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithVersion(Version version);

        /// <summary>
        /// Testuje, či fráza odôvodnenia správy HTTP je rovnaká ako fráza poskytnutá dôvodom ako reťazec.
        /// </summary>
        /// <param name="reasonPhrase">Expected reason phrase as string.</param>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithReasonPhrase(string reasonPhrase);

        /// <summary>
        /// Testuje, či hlásenie odozvy HTTP vráti stavový kód úspešnosti medzi 200 a 299
        /// </summary>
        /// <returns>The same HTTP response message test builder.</returns>
        IAndHttpResponseMessageTestBuilder ValidWithSuccessStatusCode();

        /// <summary>
        /// Získate správu odpovedí HTTP použitú pri testovaní.
        /// </summary>
        /// <returns>Instance of HttpResponseMessage.</returns>
        HttpResponseMessage AndProvideTheHttpResponseMessage();
    }
}
