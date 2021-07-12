using InternetMall.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.ControllerModels
{
    /// <summary>
    /// ID生成器接口
    /// </summary>
    public class CreateIdCount
    {
        // 构造函数
        private readonly ModelContext _context;

        public CreateIdCount(ModelContext context)
        {
            _context = context;
        }

        // 买家ID生成
        public string GetBuyerCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int buyerCount = count.Buyercount + 1;
            count.Buyercount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return buyerCount.ToString();
        }

        // 卖家ID生成
        public string GetSellerCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int sellerCount = count.Sellercount + 1;
            count.Sellercount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return sellerCount.ToString();
        }

        // 管理员ID生成
        public string GetAdministratorCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int administratorCount = count.Administratorcount + 1;
            count.Administratorcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return administratorCount.ToString();
        }

        // 商品ID生成
        public string GetCommodityCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int commodityCount = count.Commoditycount + 1;
            count.Commoditycount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return commodityCount.ToString();
        }

        // 店铺ID生成
        public string GetShopCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int shopCount = count.Shopcount + 1;
            count.Shopcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return shopCount.ToString();
        }

        // 订单ID生成
        public string GetOrderCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int orderCount = count.Ordercount + 1;
            count.Ordercount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return orderCount.ToString();
        }

        // 优惠券ID生成
        public string GetCouponCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int couponCount = count.Couponcount + 1;
            count.Couponcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return couponCount.ToString();
        }

        // 活动ID生成
        public string GetActivityCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int activityCount = count.Activitycount + 1;
            count.Activitycount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return activityCount.ToString();
        }

        // 收货信息ID生成
        public string GetReceivedCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int receivedCount = count.Receivedcount + 1;
            count.Receivedcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return receivedCount.ToString();
        }

        // 聊天消息ID生成
        public string GetMessageCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int messageCount = count.Messagecount + 1;
            count.Messagecount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return messageCount.ToString();
        }

        // 聊天室ID生成
        public string GetChatroomCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int chatroomCount = count.Chatroomcount + 1;
            count.Chatroomcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return chatroomCount.ToString();
        }
    }
}
