using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.DBContext;
using InternetMall.Models;
using ThirdParty.Json.LitJson;
using Internetmall.Services;

namespace InternetMall.Controllers
{
    public class DetailController : Controller   //商品详情与店铺详情
    {
        private readonly ModelContext _context;   //数据库上下文
        private SearchService service;             //后端service
        public DetailController(ModelContext context)
        {
            _context = context;
            service = new SearchService(_context);
        }
        public IActionResult CommodityDetails()
        {
            return View();
        }
    }
}
