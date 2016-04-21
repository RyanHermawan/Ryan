using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models
{
    public class KaryawanPresentationStub
    {
        public int Id { get; set; }
		public string NamaKaryawan { get; set; }
		public int Umur { get; set; }
		public DateTime TanggalLahir { get; set; }		
        public string Pendidikan { get; set; }
        public string Status { get; set; }
        public DateTime TanggalMasuk { get; set; }

		public KaryawanPresentationStub() { }

		public KaryawanPresentationStub(tbKaryawan dbItem) { 
			this.Id = dbItem.id;
			this.NamaKaryawan = dbItem.nama_karyawan;
			this.Umur = (int) dbItem.umur;
			this.TanggalLahir = (DateTime) dbItem.tanggal_lahir;
            this.Pendidikan = dbItem.pendidikan;
            this.Status = dbItem.status;
            this.TanggalMasuk = (DateTime) dbItem.tanggal_masuk;
		}

		public List<KaryawanPresentationStub> MapList(List<tbKaryawan> dbItems)
        {
            List<KaryawanPresentationStub> retList = new List<KaryawanPresentationStub>();

            foreach (tbKaryawan dbItem in dbItems)
                retList.Add(new KaryawanPresentationStub(dbItem));

            return retList;
        }
    }
}