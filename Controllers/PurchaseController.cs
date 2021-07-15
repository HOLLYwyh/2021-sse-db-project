using Alipay.AopSdk.Core;
using Alipay.AopSdk.Core.Domain;
using Alipay.AopSdk.Core.Request;
using Alipay.AopSdk.Core.Util;
using Internetmall.Models.AlipayModels;
using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using InternetMall.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ThirdParty.Json.LitJson;

namespace InternetMall.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private OrderService orderService;             //后端service
        private CartService cartService; 

        public PurchaseController(ModelContext context)
        {
            _context = context;
            orderService = new OrderService(_context);
            cartService = new CartService(_context);
        }

        #region 发起支付
        /// 发起支付请求
        /// </summary>
        /// <param name="tradeno">外部订单号，商户网站订单系统中唯一的订单号</param>
        /// <param name="subject">订单名称</param>
        /// <param name="totalAmout">付款金额</param>
        /// <param name="itemBody">商品描述</param>
        /// <returns></returns>
        [HttpPost]
        public void PayRequest(string tradeno, string subject, string totalAmout, string itemBody)
        {
            DefaultAopClient client = new DefaultAopClient(AlipayConfig.Gatewayurl, AlipayConfig.AppId, AlipayConfig.PrivateKey, "json", "2.0",
                AlipayConfig.SignType, AlipayConfig.AlipayPublicKey, AlipayConfig.CharSet, false);

            // 组装业务参数model
            AlipayTradePagePayModel model = new AlipayTradePagePayModel();
            model.Body = itemBody;
            model.Subject = subject;
            model.TotalAmount = totalAmout;
            model.OutTradeNo = tradeno;
            model.ProductCode = "FAST_INSTANT_TRADE_PAY";

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            // 设置同步回调地址
            request.SetReturnUrl("https://localhost:44393/Purchase/SuccessPay");
            // 设置异步通知接收地址
            request.SetNotifyUrl("");
            // 将业务model载入到request
            request.SetBizModel(model);

            var response = client.SdkExecute(request);
            Console.WriteLine($"订单支付发起成功，订单号：{tradeno}");
            //跳转支付宝支付
            Response.Redirect(AlipayConfig.Gatewayurl + "?" + response.Body);
        }
        #endregion
        /// <summary>
        /// 支付异步回调通知 需配置域名 因为是支付宝主动post请求这个action 所以要通过域名访问或者公网ip
        /// </summary>
        #region 支付异步回调
        public async void Notify()
        {
            /* 实际验证过程建议商户添加以下校验。
			1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
			2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
			3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
			4、验证app_id是否为该商户本身。
			*/
            Dictionary<string, string> sArray = GetRequestPost();
            if (sArray.Count != 0)
            {
                bool flag = AlipaySignature.RSACheckV1(sArray, AlipayConfig.AlipayPublicKey, AlipayConfig.CharSet, AlipayConfig.SignType, false);
                if (flag)
                {
                    //交易状态
                    //判断该笔订单是否在商户网站中已经做过处理
                    //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                    //请务必判断请求时的total_amount与通知时获取的total_fee为一致的
                    //如果有做过处理，不执行商户的业务程序

                    //注意：
                    //退款日期超过可退款期限后（如三个月可退款），支付宝系统发送该交易状态通知
                    Console.WriteLine(Request.Form["trade_status"]);

                    await Response.WriteAsync("success");
                }
                else
                {
                    await Response.WriteAsync("fail");
                }
            }
        }
        #endregion

        #region 支付同步回调
        /// <summary>
        /// 支付同步回调
        /// </summary>
        [HttpGet]
        public IActionResult Callback()
        {
            /* 实际验证过程建议商户添加以下校验。
			1、商户需要验证该通知数据中的out_trade_no是否为商户系统中创建的订单号，
			2、判断total_amount是否确实为该订单的实际金额（即商户订单创建时的金额），
			3、校验通知中的seller_id（或者seller_email) 是否为out_trade_no这笔单据的对应的操作方（有的时候，一个商户可能有多个seller_id/seller_email）
			4、验证app_id是否为该商户本身。
			*/
            Dictionary<string, string> sArray = GetRequestGet();
            if (sArray.Count != 0)
            {
                bool flag = AlipaySignature.RSACheckV1(sArray, AlipayConfig.AlipayPublicKey, AlipayConfig.CharSet, AlipayConfig.SignType, false);
                if (flag)
                {
                    Console.WriteLine($"同步验证通过，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "同步验证通过";
                }
                else
                {
                    Console.WriteLine($"同步验证失败，订单号：{sArray["out_trade_no"]}");
                    ViewData["PayResult"] = "同步验证失败";
                }
            }
            return View();
        }

        #endregion
        #region 解析请求参数

        private Dictionary<string, string> GetRequestGet()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();

            ICollection<string> requestItem = Request.Query.Keys;
            foreach (var item in requestItem)
            {
                sArray.Add(item, Request.Query[item]);

            }
            return sArray;

        }

        private Dictionary<string, string> GetRequestPost()
        {
            Dictionary<string, string> sArray = new Dictionary<string, string>();

            ICollection<string> requestItem = Request.Form.Keys;
            foreach (var item in requestItem)
            {
                sArray.Add(item, Request.Form[item]);

            }
            return sArray;

        }

        #endregion
        public IActionResult ConfirmOrder()    //购买信息确认
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
        public IActionResult ShoppingCart()    //购物车
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

        public IActionResult SuccessPay()   //最终购买成功
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

        public IActionResult SubmitOrder()   //支付宝付款页面
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
        //订单生成页面
        [HttpPost]
        public IActionResult SetCommodDetail([FromBody] CommodDetail commodity)   //商品详情页提交商品
        {
            Global.GCommodityID = commodity.ID;
            Global.GCommodityNum = commodity.Amount;
            Global.GConfirmOrderType = 1;
            JsonData jsondata = new JsonData();
            jsondata["commodityID"] = commodity.ID;
            return Json(jsondata.ToJson());
        }

        public IActionResult GetCommodDetail()   //订单确认-渲染商品信息
        {
            if(Global.GConfirmOrderType  == 1)  //从商品详情页渲染商品信息
            {
                List<Good> commodity = new List<Good>();
                commodity = orderService.RenderOrderPageFromDetail(Global.GCommodityID, Global.GCommodityNum);
                string str = JsonConvert.SerializeObject(commodity);
                return new ContentResult { Content = str, ContentType = "application/json" };
            }
            else   //从购物车渲染商品信息
            {
                List<Good> commodityList = new List<Good>();
                commodityList = orderService.RenderOrderPageFromCart(Global.GCart, Request.Cookies["buyerID"]);
                Global.GGoods = commodityList;  //提前保存
                string str = JsonConvert.SerializeObject(commodityList);
                return new ContentResult { Content = str, ContentType = "application/json" };
            }

        }

        public IActionResult GetReceiveInformation()    //订单确认-收货信息
        {
            List<ReceiveInformation> information = new List<ReceiveInformation>();
            information = orderService.GetReceiveInformation(Request.Cookies["buyerID"]);
            string str = JsonConvert.SerializeObject(information);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        public IActionResult GetCoupons()  //订单确认-渲染优惠券
        {
            List<CouponView> coupon = new List<CouponView>();
            coupon = orderService.GetCoupons(Request.Cookies["buyerID"]);
            string str = JsonConvert.SerializeObject(coupon);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] CommdCartOrder order)    //订单确认-生成订单
        {
            JsonData jsonData = new JsonData();
            if (Global.GConfirmOrderType == 1)  //是从商品详情页面进行渲染
            {
                if (orderService.CreateOrderFromDetail(Request.Cookies["buyerID"], Global.GCommodityID,
                   order.AddressID, Global.GCommodityNum,int.Parse(order.TotalPrice)))
                {
                    jsonData["result"] = "SUCCESS";
                }
                else
                {
                    jsonData["result"] = "FAILED";
                }
            }
            else   //从购物车进行渲染
            {
                //下方均需要前端传入的数据
                string receiveID = "1";
                int price = 1;
                //上方均需要前端传入的数据
                //orderService.CreateOrderFromChart(Request.Cookies["buyerID"],, receiveID, price);
            }
            return Json(jsonData.ToJson());
        }

        //购物车页面相关
        public IActionResult GetCartDetail()    //购物车详情
        {
            List<CartView> shopCarts = new List<CartView>();    //  购物车信息显示类 列表    
            shopCarts = cartService.GetCartProduct(Request.Cookies["buyerID"]);
            string str = JsonConvert.SerializeObject(shopCarts);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult DeleteCommodity([FromBody] CommodityID commodity)  //购物车删除商品
        {
            JsonData jsondata = new JsonData();
            if(cartService.RemoveFromCart(Request.Cookies["buyerID"], commodity.ID))
            {
                jsondata["result"] = true;
            }
            else
            {
                jsondata["result"] = false;
            }
            return Json(jsondata.ToJson());
        }
        
        [HttpPost]
        public IActionResult SetCartOrdedr([FromBody] Cart cart)    //在购物车页面跳转到支付详情
        {
            JsonData jsonData = new JsonData();
            if(cart.cart.Count == 0)  //没有商品
            {
                jsonData["result"] = "FALSE"; 
            }
            else
            {
                Global.GCart = cart;
                Global.GConfirmOrderType = 2;
                jsonData["result"] = "SUCCESS";
            }
            return Json(jsonData.ToJson());
        }
    }
}
