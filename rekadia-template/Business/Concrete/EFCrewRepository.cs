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
    public class EFCrewRepository : ICrewRepository
    {
        private BataviaDB context = new BataviaDB();

        #region crew

        public List<Crew> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<Crew> list = context.Crews;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<Crew>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<Crew>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<Crew>("Barcode desc"); //default, wajib ada atau EF error
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
            List<Crew> result = takeList.ToList();
            return result;
        }

        public Crew FindByPk(string barcode)
        {
            return context.Crews.Find(barcode);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<Crew> items = context.Crews;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<Crew>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(Crew dbItem)
        {
            if (FindByPk(dbItem.Barcode) == null) //create
            {
                context.Crews.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(Crew dbItem)
        {
            context.Crews.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
    }
}
