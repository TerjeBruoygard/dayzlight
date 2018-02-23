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

        public ActionResult Livemap(string rtime, string expm)
        {
            ModelState.Clear();
            using (var db = new DbProvider())
            {
                DateTime timeNow = DateTime.UtcNow;
                if (rtime != null) {
                    timeNow = DateTime.FromBinary(long.Parse(rtime));
                }

                var restarts = db.ServerRestartInfo.Where(
                    x => x.TimePoint <= timeNow
                ).OrderByDescending(
                    x => x.TimePoint
                ).Take(2).ToArray();

                ServerRestartEntity lastRestart = null, curRestart = null;
                if (restarts.Length >= 1)
                {
                    curRestart = restarts[0];
                    if (restarts.Length == 2)
                    {
                        lastRestart = restarts[1];
                    }
                }

                LivemapModel result = new LivemapModel()
                {
                    ExpandMenu = expm == null || expm.Equals("true")
                };

                if (curRestart != null)
                {
                    var nextRestart = db.ServerRestartInfo.Where(
                        x => x.TimePoint > curRestart.TimePoint
                    ).OrderBy(
                        x => x.TimePoint
                    ).FirstOrDefault();

                    var timepoints = db.Timepoints.Where(
                        x => x.TimePoint > curRestart.TimePoint
                    );

                    if (nextRestart != null)
                    {
                        timepoints = timepoints.Where(x => x.TimePoint < nextRestart.TimePoint);
                    }

                    result.ServerLastRestartInfo = lastRestart;
                    result.ServerCurRestartInfo = curRestart;
                    result.ServerNextRestartInfo = nextRestart;
                    result.Timepoints = timepoints.Include(
                        x => x.PlayerMovements.Select(
                            y => y.PlayerName.PlayerInfo
                        )
                    ).ToArray();
                };
                return View(result);
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