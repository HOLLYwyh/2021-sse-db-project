using Internetmall.Models.BusinessEntity;
using Internetmall.Services;
using InternetMall.DBContext;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace InternetMall.Controllers
{
    public class CommodityController : Controller   //商品详情与店铺详情
    {
        private readonly ModelContext _context;   //数据库上下文
        private CommodityDetailsService commdDetailService;             //后端service

        public CommodityController(ModelContext context)
        {
            _context = context;
            commdDetailService = new CommodityDetailsService(_context);
        }
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
        public IActionResult SetCommodityID([FromBody] CommodityID commodity)   //设置商品ID
        {
            Global.GCommodityID = commodity.ID;
            JsonData jsondata = new JsonData();
            jsondata["commodityID"] = commodity.ID;
            return Json(jsondata.ToJson());
        }

        
        public IActionResult GetMainCommodity()      //返回商品
        {
            CommodityDetailsView commodity = new CommodityDetailsView();
            commodity = commdDetailService.DisplayCommodityDetails(Global.GCommodityID);
            string str = JsonConvert.SerializeObject(commodity);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        
        public IActionResult GetRecomCommodity()   //返回推荐商品
        {
            List<Good> commodityList = new List<Good>();
            commodityList = commdDetailService.recommendCommodity(Global.GCommodityID);
            string str = JsonConvert.SerializeObject(commodityList);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }
    }
}
