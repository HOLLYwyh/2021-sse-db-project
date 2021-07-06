using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    interface ICommodityService
    {
        public  Task<bool> Create(decimal price, string category, string description, int storage, string name, string shopId,string url);
        public  Task<bool> Delete(string shopId, string commodityId);
        public Task<List<Commodity>> ShowCommodities(string shopId, string searchCondition);
    }
}
