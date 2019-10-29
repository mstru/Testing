namespace Testing.Automation.API.Setups.Handlers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    public class CustomMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.Run(() => new HttpResponseMessage(HttpStatusCode.OK), cancellationToken);
        }
    }
}
