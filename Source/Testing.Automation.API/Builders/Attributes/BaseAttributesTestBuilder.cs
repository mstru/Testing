namespace Testing.Automation.API.Builders.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Base;
    using Common.Extensions;
    using Utilities;

    /// <summary>
    /// Zakladna trieda pre vsetky test builders.
    /// </summary>
    public abstract class BaseAttributesTestBuilder : BaseTestBuilder
    {
        /// <summary>
        /// Inicializacia novej instancie <see cref="BaseAttributesTestBuilder" /> triedy.
        /// </summary>
        /// <param name="controller">Controller ktory bude testovany.</param>
        protected BaseAttributesTestBuilder(ApiController controller)
            : base(controller)
        {
            this.Validations = new List<Action<IEnumerable<object>>>();
        }

        /// <summary>
        /// Získanie overovacích akcií pre testované atribúty.
        /// </summary>
        /// <value>Zoznam validačných akcií pre atribúty.</value>
        protected ICollection<Action<IEnumerable<object>>> Validations { get; private set; }

        internal ICollection<Action<IEnumerable<object>>> GetAttributeValidations()
        {
            return this.Validations;
        }

        /// <summary>
        /// Skontroluje, či zoznam atribútov obsahuje zadaný typ atribútu.
        /// </summary>
        /// <typeparam name="TAttribute">Ocakavany typ atributu.</typeparam>
        /// <param name="failedValidationAction">Akcia, ktorá sa ma vykonať, ak zlyhá validácia.</param>
        protected void ContainingAttributeOfType<TAttribute>(Action<string, string> failedValidationAction)
            where TAttribute : Attribute
        {
            var expectedAttributeType = typeof(TAttribute);
            this.Validations.Add(attrs =>
            {
                if (attrs.All(a => a.GetType() != expectedAttributeType))
                {
                    failedValidationAction(
                        expectedAttributeType.ToFriendlyTypeName(),
                        "in fact such was not found");
                }
            });
        }

        /// <summary>
        ///  Skontroluje, či zoznam atribútov obsahuje RouteAttribute.
        /// </summary>
        /// <param name="template">Expected overridden route template of the action.</param>
        /// <param name="failedValidationAction">Akcia, ktorá sa ma vykonať, ak zlyhá validácia.</param>
        /// <param name="withName">Nepovinny parameter.</param>
        /// <param name="withOrder">Nepovinny parameter.</param>
        protected void ChangingRouteTo(
            string template,
            Action<string, string> failedValidationAction,
            string withName = null,
            int? withOrder = null)
        {
            this.ContainingAttributeOfType<RouteAttribute>(failedValidationAction);
            this.Validations.Add(attrs =>
            {
                var routeAttribute = this.TryGetAttributeOfType<RouteAttribute>(attrs);
                var actualTemplate = routeAttribute.Template;
                if (template.ToLower() != actualTemplate.ToLower())
                {
                    failedValidationAction(
                                string.Format("{0} with '{1}' template", routeAttribute.GetName(), template),
                                string.Format("in fact found '{0}'", actualTemplate));
                }

                var actualName = routeAttribute.Name;
                if (!string.IsNullOrEmpty(withName) && withName != actualName)
                {
                    failedValidationAction(
                                string.Format("{0} with '{1}' name", routeAttribute.GetName(), withName),
                                string.Format("in fact found '{0}'", actualName));
                }

                var actualOrder = routeAttribute.Order;
                if (withOrder.HasValue && withOrder != actualOrder)
                {
                    failedValidationAction(
                                string.Format("{0} with order of {1}", routeAttribute.GetName(), withOrder),
                                string.Format("in fact found {0}", actualOrder));
                }
            });
        }

        /// <summary>
        /// CSkontroluje, či zoznam atribútov obsahuje AuthorizeAttribute.
        /// </summary>
        /// <param name="failedValidationAction">Akcia, ktorá sa ma vykonať, ak zlyhá validácia.</param>
        /// <param name="withAllowedRoles">Optional expected authorized roles.</param>
        /// <param name="withAllowedUsers">Optional expected authorized users.</param>
        protected void RestrictingForAuthorizedRequests(
            Action<string, string> failedValidationAction,
            string withAllowedRoles = null,
            string withAllowedUsers = null)
        {
            this.ContainingAttributeOfType<AuthorizeAttribute>(failedValidationAction);
            var testAllowedUsers = !string.IsNullOrEmpty(withAllowedUsers);
            var testAllowedRoles = !string.IsNullOrEmpty(withAllowedRoles);
            if (testAllowedUsers || testAllowedRoles)
            {
                if (testAllowedRoles)
                {
                    this.Validations.Add(attrs =>
                    {
                        var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                        var actualRoles = authorizeAttribute.Roles;
                        if (withAllowedRoles != actualRoles)
                        {
                            failedValidationAction(
                                string.Format("{0} with allowed '{1}' roles", authorizeAttribute.GetName(), withAllowedRoles),
                                string.Format("in fact found '{0}'", actualRoles));
                        }
                    });
                }

                if (testAllowedUsers)
                {
                    this.Validations.Add(attrs =>
                    {
                        var authorizeAttribute = this.GetAttributeOfType<AuthorizeAttribute>(attrs);
                        var actualUsers = authorizeAttribute.Users;
                        if (withAllowedUsers != actualUsers)
                        {
                            failedValidationAction(
                                string.Format("{0} with allowed '{1}' users", authorizeAttribute.GetName(), withAllowedUsers),
                                string.Format("in fact found '{0}'", actualUsers));
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Získava atribút daného typu z poskytnutej kolekcie objektov a vyhadzuje výnimku, ak sa ziadny nenašiel.
        /// </summary>
        /// <typeparam name="TAttribute">Typ ocakavaneho atributu.</typeparam>
        /// <param name="attributes">Zoznam atributov.</param>
        /// <returns>Nájdený atribút daného typu.</returns>
        protected TAttribute GetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return (TAttribute)attributes.First(a => a.GetType() == typeof(TAttribute));
        }

        /// <summary>
        /// Získanie atribútu daného typu z poskytnutej kolekcie objektov.
        /// </summary>
        /// <typeparam name="TAttribute">Typ ocakavaneho atributu.</typeparam>
        /// <param name="attributes">Zoznam atributov.</param>
        /// <returns>Atribut daneho typu alebo null, ak sa nenasiel.</returns>
        protected TAttribute TryGetAttributeOfType<TAttribute>(IEnumerable<object> attributes)
            where TAttribute : Attribute
        {
            return attributes.FirstOrDefault(a => a.GetType() == typeof(TAttribute)) as TAttribute;
        }
    }
}
