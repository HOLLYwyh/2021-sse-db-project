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
    public class FavoritesController : Controller
    {
        public IActionResult Follow()
        {
            return View();
        }
        public IActionResult Favorite()
        {
            return View();
        }
    }
}
