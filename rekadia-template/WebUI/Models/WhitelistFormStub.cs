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
    public class WhitelistFormStub
    {
        public int Id { get; set; }

        [DisplayName("nama_karyawan")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Barcode { get; set; }

        [DisplayName("Tanggal_Awal")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime TanggalAwal{ get; set; }

        [DisplayName("Tanggal_Akhir")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime TanggalAkhir { get; set; }

        public WhitelistFormStub() { }

        public WhitelistFormStub(Crew_Whitelist dbItem)
        {
            Id = dbItem.Id;
            Barcode = dbItem.Barcode;
            TanggalAwal = (DateTime) dbItem.Tanggal_Awal;
            TanggalAkhir = (DateTime) dbItem.Tanggal_Akhir;
        }

        public Crew_Whitelist GetDbObject(Crew_Whitelist dbItem)
        {
            dbItem.Id = this.Id;
            dbItem.Barcode = this.Barcode;
            dbItem.Tanggal_Awal = this.TanggalAwal;
            dbItem.Tanggal_Akhir = this.TanggalAkhir;
            return dbItem;
        }
    }
}