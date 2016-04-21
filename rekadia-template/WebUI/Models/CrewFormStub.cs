using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.Enums;
using WebUI.Infrastructure.Validation;

namespace WebUI.Models
{
    public class CrewFormStub
    {
        public string Barcode { get; set; }

        [DisplayName("Nama")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string NamaCrew { get; set; }

        [DisplayName("Tanggal_Daftar")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime TanggalDaftar { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Status { get; set; }

        [DisplayName("Airport")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Airport { get; set; }

        [DisplayName("Company_Airways")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string CompanyAirways { get; set; }

        public CrewFormStub() { }

        public CrewFormStub(Crew dbItem)
        {
            Barcode = dbItem.Barcode;
            NamaCrew = dbItem.Nama;
            TanggalDaftar = (DateTime) dbItem.Tanggal_Daftar;
            Status = dbItem.Status;
            Airport = dbItem.Airport;
            CompanyAirways = dbItem.Company_Airways;
        }

        public Crew GetDbObject(Crew dbItem)
        {
            dbItem.Barcode = this.Barcode;
            dbItem.Nama = this.NamaCrew;
            dbItem.Tanggal_Daftar = this.TanggalDaftar;
            dbItem.Status = this.Status;
            dbItem.Airport = this.Airport;
            dbItem.Company_Airways = this.CompanyAirways;
            return dbItem;
        }
    }
}