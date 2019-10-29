﻿namespace Testing.Automation.API.Builders.Contracts.Base
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Web.Http;

    /// <summary>
    /// Base interface for all test builders.
    /// </summary>
    public interface IBaseTestBuilder
    {
        /// <summary>
        /// Gets the controller on which the action is tested.
        /// </summary>
        /// <returns>ASP.NET Web API controller on which the action is tested.</returns>
        ApiController AndProvideTheController();

        /// <summary>
        /// Gets the HTTP configuration with which the action will be tested.
        /// </summary>
        /// <returns>HttpConfiguration from the tested controller.</returns>
        HttpConfiguration AndProvideTheHttpConfiguration();

        /// <summary>
        /// Gets the HTTP request message with which the action will be tested.
        /// </summary>
        /// <returns>HttpRequestMessage from the tested controller.</returns>
        HttpRequestMessage AndProvideTheHttpRequestMessage();

        /// <summary>
        /// Gets the attributes on the tested controller..
        /// </summary>
        /// <returns>IEnumerable of object representing the attributes or null, if no attributes were collected on the controller.</returns>
        IEnumerable<object> AndProvideTheControllerAttributes();
    }
}
