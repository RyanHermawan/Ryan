using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;
using Business.Infrastructure;
using Business.Linq;
using Business.Entities;
using Business.Abstract;

namespace Business.Concrete
{
    public class EFWhitelistRepository : IWhitelistRepository
    {
        private BataviaDB context = new BataviaDB();

        #region whitelist

        public List<Crew_Whitelist> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<Crew_Whitelist> list = context.Crew_Whitelist;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<Crew_Whitelist>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<Crew_Whitelist>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<Crew_Whitelist>("id desc"); //default, wajib ada atau EF error
            }

            //take & skip
            var takeList = list;
            if (skip != null)
            {
                takeList = takeList.Skip(skip.Value);
            }
            if (take != null)
            {
                takeList = takeList.Take(take.Value);
            }

            //return result
            //var sql = takeList.ToString();
            List<Crew_Whitelist> result = takeList.ToList();
            return result;
        }

        public Crew_Whitelist FindByPk(int id)
        {
            return context.Crew_Whitelist.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<Crew_Whitelist> items = context.Crew_Whitelist;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<Crew_Whitelist>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(Crew_Whitelist dbItem)
        {
            if (dbItem.Id == 0) //create
            {
                context.Crew_Whitelist.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(Crew_Whitelist dbItem)
        {
            context.Crew_Whitelist.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
    }
}
