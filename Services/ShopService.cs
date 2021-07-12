using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
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
