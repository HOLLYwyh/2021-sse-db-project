using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Seller
    {
        public Seller()
        {
            Chatusers = new HashSet<Chatuser>();
            Shops = new HashSet<Shop>();
        }

        public string SellerId { get; set; }
        public string Passwd { get; set; }
        public string Nickname { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Chatuser> Chatusers { get; set; }
        public virtual ICollection<Shop> Shops { get; set; }
    }
}
