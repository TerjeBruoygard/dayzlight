using DayzlightCommon.Entities;
using DayzlightWebapp.Models;
using DayzlightWebapp.Providers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

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
                var timeStart = DateTime.Now.Date;
                var timeEnd = DateTime.Now.Date.AddDays(1);
                return View(new LivemapModel()
                {
                    ServerInfo = db.ServerInfo.FirstOrDefault(),
                    Timepoints = db.Timepoints.Include(
                        x => x.PlayerMovements
                    ).Where(
                        x => x.TimePoint >= timeStart && x.TimePoint < timeEnd
                    ).ToArray()
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