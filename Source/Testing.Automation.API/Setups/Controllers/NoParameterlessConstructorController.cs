namespace Testing.Automation.API.Setups.Controllers
{
    using Services;
    using System.Web.Http;

    public class NoParameterlessConstructorController : ApiController
    {
        public NoParameterlessConstructorController(IInjectedService service)
        {
            this.Service = service;
        }

        public IInjectedService Service { get; private set; }

        public IHttpActionResult OkAction()
        {
            return this.Ok();
        }
    }
}
