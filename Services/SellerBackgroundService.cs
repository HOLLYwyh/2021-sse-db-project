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
    public class SellerBackgroundServices : ISellerBackgroundService
    {
        private readonly ModelContext _context;
        public SellerBackgroundServices(ModelContext context)
        {
            _context = context;
        }
        //创建活动ID
        public int GetActivityCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int activityCount = count.Activitycount + 1;
            count.Activitycount += 1;
            _context.Update(count);
            _context.SaveChanges();
            return activityCount;
        }
        //创建优惠券ID
        public int GetCouponCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int couponCount = count.Couponcount + 1;
            count.Buyercount += 1;
            _context.Update(count);
            _context.SaveChanges();
            return couponCount;
        }
        //退款售后
        public async Task<string> Refundment(string orderId, string commodityId)
        {
            var modelContext = await _context.Orders.Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop).FirstOrDefaultAsync(o => o.OrdersId == orderId);
            bool judgeCommodity = false;
            bool judgeOrder = false;
            foreach (OrdersCommodity newCommodity in modelContext.OrdersCommodities)
            {
                if (newCommodity.CommodityId == commodityId)
                {
                    judgeCommodity = true;
                    newCommodity.Status = 2;
                    _context.OrdersCommodities.Update(newCommodity);
                    break;
                }
            }
            foreach (OrdersCommodity newCommodity in modelContext.OrdersCommodities)
            {
                if (newCommodity.Status != 2)
                    judgeOrder = true;
            }
            if (judgeOrder == true)
            {
                modelContext.Status = 2;
                _context.Orders.Update(modelContext);
            }
            if (judgeCommodity == true)
            {
                return "DONE";
            }
            else return "NOTDONE";
        }
        //发货
        public async Task<bool> delivery(string orderId)
        {
            var modelContext = await _context.Orders.Where(o => o.OrdersId == orderId).Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop).FirstOrDefaultAsync();
            modelContext.Status = 3;
            foreach (OrdersCommodity newOrdersCommodity in modelContext.OrdersCommodities)
            {
                newOrdersCommodity.Commodity.Storage--;
                _context.OrdersCommodities.Update(newOrdersCommodity);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public void createActivity(DateTime sTime, DateTime eTime, string activityName, short activityType, string desc)//发布活动
        {
            Activity newActivity = new Activity { ActivityId = GetActivityCount().ToString(), StartTime = sTime, EndTime = eTime, Name = activityName, Category = activityType, Description = desc };
            _context.Activities.Add(newActivity);
            _context.SaveChanges();
        }
        //显示活动
        public async Task<List<Activity>> DisPlayActivity(string activityID)
        {
            if (activityID == null)
            {
                return null;
            }
            var modelContext = await _context.Activities.Where(a => a.ActivityId == activityID).ToListAsync();
            return modelContext;
        }
        public void createCoupon(DateTime sTime, DateTime eTime, int thres, int dis1, int dis2, short type, string shopId, string commdityId)//发布优惠券
        {
            Coupon coupon = new Coupon { CouponId = GetCouponCount().ToString(), StartTime = sTime, EndTime = eTime, Threshold = thres, Discount1 = dis1, Discount2 = dis2, Category = type, ShopId = shopId};
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }
        //显示优惠券
        public async Task<List<Coupon>> DisplayCoupon(string couponID)
        {
            if (couponID == null)
            {
                return null;
            }
            var modelContext = await _context.Coupons.Where(a => a.CouponId == couponID).ToListAsync();
            return modelContext;
        }
        //筛选店铺
        public async Task<List<Shop>> ChooseShop(string sellerId)
        {
            if (sellerId == null)
            {
                return null;
            }
            var modelContext = await _context.Shops.Where(s => s.SellerId == sellerId).Include(s => s.Seller).ToListAsync();
            return modelContext;
        }
        //展示订单
        public async Task<List<Order>> DisplayOrder(string shopId)
        {
            if (shopId == null)
            {
                return null;
            }
            var modelContext = await _context.Orders.Where(o => o.ShopId == shopId).Include(o => o.Buyer).Include(o => o.Received).Include(o => o.Shop).ToListAsync();
            return modelContext;
        }
        //搜索订单
        public async Task<List<Order>> SearchOrder(string orderId, string commodityId, string commodityName, string recieverName, string recieverPhone, string buyerId)
        {
            List<Order> newList;
            var modelContext = await _context.Orders.Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop).ToListAsync();
            newList = modelContext;
            foreach (Order newOrder in newList)
            {

                if (orderId != null)
                {
                    if (newOrder.OrdersId != orderId)
                        newList.Remove(newOrder);
                }
                else if (buyerId != null)
                {
                    if (newOrder.BuyerId != buyerId)
                        newList.Remove(newOrder);
                }
                else if (commodityId != null)
                {
                    foreach (OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if (newOrdersCommodity.CommodityId != commodityId)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if (commodityName != null)
                {
                    foreach (OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if (newOrdersCommodity.Commodity.Name.Contains(commodityName) == false)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if (recieverName != null)
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
            return newList;
        }
    }
}
