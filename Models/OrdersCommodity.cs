﻿using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class OrdersCommodity
    {
        public string OrdersId { get; set; }
        public string CommodityId { get; set; }
        public int? Status { get; set; }           // 订单里物品的状态

        public virtual Commodity Commodity { get; set; }
        public virtual Order Orders { get; set; }
    }
}
