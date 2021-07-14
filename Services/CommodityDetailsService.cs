using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    public class CommodityDetailsService : ICommodityDetailsService
    {
        private readonly ModelContext _context;
        public CommodityDetailsService(ModelContext context)
        {
            _context = context;
        }
        static int GetRandomSeedbyGuid()
        {
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
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
        public List<Good> recommendCommodity(string commodityId)
        {
            List<Good> returnList = new List<Good>();
            if (commodityId == "")
                return null;
            else
            {
                Random random = new Random(GetRandomSeedbyGuid());
                Commodity inCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);
                short? inCategory = inCommodity.Category;
                List<Commodity> newList = _context.Commodities.Where(c => c.Category == inCategory).ToList();
                for (int i = 0; i < 2; i++)
                {
                    int temp = random.Next(0, newList.Count - 1);
                    Good newGood = new Good();
                    newGood.img = newList[temp].Url;
                    newGood.intro = newList[temp].Name;
                    //newGood.shop = newList[temp].Shop.Name;
                    newGood.ID = newList[temp].CommodityId;
                    newGood.price = newList[temp].Price;
                    newGood.description = newList[temp].Description;
                    newGood.Soldnum = newList[temp].Soldnum;
                    returnList.Add(newGood);
                }
                return returnList;
            }
        }
    }
}
