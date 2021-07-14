using InternetMall.DBContext;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    public class AdminActionService
    {
        private ModelContext _context;
        public AdminActionService(ModelContext context)
        {
            _context = context;
        }
        public bool DeleteCommodity(string commodityId)
        {
            if (commodityId == "")
                return false;
            else
            {
                if (_context.Commodities.Any(c => c.CommodityId == commodityId))
                {
                    Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);
                    _context.Commodities.Remove(newCommodity);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public bool DeleteBuyer(string buyerId)
        {
            if (buyerId == "")
                return false;
            else
            {
                if (_context.Buyers.Any(b => b.BuyerId == buyerId))
                {
                    Buyer newBuyer = _context.Buyers.FirstOrDefault(c => c.BuyerId == buyerId);
                    _context.Buyers.Remove(newBuyer);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public bool DeleteShop(string shopId)
        {
            if (shopId == "")
                return false;
            else
            {
                if (_context.Shops.Any(s => s.ShopId == shopId))
                {
                    List<Commodity> commodityList = _context.Commodities.Where(c => c.ShopId == shopId).ToList();
                    foreach(Commodity newCommodity in commodityList)
                    {
                        string commodityId = newCommodity.CommodityId;
                        DeleteCommodity(commodityId);
                    }
                    Shop newShop = _context.Shops.FirstOrDefault(s => s.ShopId == shopId);
                    _context.Remove(newShop);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
        public bool DeleteSeller(string sellerId)
        {
            if (sellerId == "")
                return false;
            else
            {
                if (_context.Sellers.Any(s => s.SellerId == sellerId))
                {
                    List<Shop> shopList = _context.Shops.Where(s => s.SellerId == sellerId).ToList();
                    foreach(Shop newShop in shopList)
                    {
                        string shopId = newShop.ShopId;
                        DeleteShop(shopId);
                    }
                    Seller newSeller = _context.Sellers.FirstOrDefault(s => s.SellerId == sellerId);
                    _context.Remove(newSeller);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
