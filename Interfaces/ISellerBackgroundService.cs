using Internetmall.Models;
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
        public Task<string> Refundment(string orderId, string commodityId);//退款售后
        public Task<bool> delivery(string orderId);//发货
        public void createActivity(DateTime sTime, DateTime eTime, string activityName, short activityType, string desc);//发布活动
        public Task<List<Activity>> DisPlayActivity(string activityID);//显示活动
        public void createCoupon(DateTime sTime, DateTime eTime, int thres, int dis1, int dis2, short type, string shopId, string commdityId);//发布优惠券
        public Task<List<Coupon>> DisplayCoupon(string couponID);//显示优惠券
        public Task<List<Shop>> ChooseShop(string sellerID);//选择店铺
        public string DisplayBriefOrder(string shopID);//显示简略订单信息
        public Task<SellerDetailedOrderView> DisplayDetailedOrder(string orderID);//显示订单详情
        public Task<List<Order>> SearchOrder(string orderId = null, string commodityId = null, string commodityName = null, string recieverName = null, string receiverPhone = null, string sellerId = null);//搜索订单
        public Task<List<Order>> FilterOrder(int orderStatus);//筛选订单
        public string DisplayShops(string sellerID); //显示所有店铺
    }
}
