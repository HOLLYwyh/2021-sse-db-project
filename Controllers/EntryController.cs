using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetMall.Controllers
{
    public class EntryController : Controller
    {
        // GET: Entry
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
    }
}