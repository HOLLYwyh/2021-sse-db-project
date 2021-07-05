using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    /// <summary>
    /// 查看关注显示信息类
    /// </summary>
    public class FollowShopView
    {
        public string ShopId { get; set; }
        public string BuyerId { get; set; }
        public DateTime DateCreated { get; set; }     // 关注时间
        public string ShopName { get; set; }          // 店铺名称
        //public int Score { get; set; }                // 店铺评分
  
    }
}
