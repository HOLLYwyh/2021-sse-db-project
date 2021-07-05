using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.Services;
using InternetMall.DBContext;
using InternetMall.Models;
using ThirdParty.Json.LitJson;

namespace InternetMall.Controllers
{
    public class EntryController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private BuyerService service;             //后端service
        public EntryController(ModelContext context)
        {
            _context = context;
            service = new BuyerService(_context);
        }
        // 传输页面
        public IActionResult BuyerLogIn()
        {
            if(Request.Cookies["nickName"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult BuyerSignUp()
        {
            if (Request.Cookies["nickName"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult SellorSignUp()
        {
            return View();
        }
        public IActionResult SellorLogIn()
        {
            return View();
        }
        public IActionResult AdministratorLogIn()
        {
            return View();
        }

        // 前后端交互
        [HttpPost]
        public IActionResult BuyerLogInForm([FromBody] EntryLogInBuyer logInBuyer)   //买家登录
        {
            var buyer = service.Login(logInBuyer.ID, logInBuyer.password);
            {
                {
                    if (buyer != null)
                    {
                        //设置cookie
                        HttpContext.Response.Cookies.Append("nickName", buyer.Nickname, new CookieOptions { Expires = DateTime.Now.AddSeconds(60) });
                        return Redirect("/Home/Index");
                    }
                    else
                    {
                        JsonData jsondata = new JsonData();
                        jsondata["NickName"] = "找不到名称";
                        jsondata["password"] = "找不到密码";
                        return Json(jsondata.ToJson());
                    }
                }
            }
        }

        [HttpPost]
        public IActionResult BuyerSignUpForm([FromBody] EntrySignUpBuyer signUpBuyer)  //买家注册
        {
            if (service.SignUp(signUpBuyer.phoneNumber,signUpBuyer.nickName,signUpBuyer.password))
            {
                return Redirect("/Entry/BuyerLogIn");
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["signUp"] = "注册失败";
                return Json(jsondata.ToJson());
            }
        }
    }
}