using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models.BusinessEntity
{
    public class OrderInformationView
    {
        public string orderId { get; set; }
        public DateTime? date { get; set; }
        public string receiverName { get; set; }
        public string receiverPhone { get; set; }
        public string detailAddr { get; set; }
        public string status { get; set; }
        public List<CommodityOrderView> commodityList { get; set; }
    }
}
