﻿using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class AddShoppingCart
    {
        public string BuyerId { get; set; }
        public string CommodityId { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Commodity Commodity { get; set; }
    }
}
