using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.DBContext;
using Internetmall.Models.BusinessEntity;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using ThirdParty.Json.LitJson;
using InternetMall.Models;
using InternetMall.Services;

namespace InternetMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private HomeService homeService;             //后端service
        private BuyerService buyerService;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public HomeController(ModelContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            homeService = new HomeService(_context);
            buyerService = new BuyerService(_context);
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult Index()
        {
            Global.GCommodityType = "0";
            Global.GShopType = "0";
            //return View();
            return Redirect("/Account/PersonalInformation");
        }

        //前后端交互
        [Obsolete]
        public IActionResult RcmdCommodity()   //首页商品推荐
        {
            List<Good> goods = new List<Good>();
            if (Request.Cookies["buyerNickName"] != null)   //已经登录
            {
                goods = homeService.RecommendingCommodities(true, Request.Cookies["buyerID"]);
            }
            else
            {
                goods = homeService.RecommendingCommodities(false);
            }
            //添加绝对路径
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            foreach(var good in goods)
            {
                good.img = "../.." + good.img;
            }
            string str = JsonConvert.SerializeObject(goods);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult RecmdZoneCommodities([FromBody] CommodityType commodityType)  //首页分区推荐
        {
            List<Good> goods = new List<Good>();
            int category = int.Parse(commodityType.type);
            goods = homeService.RecommendingZoneCommodities(category);
            foreach (var good in goods)
            {
                good.img = "../.." + good.img;
            }
            string str = JsonConvert.SerializeObject(goods);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }
    
        [HttpPost]
        public IActionResult RankCommodities([FromBody] CommodityType commodityType)   //首页排行榜推荐
        {
            List<rankView> rankList = new List<rankView>();
            int category = int.Parse(commodityType.type);
            rankList = homeService.Rank(category);
            string str = JsonConvert.SerializeObject(rankList);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        public IActionResult GetBuyerPic()    //返回买家头像
        {
            JsonData jsondata = new JsonData();
            Buyer buyer = buyerService.SearchByID(Request.Cookies["buyerID"]);
            if(buyer!=null)
            {
                buyer.Url = "../.." + buyer.Url; ;
                jsondata["url"] = buyer.Url;
            }
            else
            {
                jsondata["url"] = "FAILED";
            }
            return Json(jsondata.ToJson());
        }
    }
}