using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.Models;
using InternetMall.Services;
using InternetMall.DBContext;
using ThirdParty.Json.LitJson;
using Newtonsoft.Json;

namespace InternetMall.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private SecurityService service;             //后端service
        public AccountController(ModelContext context)
        {
            _context = context;
            service = new SecurityService(_context);
        }
        // 返回页面
        public IActionResult PersonalInformation()
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
        public IActionResult Security()
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
        public IActionResult Address()
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
        public JsonResult GetPhonePasswdById([FromBody] BuyerModel buyerModel)
        {
            string buyerPasswd = service.displayPasswd(buyerModel.BuyerId);
            string buyerPhone= service.displayPhone(buyerModel.BuyerId);
            JsonData jsondata = new JsonData();
            if (buyerPasswd != null && buyerPhone != null)
            {
                jsondata["buyerPasswd"] = buyerPasswd;
                jsondata["buyerPhone"] = buyerPhone;               
            }

            return Json(jsondata.ToJson());//这玩意是个string

        }

        public JsonResult UpdatePhoneById([FromBody] BuyerPhone buyerPhone)
        {
            bool flag = service.updatePhone(buyerPhone.BuyerId, buyerPhone.OldNo, buyerPhone.NewNo);
            JsonData jsondata = new JsonData();
            if (flag)
            {               
                jsondata["buyerPhone"] = buyerPhone.NewNo;
            }

            return Json(jsondata.ToJson());//这玩意是个string
        }
    }
}