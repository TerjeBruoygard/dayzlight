using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace DayzlightWebapp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Authorize(Roles = "*")]
        public ActionResult Auth()
        {
            string userName = (string)Session["UserName"];
            string[] userRoles = (string[])Session["UserRoles"];
            if (userName != null && userRoles != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userName));
                userRoles.ToList().ForEach((role) => identity.AddClaim(new Claim(ClaimTypes.Role, role)));
                identity.AddClaim(new Claim(ClaimTypes.Name, userName));
            }
            return View();
        }

        public ActionResult General()
        {
            return View();
        }

        public ActionResult Livemap()
        {
            ViewBag.Message = "Your Livemap page.";
            return View();
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