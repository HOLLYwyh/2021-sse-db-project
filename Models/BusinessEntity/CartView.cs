using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 查看购物车信息类
    /// </summary>
    public class CartView
    {
        public int errorCode { get; set; }//错误状态
        public string BuyerId { get; set; }//买家Id
        public string commodityId { get; set; }//商品Id 
        public int amount{ get; set; }// 商品数量
        public DateTime? DateCreated { get; set; }// 创建时间
        public string CommodityName { get; set; }//商品名称
        public string shopId { get; set; }//店铺Id
        public string ShopName { get; set; }// 店铺名称
        public decimal? Price{ get; set; }// 商品总价格
        public string imgUrl { get; set; }//商品图像
    }
}
