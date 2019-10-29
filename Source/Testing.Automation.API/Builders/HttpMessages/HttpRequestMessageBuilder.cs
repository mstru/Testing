namespace Testing.Automation.API.Builders.HttpMessages
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using Common.Extensions;
    using Contracts.HttpRequests;
    using Contracts.Uris;
    using Uris;
    using Utilities.Validators;
    using Helper;
    using global::Testing.Automation.Reporter.Exceptions;
    using global::Testing.Automation.Reporter;


    /// <summary>
    /// Používa sa na vytvorenie správy HTTP.
    /// </summary>
    public class HttpRequestMessageBuilder : IAndHttpRequestMessageBuilder
    {
        public readonly HttpRequestMessage requestMessage;

        public static string getTestedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessageBuilder" /> class.
        /// </summary>
        public HttpRequestMessageBuilder()
        {
            this.requestMessage = new HttpRequestMessage();
        }

        /// <summary>
        /// Pridáva obsah HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="content">HTTP content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContent(HttpContent content)
        {
            this.requestMessage.Content = content;
            return this;
        }

        /// <summary>
        /// Pridáva obsah prúdu protokolu HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream)
        {
            return this.WithContent(new StreamContent(stream));
        }

        /// <summary>
        ///Pridáva obsah prúdu protokolu HTTP k vstavanej správe o požiadavke HTTP.
        /// </summary>
        /// <param name="stream">HTTP stream content to add.</param>
        /// <param name="bufferSize">Buffer size of the added HTTP stream content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStreamContent(Stream stream, int bufferSize)
        {
            return this.WithContent(new StreamContent(stream, bufferSize));
        }

        /// <summary>
        /// Pridá HTTP bytový pohľad do vstavanej správy HTTP.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes)
        {
            return this.WithContent(new ByteArrayContent(bytes));
        }

        /// <summary>
        /// Pridá HTTP bytový pohľad do vstavanej správy HTTP.
        /// </summary>
        /// <param name="bytes">HTTP byte array content to add.</param>
        /// <param name="offset">Offset in the HTTP byte array content.</param>
        /// <param name="count">The number of bytes in the HTTP byte array content to use.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithByteArrayContent(byte[] bytes, int offset, int count)
        {
            return this.WithContent(new ByteArrayContent(bytes, offset, count));
        }

        /// <summary>
        /// Pridáva kódovaný obsah adresy URL formátu HTTP na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="nameValueCollection">Name value pairs collection.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(
            IEnumerable<KeyValuePair<string, string>> nameValueCollection)
        {
            return this.WithContent(new FormUrlEncodedContent(nameValueCollection));
        }

        /// <summary>
        /// Pridáva kódovaný obsah adresy URL formátu HTTP na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="queryString">String representing the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithFormUrlEncodedContent(string queryString)
        {
            return this.WithContent(new StringContent(queryString, Encoding.UTF8, MediaType.FormUrlEncoded));
        }

        /// <summary>
        /// Pridáva obsah JSON k vstavanej správe HTTP.
        /// </summary>
        /// <param name="body">JSON string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithBody(string body)
        {
            Reporter.Info(body, CodeBlockType.Json);
            return this.WithContent(new StringContent(body, Encoding.UTF8, MediaType.ApplicationJson));
        }

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content)
        {
            return this.WithContent(new StringContent(content));
        }

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content, string mediaType)
        {
            return this.WithContent(new StringContent(content, Encoding.UTF8, mediaType));
        }

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding)
        {
            return this.WithContent(new StringContent(content, encoding));
        }

        /// <summary>
        /// Pridá reťazec obsahu HTTP na vstavanú požiadavku HTTP.
        /// </summary>
        /// <param name="content">String content to add.</param>
        /// <param name="encoding">Encoding used in the string content.</param>
        /// <param name="mediaType">Type of media to use in the content.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithStringContent(string content, Encoding encoding, string mediaType)
        {
            return this.WithContent(new StringContent(content, encoding, mediaType));
        }

        /// <summary>
        /// Pridá hlavičku do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeader(string name, string value)
        {
            this.requestMessage.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Pridá hlavičku do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeader(string name, IEnumerable<string> values)
        {
            this.requestMessage.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Pridá zbierku hlavičiek do správy správy HTTP.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Pridá hlavičku obsahu do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="value">Value of the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeader(string name, string value)
        {
            HttpRequestMessageValidator.ValidateContent(this.requestMessage);
            this.requestMessage.Content.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Pridá hlavičku obsahu do správy správy HTTP.
        /// </summary>
        /// <param name="name">Name of the header.</param>
        /// <param name="values">Collection of values for the header.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeader(string name, IEnumerable<string> values)
        {
            HttpRequestMessageValidator.ValidateContent(this.requestMessage);
            this.requestMessage.Content.Headers.Add(name, values);
            return this;
        }

        /// <summary>
        /// Pridá kolekciu záhlavia obsahu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="headers">Dictionary of headers to add.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithContentHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            headers.ForEach(h => this.WithContentHeader(h.Key, h.Value));
            return this;
        }

        /// <summary>
        /// Pridáva metódu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="method">HTTP method represented by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithMethod(string method)
        {
            return this.WithMethod(new HttpMethod(method));
        }

        /// <summary>
        /// Pridáva metódu na vytvorenú správu o požiadavke HTTP.
        /// </summary>
        /// <param name="method">HTTP method represented by HttpMethod type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithMethod(HttpMethod method)
        {
            this.requestMessage.Method = method;
            Reporter.Info(method.GetName() + ": " + method.ToString(), CodeBlockType.Label);
            return this;
        }

        /// <summary>
        /// Pridá umiestnenie žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(string location)
        {
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                location,
                this.ThrowNewInvalidHttpRequestMessageException);
            Reporter.Info("Uri: " + string.Format("{0}{1}", MyWebApi.BaseAddress, requestMessage.RequestUri.ToString()), CodeBlockType.Label);          
            return this;
        }

        /// <summary>
        /// Pridá umiestnenie žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(Uri location)
        {
            this.requestMessage.RequestUri = location;
            return this;
        }

        /// <summary>
        /// Pridá umiestnenie žiadosti do správy HTTP.
        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(Uri location, string response, string path)
        {
            this.requestMessage.RequestUri = new Uri(location + "/" + this.GetTestedValueWithMessage(response, path));
            return this;
        }

        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(string location, string response, string path)
        {
            getTestedValue = this.GetTestedValueWithMessage(response, path);
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                location + "/" + getTestedValue,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this;
        }

        /// </summary>
        /// <param name="location">Expected location as URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(string location, string response, string path, string withAction)
        {
            getTestedValue = this.GetTestedValueWithMessage(response, path);
            this.requestMessage.RequestUri = LocationValidator.ValidateAndGetWellFormedUriString(
                location + "/" + getTestedValue + "/" + withAction,
                this.ThrowNewInvalidHttpRequestMessageException);

            return this;
        }

        /// <summary>
        /// Pridá miesto na žiadosť poskytnuté staviteľom do správy o požiadavke HTTP.
        /// </summary>
        /// <param name="uriTestBuilder">Builder for expected URI.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithRequestUri(Action<IUriTestBuilder> uriTestBuilder)
        {
            var mockedUriBuilder = new MockedUriBuilder();
            uriTestBuilder(mockedUriBuilder);
            this.requestMessage.RequestUri = mockedUriBuilder.GetUri();
            return this;
        }

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="version">HTTP version provided by string.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(string version)
        {
            var parsedVersion = VersionValidator.TryParse(version, this.ThrowNewInvalidHttpRequestMessageException);
            return this.WithVersion(parsedVersion);
        }

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="major">Major number in the provided version.</param>
        /// <param name="minor">Minor number in the provided version.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(int major, int minor)
        {
            return this.WithVersion(new Version(major, minor));
        }

        /// <summary>
        /// Pridá HTTP verziu do správy HTTP.
        /// </summary>
        /// <param name="version">HTTP version provided by Version type.</param>
        /// <returns>The same HTTP request message builder.</returns>
        public IAndHttpRequestMessageBuilder WithVersion(Version version)
        {
            this.requestMessage.Version = version;
            return this;
        }

        /// <summary>
        /// AnAndAlso metóda pre lepšiu čitateľnosť pri vytváraní správy HTTP.
        /// </summary>
        /// <returns>The same HTTP request message builder.</returns>
        public IHttpRequestMessageBuilder AndAlso()
        {
            return this;
        }

        internal HttpRequestMessage GetHttpRequestMessage()
        {
            return this.requestMessage;
        }

        internal string GetTestedValueWithMessage(string response, string path)
        {
            return JSONHelper.ResponseParserFind(response, path);
        }

        private void ThrowNewInvalidHttpRequestMessageException(string propertyName, string expectedValue, string actualValue)
        {
            throw new InvalidHttpRequestMessageException(string.Format(
                "When building HttpRequestMessage expected {0} to be {1}, but instead received {2}.",
                propertyName,
                expectedValue,
                actualValue));
        }
    }
}
