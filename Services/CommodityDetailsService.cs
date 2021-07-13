using Internetmall.Interfaces;
using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Services
{
    public class CommodityDetailsService : ICommodityDetailsService
    {
        private readonly ModelContext _context;
        public CommodityDetailsService(ModelContext context)
        {
            _context = context;
        }
        public CommodityDetailsView DisplayCommodityDetails(string commodityId)
        {
            if (commodityId == "")
                return null;
            else
            {
                Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);
                CommodityDetailsView newCommodityDetails = new CommodityDetailsView();
                newCommodityDetails.commodityId = commodityId;
                newCommodityDetails.name = newCommodity.Name;
                newCommodityDetails.intro = newCommodity.Description;
                newCommodityDetails.price = newCommodity.Price;
                newCommodityDetails.imgUrl = newCommodity.Url;
                newCommodityDetails.storage = newCommodity.Storage;
                newCommodityDetails.shopId = newCommodity.ShopId;
                newCommodityDetails.category = newCommodity.Category;
                newCommodityDetails.errorCode = 0;
                return newCommodityDetails;
            }
        }
    }
}
