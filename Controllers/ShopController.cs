using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using ThirdParty.Json.LitJson;

namespace InternetMall.Controllers
{
    public class ShopController : Controller
    {
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
            //还需要再修改
            return Ok();
        }
    }
}
