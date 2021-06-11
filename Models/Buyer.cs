using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Buyer
    {
        public Buyer()
        {
            AddShoppingCarts = new HashSet<AddShoppingCart>();
            BuyerCoupons = new HashSet<BuyerCoupon>();
            FavoriteProducts = new HashSet<FavoriteProduct>();
            FollowShops = new HashSet<FollowShop>();
            Orders = new HashSet<Order>();
            ReceiveInformations = new HashSet<ReceiveInformation>();
        }

        public string BuyerId { get; set; }
        public string Phone { get; set; }
        public string Passwd { get; set; }
        public string Nickname { get; set; }
        public int? Gender { get; set; }
        public DateTime? DateBirth { get; set; }
        public string IdNumber { get; set; }

        public virtual ICollection<AddShoppingCart> AddShoppingCarts { get; set; }
        public virtual ICollection<BuyerCoupon> BuyerCoupons { get; set; }
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual ICollection<FollowShop> FollowShops { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ReceiveInformation> ReceiveInformations { get; set; }
    }
}
