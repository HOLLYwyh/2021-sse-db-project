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
using InternetMall.Services;

namespace InternetMall.Controllers
{
    public class EntryController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private BuyerService service;             //后端service
        private SellerService sellerService;
        public EntryController(ModelContext context)
        {
            _context = context;
            service = new BuyerService(_context);
            sellerService = new SellerService(_context);
        }
        // 传输页面
        public IActionResult BuyerLogIn()
        {
            if(Request.Cookies["buyerNickName"] != null)
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
            if (Request.Cookies["buyerNickName"] != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }
        }
        public IActionResult SellerSignUp()
        {
            if (Request.Cookies["sellerNickName"] != null)
            {
                return Redirect("/SellerBackground/Home");
            }
            else
            {
                return View();
            }
        }
        public IActionResult SellerLogIn()
        {
            if (Request.Cookies["sellerNickName"] != null)
            {
                return Redirect("/SellerBackground/Home");
            }
            else
            {
                return View();
            }
        }
        public IActionResult AdministratorLogIn()
        {
            return View();
        }

        public IActionResult BuyerLogOut()    //买家退出登录
        {
            if (Request.Cookies["buyerNickName"] != null)
            {
                //设置cookie
               HttpContext.Response.Cookies.Delete("buyerNickName");
               HttpContext.Response.Cookies.Delete("buyerID");
               //HttpContext.Response.Cookies.Delete("buyerURL");
               return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Home/Index");
            }
        }

        // 前后端交互
        [HttpPost]
        public IActionResult BuyerLogInForm([FromBody] EntryLogInBuyer logInBuyer)   //买家登录
        {
            var buyer = service.Login(logInBuyer.ID, logInBuyer.password);
            if (buyer != null)
            {
               //设置cookie
               HttpContext.Response.Cookies.Append("buyerNickName", buyer.Nickname, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
               HttpContext.Response.Cookies.Append("buyerID", buyer.BuyerId, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
               //HttpContext.Response.Cookies.Append("buyerURL", buyer.Nickname, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
               return Redirect("/Home/Index");
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["buyerNickName"] = "找不到名称";
                jsondata["buyerPassword"] = "找不到密码";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult BuyerSignUpForm([FromBody] EntrySignUpBuyer signUpBuyer)  //买家注册
        {
            if (service.SignUp(signUpBuyer.phoneNumber,signUpBuyer.nickName,signUpBuyer.password))
            {
                JsonData jsondata = new JsonData();
                jsondata["signUp"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["signUp"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult SellerLogInForm([FromBody] EntryLogInSeller logInSeller)    //卖家登录
        {
            var seller = sellerService.Login(logInSeller.ID,logInSeller.password);
            if(seller != null)
            {
                //设置cookie
                HttpContext.Response.Cookies.Append("sellerNickName", seller.Nickname, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
                HttpContext.Response.Cookies.Append("sellerID", seller.SellerId, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
                //HttpContext.Response.Cookies.Append("sellerURL", seller.Nickname, new CookieOptions { Expires = DateTime.Now.AddSeconds(3600) });
                JsonData jsondata = new JsonData();
                jsondata["sellerNickName"] = seller.Nickname;
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["sellerNickName"] = "找不到名称";
                jsondata["sellerPassword"] = "找不到密码";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult SellerSignUpForm([FromBody] EntrySignUpSeller signUpSeller)  //卖家注册
        {
            if (sellerService.SignUp(signUpSeller.phoneNumber, signUpSeller.nickName, signUpSeller.password))
            {
                JsonData jsondata = new JsonData();
                jsondata["signUp"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["signUp"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        //[HttpPost]
        //public IActionResult AdministratorLogInForm([FromBody]EntryLogInAdmin loginSeller)  //管理员登录
        //{
        //}
    }
}