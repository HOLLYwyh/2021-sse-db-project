using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Internetmall.Controllers
{
    public class CommodityController : Controller
    {
        public IActionResult Details()
        {
            return View();
        }
    }
}
