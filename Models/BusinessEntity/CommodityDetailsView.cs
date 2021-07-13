using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class CommodityDetailsView
    {
        public int errorCode { get; set; }
        public string commodityId { get; set; }
        public decimal? price { get; set; }
        public int? storage { get; set; }
        public string name { get; set; }
        public string imgUrl { get; set; }
        public string shopId { get; set; }
        public string intro { get; set; }
        public short? category { get; set; }
    }
}
