using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebUI.Models;
using WebUI.Controllers;
using WebUI.Infrastructure;
using WebUI.Extension;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Web.Mvc.Filters;
using Business.Abstract;
using Business.Entities;

namespace WebUI.Controllers
{
    public class KendoGridController : MyController
    {
        private IKaryawanRepository RepoKaryawan;
        //
        // GET: /KendoGrid/
        private ILogRepository RepoLog;

        public KendoGridController(IKaryawanRepository repoKaryawan, ILogRepository repoLog)
			: base(repoLog)
        {
            RepoKaryawan = repoKaryawan;
        }

        [MvcSiteMapNode(Title = "Karyawan", ParentKey = "Dashboard", Key = "IndexKaryawan")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult Index()
        {
            return View();
        }

        public string Binding()
        {
            //kamus
            GridRequestParameters param = GridRequestParameters.Current;

            List<tbKaryawan> items = RepoKaryawan.FindAll(param.Skip, param.Take, (param.Sortings != null ? param.Sortings.ToList() : null), param.Filters);
            int total = RepoKaryawan.Count(param.Filters);

            return new JavaScriptSerializer().Serialize(new { total = total, data = new KaryawanPresentationStub().MapList(items) });
        }

        [MvcSiteMapNode(Title = "Create", ParentKey = "IndexKaryawan")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult Create()
        {

            KaryawanFormStub formStub = new KaryawanFormStub();

            return View("Form", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult Create(KaryawanFormStub model)
        {
            //bool isNameExist = RepoContractor.Find().Where(p => p.name == model.Name).Count() > 0;

            if (ModelState.IsValid)
            {
                tbKaryawan dbItem = new tbKaryawan();
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoKaryawan.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.NamaKaryawan, template);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", model);
            }
        }

        [MvcSiteMapNode(Title = "Edit", ParentKey = "IndexKaryawan", Key = "EditKaryawan", PreservedRouteParameters = "id")]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(int id)
        {
            tbKaryawan karyawan = RepoKaryawan.FindByPk(id);
            KaryawanFormStub formStub = new KaryawanFormStub(karyawan);
            return View("Form", formStub);
        }

        [HttpPost]
        [SiteMapTitle("Breadcrumb")]
        public ActionResult Edit(KaryawanFormStub model)
        {
            //bool isNameExist = RepoKompetitor.Find().Where(p => p.name == model.Name && p.id != model.Id).Count() > 0;

            if (ModelState.IsValid)
            {
                tbKaryawan dbItem = RepoKaryawan.FindByPk(model.Id);
                dbItem = model.GetDbObject(dbItem);

                try
                {
                    RepoKaryawan.Save(dbItem);
                }
                catch (Exception e)
                {
                    return View("Form", model);
                }

                //message
                string template = HttpContext.GetGlobalResourceObject("MyGlobalMessage", "CreateSuccess").ToString();
                this.SetMessage(model.NamaKaryawan, template);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Form", model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            string template = "";
            ResponseModel response = new ResponseModel(true);
            tbKaryawan dbItem = RepoKaryawan.FindByPk(id);

            RepoKaryawan.Delete(dbItem);

            return Json(response);
        }

    }
}
