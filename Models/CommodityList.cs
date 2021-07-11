using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Models
{
    public class CommodityList
    {
        public List<Commodity> commoditiesList;
        
        public CommodityList(List<Commodity> commodityList)
        {
            commoditiesList = commodityList;
        }
    }
}
