using InternetMall.DBContext;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Interfaces;
using Microsoft.EntityFrameworkCore;
using InternetMall.Constants.Orders;
using InternetMall.Models.BusinessEntity;

namespace InternetMall.Services
{
    public class SellerDataService : ISellerDataService
    {
        // 构造函数
        private readonly ModelContext _context;

        public SellerDataService(ModelContext context)
        {
            _context = context;
        }

        // 店铺关注数量
        public async Task<List<ShopFavoriteNum>> getShopFavotitesNum(string sellerid)
        {
            List<Shop> sellerShop = await _context.Shops.Where(x => x.SellerId == sellerid).ToListAsync();

            List<ShopFavoriteNum> shopFavotitesNum = new List<ShopFavoriteNum>();

            foreach (Shop item in sellerShop)
            {
                int favoriteNum = _context.FollowShops.Distinct().Count(x => x.ShopId == item.ShopId);
                ShopFavoriteNum tmp = new ShopFavoriteNum();
                tmp.ShopName = item.Name;
                tmp.FavoriteNum = favoriteNum;
                shopFavotitesNum.Add(tmp);
            }


            return shopFavotitesNum;
        }

        // 本月盈利  --- 待定！！！
        public int getMonthProfit(string sellerid)
        {
            int monthProfit = 100;

            return monthProfit;

        }

        // 待发货总数
        public async Task<List<ShopToBeShipNum>> getToBeShipNum(string sellerid)
        {
            List<Shop> sellerShops = await _context.Shops.Where(x => x.SellerId == sellerid).ToListAsync();

            List<ShopToBeShipNum> shopToBeShipNum = new List<ShopToBeShipNum>();

            foreach (Shop item in sellerShops)
            {
                int toBeNum = _context.Orders.Distinct().Count(x => x.ShopId == item.ShopId && x.Status == COrders.ToBeShip);
                ShopToBeShipNum tmp = new ShopToBeShipNum();
                tmp.ShopName = item.Name;
                tmp.ToBeShipNum = toBeNum;
                shopToBeShipNum.Add(tmp);
            }

            return shopToBeShipNum;
        }

        // 退款售后订单总数       
        public async Task<List<ShopCanceledOrderNum>> getCanceledOrderNum(string sellerid)
        {
            List<Shop> sellerShops = await _context.Shops.Where(x => x.SellerId == sellerid).ToListAsync();

            List<ShopCanceledOrderNum> shopCanceledOrderNum = new List<ShopCanceledOrderNum>();

            foreach (Shop item in sellerShops)
            {
                int canceledOrderNum = _context.Orders.Distinct().Count(x => x.ShopId == item.ShopId && x.Status == COrders.Canceled);
                ShopCanceledOrderNum tmp = new ShopCanceledOrderNum();
                tmp.ShopName = item.Name;
                tmp.CanceledOrderNum = canceledOrderNum;
                shopCanceledOrderNum.Add(tmp);
            }

            return shopCanceledOrderNum;
        }
    }
}
