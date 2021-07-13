using Internetmall.Interfaces;
using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Services
{
    public class RankService : IRankService
    {
        private ModelContext _context;
        public RankService(ModelContext context)
        {
            _context = context;
        }
        public List<Good> rank(int rankType)
        {
            List<Good> returnList = new List<Good>();
            List<Commodity> commodityList = _context.Commodities.OrderByDescending(c => c.Soldnum).Include(c => c.Shop).ToList();
            int count = 0;
            foreach (Commodity newCommodity in commodityList)
            {
                count++;
                Good newGood = new Good();
                newGood.img = newCommodity.Url;
                newGood.intro = newCommodity.Name;
                newGood.ID = newCommodity.CommodityId;
                newGood.description = newCommodity.Description;
                newGood.price = newCommodity.Price;
                returnList.Add(newGood);
                if (count == 15)
                    break;
            }
            return returnList;
        }
    }
}
