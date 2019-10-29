namespace Testing.Automation.API.Common.Servers
{
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Trieda HTTP message handler.
    /// </summary>
    public class ServerHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpMessageInvoker httpMessageInvoker;
        private readonly bool disposeInvoker;

        /// <summary>
        /// Inicializacia novej instancie <see cref="ServerHttpMessageHandler" /> triedy.
        /// </summary>
        /// <param name="httpMessageInvoker">HTTP message invoker na spracovanie ziadosti.</param>
        /// <param name="disposeInvoker">Označuje, či má byť invoker zlikvidovaný po spracovaní žiadosti.</param>
        public ServerHttpMessageHandler(HttpMessageInvoker httpMessageInvoker, bool disposeInvoker)
        {
            this.httpMessageInvoker = httpMessageInvoker;
            this.disposeInvoker = disposeInvoker;
        }

        /// <summary>
        /// Odošle žiadosť HTTP na poskytnutý server.
        /// </summary>
        /// <param name="request">HTTP request message na odoslanie.</param>
        /// <param name="cancellationToken">Cancellation token na cancel operacie.</param>
        /// <returns>Uloha HttpResponseMessage.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return this.httpMessageInvoker.SendAsync(request, cancellationToken);
        }

        /// <summary>
        /// Uvolnuje nespracovane zdroje pouzite systemom System.Net.Http.HttpMessageHandlers.
        /// </summary>
        /// <param name="disposing">True ak chcem uvolnit nespracovane a spracovane zdroje, false ak chceme uvolnit iba nespracovane zdroje.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.disposeInvoker)
            {
                this.httpMessageInvoker.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
