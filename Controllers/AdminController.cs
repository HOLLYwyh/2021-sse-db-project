using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminWork()
        {
            return View();
        }
    }
}
