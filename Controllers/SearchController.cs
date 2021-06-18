using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult SearchShop()
        {
            return View();
        }

        public IActionResult SearchCommodity()
        {
            return View();
        }
    }
}
