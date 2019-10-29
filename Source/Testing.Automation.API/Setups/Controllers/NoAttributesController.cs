namespace Testing.Automation.API.Setups.Controllers
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class NoAttributesController : ApiController
    {
        public IHttpActionResult WithParameter(int id)
        {
            return this.Ok(id);
        }

        public IHttpActionResult WithCookies()
        {
            return this.Ok(string.Join("!", this.Request.Headers.GetCookies().SelectMany(c => c.Cookies.Select(ic => string.Format("{0}+{1}", ic.Name, ic.Value)))));
        }

        public void VoidAction()
        {
        }
    }
}
