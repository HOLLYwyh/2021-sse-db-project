using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetMall.Services
{
    public class CommodityService : ICommodityService
    {
        private readonly ModelContext _context;

        public CommodityService(ModelContext context)
        {
            _context = context;
        }

        public async Task<bool> Create(decimal price, string category, string description, int storage, string name, string shopId,string url)
        {
            Commodity commodity = new Commodity();
            //result是查找结果:查看店内是否已经有了重名的商品
            var result = await _context.Commodities
                .Where(c => c.ShopId == shopId && c.Name == name).ToListAsync();
            if (result == null)                   //不存在，则插入该商品
            {
                commodity.CommodityId = "000001"; //暂时随便生成一个商品ID，后续商讨设计生成规则
                commodity.Price = price;
                switch (category)
                {
                    case "Unkown": commodity.Category = 0;break;                //未定义 
                    case "Clothing": commodity.Category = 1;break;              //服装 
                    case "Electronics": commodity.Category = 2;break;           //电子产品
                    case "Books": commodity.Category = 3;break;                 //书籍
                    case "Pets": commodity.Category = 4;break;                  //宠物
                    case "Sports": commodity.Category = 5;break;                //运动 
                    case "Food": commodity.Category = 6;break;                  //食品 
                    case "Home": commodity.Category = 7;break;                  //家居
                    case "Beauty": commodity.Category = 8;break;                //美妆 
                    case "Bodycare": commodity.Category = 9;break;              //洗护 
                    case "ClothingMen": commodity.Category = 10;break;          //男装
                    case "ClothingWomen": commodity.Category = 11;break;        //女装 
                    case "ClothingBoys": commodity.Category = 12;break;         //童装男 
                    case "ClothingGrirls": commodity.Category = 13;break;       //童装女 
                    case "ClothingBaby": commodity.Category = 14;break;         //婴儿服装 
                    case "ClothingOther": commodity.Category = 19;break;        //其它服装
                    case "ElectronicsTV": commodity.Category = 20;break;        //电视 
                    case "ElectronicsCamera": commodity.Category = 21;break;    //相机
                    case "ElectronicsCellphone": commodity.Category = 22;break; //手机 
                    case "ElectronicsHeadphone": commodity.Category = 23;break; //耳机 
                    case "ElectronicsWatch": commodity.Category = 24;break;     //手表
                    case "ElectronicsComputer": commodity.Category = 25;break;  //台式电脑 
                    case "ElectronicsLaptop": commodity.Category = 26;break;    //笔记本电脑
                    case "ElectronicsTablet": commodity.Category = 27;break;    //平板 
                    case "ElectronicsOther": commodity.Category = 29;break;     //其它电子产品 
                    case "BooksChildren": commodity.Category = 30;break;        //儿童书籍 
                    case "BooksTextbook": commodity.Category = 31;break;        //课本
                    case "BookAudio": commodity.Category = 32;break;            //音频书籍 
                    case "BookKindle": commodity.Category = 33;break;           //Kindle专区 
                    case "BookMagazine": commodity.Category = 34;break;         //杂志
                    case "BookNovel": commodity.Category = 35;break;            //小说
                    case "BookOther": commodity.Category = 39;break;            //其它书籍
                    case "PetsDog": commodity.Category = 40;break;              //狗 
                    case "PetsCat": commodity.Category = 41;break;              //猫
                    case "PetsBird": commodity.Category = 42;break;             //鸟 
                    case "PetsPlant": commodity.Category = 43;break;            //植物 
                    case "PetsFosh": commodity.Category = 44;break;             //鱼 
                    case "PetsOther": commodity.Category = 49;break;            //其它宠物
                    case "SportsAthletic": commodity.Category = 50;break;       //田径
                    case "SportsFitness": commodity.Category = 51;break;        //健身 
                    case "SportsFishing": commodity.Category = 52;break;        //钓鱼 
                    case "SportsGolf": commodity.Category = 53;break;           //高尔夫 
                    case "SportsSoccer": commodity.Category = 54;break;         //足球
                    case "SportsBasketball": commodity.Category = 55;break;     //篮球
                    case "SportsVolleyball": commodity.Category = 56;break;     //排球
                    case "SportsTableTennis": commodity.Category = 57;break;    //乒乓球
                    case "SportsTennis": commodity.Category = 58;break;         //网球 
                    case "SportsOthers": commodity.Category = 59;break;         //其它运动 
                    case "FoodSnack": commodity.Category = 60;break;            //零食小吃
                    case "FoodBreakfast": commodity.Category = 61;break;        //早餐 
                    case "FoodBeverage": commodity.Category = 62;break;         //饮品
                    case "FoodStaple": commodity.Category = 63;break;           //食品原材料
                    case "FoodBabyfood": commodity.Category = 64;break;         //婴儿食品 
                    case "FoodCandyChocolate": commodity.Category = 65;break;   //糖果巧克力
                    case "FoodOther": commodity.Category = 69;break;            //其它食物 
                    case "HomeBathroom": commodity.Category = 70;break;         //卫生间 
                    case "HomeBedroom": commodity.Category = 71;break;          //卧室 
                    case "HomeKitchen": commodity.Category = 72;break;          //厨房
                    case "HomeLivingroom": commodity.Category = 73;break;       //客厅 
                    case "HomeDiningroom": commodity.Category = 74;break;       //餐厅 
                    case "HomeEntryway": commodity.Category = 75;break;         //入口通道 
                    case "HomeLaunday": commodity.Category = 76;break;          //洗衣房
                    case "HomeOffice": commodity.Category = 77;break;           //书房办公室
                    case "HomeOutdoor": commodity.Category = 78;break;          //屋外 
                    case "HomeOther": commodity.Category = 79;break;            //其它家居 
                    case "BeautyMen": commodity.Category = 80;break;            //男士专区 
                    case "BeautyLipstick": commodity.Category = 81;break;       //口红 
                    case "BeautyBase": commodity.Category = 82;break;           //粉底 
                    case "BeautyBlusher": commodity.Category = 83;break;        //腮红
                    case "BeautyFragrance": commodity.Category = 84;break;      //香水香薰 
                    case "BeautyEyeshadow": commodity.Category = 85;break;      //眼影 
                    case "BeautyEyeliner": commodity.Category = 86;break;       //眼线 
                    case "BeautyBlock": commodity.Category = 87;break;          //隔离 
                    case "BeautyTakeoff": commodity.Category = 88;break;        //卸妆 
                    case "BeautyOther": commodity.Category = 89;break;          //其它美容用品 
                    case "BodycareFemale": commodity.Category = 90;break;       //女性护理 
                    case "BodycareSoap": commodity.Category = 91;break;         //肥皂香皂洗手液 
                    case "BodycareShampoo": commodity.Category = 92;break;      //洗发水 
                    case "BodycareShowergel": commodity.Category = 93;break;    //沐浴露 
                    case "BodycareOral": commodity.Category = 94;break;         //口腔清洁 
                    case "BodycareHaircare": commodity.Category = 95;break;     //护发素 
                    case "BodycareSkincare": commodity.Category = 96;break;     //护肤品 
                    case "BodycareOther": commodity.Category = 99;break;        //其它洗护用品 
                }
                commodity.Storage = storage;
                commodity.Name = name;
                commodity.ShopId = shopId;
                commodity.Url = url;

                _context.Add(commodity);
                await _context.SaveChangesAsync();//保存更新（异步保存，避免等待）
                return true;
            }
            else return false;                    
        }



        public async Task<bool> Delete(string shopId, string commodityId)
        {
            //查找是否存在需要删除的元组
            var result = await _context.Commodities
                .Where(c => c.CommodityId == commodityId && c.ShopId == shopId).ToListAsync();//如果运行失败，可以考虑将条件筛选分步执行,也可以考虑用find（）
            if (result != null)
            {
                foreach (Commodity commodity in result) //挨个检查
                {
                    _context.Remove(commodity);
                    await _context.SaveChangesAsync();
                }
                return true;
            }

            return false; //没有需要删除的元组
        }
        public async Task<List<Commodity>> ShowCommodities(string shopId, string searchCondition)
        {
            List<Commodity> commoditiesList;    //用来存放查找结果

            if (searchCondition== "ON_SALE")    //还有库存的商品
            {
                commoditiesList = await _context.Commodities
                    .Where(c => c.ShopId == shopId && c.Storage > 0).ToListAsync();
            }
            else if(searchCondition== "SOLD_OUT") //没有库存的商品
            {
                commoditiesList = await _context.Commodities
                    .Where(c => c.ShopId == shopId && c.Storage == 0).ToListAsync();
            }
            else                                 //所有的商品
            {
                commoditiesList = await _context.Commodities
                    .Where(c => c.ShopId == shopId).ToListAsync();
            }
           
            return commoditiesList;
        }
    }
}
