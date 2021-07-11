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

        // 生成店铺
       　public bool createShop(string sellerid, string shopName, short category, string description)
        {
            Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.Name == shopName).FirstOrDefault();

            if (shop == null)
            {
                // 随机数生成店铺id
                Random rd = new Random();

                string shopid = rd.Next(0, 1000).ToString(); 

                shop = new Shop { SellerId = sellerid, ShopId = shopid, Name = shopName, Category = category, Description = description };
                _context.Shops.Add(shop);
            }

            if (_context.SaveChanges() > 0)
                return true;

            return false;
        }

        // 查看卖家所有店铺
        public async Task<List<Shop>> viewSellerShop(string sellerid)
        {           
            List<Shop> shopsList = await _context.Shops.Where(x => x.SellerId == sellerid).ToListAsync();

            if (shopsList == null)   // 该卖家没有店铺
                return null;

            return shopsList;
        }

        // 删除店铺
        public async Task<bool> deleteShop(string sellerid, string shopid)
        {
            Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.ShopId == shopid).FirstOrDefault();

            _context.Shops.Remove(shop);

            if (await _context.SaveChangesAsync() > 0)
                return true;

            return false;
        }

    }
}
