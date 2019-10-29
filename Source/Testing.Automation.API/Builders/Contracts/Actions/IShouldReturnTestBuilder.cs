﻿namespace Testing.Automation.API.Builders.Contracts.Actions
{
    using System;
    using System.Net;
    using Base;
    using HttpActionResults.BadRequest;
    using HttpActionResults.Content;
    using HttpActionResults.Created;
    using HttpActionResults.InternalServerError;
    using HttpActionResults.Json;
    using HttpActionResults.Ok;
    using HttpActionResults.Redirect;
    using HttpActionResults.Unauthorized;
    using HttpResponseMessages;
    using Models;

    /// <summary>
    /// Used for testing action returned result.
    /// </summary>
    /// <typeparam name="TActionResult">Result from invoked action in ASP.NET Web API controller.</typeparam>
    public interface IShouldReturnTestBuilder<TActionResult> : IBaseTestBuilderWithActionResult<TActionResult>
    {
        /// <summary>
        /// Tests whether action result is the default value of the type.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> DefaultValue();

        /// <summary>
        /// Tests whether action result is null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Null();

        /// <summary>
        /// Tests whether action result is not null.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotNull();

        /// <summary>
        /// Tests whether action result is HttpResponseMessage.
        /// </summary>
        /// <returns>HTTP response message test builder.</returns>
        IHttpResponseMessageTestBuilder HttpResponseMessage();

        /// <summary>
        /// Tests whether action result is OkResult or OkNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Ok test builder.</returns>
        IOkTestBuilder Ok();

        /// <summary>
        /// Tests whether action result is CreatedNegotiatedContentResult{T} or CreatedAtRouteNegotiatedContentResult{T}.
        /// </summary>
        /// <returns>Created test builder.</returns>
        ICreatedTestBuilder Created();

        /// <summary>
        /// Tests whether action result is NegotiatedContentResult{T} or FormattedContentResult{T}.
        /// </summary>
        /// <returns>Content test builder.</returns>
        IContentTestBuilder Content();

        /// <summary>
        /// Tests whether action result is RedirectResult or RedirectToRouteResult.
        /// </summary>
        /// <returns>Redirect test builder.</returns>
        IRedirectTestBuilder Redirect();

        /// <summary>
        /// Tests whether action result is StatusCodeResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode();

        /// <summary>
        /// Tests whether action result is StatusCodeResult and is the same as provided HttpStatusCode.
        /// </summary>
        /// <param name="statusCode">HttpStatusCode enumeration.</param>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> StatusCode(HttpStatusCode statusCode);

        /// <summary>
        /// Tests whether action result is NotFoundResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> NotFound();

        /// <summary>
        /// Tests whether action result is BadRequestResult, InvalidModelStateResult or BadRequestErrorMessageResult.
        /// </summary>
        /// <returns>Bad request test builder.</returns>
        IBadRequestTestBuilder BadRequest();

        /// <summary>
        /// Tests whether action result is ConflictResult.
        /// </summary>
        /// <returns>Base test builder with action result.</returns>
        IBaseTestBuilderWithActionResult<TActionResult> Conflict();

        /// <summary>
        /// Tests whether action result is UnauthorizedResult.
        /// </summary>
        /// <returns>Unauthorized result test builder.</returns>
        IUnauthorizedTestBuilder Unauthorized();

        /// <summary>
        /// Tests whether action result is InternalServerErrorResult or ExceptionResult.
        /// </summary>
        /// <returns>Internal server error test builder.</returns>
        IInternalServerErrorTestBuilder InternalServerError();

        /// <summary>
        /// Tests whether action result is JSON Result.
        /// </summary>
        /// <returns>JSON test builder.</returns>
        IJsonTestBuilder Json();

        /// <summary>
        /// Tests whether action result is of the provided generic type.
        /// </summary>
        /// <typeparam name="TResponseModel">Expected response type.</typeparam>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType<TResponseModel>();

        /// <summary>
        /// Tests whether action result is of the provided type.
        /// </summary>
        /// <param name="returnType">Expected return type.</param>
        /// <returns>Response model details test builder.</returns>
        IModelDetailsTestBuilder<TActionResult> ResultOfType(Type returnType);
    }
}
