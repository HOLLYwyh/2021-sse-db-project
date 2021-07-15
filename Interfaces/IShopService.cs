using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
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
        public List<Shop> viewSellerShop(string sellerid);

        // 查看卖家一个店铺
        public Shop viewOneShop(string sellerid, string shopName);

        // 查看卖家一个店铺及所有商品
        //public List<ShopCommodityView> getShopCommodities(string shopid);
        public ShopCommodityView getShopCommodities(string shopid);


        // 判断卖家是否拥有店铺
        public bool sellerShopExist(string sellerid);

        // 删除店铺
        public bool deleteShop(string sellerid, string shopid);
    }
}
