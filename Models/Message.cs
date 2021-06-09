using System;
using System.Collections.Generic;

#nullable disable

namespace InternetMall.Models
{
    public partial class Message
    {
        public string UserId { get; set; }
        public string ShopId { get; set; }
        public string FilePath { get; set; }
    }
}
