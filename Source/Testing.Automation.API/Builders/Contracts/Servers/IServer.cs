namespace Testing.Automation.API.Builders.Contracts.Servers
{
    using System.Web.Http;

    /// <summary>
    /// Poskytuje možnosti spustenia a zastavenia serverov HTTP, ako aj spracovanie žiadostí HTTP na plné testovanie potrubia.
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// Starts new global HTTP server.
        /// </summary>
        /// <param name="httpConfiguration">Optional HTTP configuration to use. If no configuration is provided, the global configuration will be used instead.</param>
        /// <returns>Server builder.</returns>
        IApiBuilder Starts(HttpConfiguration httpConfiguration = null);

        /// <summary>
        /// Starts new global OWIN server.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Network port on which the server will listen for requests.</param>
        /// <param name="host">Network host on which the server will listen for requests.</param>
        /// <returns>Server builder.</returns>
        IApiBuilder Starts<TStartup>();

        /// <summary>
        /// Configures global remote server.
        /// </summary>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        /// <returns>Server builder.</returns>
        IApiBuilder IsLocatedAt(string baseAddress);

        /// <summary>
        /// Stops all currently started global HTTP or OWIN servers.
        /// </summary>
        void Stops();

        /// <summary>
        /// Processes HTTP requests on globally started HTTP or OWIN servers. If global OWIN server is started, it will be used. If not the method will check for global HTTP server to use. If such is not found, a new instance of HTTP server is created with the global HTTP configuration.
        /// </summary>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IApiBuilder Working();

        /// <summary>
        /// Starts new HTTP server to process a request.
        /// </summary>
        /// <param name="httpConfiguration">HTTP configuration to use in the testing.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IApiBuilder Working(HttpConfiguration httpConfiguration);

        /// <summary>
        /// Starts new OWIN server to process a request.
        /// </summary>
        /// <typeparam name="TStartup">OWIN startup class to use.</typeparam>
        /// <param name="port">Network port on which the server will listen for requests.</param>
        /// <param name="host">Network host on which the server will listen for requests.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IApiBuilder Working<TStartup>();

        /// <summary>
        /// Processes HTTP request on globally configured remote HTTP server.
        /// </summary>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IApiBuilder WorkingRemotely();

        /// <summary>
        /// Processes HTTP request on the remote HTTP server located at the provided base address.
        /// </summary>
        /// <param name="baseAddress">Base address to use for the requests.</param>
        /// <returns>Server builder to set specific HTTP requests.</returns>
        IApiBuilder WorkingRemotely(string baseAddress);
    }
}
