namespace Testing.Automation.API.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using Common.Extensions;
    using Contracts.Attributes;
    using global::Testing.Automation.Reporter.Exceptions;

    /// <summary>
    /// Pouziva sa na testovanie atributov.
    /// </summary>
    public class ActionAttributesTestBuilder : BaseAttributesTestBuilder, IAndActionAttributesTestBuilder
    {
        private readonly string testedActionName;

        /// <summary>
        /// Inicializacia novej instancie <see cref="ActionAttributesTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller na ktorom sa testuju atributy.</param>
        /// <param name="actionName">Nazov testovanej akcie.</param>
        public ActionAttributesTestBuilder(ApiController controller, string actionName)
            : base(controller)
        {
            this.testedActionName = actionName;
        }

        /// <summary>
        /// Skontroluje ci zoznam atributov obsahuju zadany typ atributu.
        /// </summary>
        /// <typeparam name="TAttribute">Typ ocakavaneho atributu.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ContainingAttributeOfType<TAttribute>()
            where TAttribute : Attribute
        {
            this.ContainingAttributeOfType<TAttribute>(this.ThrowNewAttributeAssertionException);
            return this;
        }

        /// <summary>
        /// Skontroluje ci zoznam atributov obsahuju ActionNameAttribute.
        /// </summary>
        /// <param name="actionName">Ocakavany nadradeny nazov akcie.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingActionNameTo(string actionName)
        {
            this.ContainingAttributeOfType<ActionNameAttribute>();
            this.Validations.Add(attrs =>
            {
                var actionNameAttribute = this.GetAttributeOfType<ActionNameAttribute>(attrs);
                var actualActionName = actionNameAttribute.Name;
                if (actionName != actualActionName)
                {
                    this.ThrowNewAttributeAssertionException(
                        string.Format("{0} with '{1}' name", actionNameAttribute.GetName(), actionName),
                        string.Format("in fact found '{0}'", actualActionName));
                }
            });

            return this;
        }

        /// <summary>
        /// Skontroluje ci zoznam atributov obsahuju RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="withName">Nepovinny paramter.</param>
        /// <param name="withOrder">Nepovinny paramter</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder ChangingRouteTo(
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
        /// CSkontroluje ci zoznam atributov obsahuju AllowAnonymousAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder AllowingAnonymousRequests()
        {
            return this.ContainingAttributeOfType<AllowAnonymousAttribute>();
        }

        /// <summary>
        /// Skontroluje ci zoznam atributov obsahuju AuthorizeAttribute.
        /// </summary>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForAuthorizedRequests(
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
        /// Skontroluje ci zoznam atributov obsahuju NonActionAttribute.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder DisablingActionCall()
        {
            return this.ContainingAttributeOfType<NonActionAttribute>();
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <typeparam name="THttpMethod">Attribut typu IActionHttpMethodProvider.</typeparam>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod<THttpMethod>()
            where THttpMethod : Attribute, IActionHttpMethodProvider, new()
        {
            return this.RestrictingForRequestsWithMethods((new THttpMethod()).HttpMethods);
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP poskytovana ako string.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(string httpMethod)
        {
            return this.RestrictingForRequestsWithMethod(new HttpMethod(httpMethod));
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethod">HTTP metoda poskytovana ako HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethod(HttpMethod httpMethod)
        {
            return this.RestrictingForRequestsWithMethods(new List<HttpMethod> { httpMethod });
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP metoda poskytovana ako zoznam strings.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<string> httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.Select(m => new HttpMethod(m)));
        }

        /// <summary>
        ///  Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttributealebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP metoda poskytovana ako string parameter.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params string[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP metoda poskytovana ako zoznam HttpMethod classes.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(IEnumerable<HttpMethod> httpMethods)
        {
            this.Validations.Add(attrs =>
            {
                var totalAllowedHttpMethods = attrs.OfType<IActionHttpMethodProvider>().SelectMany(a => a.HttpMethods);

                httpMethods.ForEach(httpMethod =>
                {
                    if (!totalAllowedHttpMethods.Contains(httpMethod))
                    {
                        this.ThrowNewAttributeAssertionException(
                            string.Format("attribute restricting requests for HTTP '{0}' method", httpMethod.Method),
                            "in fact none was found");
                    }
                });
            });

            return this;
        }

        /// <summary>
        /// Skontroluje, či zhromaždené atribúty obmedzujú žiadosť na konkrétnu metódu HTTP (AcceptVerbsAttribute alebo HttpGetAttribute, HttpPostAttribute, etc.).
        /// </summary>
        /// <param name="httpMethods">HTTP metoda poskytovana ako parameter HttpMethod class.</param>
        /// <returns>The same attributes test builder.</returns>
        public IAndActionAttributesTestBuilder RestrictingForRequestsWithMethods(params HttpMethod[] httpMethods)
        {
            return this.RestrictingForRequestsWithMethods(httpMethods.AsEnumerable());
        }

        /// <summary>
        /// AndAlso metóda pre lepšiu čitateľnosť pri reťazových testoch atribútov.
        /// </summary>
        /// <returns>The same attributes test builder.</returns>
        public IActionAttributesTestBuilder AndAlso()
        {
            return this;
        }

        private void ThrowNewAttributeAssertionException(string expectedValue, string actualValue)
        {
            throw new AttributeAssertionException(string.Format(
                        "When calling {0} action in {1} expected action to have {2}, but {3}.",
                        this.testedActionName,
                        this.Controller.GetName(),
                        expectedValue,
                        actualValue));
        }
    }
}
