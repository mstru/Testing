namespace Testing.Automation.API.Builders.Routes
{
    using System.Web.Http;

    /// <summary>
    /// Base class for all route test builders.
    /// </summary>
    public abstract class BaseRouteTestBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRouteTestBuilder" /> class.
        /// </summary>
        /// <param name="httpConfiguration">Instance of HttpConfiguration.</param>
        protected BaseRouteTestBuilder(HttpConfiguration httpConfiguration)
        {
            this.HttpConfiguration = httpConfiguration;
        }

        /// <summary>
        /// Gets the HTTP configuration used in the route test.
        /// </summary>
        /// <value>Instance of HttpConfiguration.</value>
        protected HttpConfiguration HttpConfiguration { get; private set; }
    }
}
