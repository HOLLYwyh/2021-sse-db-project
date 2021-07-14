using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class ReceiveInformation
    {
        public ReceiveInformation()
        {
            Orders = new HashSet<Order>();
        }

        public string ReceivedId { get; set; }
        public string Phone { get; set; }
        public string ReceiverName { get; set; }
        public string BuyerId { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string DetailAddr { get; set; }
        public string Tag { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
