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
        //确认订单
        public bool ConfirmOrder()
        {
            foreach (string orderId in Global.GOrderID)
            {
                if (orderId == "")
                    return false;
                else
                {
                    if (_context.Orders.Any(o => o.OrdersId == orderId))
                    {
                        Order newOrder = _context.Orders.FirstOrDefault(o => o.OrdersId == orderId);
                        if (newOrder.Status == COrders.ToBePay)
                        {
                            newOrder.Status = COrders.ToBeShip;
                            List<OrdersCommodity> cartList = _context.OrdersCommodities.Where(a => a.OrdersId == orderId).ToList();
                            foreach (OrdersCommodity newItem in cartList)
                            {
                                if (newItem.Status == COrders.ToBePay)
                                {
                                    newItem.Status = COrders.ToBeShip;
                                    _context.OrdersCommodities.Update(newItem);
                                }
                            }
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            return true;
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
        public bool CreateOrderFromChart(string buyerId, List<Good> cartCommodityList, string receivedId, decimal price)
        {
            if (buyerId == "" || cartCommodityList.Count == 0 || receivedId == "" || price == 0)
                return false;
            else
            {
                List<Commodity> commodityList = new List<Commodity>();
                List<Shop> shopList = new List<Shop>();
                Global.GOrderID.Clear();
                CreateIdCount create = new CreateIdCount(_context);
                decimal oldPrice = 0;
                foreach (Good cartCommodity in cartCommodityList)
                {
                    Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == cartCommodity.ID);
                    Shop newShop = _context.Shops.FirstOrDefault(s => s.Name == cartCommodity.shop);
                    oldPrice = oldPrice + ((int)newCommodity.Price) * cartCommodity.Soldnum;
                    commodityList.Add(newCommodity);
                    if (shopList.Any(s => s.ShopId == newShop.ShopId) == false)
                        shopList.Add(_context.Shops.FirstOrDefault(s => s.ShopId == newShop.ShopId));
                    AddShoppingCart scCommodity = _context.AddShoppingCarts.FirstOrDefault(a => a.CommodityId == cartCommodity.ID && a.BuyerId == buyerId);
                    _context.AddShoppingCarts.Remove(scCommodity);
                }
                List<BuyerCoupon> couponList = _context.BuyerCoupons.Where(c => c.BuyerId == buyerId).ToList();
                foreach (BuyerCoupon newBuyerCoupon in couponList)
                {
                    Coupon newCoupon = _context.Coupons.FirstOrDefault(c => c.CouponId == newBuyerCoupon.CouponId);
                    if (newCoupon.Threshold <= oldPrice && newCoupon.Discount1 == (oldPrice - price))
                    {
                        newBuyerCoupon.Amount--;
                        if (newBuyerCoupon.Amount == 0)
                            _context.BuyerCoupons.Remove(newBuyerCoupon);
                        else _context.BuyerCoupons.Update(newBuyerCoupon);
                    }
                }
                decimal discountNumber = price / shopList.Count;
                foreach (Shop newShop in shopList)
                {
                    Order newOrder = new Order();
                    string newOrderId=create.GetOrderCount();
                    newOrder.OrdersId = newOrderId;
                    Global.GOrderID.Add(newOrderId);
                    newOrder.BuyerId = buyerId;
                    newOrder.ShopId = newShop.ShopId;
                    newOrder.OrdersDate = DateTime.Now;
                    newOrder.Status = COrders.ToBePay;
                    newOrder.ReceivedId = receivedId;
                    decimal amount = 0;
                    foreach (Commodity newCommodity in commodityList)
                    {
                        Good newcart = cartCommodityList.FirstOrDefault(c => c.ID == newCommodity.CommodityId);
                        OrdersCommodity newOrdersCommodity = new OrdersCommodity();
                        newOrdersCommodity.OrdersId = newOrder.OrdersId;
                        newOrdersCommodity.CommodityId = newCommodity.CommodityId;
                        newOrdersCommodity.Status = COrders.ToBePay;
                        newOrdersCommodity.Amount = newcart.Soldnum;
                        _context.OrdersCommodities.Add(newOrdersCommodity);
                        if (newCommodity.ShopId == newShop.ShopId)
                        {
                            amount = amount + ((int)newCommodity.Price) * newcart.Soldnum;
                        }
                    }
                    amount = amount - (oldPrice - price) / discountNumber;
                    newOrder.Orderamount = amount;
                    _context.Orders.Add(newOrder);
                }
                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
        }
        // 从商品详情页面创建订单
        public bool CreateOrderFromDetail(string buyerid, string commodityid, string receivedId, int amount, decimal price)
        {
            // OrderId生成
            CreateIdCount orderCount = new CreateIdCount(_context);
            string orderId = orderCount.GetOrderCount();
            Global.GOrderID.Clear();
            Global.GOrderID.Add(orderId);
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
