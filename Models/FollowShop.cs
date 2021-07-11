using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class FollowShop
    {
        public string ShopId { get; set; }
        public string BuyerId { get; set; }
        public DateTime? DateCreated { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Shop Shop { get; set; }
    }
}
