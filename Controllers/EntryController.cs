﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace InternetMall.Controllers
{
    public class EntryController : Controller
    {
        // GET: Entry
        public IActionResult BuyerLogIn()
        {
            return View();
        }
        public IActionResult BuyerSignUp()
        {
            return View();
        }
        public IActionResult SellorSignUp()
        {
            return View();
        }
        public IActionResult SellorLogIn()
        {
            return View();
        }
        public IActionResult AdministratorLogIn()
        {
            return View();
        }
    }
}