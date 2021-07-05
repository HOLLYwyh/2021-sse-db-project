using InternetMall.Models;
using InternetMall.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    interface ISellerBackgroundService
    {
        public Task<bool> Refundment(string orderId, string commodityId);//退款售后
        public Task<List<Shop>> ChoseShop(string sellerID);//筛选店铺
        public void PublishActivity(DateTime sTime, DateTime eTime, string activityName, int activityType, string desc);//发布活动
        public Task<Activity> DisplayActivity(string activityId);//显示活动
        public void PublishCoupon(string couponId, DateTime sTime, DateTime eTime, int thres, int dis1, int dis2, int type, string shopId, string commdityId);//发布优惠券
        public Task<Coupon> DisplayCoupon(string couponId);//显示优惠券
        public Task<List<Order>> DisplayOrder(string shopID);//显示订单
        public Task<List<Order>> SearchOrder(string orderId = null, string commodityId = null, string commodityName = null, string recieverName = null,string receiverPhone=null, string sellerId = null);//搜索订单
        public Task<List<Order>> FilterOrder(int orderType);//筛选订单
    }
}
