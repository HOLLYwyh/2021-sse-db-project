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
using Internetmall.Models.BusinessEntity;

namespace Internetmall.Services
{
    public class SearchService : ISearchService
    {
        private ModelContext _context;
        public SearchService(ModelContext context)
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
        //搜索商品
        public List<Good> SearchCommodity(string commodityName , int searchType = 0)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> newCommodityList = new List<Commodity>();
            List<Good> returnList = new List<Good>(); 
            int count = 0;
            if(searchType == 0)
            {
                List<Commodity> commodityList = _context.Commodities.Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    if (item.Name.Contains(commodityName))
                    {
                        count++;
                        newCommodityList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 12)
                {
                    foreach (Commodity newCommodity in newCommodityList)
                    {
                        count++;
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                        if (count == 12)
                            break;
                    }
                }
                else
                {
                    for(int i=count;i<=12; i++)
                    {
                        string randCommodityID = random.Next(0, 120).ToString();
                        Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c =>c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                    }
                }
            }
            else if (searchType == 1)
            {
                List<Commodity> commodityList = _context.Commodities.OrderByDescending(c => c.Price).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    if (item.Name.Contains(commodityName))
                    {
                        count++;
                        newCommodityList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 12)
                {
                    foreach (Commodity newCommodity in newCommodityList)
                    {
                        count++;
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                        if (count == 12)
                            break;
                    }
                }
                else
                {
                    for (int i = count; i <= 12; i++)
                    {
                        string randCommodityID = random.Next(0, 120).ToString();
                        Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                    }
                }
            }
            else if (searchType == 2)
            {
                List<Commodity> commodityList = _context.Commodities.OrderBy(c => c.Price).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    if (item.Name.Contains(commodityName))
                    {
                        count++;
                        newCommodityList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 12)
                {
                    foreach (Commodity newCommodity in newCommodityList)
                    {
                        count++;
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                        if (count == 12)
                            break;
                    }
                }
                else
                {
                    for (int i = count; i <= 12; i++)
                    {
                        string randCommodityID = random.Next(0, 120).ToString();
                        Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                    }
                }
            }
            else if (searchType == 3)
            {
                List<Commodity> commodityList = _context.Commodities.OrderByDescending(c => c.Soldnum).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    if (item.Name.Contains(commodityName))
                    {
                        count++;
                        newCommodityList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 12)
                {
                    foreach (Commodity newCommodity in newCommodityList)
                    {
                        count++;
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                        if (count == 12)
                            break;
                    }
                }
                else
                {
                    for (int i = count; i <= 12; i++)
                    {
                        string randCommodityID = random.Next(0, 120).ToString();
                        Commodity newCommodity = _context.Commodities.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        returnList.Add(newGood);
                    }
                }
            }
            return returnList;
        }
        //搜索店铺
        public List<ShopView> SearchShop(string shopName, int searchType)//根据搜索字，寻找所有包含关键字的店铺，返回店铺列表
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Shop> newShopList = new List<Shop>();
            List<ShopView> returnList = new List<ShopView>();
            int count = 0;
            if (searchType == 0)
            {
                List<Shop> shopList = _context.Shops.ToList();
                foreach (var item in shopList)
                {
                    if (item.Name.Contains(shopName))
                    {
                        count++;
                        newShopList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 4)
                {
                    foreach (Shop newShop in newShopList)
                    {
                        count++;
                        ShopView newShopView = new ShopView();
                        newShopView.shopID = newShop.ShopId;
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        returnList.Add(newShopView);
                        if (count == 4)
                            break;
                    }
                }
                else
                {
                    for (int i = count; i <= 4; i++)
                    {
                        string randShopID = random.Next(1, 1).ToString();
                        Shop newShop = _context.Shops.FirstOrDefault(c => c.ShopId == randShopID);
                        ShopView newShopView = new ShopView();
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        returnList.Add(newShopView);
                    }
                }
            }
            else if (searchType == 1)
            {
                List<Shop> shopList = _context.Shops.OrderByDescending(s => s.CreditScore).ToList();
                foreach (var item in shopList)
                {
                    if (item.Name.Contains(shopName))
                    {
                        count++;
                        newShopList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                if (count >= 4)
                {
                    foreach (Shop newShop in newShopList)
                    {
                        count++;
                        ShopView newShopView = new ShopView();
                        newShopView.shopID = newShop.ShopId;
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        returnList.Add(newShopView);
                        if (count == 4)
                            break;
                    }
                }
                else
                {
                    for (int i = count; i <= 4; i++)
                    {
                        string randShopID = random.Next(1, 1).ToString();
                        Shop newShop = _context.Shops.FirstOrDefault(c => c.ShopId == randShopID);
                        ShopView newShopView = new ShopView();
                        newShopView.shopID = newShop.ShopId;
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        returnList.Add(newShopView);
                    }
                }
            }
            return returnList;
        }
        public Commodity GetCommodityById(string id)
        {
            return _context.Commodities.FirstOrDefault(c => c.CommodityId == id);
        }
        //    public string GetCommodityById(string id)
        //    {
        //        return JsonConvert.SerializeObject(_context.Commodities.FirstOrDefault(c => c.CommodityId == id));
        //    }
    }
}
