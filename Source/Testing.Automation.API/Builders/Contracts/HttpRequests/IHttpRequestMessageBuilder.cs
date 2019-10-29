
namespace Testing.Automation.API.Builders.Contracts.HttpRequests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using Uris;

    /// <summary>
    /// Používa sa na vytvorenie správy HTTP.
    /// </summary>
    public interface IHttpRequestMessageBuilder
    {
        /// <summary>
        /// Pridáva obsah HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="content">HTTP content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContent(HttpContent content);

        /// <summary>
        /// Pridáva obsah protokolu HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream);

        /// <summary>
        /// Pridáva obsah protokolu HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <param name="bufferSize">Buffer size of the added HTTP stream content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize);

        /// <summary>
        /// Pridá HTTP bytový pohľad do vstavanej správy HTTP.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes);

        /// <summary>
        /// Pridá HTTP bytový pohľad do vstavanej správy HTTP.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <param name="offset">Offset in the HTTP byte array content.</param>
        /// <param name="count">The number of bytes in the HTTP byte array content to use.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count);

        /// <summary>
        /// Pridáva kódovaný obsah adresy URL formátu HTTP na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="nameValueCollection">Name value pairs collection.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection);

        /// <summary>
        /// Adds HTTP form URLs to the built HTTP request message.
        /// </summary>
        /// <param name="queryString">String representing the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(string queryString);

        /// <summary>
        /// Pridáva obsah JSON k vstavanej správe HTTP.
        /// </summary>
        /// <param name="jsonContent">JSON string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithBody(string jsonContent);

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content);

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, string mediaType);

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding);

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType);

        /// <summary>
        /// Pridá hlavičku do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeader(string name, string value);

        /// <summary>
        /// Pridá hlavičku do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Pridá zbierku hlavičiek do správy správy HTTP.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Pridá hlavičku obsahu do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeader(string name, string value);

        /// <summary>
        /// Pridá hlavičku obsahu do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeader(string name, IEnumerable<string> values);

        /// <summary>
        /// Pridá kolekciu záhlavia obsahu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers);

        /// <summary>
        /// Pridáva metódu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithMethod(string method);

        /// <summary>
        /// Pridáva metódu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="method">HTTP method represented by HttpMethod type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithMethod(HttpMethod method);

        /// <summary>
        /// Pridá uri žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(string location);

        /// <summary>
        /// Pridá uri žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(Uri location);


        /// <summary>
        /// Pridá uri žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(Uri location, string response, string path);

        /// <summary>
        /// Pridá uri žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(string location, string response, string path);

        /// <summary>
        /// Pridá uri žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(string location, string response, string path, string withAction);

        /// <summary>
        /// Pridá uri žiadosti do správy HTTP..
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder);

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="version">HTTP version provided by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(string version);

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="major">Major number in the provided version.</param>
        /// <param name="minor">Minor number in the provided version.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(int major, int minor);

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="version">HTTP version provided by Version type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        IAndHttpRequestMessageBuilder WithVersion(Version version);
    }
}
