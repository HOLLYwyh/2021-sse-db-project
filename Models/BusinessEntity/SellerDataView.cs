using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 卖家后台逻辑实体定义
    /// </summary>

    // 店铺关注数
    public class ShopFavoriteNum
    {
        // 店铺名称
        public string ShopName { get; set; }

        // 点赞数
        public int FavoriteNum { get; set; }
    }

    // 店铺待发货数量
    public class ShopToBeShipNum
    {
        // 店铺名称
        public string ShopName { get; set; }

        // 店铺代发货数量
        public int ToBeShipNum { get; set; }
    }

    // 退款售后订单总数
    public class ShopCanceledOrderNum
    {
        // 店铺名称
        public string ShopName { get; set; }

        // 退款售后订单数
        public int CanceledOrderNum { get; set; }
    }

    // 盈利分析 —— 按日，周，月，年进行分析
    public class ShopMonthlyProfit
    {
        // 店铺名称
        public string ShopName { get; set; }

        // 时间段
        public DateTime StartTime { get; set; }  // 开始时间

        public DateTime EndTime { get; set; }   // 结束时间

        // 盈利值
        public double monthProfit { get; set; }
    }
}

