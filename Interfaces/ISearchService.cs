using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internetmall.Models.BusinessEntity;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internetmall.Interfaces
{
    interface ISearchService
    {
        //������Ʒ
        public List<Good> SearchCommodity(string commodityName, int searchType = 0);
        //��������
        public List<ShopView> SearchShop(string shopName, int searchType = 0);
    }
}
