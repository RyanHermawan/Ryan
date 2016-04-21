using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Business.Entities;

namespace WebUI.Models
{
    public class WhitelistPresentationStub
    {
        public int Id { get; set; }
        public string Barcode { get; set; }
		public DateTime TanggalAwal { get; set; }		
        public DateTime TanggalAkhir { get; set; }

        public WhitelistPresentationStub() { }

		public WhitelistPresentationStub(Crew_Whitelist dbItem) { 
			this.Id = dbItem.Id;
			this.Barcode = dbItem.Barcode;
			this.TanggalAwal = (DateTime) dbItem.Tanggal_Awal;
            this.TanggalAkhir = (DateTime) dbItem.Tanggal_Akhir;
		}

		public List<WhitelistPresentationStub> MapList(List<Crew_Whitelist> dbItems)
        {
            List<WhitelistPresentationStub> retList = new List<WhitelistPresentationStub>();

            foreach (Crew_Whitelist dbItem in dbItems)
                retList.Add(new WhitelistPresentationStub(dbItem));

            return retList;
        }
    }
}