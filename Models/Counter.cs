using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Counter
    {
        public string ID { get; set; }
        public int Buyercount { get; set; }
        public int Sellercount { get; set; }
        public int Administratorcount { get; set; }
        public int Commoditycount { get; set; }
        public int Shopcount { get; set; }
        public int Ordercount { get; set; }
        public int Couponcount { get; set; }
        public int Activitycount { get; set; }
        public int Receivedcount { get; set; }
        public int Messagecount { get; set; }
        public int Chatroomcount { get; set; }
    }
}
