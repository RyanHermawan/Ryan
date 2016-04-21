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
    public interface IWhitelistRepository
    {
        List<Crew_Whitelist> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        Crew_Whitelist FindByPk(int id);
        int Count(FilterInfo filters = null);
        void Save(Crew_Whitelist dbItem);
        void Delete(Crew_Whitelist dbItem);
    }
}
