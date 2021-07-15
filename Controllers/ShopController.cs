using Microsoft.AspNetCore.Mvc;
using System;using System.Linq;

using System.Collections.Generic;
using System.Threading.Tasks;
using InternetMall.Models;
using ThirdParty.Json.LitJson;
using InternetMall.DBContext;
using InternetMall.Services;
using InternetMall.Models.BusinessEntity;
using Newtonsoft.Json;

namespace InternetMall.Controllers
{
    public class ShopController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private ShopService shopService;             //后端service

        public ShopController(ModelContext context)
        {
            _context = context;
            shopService = new ShopService(_context);
        }

        public IActionResult Shop()
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
        public IActionResult SetShopID([FromBody] ShopID shop)   //设置商品ID
        {
            Global.GShopId = shop.ID;
            JsonData jsondata = new JsonData();
            jsondata["shopID"] = shop.ID;
            return Json(jsondata.ToJson());
        }

        public IActionResult GetShop()    //商品详情页面
        {
            //正在修改
            ShopCommodityView shop = new ShopCommodityView();
            shop = shopService.getShopCommodities(Global.GShopId);

            string str = JsonConvert.SerializeObject(shop);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }
    }
}
