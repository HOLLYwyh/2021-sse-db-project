using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.Models;
using InternetMall.DBContext;
using Internetmall.Services;
using Internetmall.Models.BusinessEntity;
using Newtonsoft.Json;

namespace InternetMall.Controllers
{
    public class RankingController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private RankService rankService;             //后端service

        public RankingController(ModelContext context)
        {
            _context = context;
            rankService = new RankService(_context);
        }

        public IActionResult UniversalList()
        {
            if (Request.Cookies["buyerNickName"] != null)
            {
                return View();
            }
            else
            {
                return Redirect("/Entry/BuyerLogIn");
            }
        }

        //前后端交互
        [HttpPost]
        public IActionResult GetRankList([FromBody]RankType rankType)
        {
            List<Good> rankList = new List<Good>();
            rankList = rankService.rank(rankType.Type);
            string str = JsonConvert.SerializeObject(rankList);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }
    }

}
