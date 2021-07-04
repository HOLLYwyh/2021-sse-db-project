using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    interface ICommodities
    {
        public  Task<bool> Create(decimal price, string category, string description, int storage, string name, string shop_id,string url);
        public  Task<bool> Delete(string shop_id, string commodity_id);
        public Task<List<Commodity>> ShowCommodities(string shopId, string searchCondition);
    }
}
