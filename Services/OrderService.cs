using InternetMall.Constants.Orders;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using InternetMall.Models.ControllerModels;
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

        // 创建订单
        public bool createOrder(string buyerid, string commodityid, string receivedId)
        {
            // OrderId生成
            CreateIdCount orderCount = new CreateIdCount(_context);
            string orderid = orderCount.GetOrderCount();

            Commodity commodity = _context.Commodities.Where(x => x.CommodityId == commodityid).FirstOrDefault();

            // 创建联系集 - 初始时商品状态为待付款
            OrdersCommodity ordersCommodity = new OrdersCommodity { OrdersId = orderid, CommodityId = commodityid, Status = COrders.ToBePay};
            _context.OrdersCommodities.Add(ordersCommodity);

            // 创建Order —— 初始状态为待付款
            Order order = new Order { OrdersId = orderid, BuyerId = buyerid, OrdersDate = DateTime.Now,
                    Status = COrders.ToBePay, ShopId = commodity.ShopId, ReceivedId = receivedId, Orderamount = 0};
     
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
        public List<OrderView> getOrderByBuyerId(string buyerid)
        {
            List<OrderView> ordersView = new List<OrderView>();

            List<Order> orders = _context.Orders.Where(x => x.BuyerId == buyerid).ToList();

            foreach (Order order in orders)
            {

                OrderView orderView = new OrderView();

            }

            return ordersView;
        }

        // 根据状态查看买家订单
        public List<OrderView> getOrderByStatus(string buyerid, int status)
        {
            List<OrderView> orders = new List<OrderView>();


            return orders;
        }

        // 查看订单详情
        public List<OrderView> getOrderByStatus(string orderid)
        {
            List<OrderView> orders = new List<OrderView>();


            return orders;
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
