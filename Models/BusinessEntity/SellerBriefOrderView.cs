using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class SellerBriefOrderView
    {
        public DateTime? date { get; set; }
        public string buyerName { get; set; }
        public string address { get; set; }
        public string buyerPhone { get; set; }
        public string orderID { get; set; }
        public string condition { get; set; }
        public string tag { get; set; }
        public bool show { get; set; }
        
    }
}
