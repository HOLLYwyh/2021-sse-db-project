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

    }
}
