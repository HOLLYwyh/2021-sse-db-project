using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Order
    {
        public Order()
        {
            OrdersCommodities = new HashSet<OrdersCommodity>();
        }

        public string OrdersId { get; set; }
        public string BuyerId { get; set; }
        public DateTime? OrdersDate { get; set; }
        public int? Status { get; set; }
        public string ReceivedId { get; set; }
        public string ShopId { get; set; }
        public decimal Orderamount { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ReceiveInformation Received { get; set; }
        public virtual Shop Shop { get; set; }
        public virtual ICollection<OrdersCommodity> OrdersCommodities { get; set; }
    }
}
