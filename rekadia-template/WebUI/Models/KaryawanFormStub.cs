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
    public class KaryawanFormStub
    {
        public int Id { get; set; }

        [DisplayName("nama_karyawan")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string NamaKaryawan { get; set; }

        [DisplayName("Umur")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public int Umur { get; set; }

        [DisplayName("tanggal_lahir")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime TanggalLahir { get; set; }

        [DisplayName("Pendidikan")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Pendidikan { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        [StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public string Status { get; set; }

        [DisplayName("tanggal_masuk")]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        //[StringLength(50, ErrorMessageResourceName = "StringLength", ErrorMessageResourceType = typeof(Resources.MyGlobalErrors))]
        public DateTime TanggalMasuk { get; set; }

        public KaryawanFormStub() { }

        public KaryawanFormStub(tbKaryawan dbItem)
        {
            Id = dbItem.id;
            NamaKaryawan = dbItem.nama_karyawan;
            Umur = (int) dbItem.umur;
            TanggalLahir = (DateTime) dbItem.tanggal_lahir;
            Pendidikan = dbItem.pendidikan;
            Status = dbItem.status;
            TanggalMasuk = (DateTime) dbItem.tanggal_masuk;
        }

        public tbKaryawan GetDbObject(tbKaryawan dbItem)
        {
            dbItem.id = this.Id;
            dbItem.nama_karyawan = this.NamaKaryawan;
            dbItem.umur = this.Umur;
            dbItem.tanggal_lahir = this.TanggalLahir;
            dbItem.pendidikan = this.Pendidikan;
            dbItem.status = this.Status;
            dbItem.tanggal_masuk = this.TanggalMasuk;
            return dbItem;
        }
        //public tbKaryawan GetKaryawan()
        //{
        //    tbKaryawan dbItem = new tbKaryawan { id = this.Id, nama_karyawan = NamaKaryawan, umur = Umur,  
        //                tanggal_lahir = TanggalLahir, pendidikan = Pendidikan, status = Status, tanggal_masuk = TanggalMasuk};

        //    return dbItem;
        //}
    }
}