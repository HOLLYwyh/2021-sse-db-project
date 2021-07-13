using Internetmall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Interfaces
{
    interface ICommodityDetailsService
    {
        public CommodityDetailsView DisplayCommodityDetails(string CommodityId);
    }
}
