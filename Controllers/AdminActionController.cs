using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using InternetMall.Models.ControllerModels;
using Newtonsoft.Json;
using InternetMall.DBContext;
using InternetMall.Services;
using ThirdParty.Json.LitJson;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;

namespace InternetMall.Controllers
{
    public class AdminActionController : Controller
    {
        private readonly ModelContext _context;
        private AdminActionService adminActionService;

        public AdminActionController(ModelContext context)
        {
            _context = context;
            adminActionService = new AdminActionService(_context);
        }

        /******************** 管理活动 *****************/
        [HttpPost]
        public IActionResult ReleaseOneActivity([FromBody] ReleaseActivity releaseActivity)  // 发布活动
        {
            if (adminActionService.releaseActivity(releaseActivity.name, releaseActivity.type, releaseActivity.date1, releaseActivity.date2, releaseActivity.desc, releaseActivity.constrict, releaseActivity.minus))
            {
                JsonData jsondata = new JsonData();
                jsondata["addActivity"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["addActivity"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult DisplayOneActivity([FromBody] LookActivity lookActivity)     // 某个活动详情
        {
            List<ActivityView> activityView = adminActionService.getOneActivity(lookActivity.ID);
            string str = JsonConvert.SerializeObject(activityView);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult DisplayAllActivities()          // 查看所有活动详情
        {
            List<ActivityView> activities = adminActionService.getActivities();
            string str = JsonConvert.SerializeObject(activities);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult DeleteOneActivity([FromBody] LookActivity deleteActivity)  // 删除活动
        {
            if (adminActionService.deleteOneActivity(deleteActivity.ID))
            {
                JsonData jsondata = new JsonData();
                jsondata["delete"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["delete"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }
        /*************** 封禁功能 ********************/
        [HttpPost]
        public IActionResult DeleteBuyer([FromBody] DeleteBuyer deleteBuyer)  // 删除买家
        {
            if (adminActionService.DeleteBuyer(deleteBuyer.ID))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteBuyer"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteBuyer"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult DeleteSeller([FromBody] DeleteSeller deleteSeller)  // 封禁卖家
        {
            if (adminActionService.DeleteSeller(deleteSeller.ID))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteSeller"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteSeller"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }


        [HttpPost]
        public IActionResult DeleteCommodity([FromBody] DeleteCommodity deleteCommodity)  // 下架商品
        {
            if (adminActionService.DeleteCommodity(deleteCommodity.ID))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteCommodity"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteCommodity"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult DeleteShop([FromBody] DeleteShop deleteShop)  // 封禁店铺
        {
            if (adminActionService.DeleteShop(deleteShop.ID))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteShop"] = "SUCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteShop"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }
    }
}
