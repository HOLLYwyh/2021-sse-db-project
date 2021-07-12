using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Shop
    {
        public Shop()
        {
            Commodities = new HashSet<Commodity>();
            CouponShops = new HashSet<CouponShop>();
            Coupons = new HashSet<Coupon>();
            FollowShops = new HashSet<FollowShop>();
            Orders = new HashSet<Order>();
        }

        public string ShopId { get; set; }
        public string SellerId { get; set; }
        public string Name { get; set; }
        public short? CreditScore { get; set; }
        public short? Category { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public virtual Seller Seller { get; set; }
        public virtual ICollection<Commodity> Commodities { get; set; }
        public virtual ICollection<CouponShop> CouponShops { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<FollowShop> FollowShops { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
