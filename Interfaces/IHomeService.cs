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
        //首页展示商品推荐
        public Task<ListToJsonView<Commodity>> RecommendingCommodities(bool inFo, string buyerId = null, int commodityCategory = -1);
    }
}
