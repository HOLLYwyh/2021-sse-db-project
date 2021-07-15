using InternetMall.DBContext;
using InternetMall.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.ControllerModels
{

    public class ReleaseActivity           // 发布活动
    {
        public string name { get; set; }
        public DateTime date1 { get; set; }
        public DateTime date2 { get; set; }
        public string type { get; set; }
        public string desc { get; set; }
        public decimal? constrict { get; set; }
        public decimal? minus { get; set; }
    }

    public class LookActivity            // 查看活动
    {
        public string ID { get; set; }

    }


    public class DeleteBuyer            // 删除买家
    {
        public string ID { get; set; }
    }

    public class DeleteSeller           // 删除卖家
    {
        public string ID { get; set; }
    }

    public class DeleteCommodity       // 下架商品
    {
        public string ID { get; set; }
    }


    public class DeleteShop            // 封禁店铺
    {
        public string ID { get; set; }

     }
}
