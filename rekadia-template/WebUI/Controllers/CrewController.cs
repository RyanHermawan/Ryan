using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Script.Serialization;
using WebUI.Infrastructure;
using WebUI.Infrastructure.Abstract;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using Business.Entities;

namespace WebUI.Controllers
{
    public class CrewController : Controller
    {
        private IAuthProvider auth;
        private BataviaDB context = new BataviaDB();

        private ICrewRepository RepoCrew;
        private IWhitelistRepository RepoWhite;
        private ILogRepository RepoLog;

        public CrewController(IAuthProvider authParam, ICrewRepository repoCrew, IWhitelistRepository repoWhite,
            ILogRepository repoLog)
        {
            auth = authParam;
            RepoCrew = repoCrew;
            RepoWhite = repoWhite;
        }
        
        //
        // GET: /Crew/

        #region crew

        [AuthorizeAdmin(Roles = "Admin")]
        [MvcSiteMapNode(Title = "Crew", ParentKey = "Dashboard", Key = "IndexCrew")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CrewCRUD()
        {
            return View();
        }

        public string BindingCrew()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<Crew> items = RepoCrew.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoCrew.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new CrewPresentationStub().MapList(items) });
        }

        [AuthorizeAdmin(Roles = "Admin")]
        [MvcSiteMapNode(Title = "CreateCrew", ParentKey = "IndexCrew")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CreateCrew()
        {
            CrewFormStub formStub = new CrewFormStub();

            return View("FormCrew", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CreateCrew(CrewFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;

            if (ModelState.IsValid)
            {
                Crew dbItem = new Crew();
                dbItem = model.GetDbObject(dbItem);

                //Generate ID untuk Crew
                string barcode = DateTime.Now.ToString("ddMMyyyy");
                dbItem.Barcode = barcode + "-" + (RepoCrew.FindAll().Count + 1).ToString("D3");

                try
                {
                    RepoCrew.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormCrew", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.NamaCrew, template);

                return RedirectToAction("CrewCRUD");
            }
            else
            {
                return View("FormCrew", model);
            }
        }

        [AuthorizeAdmin(Roles = "Admin")]
        [MvcSiteMapNode(Title = "EditCrew", ParentKey = "IndexCrew", Key = "EditCrew", PreservedRouteParameters = "id")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult EditCrew(string barcode)
        {
            Crew crew = RepoCrew.FindByPk(barcode);
            CrewFormStub formStub = new CrewFormStub(crew);
            return View("FormCrew", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult EditCrew(CrewFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                Crew dbItem = RepoCrew.FindByPk(model.Barcode);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoCrew.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormCrew", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.NamaCrew, template);

                return View("CrewCRUD");
            }
            else
            {
                return View("FormCrew", model);
            }
        }

        [HttpPost]
        public JsonResult DeleteCrew(string barcode)
        {
            string template = "";
            ResponseModel response = new ResponseModel(true);
            Crew dbItem = RepoCrew.FindByPk(barcode);

            RepoCrew.Delete(dbItem);

            return Json(response);
        }
        #endregion

        #region whitelist

        [AuthorizeAdmin(Roles = "Whitelist")]
        [MvcSiteMapNode(Title = "Whitelist", ParentKey = "Dashboard", Key = "IndexWhitelist")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CrewWhitelist()
        {
            return View();
        }

        public string BindingWhitelist()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<Crew_Whitelist> items = RepoWhite.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoWhite.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new WhitelistPresentationStub().MapList(items) });
        }
        public string BindingCrewToday()
        {
            GridRequestParameters param = GridRequestParameters.Current;

            List<Crew_Whitelist> items = RepoWhite.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoWhite.Count(param.Filters);

            List<Crew> newList = new List<Crew>();
            foreach (var item in items)
                if ( (item.Tanggal_Awal <= DateTime.Now
                    && item.Tanggal_Akhir >= DateTime.Now)
                    && newList.FirstOrDefault(x => x.Barcode == item.Barcode) == null)
                {
                    newList.Add(context.Crews.FirstOrDefault(x => x.Barcode == item.Barcode));
                }

            return new JavaScriptSerializer().Serialize(new { total = newList.Count(), data = new CrewPresentationStub().MapList(newList) });
        }

        [AuthorizeAdmin(Roles = "Whitelist")]
        [MvcSiteMapNode(Title = "CreateWhitelist", ParentKey = "IndexWhitelist")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CreateWhitelist()
        {
            WhitelistFormStub formStub = new WhitelistFormStub();

            List<object> newList = new List<object>();
            foreach (var crew in context.Crews)
                newList.Add(new
                {
                    Id = crew.Barcode,
                    Name = crew.Barcode + " " + crew.Nama
                });
            this.ViewData["Crew"] = new SelectList(newList, "Name", "Id");
            //ViewBag.Crew = items;

            return View("FormWhitelist", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult CreateWhitelist(WhitelistFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;

            if (ModelState.IsValid)
            {
                Crew_Whitelist dbItem = new Crew_Whitelist();
                dbItem = model.GetDbObject(dbItem);
                
                try
                {
                    RepoWhite.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormWhitelist", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Id.ToString(), template);

                return RedirectToAction("CrewWhitelist");
            }
            else
            {
                return View("FormWhitelist", model);
            }
        }

        [AuthorizeAdmin(Roles = "Whitelist")]
        [MvcSiteMapNode(Title = "EditWhitelist", ParentKey = "IndexWhitelist", Key = "EditWhitelist", PreservedRouteParameters = "id")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult EditWhitelist(int id)
        {
            Crew_Whitelist white = RepoWhite.FindByPk(id);
            List<object> newList = new List<object>();
            foreach (var crew in context.Crews)
                newList.Add(new
                {
                    Id = crew.Barcode,
                    Name = crew.Barcode + " " + crew.Nama
                });
            this.ViewData["Crew"] = new SelectList(newList, "Name", "Id");

            WhitelistFormStub formStub = new WhitelistFormStub(white);
            return View("FormWhitelist", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult EditWhitelist(WhitelistFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                Crew_Whitelist dbItem = RepoWhite.FindByPk(model.Id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoWhite.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("FormCrew", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.Id.ToString(), template);

                return View("CrewWhitelist");
            }
            else
            {
                return View("FormWhitelist", model);
            }
        }

        [HttpPost]
        public JsonResult DeleteWhitelist(int id)
        {
            string template = "";
            ResponseModel response = new ResponseModel(true);
            Crew_Whitelist dbItem = RepoWhite.FindByPk(id);

            RepoWhite.Delete(dbItem);

            return Json(response);
        }
        #endregion
        
        
    }
}
