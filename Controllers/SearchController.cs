using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using ThirdParty.Json.LitJson;
using Internetmall.Models.BusinessEntity;
using Internetmall.Services;
using InternetMall.DBContext;
using Newtonsoft.Json;

namespace InternetMall.Controllers
{
    public class SearchController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private SearchService searchService;             //后端service

        public SearchController(ModelContext context)
        {
            _context = context;
            searchService = new SearchService(_context);
        }

        public IActionResult SearchShop()
        {
            if (Request.Cookies["buyerNickName"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Entry/BuyerLogIn");
            }
        }

        public IActionResult SearchCommodity()
        {
            if (Request.Cookies["buyerNickName"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Entry/BuyerLogIn");
            }
        }

        //前后端交互
        [HttpPost]
        public IActionResult SetSearchName([FromBody] SearchName name)   //设置搜索框内容
        {
            Global.GSearchName = name.Context;
            JsonData jsondata = new JsonData();
            jsondata["searchResult"] = name.Context;
            return Json(jsondata.ToJson());
        }
        [HttpPost]
        public IActionResult SetSearchCommodityType([FromBody] SearchCommodityType name)  //设置商品类别
        {
            Global.GCommodityType = name.Type;
            JsonData jsondata = new JsonData();
            jsondata["commodityType"] = name.Type;
            return Json(jsondata.ToJson());
        }
        [HttpPost]
        public IActionResult SetSearchShopType([FromBody]SearchShopType name)   //设置店铺类别
        {
            Global.GShopType = name.Type;
            JsonData jsondata = new JsonData();
            jsondata["shopType"] = name.Type;
            return Json(jsondata.ToJson());
        }
        
        public IActionResult GetSearchName()
        {
            JsonData jsondata = new JsonData();
            jsondata["searchResult"] = Global.GSearchName;
            return Json(jsondata.ToJson());
        }

        [HttpPost]
        public IActionResult GetCommodities([FromBody] SearchName name)   //根据名称和类别来渲染页面
        {
            List<Good> commodityList = new List<Good>();
            string commodityName = name.Context;
            int type = int.Parse(Global.GCommodityType);
            commodityList = searchService.SearchCommodity(commodityName,type);
            foreach (var good in commodityList)
            {
                good.img = "../.." + good.img;
            }
            string str = JsonConvert.SerializeObject(commodityList);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult GetShops([FromBody]SearchName name)  //根据名称和类别来返回店铺
        {
            List<ShopView> shopList = new List<ShopView>();
            string shopName = name.Context;
            int type = int.Parse(Global.GShopType);
            shopList = searchService.SearchShop(shopName,type);
            foreach (var shop in shopList)
            {
                shop.img = "../.." + shop.img;
            }
            string str = JsonConvert.SerializeObject(shopList);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        public IActionResult GetCommodityType()    //返回商品筛选类别
        {
            JsonData jsondata = new JsonData();     
            if (Global.GCommodityType == "0")
            {
                jsondata["type"] = "1";
            }
            else if(Global.GCommodityType == "1")
            {
                jsondata["type"] = "2-1";
            }
            else if (Global.GCommodityType == "2")
            {
                jsondata["type"] = "2-2";
            }
            else
            {
                jsondata["type"] = "3";
            }
            return Json(jsondata.ToJson());
        }
        public IActionResult GetShopType()   //返回店铺筛选类别
        {
            JsonData jsondata = new JsonData();

            if (Global.GShopType == "0")
            {
                jsondata["type"] = "1";
            }
            else
            {
                jsondata["type"] = "2";
            }
            return Json(jsondata.ToJson());
        }
    }
}
