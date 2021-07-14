using Internetmall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    interface ICommodityDetailsService
    {
        public CommodityDetailsView DisplayCommodityDetails(string CommodityId);
        public List<Good> recommendCommodity(string CommodityId);
    }
}
