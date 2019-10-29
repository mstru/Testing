namespace Testing.Automation.API.Setups.Controllers
{
    using System.Web.Http;

    [AllowAnonymous]
    [Route("api/test", Name = "TestRouteAttributes", Order = 1)]
    public class AttributesController : ApiController
    {
        [AllowAnonymous]
        public IHttpActionResult WithAttributesAndParameters(int id)
        {
            return this.Ok(id);
        }
    }
}
