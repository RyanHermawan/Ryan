﻿using Business.Entities;
using Business.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;

namespace Business.Abstract
{
    public interface ILogRepository
    {
		List<log> Find(int? skip = null, int? take = null, List<SortingInfo> sortings = null, FilterInfo filters = null);
        //log FindByPk(int id);
        int Count(FilterInfo filters = null);
        long Save(log dbItem);
        void Truncate();
	}
}
