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
        public List<Commodity> SearchCommodity(string commodityName)
        {
            List<Commodity> ansCom = new List<Commodity>();
            foreach (var item in _context.Commodities)
            {
                if (item.Name.Contains(commodityName))
                {
                    ansCom.Add(item);//把搜索到包含关键字的商品加入列表
                }
            }
            return ansCom;
        }
    }


}
