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
using System.Text.RegularExpressions;

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
        public List<Good> SearchCommodity(string commodityName, int searchType = 0)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> newCommodityList = new List<Commodity>();
            List<Good> returnList = new List<Good>();
            List<Good> tempList = new List<Good>();
            int count = 0;
            int number = 0;
            if (commodityName == "")
            {
                for (int i = 0; i < 12; i++)
                {
                    string randCommodityID = random.Next(2, 120).ToString();
                    Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == randCommodityID);//Include(c => c.Shop).FirstOrDefault(c =>c.CommodityId == randCommodityID);
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    //newGood.shop = newCommodity.Shop.Name;
                    newGood.ID = newCommodity.CommodityId;
                    newGood.price = newCommodity.Price;
                    newGood.Soldnum = newCommodity.Soldnum;
                    returnList.Add(newGood);
                }
            }
            else if (searchType == 0)
            {
                List<Commodity> commodityList = _context.Commodities.Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    Regex r = new Regex("[" + commodityName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newCommodityList.Add(item);//把搜索到包含关键字的商品加入列表
                    }
                }
                foreach (Commodity newCommodity in newCommodityList)
                {
                    number++;
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    //newGood.shop = newCommodity.Shop.Name;
                    newGood.ID = newCommodity.CommodityId;
                    newGood.price = newCommodity.Price;
                    newGood.Soldnum = newCommodity.Soldnum;
                    returnList.Add(newGood);
                    if (number == 12)
                        break;
                }
                if (count < 12)
                {
                    for (int i = count; i < 12; i++)
                    {
                        string randCommodityID = random.Next(2, 120).ToString();
                        Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == randCommodityID);//Include(c => c.Shop).FirstOrDefault(c =>c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        //newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        newGood.price = newCommodity.Price;
                        newGood.Soldnum = newCommodity.Soldnum;
                        returnList.Add(newGood);
                    }
                }
            }
            else if (searchType == 1)
            {
                List<Commodity> commodityList = _context.Commodities.OrderByDescending(c => c.Price).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    Regex r = new Regex("[" + commodityName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newCommodityList.Add(item);
                    }
                }
                foreach (Commodity newCommodity in newCommodityList)
                {
                    number++;
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    //newGood.shop = newCommodity.Shop.Name;
                    newGood.ID = newCommodity.CommodityId;
                    newGood.price = newCommodity.Price;
                    newGood.Soldnum = newCommodity.Soldnum;
                    tempList.Add(newGood);
                    if (number == 12)
                        break;
                }
                if (count < 12)
                {

                    for (int i = count; i < 12; i++)
                    {
                        string randCommodityID = random.Next(2, 120).ToString();
                        Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == randCommodityID);//.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        //newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        newGood.price = newCommodity.Price;
                        newGood.Soldnum = newCommodity.Soldnum;
                        tempList.Add(newGood);
                    }
                    int[] judge = new int[tempList.Count];
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        int maxIndex = 0;
                        while (judge[maxIndex] == 1) maxIndex++;
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            if (tempList[j].price > tempList[maxIndex].price && judge[j] != 1)
                                maxIndex = j;
                        }
                        Good tempGood = new Good();
                        tempGood = tempList[maxIndex];
                        returnList.Add(tempGood);
                        judge[maxIndex] = 1;
                    }
                }
            }
            else if (searchType == 2)
            {
                List<Commodity> commodityList = _context.Commodities.OrderBy(c => c.Price).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    Regex r = new Regex("[" + commodityName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newCommodityList.Add(item);
                    }
                }
                foreach (Commodity newCommodity in newCommodityList)
                {
                    number++;
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    //newGood.shop = newCommodity.Shop.Name;
                    newGood.ID = newCommodity.CommodityId;
                    newGood.price = newCommodity.Price;
                    newGood.Soldnum = newCommodity.Soldnum;
                    tempList.Add(newGood);
                    if (number == 12)
                        break;
                }
                if (count < 12)
                {
                    for (int i = count; i < 12; i++)
                    {
                        string randCommodityID = random.Next(2, 120).ToString();
                        Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == randCommodityID);//.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        //newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        newGood.price = newCommodity.Price;
                        newGood.Soldnum = newCommodity.Soldnum;
                        tempList.Add(newGood);
                    }
                    int[] judge = new int[tempList.Count];
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        int maxIndex = 0;
                        while (judge[maxIndex] == 1) maxIndex++;
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            if (tempList[j].price < tempList[maxIndex].price && judge[j] != 1)
                                maxIndex = j;
                        }
                        Good tempGood = new Good();
                        tempGood = tempList[maxIndex];
                        returnList.Add(tempGood);
                        judge[maxIndex] = 1;
                    }
                }
            }
            else if (searchType == 3)
            {
                List<Commodity> commodityList = _context.Commodities.OrderByDescending(c => c.Soldnum).Include(c => c.Shop).ToList();
                foreach (var item in commodityList)
                {
                    Regex r = new Regex("[" + commodityName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newCommodityList.Add(item);
                    }
                }
                foreach (Commodity newCommodity in newCommodityList)
                {
                    number++;
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    //newGood.shop = newCommodity.Shop.Name;
                    newGood.ID = newCommodity.CommodityId;
                    newGood.price = newCommodity.Price;
                    newGood.Soldnum = newCommodity.Soldnum;
                    tempList.Add(newGood);
                    if (number == 12)
                        break;
                }
                if (count < 12)
                {
                    for (int i = count; i < 12; i++)
                    {
                        string randCommodityID = random.Next(2, 120).ToString();
                        Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == randCommodityID);//.Include(c => c.Shop).FirstOrDefault(c => c.CommodityId == randCommodityID);
                        Good newGood = new Good();
                        newGood.img = newCommodity.Url;
                        newGood.intro = newCommodity.Name;
                        //newGood.shop = newCommodity.Shop.Name;
                        newGood.ID = newCommodity.CommodityId;
                        newGood.price = newCommodity.Price;
                        newGood.Soldnum = newCommodity.Soldnum;
                        tempList.Add(newGood);
                    }
                    int[] judge = new int[tempList.Count];
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        int maxIndex = 0;
                        while (judge[maxIndex] == 1) maxIndex++;
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            if (tempList[j].Soldnum > tempList[maxIndex].Soldnum && judge[j] != 1)
                                maxIndex = j;
                        }
                        Good tempGood = new Good();
                        tempGood = tempList[maxIndex];
                        returnList.Add(tempGood);
                        judge[maxIndex] = 1;
                    }
                }
            }
            return returnList;
        }
        //搜索店铺
        public List<ShopView> SearchShop(string shopName, int searchType = 0)//根据搜索字，寻找所有包含关键字的店铺，返回店铺列表
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Shop> newShopList = new List<Shop>();
            List<ShopView> returnList = new List<ShopView>();
            List<ShopView> tempList = new List<ShopView>();
            int count = 0;
            int number = 0;
            if (shopName == "")
            {
                for (int i = 0; i < 4; i++)
                {
                    string randShopID = random.Next(1, 1).ToString();
                    Shop newShop = _context.Shops.FirstOrDefault(c => c.ShopId == randShopID);
                    ShopView newShopView = new ShopView();
                    newShopView.shopName = newShop.Name;
                    newShopView.shopDescription = newShop.Description;
                    newShopView.img = newShop.Url;
                    newShopView.creditScore = newShop.CreditScore;
                    newShopView.shopID = newShop.ShopId;
                    returnList.Add(newShopView);
                }
            }
            else if (searchType == 0)
            {
                List<Shop> shopList = _context.Shops.ToList();
                foreach (var item in shopList)
                {
                    Regex r = new Regex("[" + shopName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newShopList.Add(item);
                    }
                }
                foreach (Shop newShop in newShopList)
                {
                    number++;
                    ShopView newShopView = new ShopView();
                    newShopView.shopID = newShop.ShopId;
                    newShopView.shopName = newShop.Name;
                    newShopView.shopDescription = newShop.Description;
                    newShopView.img = newShop.Url;
                    newShopView.creditScore = newShop.CreditScore;
                    returnList.Add(newShopView);
                    if (number == 4)
                        break;
                }
                if (count < 4)
                {
                    for (int i = count; i < 4; i++)
                    {
                        string randShopID = random.Next(1, 1).ToString();
                        Shop newShop = _context.Shops.FirstOrDefault(c => c.ShopId == randShopID);
                        ShopView newShopView = new ShopView();
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        newShopView.shopID = newShop.ShopId;
                        returnList.Add(newShopView);
                    }
                }
            }
            else if (searchType == 1)
            {
                List<Shop> shopList = _context.Shops.OrderByDescending(s => s.CreditScore).ToList();
                foreach (var item in shopList)
                {
                    Regex r = new Regex("[" + shopName + "]");
                    MatchCollection m = r.Matches(item.Name);
                    if (m.Count != 0)
                    {
                        count++;
                        newShopList.Add(item);
                    }
                }
                foreach (Shop newShop in newShopList)
                {
                    number++;
                    ShopView newShopView = new ShopView();
                    newShopView.shopID = newShop.ShopId;
                    newShopView.shopName = newShop.Name;
                    newShopView.shopDescription = newShop.Description;
                    newShopView.img = newShop.Url;
                    newShopView.creditScore = newShop.CreditScore;
                    tempList.Add(newShopView);
                    if (number == 4)
                        break;
                }
                if (count < 4)
                {
                    for (int i = count; i < 4; i++)
                    {
                        string randShopID = random.Next(1, 1).ToString();
                        Shop newShop = _context.Shops.FirstOrDefault(c => c.ShopId == randShopID);
                        ShopView newShopView = new ShopView();
                        newShopView.shopID = newShop.ShopId;
                        newShopView.shopName = newShop.Name;
                        newShopView.shopDescription = newShop.Description;
                        newShopView.img = newShop.Url;
                        newShopView.creditScore = newShop.CreditScore;
                        tempList.Add(newShopView);
                    }
                    int[] judge = new int[tempList.Count];
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        int maxIndex = 0;
                        while (judge[maxIndex] == 1) maxIndex++;
                        for (int j = 0; j < tempList.Count; j++)
                        {
                            if (tempList[j].creditScore > tempList[maxIndex].creditScore && judge[j] != 1)
                                maxIndex = j;
                        }
                        ShopView tempShopView = new ShopView();
                        tempShopView = tempList[maxIndex];
                        returnList.Add(tempShopView);
                        judge[maxIndex] = 1;
                    }
                }
            }
            return returnList;
        }
    }
}
