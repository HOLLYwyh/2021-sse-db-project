using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Chatuser
    {
        public string Buyerid { get; set; }
        public string Sellerid { get; set; }
        public string Chatroomid { get; set; }

        public virtual Buyer Buyer { get; set; }
        public virtual Chatroom Chatroom { get; set; }
        public virtual Seller Seller { get; set; }
    }
}
