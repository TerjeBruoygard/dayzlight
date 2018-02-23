using DayzlightCommon.Entities;
using DayzlightWebapp.Models;
using DayzlightWebapp.Providers;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using System.Reflection;

namespace DayzlightWebapp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        public ActionResult General()
        {
            return View();
        }

        public ActionResult Livemap(string rtime, string command, string data)
        {
            ModelState.Clear();
            using (var db = new DbProvider())
            {
                if (command == null)
                {
                    DateTime timeNow = DateTime.UtcNow;
                    if (rtime != null)
                    {
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
                        Settings = db.Admins.Include(
                            x => x.LivemapSettings
                        ).First(
                            x => x.Login.Equals(User.Identity.Name)
                        ).LivemapSettings
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
                else
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var admin = db.Admins.Include(
                                x => x.LivemapSettings
                            ).First(
                                x => x.Login.Equals(User.Identity.Name)
                            );
                            var settings = admin.LivemapSettings;
                            var field = settings.GetType().GetProperty(command);
                            if (field == null) throw new Exception("Unknown command.");
                            if (field.PropertyType == typeof(bool)) field.SetValue(settings, data == "true");
                            else if (field.PropertyType == typeof(string)) field.SetValue(settings, data);
                            else if (field.PropertyType == typeof(double)) field.SetValue(settings, double.Parse(data));
                            else throw new Exception("Command type not supported.");
                            db.SaveChanges();
                            transaction.Commit();
                            return new EmptyResult();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw ex;
                        }
                    }
                }
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