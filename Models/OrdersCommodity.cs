using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class OrdersCommodity
    {
        public string OrdersId { get; set; }
        public string CommodityId { get; set; }

        public virtual Commodity Commodity { get; set; }
        public virtual Order Orders { get; set; }
    }
}
