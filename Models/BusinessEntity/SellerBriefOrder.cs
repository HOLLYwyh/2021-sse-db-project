using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class SellerBriefOrder
    {
        public string buyerNickname { get; set; }
        public string url { get; set; }
        public decimal? price { get; set; }
        public string status { get; set; }
        public DateTime? date { get; set; }
    }
}
