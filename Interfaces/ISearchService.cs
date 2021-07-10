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

        public List<Commodity> SearchCommodity(string commodityName);
        public List<Shop> SortedShop(int key);  //按照指定的key，返回排序后的商店列表
    }
}
