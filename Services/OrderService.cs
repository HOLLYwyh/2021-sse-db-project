using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    /// <summary>
    /// 买家订单处理中心
    /// </summary>
    public class OrderService : IOrderService
    {
        // 构造函数
        private readonly ModelContext _context;

        public OrderService(ModelContext context)
        {
            _context = context;
        }

        // 通过订单id查询订单是否存在
        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.OrdersId == id);
        }      

        // 根据买家id查看所有订单
        public async Task<Order> getOrderByBuyerid(string buyerid)
        {
            if(buyerid == null)
            {
                return null;
            }

            var order = await _context.Orders
                .Include(o => o.Buyer)
                .Include(o => o.Received)
                .Include(o => o.Shop)
                .FirstOrDefaultAsync(m => m.BuyerId == buyerid);

            if(order==null)
            {
                return null;
            }

            return order;
        }

        // 创建订单
        //public bool createOrder(string buyerid, string shopName, short category, string description)
        //{
        //    Shop shop = _context.Shops.Where(x => x.SellerId == sellerid && x.Name == shopName).FirstOrDefault();

        //    if (shop == null)
        //    {
        //        shop = new Shop { SellerId = sellerid, ShopId = GetShopCount().ToString(), Name = shopName, Category = category, Description = description };

        //        _context.Shops.Add(shop);
        //    }

        //    if (_context.SaveChanges() > 0)
        //        return true;

        //    return false;
        //}
        public void createOrder(string buyerid, string commodityid)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (cart == null)
            {
                cart = new AddShoppingCart { BuyerId = buyerid, CommodityId = commodityid, Quantity = 1, DateCreated = DateTime.Now };
                _context.AddShoppingCarts.Add(cart);
            }

            else
                cart.Quantity++;

            _context.SaveChanges();
        }

        // 删除订单
        public async Task removeOrder(string buyerid, string commodityid)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (cart != null)
            {
                _context.AddShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        // 删除所有订单
        public async Task removeAllOrder(string buyerid)
        {
            var carts = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid);
            _context.AddShoppingCarts.RemoveRange(carts);
            await _context.SaveChangesAsync();
        }
    }
}
