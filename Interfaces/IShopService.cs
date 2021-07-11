using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface IShopService
    {
        // 生成店铺
       　public bool createShop(string sellerid, string shopName, short category, string description);

        // 查看卖家所有店铺
        public Task<List<Shop>> viewSellerShop(string sellerid);
       
        // 删除店铺
        public Task<bool> deleteShop(string sellerid, string shopid);     
    }
}
