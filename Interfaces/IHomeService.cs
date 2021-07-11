using Internetmall.Models;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Interfaces
{
    interface IHomeService
    {
        public Task<CommodityList> RecommendingCommodities(bool inFo, string buyerId = null, int commodityCategory = -1);
    }
}
