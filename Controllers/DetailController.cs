using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Controllers
{
    public class DetailController : Controller   //商品详情与店铺详情
    {
        public IActionResult CommodityDetails()
        {
            return View();
        }
    }
}
