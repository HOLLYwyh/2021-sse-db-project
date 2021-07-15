using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    public class BuyerCouponView
    {
        public string CouponId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal? Threshold { get; set; }
        public decimal? Discount1 { get; set; }
        public string Category { get; set; }
        public string ShopId { get; set; }
        public string ActivityId { get; set; }
    }
}
