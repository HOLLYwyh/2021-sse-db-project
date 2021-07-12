using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.DBContext;
using InternetMall.Models;
using ThirdParty.Json.LitJson;
using Internetmall.Services;

namespace InternetMall.Controllers
{
    public class DetailController : Controller   //商品详情与店铺详情
    {
        private readonly ModelContext _context;   //数据库上下文
        private SearchService service;             //后端service
        public DetailController(ModelContext context)
        {
            _context = context;
            service = new SearchService(_context);
        }
        public IActionResult CommodityDetails()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetCommodityDetails([FromBody] CommodityId commodityId)
        {
            var com = service.GetCommodityById(commodityId.id);
            JsonData jsondata = new JsonData();
            if (com != null)
            {
                jsondata["Id"] = com.CommodityId;
                jsondata["Price"] = com.Price.ToString();
                jsondata["Category"] = com.Category;
                jsondata["Storage"] = com.Storage;
                jsondata["Name"] = com.Name;
                jsondata["ShopId"] = com.ShopId;
                jsondata["Url"] = com.Url;
                jsondata["Soldnum"] = com.Soldnum;
                jsondata["Description"] = com.Description;
            }
           
            return Json(jsondata.ToJson());
        }

    }
}
