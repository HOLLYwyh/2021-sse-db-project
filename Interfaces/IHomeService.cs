using Internetmall.Models;
using Internetmall.Models.BusinessEntity;
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
        public Task<List<Good>> RecommendingCommodities(bool inFo, string buyerId = null, int commodityCategory = -1);
    }
}
