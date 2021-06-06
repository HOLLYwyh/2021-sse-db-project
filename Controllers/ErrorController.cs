using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Controllers
{
    public class ErrorController : Controller
    {
        // GET: 404
        public ActionResult Error404()
        {
            return View();
        }
    }
}