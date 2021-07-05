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
        public string BuyerId { get; set; }

        public string CommodityId { get; set; }    

        public int Quantity { get; set; }             // 商品数量

        public DateTime DateCreated { get; set; }     // 创建时间
        
        public string CommodityName { get; set; }     //商品名称

        public string ShopName { get; set; }          // 店铺名称

        public decimal Price{ get; set; }             // 商品价格

    
    }
}
