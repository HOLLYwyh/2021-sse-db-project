using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    public class ShopService : IShopService
    {
        // 构造函数
        private readonly ModelContext _context;

        public ShopService(ModelContext context)
        {
            _context = context;
        }
        private int GetShopCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int shopCount = count.Shopcount + 1;
            count.Shopcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return shopCount;
        }

        // 生成店铺
       　public bool createShop(string sellerid, string shopName, short category, string description)
        {
            Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.Name == shopName).FirstOrDefault();

            if (shop == null)
            {
                shop = new Shop { SellerId = sellerid, ShopId = GetShopCount().ToString(), Name = shopName, Category = category, Description = description };

                _context.Shops.Add(shop);
            }

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }
        // 店铺信息及其拥有的所有商品
        //public List<ShopCommodityView> getShopCommodities(string shopid)
        public ShopCommodityView getShopCommodities(string shopid)
        {
            Shop shop = _context.Shops.Where(x => x.ShopId == shopid).FirstOrDefault();  // 店铺信息

            //List<ShopCommodityView> shopCommodityViews = new List<ShopCommodityView>();    //  返回信息列表     

            List<Commodity> shopCommodities = _context.Commodities.Where(x => x.ShopId == shopid).ToList();    // 所有商品

            List<CommodityView> commodityViews = new List<CommodityView>();

            foreach (var shopcommodity in shopCommodities)
            {
                CommodityView commodityView = new CommodityView
                {
                    CommodityId = shopcommodity.CommodityId,
                    ShopId = shopcommodity.ShopId,
                    Url = shopcommodity.Url,
                    Category = shopcommodity.Category,
                    Name = shopcommodity.Name,
                    Price = shopcommodity.Price,
                    Storage = shopcommodity.Storage,
                    Soldnum = shopcommodity.Soldnum,
                    Description = shopcommodity.Description
                };
                commodityViews.Add(commodityView);
            }
            ShopView shopView = new ShopView
            {
                shopID = shop.ShopId,
                shopName = shop.Name,
                shopDescription = shop.Description,
                img = shop.Url,
                creditScore = shop.CreditScore
            };

            ShopCommodityView shopCommodityView = new ShopCommodityView
            {
                Shop = shopView,
                CommodityViews = commodityViews
            };

            return shopCommodityView;
        }

        // 查看卖家所有店铺
        public List<Shop> viewSellerShop(string sellerid)
        {
            List<Shop> shopsList = _context.Shops.Where(x => x.SellerId == sellerid).ToList();

            return shopsList;
        }

        // 查看卖家一个店铺
        public Shop viewOneShop(string sellerid, string shopName)
        {
            Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.Name == shopName).FirstOrDefault();

            return shop;
        }

        // 判断卖家是否拥有店铺
        public bool sellerShopExist(string sellerid)
        {
            return _context.Shops.Any(x => x.SellerId == sellerid);
        }

        // 删除店铺
        public bool deleteShop(string sellerid, string shopid)
        {
            Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.ShopId == shopid).FirstOrDefault();

            _context.Shops.Remove(shop);

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }

    }
}
