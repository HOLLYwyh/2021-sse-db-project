using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 订单详情
    /// </summary>
    public class OrderDetailView
    {
        public string OrdersId { get; set; }         // 订单ID
        public string BuyerId { get; set; }          // 买家ID
        public string ReceivedId { get; set; }      // 收货详情ID
        public string ShopId { get; set; }          // 店铺ID         
        public int? Status { get; set; }            // 订单的状态
        public DateTime? OrdersDate { get; set; }   // 下单时间

        // public decimal Orderamount { get; set; } // 订单包含物品数量      
        public decimal? Price { get; set; }         // 商品价格
        public short? Category { get; set; }        // 商品类别          
        public string CommodityName { get; set; }   // 商品名称    
        public string CommodityUrl { get; set; }    // 商品图片
        public string ShopName { get; set; }        // 店铺名称
        public string ReceiverPhone { get; set; }   // 收货人电话号码
        public string ReceiverName { get; set; }    // 收货人姓名
        public string Country { get; set; }         // 国家
        public string Province { get; set; }        // 省份
        public string City { get; set; }            // 城市
        public string District { get; set; }        // 街区
        public string DetailAddr { get; set; }      // 地址详情
    }
}
