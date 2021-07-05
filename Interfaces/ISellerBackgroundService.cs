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
        public Task<List<Shop>> ChoseShop(string sellerID);
        public void publicActivity(DateTime sTime, DateTime eTime, string activityName, int activityType, string desc);
        public void publicCoupon(string couponId, DateTime sTime, DateTime eTime, int thres, int dis1, int dis2, int type, string shopId, string commdityId);
        public Task<List<Order>> DisplayOrder(string shopID);
        public Task<List<Order>> SearchOrder(string orderId = null, string commodityId = null, string commodityName = null, string recieverName = null,string receiverPhone=null, string sellerId = null);
    }
}
