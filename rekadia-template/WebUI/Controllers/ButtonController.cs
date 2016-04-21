using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ButtonController : Controller
    {
        //
        // GET: /Button/

        public ActionResult Index()
        {
            CalculateDate calDate = new CalculateDate(new DateTime(2016, 04, 16, 11, 0, 0), DateTime.Now );
            ViewBag.View = calDate.ToString();
            return View();
        }

        public ActionResult Danger()
        {
            return View("Danger");
        }
        public ActionResult Default()
        {
            return View("Default");
        }
        public ActionResult Primary()
        {
            return View();
        }

    }
}
