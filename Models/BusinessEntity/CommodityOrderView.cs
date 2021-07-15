using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class CommodityOrderView
    {
        public string CommodityId { get; set; }
        public string Url { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
    }
}
