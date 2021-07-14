using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 订单显示信息类
    /// </summary>
    public class OrderView
    {
        public string OrdersId { get; set; }
        public string BuyerId { get; set; }
        public string ReceivedId { get; set; }
        public string CommodityId { get; set; }
        public string ShopId { get; set; }
        public string CommodityName { get; set; }    // 商品
        public string Name { get; set; }             // 卖家后台：买家昵称   买家:店铺名称
        public DateTime? OrdersDate { get; set; }
        public int? Status { get; set; }             // 订单的状态         
        public decimal? Price { get; set; }          // 商品价格}
        public string Url { get; set; }              // 卖家后台：买家头像  买家：商品图像
    }
}
