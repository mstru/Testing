namespace Testing.Automation.API.Builders.Attributes
{
    using System;
    using System.Web.Http;
    using Common.Extensions;
    using Contracts.Attributes;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    ///Pouziva sa na testovanie controller atributov.
    /// </summary>
    public class ControllerAttributesTestBuilder : BaseAttributesTestBuilder, IAndControllerAttributesTestBuilder
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="ControllerAttributesTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller je testovany.</param>
        public ControllerAttributesTestBuilder(ApiController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// Skontroluje ci zoznam atributov obsahuje zadany typ atributu.
        /// </summary>
        /// <typeparam name="TAttribute">Typ ocakavaneho atributu.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obsahujú RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Optional expected route name.</param>
        /// <param name="withOrder">Optional expected route order.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ChangingRouteTo(
            string template,
            string withName = null,
            int? withOrder = null)
        {
            this.ChangingRouteTo(
                template,
                this.ThrowNewAttributeAssertionException,
                withName,
                withOrder);

            return this;
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obsahujú RoutePrefixAttribute.
        /// </summary>
        /// <param name="prefix">Expected overridden route prefix of the controller.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder ChangingRoutePrefixTo(string prefix)
        {
            this.ContainingAttributeOfType<RoutePrefixAttribute>();
            this.Validations.Add(attrs =>
            {
                var routePrefixAttribute = this.GetAttributeOfType<RoutePrefixAttribute>(attrs);
                var actualRoutePrefix = routePrefixAttribute.Prefix;
                if (prefix.ToLower() != actualRoutePrefix.ToLower())
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} with '{1}' prefix", routePrefixAttribute.GetName(), prefix),
                        string.Format("in fact found '{0}'", actualRoutePrefix));
                }
            });

            return this;
        }

        /// <summary>
        /// CSkontroluje, či zhromaždené atribúty obsahujú AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// CSkontroluje, či zhromaždené atribúty obsahujú AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndControllerAttributesTestBuilder RestrictingForAuthorizedRequests(
            string withAllowedRoles = null,
            string withAllowedUsers = null)
        {
            this.RestrictingForAuthorizedRequests(
                this.ThrowNewAttributeAssertionException,
                withAllowedRoles,
                withAllowedUsers);

            return this;
        }

        /// <summary>
        /// AndAlso pre lepsiu citatelnost.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IControllerAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                "When testing {0} was expected to have {1}, but {2}.",
                this.Controller.GetName(),
                expectedValue,
                actualValue));
        }
    }
}
