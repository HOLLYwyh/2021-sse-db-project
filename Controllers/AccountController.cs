using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternetMall.Controllers
{
    public class AccountController : Controller
    {
        // 返回页面
        public IActionResult PersonalInformation()
        {
            if (Request.Cookies["nickName"] != null)
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
            if (Request.Cookies["nickName"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Entry/BuyerLogIn");
            }
        }

        //前后端交互


    }
}