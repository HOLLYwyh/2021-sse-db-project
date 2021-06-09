using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class CouponShop
    {
        public string CouponId { get; set; }
        public string ShopId { get; set; }
        public int? Amount { get; set; }

        public virtual Coupon Coupon { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
