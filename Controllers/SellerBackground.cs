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
    public class SellerBackgroundController : Controller
    {
        //显示页面
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Goods()
        {
            return View();
        }

        //前后端交互
       //[HttpPost]
       //public IActionResult UploadCommodity()
       //{

       //}
    }
}
