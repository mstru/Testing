namespace Testing.Automation.API.Setups.Common
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
