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
    /// <summary>
    /// 后台数据分析管理中心
    /// </summary>
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

        // 根据时间段查看卖家各店铺的销售额
        public List<ShopProfit> getShopProfit(string sellerid, DateTime startTime, DateTime endTime)
        {
            List<Shop> shops = _context.Shops.Where(x => x.SellerId == sellerid).ToList();

            List<ShopProfit> shopProfits = new List<ShopProfit>();

            foreach (Shop shop in shops)
            {
                // 找到时间段内店铺的所有订单
                // List<Order> shopOrders = _context.Orders.Where(x => x.ShopId == shop.ShopId && x.Status == COrders.Done
                //                        && x.OrdersDate > startTime && x.OrdersDate < endTime).ToList();

                decimal? profit = _context.OrdersCommodities.Include(x => x.Orders).Where(x => x.Orders.ShopId == shop.ShopId && x.Status == COrders.Done
                                         && x.Orders.OrdersDate > startTime && x.Orders.OrdersDate < endTime).Include(x => x.Commodity).Sum(x => x.Commodity.Price);
                // 找到订单的联系集
                // List<OrdersCommodity> shopOrdersCommodities = _context.OrdersCommodities.Where(x => x.OrdersId == shopOrders.OrderId)

                // 查找销售额
                //double profit = 0.0;

                //foreach (OrdersCommodity ordersCommodity in shopOrdersCommodities)
                //{
                //    Commodity commodity = _context.Commodities.Where(x => x.CommodityId == ordersCommodity.CommodityId).FirstOrDefault();
                //    profit = profit + (double)commodity.Price;
                //}

                ShopProfit shopProfit = new ShopProfit
                {
                    ShopName = shop.Name,
                    Profit = profit
                };

                shopProfits.Add(shopProfit);
            }

            return shopProfits;
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
