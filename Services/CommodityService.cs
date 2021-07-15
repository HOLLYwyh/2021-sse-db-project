using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace InternetMall.Services
{
    public class CommodityService : ICommodityService
    {
        private readonly ModelContext _context;

        public CommodityService(ModelContext context)
        {
            _context = context;
        }
 
        public bool Create(decimal price, string category, string description, int storage, string name, string shopId, string url)
        {
            Commodity commodity = new Commodity();
            //result是查找结果:查看店内是否已经有了重名的商品
            Commodity result =  _context.Commodities
                .Where(c => c.ShopId == shopId && c.Name == name).FirstOrDefault();
            if (result == null)                   //不存在，则插入该商品
            {
                Counter counts = _context.Counters.FirstOrDefault(c=>c.ID=="0");
                //commodity.CommodityId = (counts.Commoditycount + 1).ToString();//生成商品ID
                //修改表的计数
                commodity.CommodityId = "100";
                counts.Commoditycount = counts.Commoditycount + 1;
                _context.Counters.Update(counts);

                commodity.Description = description;
                commodity.Price = price;
                commodity.Storage = storage;
                commodity.Name = name;
                commodity.ShopId = shopId;
                commodity.Url = url;
                switch (category)
                {
                    case "未定义": commodity.Category = 0; break;                //未定义 
                    case "服装": commodity.Category = 1; break;                  //服装 
                    case "电子产品": commodity.Category = 2; break;           //电子产品
                    case "书籍": commodity.Category = 3; break;                 //书籍
                    case "宠物": commodity.Category = 4; break;                  //宠物
                    case "运动": commodity.Category = 5; break;                //运动 
                    case "食品": commodity.Category = 6; break;                  //食品 
                    case "家居": commodity.Category = 7; break;                  //家居
                    case "美妆": commodity.Category = 8; break;                //美妆 
                    case "洗护": commodity.Category = 9; break;              //洗护
                }
                
                _context.Commodities.Add(commodity);
                 _context.SaveChanges();//保存更新（异步保存，避免等待）
                return true;
            }
            else return false;
        }

        public bool Delete(string shopId, string commodityId)
        {
            //查找是否存在需要删除的元组
            var result =  _context.Commodities
                .Where(c => c.CommodityId == commodityId && c.ShopId == shopId).ToList();//如果运行失败，可以考虑将条件筛选分步执行,也可以考虑用find（）
            if (result != null)
            {
                foreach (Commodity commodity in result) //挨个检查
                {
                    _context.Commodities.Remove(commodity);
                    _context.SaveChanges();
                }
                return true;
            }

            return false; //没有需要删除的元组
        }

        public  string ShowCommodities(string shopId, string searchCondition)
        {
            List<Commodity> commoditiesList;    //用来存放查找结果

            if (searchCondition == "ON_SALE")    //还有库存的商品
            {
                commoditiesList =  _context.Commodities
                    .Where(c => c.ShopId == shopId && c.Storage > 0).ToList();
            }
            else if (searchCondition == "SOLD_OUT") //没有库存的商品
            {
                commoditiesList =  _context.Commodities
                    .Where(c => c.ShopId == shopId && c.Storage == 0).ToList();
            }
            else                                 //所有的商品
            {
                commoditiesList =  _context.Commodities
                    .Where(c => c.ShopId == shopId).ToList();
            }

            return JsonConvert.SerializeObject(commoditiesList);
        }
    }
}
