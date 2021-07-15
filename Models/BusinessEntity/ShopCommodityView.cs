using Internetmall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    public class ShopCommodityView
    {
        public ShopView Shop { get; set; }                         // 店铺信息
        public List<CommodityView> CommodityViews { get; set; } // 店铺所有商品
    }
}
