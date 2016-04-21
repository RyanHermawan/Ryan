using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Business.Infrastructure;
using Business.Entities;

namespace Business.Abstract
{
    public interface ICrewRepository
    {
        List<Crew> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        Crew FindByPk(string barcode);
        int Count(FilterInfo filters = null);
        void Save(Crew dbItem);
        void Delete(Crew dbItem);
    }
}
