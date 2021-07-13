using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
    public class BuyerModel
    {
        public string BuyerId { get; set; }
    }
    public class BuyerPhone
    {
        public string BuyerId { get; set; }
        public string OldNo { get; set; }
        public string NewNo { get; set; }
    }
    public class BuyerPasswd
    {
        public string BuyerId { get; set; }
        public string OldPasswd { get; set; }
        public string NewPasswd { get; set; }
    }


}
