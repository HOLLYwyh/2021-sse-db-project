using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetMall.Services
{
    public class SellerBackgroundServices: ISellerBackgroundService
    {
        private readonly ModelContext _context;
        public async Task<bool> Refundment(string orderId, string commodityId)//退款售后
        {
            Order newOrder = await _context.Orders.Include(o => o.OrdersCommodities).FirstOrDefaultAsync(o => o.OrdersId == orderId);
            bool commodityJudge = false;
            bool orderJudge = true;
            foreach(OrdersCommodity commodity in newOrder.OrdersCommodities)
            {
                if (commodity.status != 10)
                    orderJudge = false;
                if (commodity.CommodityId == commodityId)
                {
                    commodityJudge = true;
                    commodity.status = 10;
                }
            }
            if (orderJudge == true)
                newOrder.Status = 10;
            await _context.SaveChangesAsync();
            if (commodityJudge == false)
                return false;
            else return true;
        }
        public SellerBackgroundServices(ModelContext context)
        {
            _context = context;
        }

        public async Task<List<Shop>> ChoseShop(string sellerId)//筛选店铺
        {
            if(sellerId==null)
            {
                return null;
            }
            var modelContext = _context.Shops.Where(s => s.SellerId == sellerId).Include(s => s.Seller);
            return await modelContext.ToListAsync();
        }

        public void PublishActivity(DateTime sTime, DateTime eTime, string activityName, int activityType, string desc)//发布活动
        {
            Activity newActivity = new Activity { ActivityId = Guid.NewGuid().ToString(), StartTime = sTime, EndTime = eTime, Name = activityName, Category = activityType, description = desc };
            _context.Activities.Add(newActivity);
            _context.SaveChanges();
        }

        public async Task<Activity> DisplayActivity(string activityId)//显示活动
        {
            if (activityId == null)
            {
                return null;
            }
            Activity newActivity = await _context.Activities.Where(a => a.ActivityId == activityId).FirstOrDefaultAsync();
            return newActivity;
        }
        public void PublishCoupon(string couponId, DateTime sTime, DateTime eTime, int thres, int dis1, int dis2, int type, string shopId, string commdityId)//发布优惠券
        {
            Coupon coupon = new Coupon { CouponId = couponId, StartTime = sTime, EndTime = eTime, Threshold = thres, Discount1 = dis1, Discount2 = dis2, Category = type, ShopId = shopId, CommodityId = commdityId };
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }

        public async Task<Coupon> DisplayCoupon(string couponId)//显示优惠券
        {
            if (couponId == null)
            {
                return null;
            }
            Coupon newCoupon = await _context.Coupons.Where(c => c.CouponId == couponId).FirstOrDefaultAsync();
            return newCoupon;
        }
        public async Task<List<Order>> DisplayOrder(string shopId)//展示订单
        {
            if (shopId == null)
            {
                return null;
            }
            var modelContext = _context.Orders.Where(o => o.ShopId == shopId).Include(o => o.Buyer).Include(o => o.Received).Include(o => o.Shop);
            return await modelContext.ToListAsync();
        }

        public async Task<List<Order>> SearchOrder(string orderId, string commodityId, string commodityName, string recieverName,string recieverPhone, string buyerId)//搜索订单
        {
            List<Order> newList;
            var modelContext = await _context.Orders.Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop)
                                                    .Include(o => o.OrdersCommodities)
                                                    .ToListAsync();
            newList = modelContext;
            foreach(Order newOrder in newList)
            {

                if (orderId != null)
                {
                    if (newOrder.OrdersId != orderId)
                        newList.Remove(newOrder);
                }
                else if(buyerId != null)
                {
                    if (newOrder.BuyerId != buyerId)
                        newList.Remove(newOrder);
                }
                else if(commodityId != null)
                {
                    foreach(OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if(newOrdersCommodity.CommodityId != commodityId)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if(commodityName !=null)
                {
                    foreach (OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if (newOrdersCommodity.Commodity.Name.Contains(commodityName) == false)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if(recieverName !=null)
                {
                    if (newOrder.Received.ReceiverName != recieverName)
                        newList.Remove(newOrder);
                }
                else if (recieverPhone != null)
                {
                    if (newOrder.Received.Phone != recieverPhone)
                        newList.Remove(newOrder);
                }
            }
            return  newList;
        }
        public async Task<List<Order>> FilterOrder(int orderType)//筛选订单
        {
            if (orderType < 0 || orderType > 9)
            {
                return null;
            }
            List<Order> newList;
            var modelContext = await _context.Orders.Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop).ToListAsync();
            newList = modelContext;
            foreach (Order newOrder in newList)
            {
                if(newOrder.Status != orderType)
                    newList.Remove(newOrder);
            }
            return newList;
        }
    }
}
