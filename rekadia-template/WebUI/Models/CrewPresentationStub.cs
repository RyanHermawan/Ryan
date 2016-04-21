using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models
{
    public class CrewPresentationStub
    {
        public string Barcode { get; set; }
		public string NamaCrew { get; set; }
		public DateTime TanggalDaftar { get; set; }		
        public string Status { get; set; }
        public string Airport { get; set; }
        public string CompanyAirways { get; set; }

		public CrewPresentationStub() { }

		public CrewPresentationStub(Crew dbItem) {
            Barcode = dbItem.Barcode;
            NamaCrew = dbItem.Nama;
            TanggalDaftar = (DateTime)dbItem.Tanggal_Daftar;
            Status = dbItem.Status;
            Airport = dbItem.Airport;
            CompanyAirways = dbItem.Company_Airways;
		}

		public List<CrewPresentationStub> MapList(List<Crew> dbItems)
        {
            List<CrewPresentationStub> retList = new List<CrewPresentationStub>();

            foreach (Crew dbItem in dbItems)
                retList.Add(new CrewPresentationStub(dbItem));

            return retList;
        }
    }
}