using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.Models;
using ThirdParty.Json.LitJson;
using InternetMall.DBContext;
using InternetMall.Services;

namespace InternetMall.Controllers
{
    public class DialogController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private BuyerService service;

        public DialogController(ModelContext context)
        {
            _context = context;
            service = new BuyerService(_context);
        }
        public IActionResult Service()
        {
            return View();
        }

        [HttpPost]
        public string GetBuyerNicknameById()   //设置商品ID
        {
            string buyerId = Global.GBuyerID;
            Buyer buyer = service.SearchByID(buyerId);
            //JsonData jsondata = new JsonData();
            //jsondata["BuyerNickname"] = buyer.Nickname;
            return buyer.Nickname;
        }

    }

}
