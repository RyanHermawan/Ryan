using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Business.Entities;
using WebUI.Models;
using WebUI.Infrastructure.Concrete;
using System.Security.Principal;

namespace WebUI.Infrastructure
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        BataviaDB context = new BataviaDB();
        private readonly string[] allowedroles;
        public string ModuleName { get; set; } 

        public AuthorizeAdminAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string cookieName = FormsAuthentication.FormsCookieName;

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated ||
                filterContext.HttpContext.Request.Cookies == null ||
                filterContext.HttpContext.Request.Cookies[cookieName] == null
            )
            {
                HandleUnauthorizedRequest(filterContext);
                return;
            }

            var authCookie = filterContext.HttpContext.Request.Cookies[cookieName];
            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            string[] roles = authTicket.UserData.Split(',');

            var userIdentity = new GenericIdentity(authTicket.Name);
            var userPrincipal = new GenericPrincipal(userIdentity, roles);

            filterContext.HttpContext.User = userPrincipal;
            base.OnAuthorization(filterContext);

            //

            //bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
            //|| filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true);
            //if (!skipAuthorization)
            //{
            //    base.OnAuthorization(filterContext);
            //    if (filterContext.HttpContext.Request.IsAuthenticated)
            //    {
            //        //kamus
            //        if (filterContext.HttpContext.Session["username"] == null)
            //        {
            //            FormsAuthentication.SignOut();
            //            filterContext.Result = new RedirectToRouteResult(new
            //                 RouteValueDictionary(new { controller = "Crew", action = "Login", area = "", ReturnUrl = filterContext.HttpContext.Request.Url.PathAndQuery }));
            //        }
            //        else
            //        {
            //            //List<ModuleAction> moduleAccess;
            //            //List<string> moduleNameList;
            //            //bool hasAccess = true;

            //            ////algoritma
            //            //moduleAccess = (HttpContext.Current.User as CustomPrincipal).Modules;

            //            //if (ModuleName != null)
            //            //{
            //            //    moduleNameList = ModuleName.Split(',').ToList();
            //            //    if (moduleAccess.Where(m => moduleNameList.Contains(m.ModuleName)).Count() == 0)
            //            //    {
            //            //        hasAccess = false;
            //            //    }
            //            //}
            //        }
            //    }
            //}
        }
    }
}