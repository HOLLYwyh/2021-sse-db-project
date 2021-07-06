﻿using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Commodity
    {
        public Commodity()
        {
            AddShoppingCarts = new HashSet<AddShoppingCart>();
            Coupons = new HashSet<Coupon>();
            FavoriteProducts = new HashSet<FavoriteProduct>();
            OrdersCommodities = new HashSet<OrdersCommodity>();
        }

        public string CommodityId { get; set; }
        public decimal? Price { get; set; }
        public short? Category { get; set; }
        public int? Storage { get; set; }
        public string Name { get; set; }
        public string ShopId { get; set; }
        public string Url { get; set; }

        public virtual Shop Shop { get; set; }
        public virtual ICollection<AddShoppingCart> AddShoppingCarts { get; set; }
        public virtual ICollection<Coupon> Coupons { get; set; }
        public virtual ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual ICollection<OrdersCommodity> OrdersCommodities { get; set; }
    }
}
