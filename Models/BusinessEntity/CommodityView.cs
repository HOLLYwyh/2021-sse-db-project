using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models.BusinessEntity
{
    public class CommodityView
    {
        public string CommodityId { get; set; }    
        public string ShopId { get; set; }
        public decimal? Price { get; set; }
        public short? Category { get; set; }
        public int? Storage { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int Soldnum { get; set; }
        public string Description { get; set; }
    }
}
