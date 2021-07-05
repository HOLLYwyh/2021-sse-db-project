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
        public IActionResult BuyerLogInForm()   //买家登录
        {
            var buyer = service.Login(Request.Form["ID"], Request.Form["password"]);
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
                        return Redirect("/Entry/BuyerLogIn");
                    }
                }
            }
        }
        public IActionResult BuyerSignUpForm()  //买家注册
        {
            //买家注册的页面，还没有开始写
            if (service.SignUp(Request.Form["phoneNumber"], Request.Form["nickName"], Request.Form["password"]))
            {
                return Redirect("/Entry/BuyerLogIn");
            }
            else
            {
                return Redirect("/Entry/BuyerSignUp");
            }
        }
    }
}