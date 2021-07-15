using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
    public class BuyerModel
    {
        public string BuyerId { get; set; }
    }
    public class BuyerPhone
    {
        public string BuyerId { get; set; }
        public string OldNo { get; set; }
        public string NewNo { get; set; }
    }
    public class BuyerPasswd
    {
        public string BuyerId { get; set; }
        public string OldPasswd { get; set; }
        public string NewPasswd { get; set; }
    }
    public class BuyerInfo
    {
        public string BuyerId { get; set; }
        public string UpdatedNickname { get; set; }
        public int UpdatedGender { get; set; }
        public DateTime UpdatedBirth { get; set; }
        public string UpdatedUrl { get; set; }
       
    }
    /************************ 买家收货信息 *******************************/
    public class AddReceiveInformation                                 // 添加收货信息
    {
        public string BuyerId { get; set; }
        public string Phone { get; set; }
        public string ReceiverName { get; set; }

        // public string Country { get; set; }
        // public string Province { get; set; }
        // public string City { get; set; }
        // public string District { get; set; }
        public string DetailAddr { get; set; }
        public string Tag { get; set; }
    }

    public class UpdateReceiveInformation                            // 更新收货信息
    {
        public string ReceivedId { get; set; }
        public string Phone { get; set; }
        public string ReceiverName { get; set; }

        // public string Country { get; set; }
        // public string Province { get; set; }
        // public string City { get; set; }
        // public string District { get; set; }
        public string DetailAddr { get; set; }
        public string Tag { get; set; }
    }


    public class DeleteReceiveInformation                           // 删除收货地址
    {
        public string ReceiveId { get; set; }
    }

    /********************* 收藏夹服务 —— 关注商品 ***********************/
    public class BuyerFavorites                         // 查看关注信息
    {
        public string BuyerId { get; set; }
        public string CommodityId { get; set; }
        public string CommodityImg { get; set; }         // 商品图片
        public DateTime? DateCreated { get; set; }       // 收藏时间
        public string ShopName { get; set; }             // 店铺名称
        public string CommodityName { get; set; }        // 商品名称
        public decimal? Price { get; set; }              // 商品价格  
    }

    public class AddOrDeleteFavorites                          // 添加或删除某件商品的关注
    {
        public string buyerid { get; set; }
        public string commodityid { get; set; }
    }

    public class DeleteAllFavorites                          // 删除所有关注
    {
        public string buyerid { get; set; }
    }

    /********************** 买家关注店铺服务 ***************************/
    public class AddOrDeleteFollowShop                         // 添加或删除某件店铺的关注
    {
        public string buyerid { get; set; }
        public string shopid { get; set; }
    }

    public class DeleteAllFollowShops                          // 删除所有店铺关注
    {
        public string buyerid { get; set; }
    }

    public class LookFollowShops                              // 查看关注信息店铺
    {
        public string buyerid { get; set; }
        public string shopid { get; set; }
        public string imgPath { get; set; }
        public DateTime DateCreated { get; set; }
        public string shopName { get; set; }
        List<Commodity> commodities { get; set; }
    }

    /****************** 优惠券 *********************/
    public class LookCoupons                       // 查看优惠券
    {
        public string BuyerId { get; set; }
    }

    public class UseCoupon                       // 使用优惠券
    {
        public string CouponId { get; set; }
    }

    /********************* 卖家店铺数据分析 ******************/
    public class ShopData
    {
        public string ShopID { get; set; }
    }

    /********************* 买家订单 *************************/
    public class BuyerOrder
    {
        public string BuyerId { get; set; }
    }
}
