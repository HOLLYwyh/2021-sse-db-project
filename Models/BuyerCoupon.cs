using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class BuyerCoupon
    {
        public string BuyerId { get; set; }
        public string CouponId { get; set; }
        public short? Amount { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Coupon Coupon { get; set; }
    }
}
