using Internetmall.Models.BusinessEntity;
using InternetMall.Constants.Orders;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        //public Good RenderOrderPageFromDetail(string commodityId, int amount)   //从详情页购买
        //{
        //    if (commodityId == "")
        //        return null;
        //    else
        //    {
        //        if (_context.Commodities.Any(c => c.CommodityId == commodityId))
        //        {
        //            Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == commodityId);
        //            Good newGood = new Good();
        //            newGood.img = newCommodity.Url;
        //            newGood.intro = newCommodity.Name;
        //            newGood.shop = newCommodity.Shop.Name;
        //            newGood.ID = newCommodity.CommodityId;
        //            newGood.price = newCommodity.Price * amount;
        //            newGood.description = newCommodity.Description;
        //            newGood.Soldnum = newCommodity.Soldnum;
        //            return newGood;
        //        }
        //        else return null;
        //    }
        //}

        public Good RenderOrderPageFromDetail(string commodityId, int amount)
        {
            if (commodityId == "")
                return null;
            else
            {
                if (_context.Commodities.Any(c => c.CommodityId == commodityId))
                {
                    var query = from commodity in _context.Set<Commodity>().Where(c => c.CommodityId == commodityId) join shop in _context.Set<Shop>() on commodity.ShopId equals shop.ShopId select new { commodity, shop };
                    var newCommodity = query.ToList();
                    Good newGood = new Good();
                    newGood.img = newCommodity[0].commodity.Url;
                    newGood.intro = newCommodity[0].commodity.Name;
                    newGood.shop = newCommodity[0].shop.Name;
                    newGood.ID = newCommodity[0].commodity.CommodityId;
                    newGood.price = newCommodity[0].commodity.Price * amount;
                    newGood.description = newCommodity[0].commodity.Description;
                    newGood.Soldnum = newCommodity[0].commodity.Soldnum;
                    return newGood;
                }
                else return null;
            }
        }

        // 通过订单id查询订单是否存在
        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.OrdersId == id);
        }

        // 创建订单
        public bool createOrder(string buyerid, string commodityid, string receivedId, int amount)
        {
            // OrderId生成
            CreateIdCount orderCount = new CreateIdCount(_context);
            string orderId = orderCount.GetOrderCount();

            Commodity commodity = _context.Commodities.Where(x => x.CommodityId == commodityid).FirstOrDefault();

            // 创建联系集 - 初始时商品状态为待付款
            int price = (int)commodity.Price;
            OrdersCommodity ordersCommodity = new OrdersCommodity { OrdersId = orderId, CommodityId = commodityid, Status = COrders.ToBePay ,Amount=amount/price };
            _context.OrdersCommodities.Add(ordersCommodity);

            // 创建Order —— 初始状态为待付款
            Order order = new Order
            {
                OrdersId = orderId,
                BuyerId = buyerid,
                OrdersDate = DateTime.Now,
                Status = COrders.ToBePay,
                ShopId = commodity.ShopId,
                ReceivedId = receivedId,
                Orderamount = amount
            };

            _context.Orders.Add(order);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 更新订单状态
        public bool updateOrderStatus(string orderid, int newStatus)
        {
            Order order = _context.Orders.Where(x => x.OrdersId == orderid).FirstOrDefault();
            order.Status = newStatus;
            _context.Orders.Update(order);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 更新订单中商品的状态
        public bool updateCommodityStatus(string commodityid, int newStatus)
        {
            OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.OrdersId == commodityid).FirstOrDefault();
            ordersCommodity.Status = newStatus;
            _context.OrdersCommodities.Update(ordersCommodity);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 查看买家所有订单
        public string getOrderByBuyerId(string buyerid)
        {
            List<OrderView> ordersView = new List<OrderView>();

            List<Order> orders = _context.Orders.Where(x => x.BuyerId == buyerid).ToList();

            foreach (Order order in orders)
            {
                OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.OrdersId == order.OrdersId).FirstOrDefault();

                Commodity commodity = _context.Commodities.Where(x => x.CommodityId == ordersCommodity.CommodityId).FirstOrDefault();

                Shop shop = _context.Shops.Where(x => x.ShopId == order.ShopId).FirstOrDefault();

                OrderView orderView = new OrderView
                {
                    OrdersId = order.OrdersId,
                    BuyerId = order.BuyerId,
                    ReceivedId = order.ReceivedId,
                    CommodityId = commodity.CommodityId,
                    ShopId = order.ShopId,
                    CommodityName = commodity.Name,
                    Name = shop.Name,
                    OrdersDate = order.OrdersDate,
                    Status = order.Status,
                    Price = commodity.Price,
                    Url = commodity.Url,
                };

                ordersView.Add(orderView);
            }

            return JsonConvert.SerializeObject(ordersView);
        }

        // 根据状态查看买家订单
        public List<OrderView> getOrderByStatus(string buyerid, int status)
        {
            List<OrderView> ordersView = new List<OrderView>();

            List<Order> orders = _context.Orders.Where(x => x.BuyerId == buyerid && x.Status == status).ToList();

            foreach (Order order in orders)
            {
                OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.OrdersId == order.OrdersId).FirstOrDefault();

                Commodity commodity = _context.Commodities.Where(x => x.CommodityId == ordersCommodity.CommodityId).FirstOrDefault();

                Shop shop = _context.Shops.Where(x => x.ShopId == order.ShopId).FirstOrDefault();

                OrderView orderView = new OrderView
                {
                    OrdersId = order.OrdersId,
                    BuyerId = order.BuyerId,
                    ReceivedId = order.ReceivedId,
                    CommodityId = commodity.CommodityId,
                    ShopId = order.ShopId,
                    CommodityName = commodity.Name,
                    Name = shop.Name,
                    OrdersDate = order.OrdersDate,
                    Status = order.Status,
                    Price = commodity.Price,
                    Url = commodity.Url,
                };

                ordersView.Add(orderView);
            }

            return ordersView;
        }

        // 查看订单详情
        public OrderDetailView getOrderByStatus(string orderid)
        {
            Order order = _context.Orders.Where(x => x.OrdersId == orderid).FirstOrDefault();
            OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.OrdersId == orderid).FirstOrDefault();
            Commodity commodity = _context.Commodities.Where(x => x.CommodityId == ordersCommodity.CommodityId).FirstOrDefault();
            Shop shop = _context.Shops.Where(x => x.ShopId == order.ShopId).FirstOrDefault();
            ReceiveInformation receive = _context.ReceiveInformations.Where(x => x.ReceivedId == order.ReceivedId).FirstOrDefault();

            OrderDetailView orderDetail = new OrderDetailView
            {
                OrdersId = order.OrdersId,           // 订单ID
                BuyerId = order.BuyerId,             // 买家ID
                ReceivedId = order.ReceivedId,       // 收货详情ID
                ShopId = order.ShopId,               // 店铺ID         
                Status = order.Status,               // 订单的状态
                OrdersDate = order.OrdersDate,       // 下单时间

                // Orderamount // 订单包含物品数量      
                Price = commodity.Price,             // 商品价格
                Category = commodity.Category,        // 商品类别          
                CommodityName = commodity.Name,      // 商品名称    
                CommodityUrl = commodity.Url,        // 商品图片
                ShopName = shop.Name,                 // 店铺名称
                ReceiverPhone = receive.Phone,        // 收货人电话号码
                ReceiverName = receive.ReceiverName, // 收货人姓名
                Country = receive.Country,           // 国家
                Province = receive.Province,          // 省份
                City = receive.City,                  // 城市
                District = receive.District,          // 街区
                DetailAddr = receive.DetailAddr       // 地址详情
            };

            return orderDetail;
        }

        // 删除订单
        public bool removeOrder(string buyerid, string commodityid)
        {
            OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.CommodityId == commodityid).FirstOrDefault();
            if (ordersCommodity != null)
            {
                Order orders = _context.Orders.Where(x => x.OrdersId == ordersCommodity.OrdersId).FirstOrDefault();
                if (orders != null)
                    _context.Orders.Remove(orders);

                _context.OrdersCommodities.Remove(ordersCommodity);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        // 删除所有订单
        public bool removeAllOrder(string buyerid)
        {
            List<Order> orders = _context.Orders.Where(x => x.BuyerId == buyerid).ToList();

            foreach (Order order in orders)
            {
                OrdersCommodity ordersCommodity = _context.OrdersCommodities.Where(x => x.OrdersId == order.OrdersId).FirstOrDefault();
                if (ordersCommodity != null)
                    _context.OrdersCommodities.Remove(ordersCommodity);
            }
            _context.Orders.RemoveRange(orders);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }
    }
}
