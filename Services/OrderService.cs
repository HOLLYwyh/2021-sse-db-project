using Internetmall.Models.BusinessEntity;
using Internetmall.Models.ControllerModels;
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

        public List<CouponView> GetCoupons(string buyerId)
        {
            if (buyerId == "")
                return null;
            else
            {
                List<BuyerCoupon> newList = _context.BuyerCoupons.Where(b => b.BuyerId == buyerId).ToList();
                List<CouponView> returnList = new List<CouponView>();
                foreach(BuyerCoupon newItem in newList)
                {
                    Coupon tempCoupon = _context.Coupons.FirstOrDefault(c => c.CouponId == newItem.CouponId);
                    CouponView couponView = new CouponView();
                    couponView.CouponId = tempCoupon.CouponId;
                    couponView.StartTime = tempCoupon.StartTime;
                    couponView.EndTime = tempCoupon.EndTime;
                    couponView.Threshold = tempCoupon.Threshold;
                    couponView.Discount = tempCoupon.Discount1;
                    returnList.Add(couponView);
                }
                return returnList;
            }
        }
        public List<ReceiveInformation>GetReceiveInformation(string buyerId)
        {
            if (buyerId == "")
                return null;
            else
            {
                if (_context.ReceiveInformations.Any(r => r.BuyerId == buyerId))
                {
                    List<ReceiveInformation> returnList = _context.ReceiveInformations.Where(r => r.BuyerId == buyerId).ToList();
                    return returnList;
                }
                else return null;
            }
        }
        //从购物车渲染订单
        public List<Good> RenderOrderPageFromCart(Cart newCart, string buyerId)
        {
            if (newCart.cart.Count == 0 || buyerId == "")
                return null;
            else
            {
                List<Good> returnList = new List<Good>();
                foreach (CartCommodity newCommodity in newCart.cart)
                {
                    Good newGood = new Good();
                    var query = from commodity in _context.Set<Commodity>().Where(c => c.CommodityId == newCommodity.commodityId) join shop in _context.Set<Shop>() on commodity.ShopId equals shop.ShopId select new { commodity, shop };
                    var tempCommodity = query.ToList();
                    newGood.img = tempCommodity[0].commodity.Url;
                    newGood.intro = tempCommodity[0].commodity.Name;
                    newGood.shop = tempCommodity[0].shop.Name;
                    newGood.ID = tempCommodity[0].commodity.CommodityId;
                    newGood.price = tempCommodity[0].commodity.Price;
                    newGood.description = tempCommodity[0].commodity.Description;
                    newGood.Soldnum = int.Parse(newCommodity.amount);
                    returnList.Add(newGood);
                }
                return returnList;
            }
        }
        //从商品详情页跳转到订单页面的渲染
        public List<Good> RenderOrderPageFromDetail(string commodityId, int amount)
        {
            if (commodityId == "")
                return null;
            else
            {
                if (_context.Commodities.Any(c => c.CommodityId == commodityId))
                {
                    List<Good> returnList = new List<Good>(); 
                    var query = from commodity in _context.Set<Commodity>().Where(c => c.CommodityId == commodityId) join shop in _context.Set<Shop>() on commodity.ShopId equals shop.ShopId select new { commodity, shop };
                    var newCommodity = query.ToList();
                    Good newGood = new Good();
                    newGood.img = newCommodity[0].commodity.Url;
                    newGood.intro = newCommodity[0].commodity.Name;
                    newGood.shop = newCommodity[0].shop.Name;
                    newGood.ID = newCommodity[0].commodity.CommodityId;
                    newGood.price = newCommodity[0].commodity.Price;
                    newGood.description = newCommodity[0].commodity.Description;
                    newGood.Soldnum = amount;
                    returnList.Add(newGood);
                    return returnList;
                }
                else return null;
            }
        }

        // 通过订单id查询订单是否存在
        private bool OrderExists(string id)
        {
            return _context.Orders.Any(e => e.OrdersId == id);
        }
        // 从购物车页面创建订单
        public bool CreateOrderFromChart(string buyerId, List<CartCommodityModels> cartCommodityList, string receivedId, int price)
        {
            if (buyerId == "" || cartCommodityList.Count == 0 || receivedId == "" || price == 0)
                return false;
            else
            {
                List<Commodity> commodityList = new List<Commodity>();
                List<Shop> shopList = new List<Shop>();
                CreateIdCount create = new CreateIdCount(_context);
                decimal oldPrice = 0;
                foreach(CartCommodityModels cartCommodity in cartCommodityList)
                {
                    Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == cartCommodity.CommodityId);
                    oldPrice = oldPrice + ((int)newCommodity.Price) * cartCommodity.amount;
                    commodityList.Add(newCommodity);
                    if (shopList.Any(s => s.ShopId == newCommodity.Shop.ShopId) == false)
                        shopList.Add(_context.Shops.FirstOrDefault(s => s.ShopId == newCommodity.Shop.ShopId));
                    AddShoppingCart scCommodity = _context.AddShoppingCarts.FirstOrDefault(a => a.CommodityId == cartCommodity.CommodityId && a.BuyerId == buyerId);
                    _context.AddShoppingCarts.Remove(scCommodity);
                }
                decimal discountNumber = price / shopList.Count;
                foreach(Shop newShop in shopList)
                {
                    Order newOrder = new Order();
                    newOrder.OrdersId = create.GetOrderCount();
                    newOrder.BuyerId = buyerId;
                    newOrder.ShopId = newShop.ShopId;
                    newOrder.OrdersDate = DateTime.Now;
                    newOrder.Status = COrders.ToBePay;
                    newOrder.ReceivedId = receivedId;
                    decimal amount = 0;
                    foreach(Commodity newCommodity in commodityList)
                    {
                        CartCommodityModels newcart = cartCommodityList.FirstOrDefault(c => c.CommodityId == newCommodity.CommodityId);
                        OrdersCommodity newOrdersCommodity = new OrdersCommodity();
                        newOrdersCommodity.OrdersId = newOrder.OrdersId;
                        newOrdersCommodity.CommodityId = newCommodity.CommodityId;
                        newOrdersCommodity.Status = COrders.ToBePay;
                        newOrdersCommodity.Amount = newcart.amount;
                        _context.OrdersCommodities.Add(newOrdersCommodity);
                        if (newCommodity.ShopId == newShop.ShopId )
                        {          
                            amount = amount + ((int)newCommodity.Price) * newcart.amount;
                        }
                    }
                    amount = amount - (oldPrice - price) / discountNumber;
                    newOrder.Orderamount = amount;
                    _context.Orders.Add(newOrder);
                }
                _context.SaveChanges();
                return true;
            }
        }
        // 从商品详情页面创建订单
        public bool CreateOrderFromDetail(string buyerid, string commodityid, string receivedId, int amount, int price)
        {
            // OrderId生成
            CreateIdCount orderCount = new CreateIdCount(_context);
            string orderId = orderCount.GetOrderCount();

            Commodity commodity = _context.Commodities.Where(x => x.CommodityId == commodityid).FirstOrDefault();

            // 创建联系集 - 初始时商品状态为待付款
            OrdersCommodity ordersCommodity = new OrdersCommodity { OrdersId = orderId, CommodityId = commodityid, Status = COrders.ToBePay ,Amount=amount };
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
                Orderamount = price
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
        public List<OrderInformationView> getOrderByBuyerId(string buyerid)
        {
            List<OrderInformationView> returnList = new List<OrderInformationView>();
            List<Order> orderList = _context.Orders.Where(o => o.BuyerId == buyerid).Include(o => o.OrdersCommodities).Include(o => o.Received).ToList();
            foreach (Order newOrder in orderList)
            {
                List<CommodityOrderView> commodities = new List<CommodityOrderView>();
                OrderInformationView returnItem = new OrderInformationView();
                returnItem.orderId = newOrder.OrdersId;
                returnItem.date = newOrder.OrdersDate;
                returnItem.receiverName = newOrder.Received.ReceiverName;
                returnItem.receiverPhone = newOrder.Received.Phone;
                returnItem.detailAddr = newOrder.Received.DetailAddr;
                switch (newOrder.Status)
                {
                    case 1: returnItem.status = "待付款"; break;
                    case 2: returnItem.status = "待发货"; break;
                    case 4: returnItem.status = "待收货"; break;
                    case 6: returnItem.status = "已完成"; break;

                }
                foreach (OrdersCommodity commodity in newOrder.OrdersCommodities)
                {
                    Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodity.CommodityId);
                    CommodityOrderView tempItem = new CommodityOrderView();
                    tempItem.CommodityId = newCommodity.CommodityId;
                    tempItem.name = newCommodity.Name;
                    tempItem.Url = newCommodity.Url;
                    tempItem.amount = commodity.Amount;
                    commodities.Add(tempItem);
                }
                returnItem.commodityList = commodities;
                returnList.Add(returnItem);
            }
            return returnList;

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
