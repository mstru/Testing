namespace Testing.Automation.API.Common.Extensions
{
    using System;
    using System.Net.Http;

    /// <summary>
    /// Poskytuje metódy rozšírenia na HttpRequestMessage
    /// </summary>
    public static class HttpRequestMessageExtensions
    {
        /// <summary>
        /// Transformuje URI vyžiadania HTTP z relatívneho na absolútneho s falošným hostiteľom.
        /// </summary>
        /// <param name="request">HTTP žiadosť o premenu</param>
        public static void TransformToAbsoluteRequestUri(this HttpRequestMessage request)
        {
            if (request.RequestUri != null && !request.RequestUri.IsAbsoluteUri)
            {
                //request.RequestUri = new Uri(WebApiTesting.BaseAddress, request.RequestUri.OriginalString);
                request.RequestUri = new Uri(MyWebApi.BaseAddress + request.RequestUri.OriginalString);
            }       
        }
    }
}
