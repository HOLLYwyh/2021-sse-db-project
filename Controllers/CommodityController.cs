using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace InternetMall.Controllers
{
    public class CommodityController : Controller   //商品详情与店铺详情
    {
        public IActionResult Details()
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
        public IActionResult SetCommodityID([FromBody] CommodityID commodity)   //设置卖家ID
        {
            Global.GCommodityID = commodity.ID;
            JsonData jsondata = new JsonData();
            jsondata["commodityID"] = commodity.ID;
            return Json(jsondata.ToJson());
        }

        public IActionResult GetCommodity()      //返回商品
        {
            //这里还需要修改
            return Ok();
        }

    }
}
