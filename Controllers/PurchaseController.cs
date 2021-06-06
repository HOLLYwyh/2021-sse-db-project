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
    public class PurchaseController : Controller
    {
        public IActionResult ShoppingCart()
        {
            return View();
        }
        public IActionResult Settle()
        {
            return View();
        }
        public IActionResult Payment()
        {
            return View();
        }
        public IActionResult SubmitOrder()
        {
            return View();
        }
    }
}
