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
        // GET: Account
        public IActionResult PersonalInformation()
        {
            return View();
        }
        public IActionResult Security()
        {
            return View();
        }

    }
}