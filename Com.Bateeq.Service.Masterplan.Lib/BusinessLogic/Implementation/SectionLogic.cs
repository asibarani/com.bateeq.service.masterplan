﻿using Com.Bateeq.Service.Masterplan.Lib.Models;
using Com.Bateeq.Service.Masterplan.Lib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Com.Moonlay.NetCore.Lib.Service;
using Com.Bateeq.Service.Masterplan.Lib.Helpers;
using System.Reflection;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;
using System.Linq.Dynamic.Core;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Com.Moonlay.Models;

namespace Com.Bateeq.Service.Masterplan.Lib.BusinessLogic.Implementation
{
    public class SectionLogic : BaseLogic<Section>
    {
        public SectionLogic(IServiceProvider serviceProvider, MasterplanDbContext dbContext) : base(serviceProvider, dbContext)
        {
        }

        public override ReadResponse<Section> ReadModel(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<Section> query = this.DbSet;

            List<string> searchAttributes = new List<string>()
                {
                    "Code","Name"
                };
            query = QueryHelper.Search(query, searchAttributes, keyword);

            Dictionary<string, object> filterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            query = QueryHelper.Filter(query, filterDictionary);

            List<string> selectedFields = new List<string>()
                {
                    "Id", "Code", "Name", "Remark"
                };
            query = query
                .Select(field => new Section
                {
                    Id = field.Id,
                    Code = field.Code,
                    Name = field.Name,
                    Remark = field.Remark
                });

            Dictionary<string, string> orderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            query = QueryHelper.Order(query, orderDictionary);

            Pageable<Section> pageable = new Pageable<Section>(query, page - 1, size);
            List<Section> data = pageable.Data.ToList<Section>();
            int totalData = pageable.TotalCount;

            return new ReadResponse<Section>(data, totalData, orderDictionary, selectedFields);
        }
    }
}
