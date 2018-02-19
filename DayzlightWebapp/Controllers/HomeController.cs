using DayzlightWebapp.Models;
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
            return View(new LivemapModel()
            {
                Name = "namalsk",
                Width = 12800,
                Height = 12800
            });
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