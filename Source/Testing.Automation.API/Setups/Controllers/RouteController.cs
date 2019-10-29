﻿namespace Testing.Automation.API.Setups.Controllers
{
    using Models;
    using Services;
    using System.Threading.Tasks;
    using System.Web.Http;

    [RoutePrefix("api/routes")]
    public class RouteController : ApiController
    {
        public RouteController(IInjectedService injectedService, IAnotherInjectedService anotherService)
        {
        }

        public void VoidAction()
        {
        }

        public IHttpActionResult WithParameter(int id)
        {
            return this.Ok();
        }

        public IHttpActionResult WithParameterAndQueryString(int id, string value)
        {
            return this.Ok();
        }

        [HttpGet]
        public IHttpActionResult GetMethod()
        {
            return this.Ok();            
        }

        [HttpGet]
        public IHttpActionResult GetMethod(int id)
        {
            return this.Ok();
        }

        public IHttpActionResult QueryString(string first, int second)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithModel(RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithParameterAndModel(int id, RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithQueryStringAndModel(string value, RequestModel someModel)
        {
            return this.Ok();
        }

        [HttpPost]
        public IHttpActionResult PostMethodWithModelAndAttribute([FromUri]RequestModel someModel)
        {
            return this.Ok();
        }

        [Route("test")]
        public IHttpActionResult WithRouteAttribute()
        {
            return this.Ok();
        }

        [ActionName("ChangedActionName")]
        public IHttpActionResult WithActionNameAttribute()
        {
            return this.Ok();
        }

        [HttpGet]
        public IHttpActionResult HeaderRoute()
        {
            return this.Ok();
        }

        public IHttpActionResult SameAction(RequestModel model)
        {
            return this.Ok();
        }

        public IHttpActionResult SameAction(ResponseModel model)
        {
            return this.Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> AsyncAction()
        {
            return await Task.Run(() => this.Ok());
        }
    }
}
