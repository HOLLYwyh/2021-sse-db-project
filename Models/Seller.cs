using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Shops = new HashSet<Shop>();
        }

        public string SellerId { get; set; }
        public string Passwd { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Shop> Shops { get; set; }
    }
}
