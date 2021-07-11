using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internetmall.Interfaces
{
    interface ISearchService
    {
        public List<Shop> SearchShop(string shopName);

        public Task<List<Commodity>> SearchCommodity(string commodityName, int key = 0);//commodityName是搜索关键词，key是搜索结果的排序依据(默认按照销量)。0——随机排序，1——价格升序，2——价格降序，3——销量降序
    }
}
