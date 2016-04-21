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
    public class EFKaryawanRepository : IKaryawanRepository
    {
        private Karyawan context = new Karyawan();

        #region karyawan

        public List<tbKaryawan> FindAll(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null)
        {
            IQueryable<tbKaryawan> list = context.tbKaryawans;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                filters.FormatFieldToUnderscore();
                GridHelper.ProcessFilters<tbKaryawan>(filters, ref list);
            }

            if (sortings != null && sortings.Count > 0)
            {
                foreach (var s in sortings)
                {
                    s.FormatSortOnToUnderscore();
                    list = list.OrderBy<tbKaryawan>(s.SortOn + " " + s.SortOrder);
                }
            }
            else
            {
                list = list.OrderBy<tbKaryawan>("id desc"); //default, wajib ada atau EF error
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
            List<tbKaryawan> result = takeList.ToList();
            return result;
        }

        public tbKaryawan FindByPk(int id)
        {
            return context.tbKaryawans.Find(id);
        }

        public int Count(FilterInfo filters = null)
        {
            IQueryable<tbKaryawan> items = context.tbKaryawans;

            if (filters != null && (filters.Filters != null && filters.Filters.Count > 0))
            {
                GridHelper.ProcessFilters<tbKaryawan>(filters, ref items);
            }

            return items.Count();
        }

        public void Save(tbKaryawan dbItem)
        {
            if (dbItem.id == 0) //create
            {
                context.tbKaryawans.Add(dbItem);
            }
            else //edit
            {
                var entry = context.Entry(dbItem);
                entry.State = EntityState.Modified;
            }
            context.SaveChanges();
        }

        public void Delete(tbKaryawan dbItem)
        {
            context.tbKaryawans.Remove(dbItem);
            context.SaveChanges();
        }

        #endregion
    }
}
