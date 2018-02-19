using DayzlightWebapp.Models;
using DayzlightWebapp.Providers;
using System.Linq;
using System.Web.Mvc;

namespace DayzlightWebapp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Livemap()
        {
            using (var db = new DbProvider())
            {
                return View(new LivemapModel()
                {
                    ServerInfo = db.ServerInfo.FirstOrDefault()
                });
            }
        }

        public ActionResult Users()
        {
            ViewBag.Message = "Your Users page.";
            return View();
        }

        public ActionResult Tools()
        {
            ViewBag.Message = "Your Tools page.";
            return View();
        }
    }
}