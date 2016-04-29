using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IAuthProvider auth;
        private BataviaDB context = new BataviaDB();
        public AdminController(IAuthProvider authParam)
        {
            auth = authParam;
        }

        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl = null)
        {
            if (Request.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToAction("CrewCRUD", "Crew");
                else
                    return RedirectToAction("CrewWhitelist", "Crew");
            }
            else
            {
                if (ReturnUrl != null)
                    ViewBag.ReturnUrl = ReturnUrl;
                return View();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                Session["username"] = model.Username;

                var whitelist = context.Admins.Where(x => x.Username == model.Username &&
                                        x.Password == model.Password)
                                 .Select(x => x.Whitelist)
                                 .FirstOrDefault();

                string roles = "";
                if (whitelist == "0")
                    roles = "Admin";
                else
                    roles = "Whitelist";

                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    1,
                    model.Username,  //user id
                    DateTime.Now,
                    DateTime.Now.AddMinutes(60),  // expiry
                    false,  //do not remember
                    roles,
                    "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                   FormsAuthentication.Encrypt(authTicket))
                {
                    HttpOnly = true,
                    Expires = authTicket.Expiration
                };
                //Response.Cookies.Add(cookie);
                Response.SetCookie(cookie);

                if (whitelist == "0")
                    return RedirectToAction("CrewCRUD","Crew");
                else if (whitelist == "1")
                    return RedirectToAction("CrewWhitelist","Crew");
                else
                    ModelState.AddModelError("", "User data is incorrect!");
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.LoginUrl);
        }
    }
}
