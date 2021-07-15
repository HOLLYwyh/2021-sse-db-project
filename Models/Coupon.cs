using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Coupon
    {
        public Coupon()
        {
            BuyerCoupons = new HashSet<BuyerCoupon>();
            CouponShops = new HashSet<CouponShop>();
        }

        public string CouponId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Threshold { get; set; }
        public decimal? Discount1 { get; set; }
        public decimal? Discount2 { get; set; }
        public short? Category { get; set; }
        public string ShopId { get; set; }
        public string ActivityId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<BuyerCoupon> BuyerCoupons { get; set; }
        public virtual ICollection<CouponShop> CouponShops { get; set; }
    }
}
