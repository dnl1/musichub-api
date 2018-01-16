using System.Web.Mvc;

namespace MusicHubAPI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return File(Server.MapPath("~/index.html"), "text/html");
        }
    }
}