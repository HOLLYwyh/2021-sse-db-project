using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface ICreateIdCount
    {
        // 买家ID生成
        public string GetBuyerCount();       

        // 卖家ID生成
        public string GetSellerCount();        

        // 管理员ID生成
        public string GetAdministratorCount();      

        // 商品ID生成
        public string GetCommodityCount();       

        // 店铺ID生成
        public string GetShopCount();     

        // 订单ID生成
        public string GetOrderCount();        

        // 优惠券ID生成
        public string GetCouponCount();      

        // 活动ID生成
        public string GetActivityCount();       

        // 收货信息ID生成
        public string GetReceivedCount();       

        // 聊天消息ID生成
        public string GetMessageCount();      

        // 聊天室ID生成
        public string GetChatroomCount();
        
    }
}
