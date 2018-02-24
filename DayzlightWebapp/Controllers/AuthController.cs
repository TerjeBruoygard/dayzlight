using DayzlightWebapp.Models;
using DayzlightWebapp.Providers;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace DayzlightWebapp.Controllers
{
    public class AuthController : Controller
    {
        [AllowAnonymous]
        [Authorize(Roles = "*")]
        public ActionResult Login()
        {
            if (ModelState.IsValid &&
                User.Identity.IsAuthenticated &&
                Roles.GetRolesForUser(User.Identity.Name).Any(x => x.Equals("Administrator")))
            {
                return RedirectToAction("General", "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "*")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SigninModel signinModel, string returnUrl)
        {
            if (signinModel != null && signinModel.Login != null && signinModel.Password != null)
            {
                var authProvider = new AuthProvider();
                if (authProvider.Login(signinModel))
                {
                    FormsAuthentication.SetAuthCookie(signinModel.Login, signinModel.RememberMe);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                    && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("General", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login or password.");
                }
            }

            return View(signinModel);
        }

        [HttpGet]
        [AllowAnonymous]
        [Authorize(Roles = "*")]
        public ActionResult Logout()
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                FormsAuthentication.SignOut();               
            }

            return RedirectToAction("Login");
        }
    }
}