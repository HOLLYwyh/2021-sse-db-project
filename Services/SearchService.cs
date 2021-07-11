using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetMall.Models;
using InternetMall.DBContext;
using Internetmall.Interfaces;

namespace Internetmall.Services
{
    public class SearchService : ISearchService
    {
        private ModelContext _context;
        public SearchService(ModelContext context)
        {
            _context = context;
        }
        public List<Shop> SearchShop(string shopName)//根据搜索字，寻找所有包含关键字的店铺，返回店铺列表
        {
            List<Shop> ansShop = new List<Shop>();//定义存储找到的店铺列表
            foreach (var item in _context.Shops)
            {
                if(item.Name.Contains(shopName))
                {
                    ansShop.Add(item);//把搜索到包含关键字的店铺加入列表
                }
            }
            return ansShop;
        }
        public async Task<List<Commodity>> SearchCommodity(string commodityName, int key = 0)
        {
            List<Commodity> result = new List<Commodity>();
            if (key == 0)        //随机
            {
                result = await _context.Commodities
                  .Where(c => c.Name.Contains(commodityName)).ToListAsync();
            }
            else if (key == 1)//价格升序
            {
                result = await _context.Commodities
                  .Where(c => c.Name.Contains(commodityName)).OrderBy(c => c.Price).ToListAsync();
            }
            else if (key == 2)//价格降序
            {
                result = await _context.Commodities
                  .Where(c => c.Name.Contains(commodityName)).OrderByDescending(c => c.Price).ToListAsync();
            }
            else              //销量降序
            {
                result = await _context.Commodities
                  .Where(c => c.Name.Contains(commodityName)).OrderByDescending(c => c.Soldnum).ToListAsync();
            }
            return result;
        }
    }


}
