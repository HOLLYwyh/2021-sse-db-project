using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using InternetMall.Models;
using InternetMall.Services;
using InternetMall.DBContext;
using ThirdParty.Json.LitJson;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using InternetMall.Models.BusinessEntity;
using Internetmall.Models.BusinessEntity;

namespace InternetMall.Controllers
{
    public class AccountController : Controller
    {
        private readonly ModelContext _context;   //数据库上下文
        private SecurityService service1;             //后端service
        private BuyerService service2;
        private FavoriteProductService favoriteProductService;
        private FollowShopService followShopService;
        private ReceiveInformationService receiveInformationService;
        private BuyerCouponService buyerCouponService;
        private OrderService orderService;
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public AccountController(ModelContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            service1 = new SecurityService(_context);
            service2 = new BuyerService(_context);
            favoriteProductService = new FavoriteProductService(_context);
            followShopService = new FollowShopService(_context);
            receiveInformationService = new ReceiveInformationService(_context);
            buyerCouponService = new BuyerCouponService(_context);
            orderService = new OrderService(_context);

            _hostingEnvironment = hostingEnvironment;
        }
        // 返回页面
        public IActionResult PersonalInformation()
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
        public IActionResult Security()
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
        public IActionResult Address()
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
        public IActionResult orders()
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
        public IActionResult coupon()
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
        public JsonResult GetPhonePasswdById([FromBody] BuyerModel buyerModel)
        {
            string buyerPasswd = service1.displayPasswd(buyerModel.BuyerId);
            string buyerPhone = service1.displayPhone(buyerModel.BuyerId);
            JsonData jsondata = new JsonData();
            if (buyerPasswd != null && buyerPhone != null)
            {
                jsondata["buyerPasswd"] = buyerPasswd;
                jsondata["buyerPhone"] = buyerPhone;
            }

            return Json(jsondata.ToJson());//这玩意是个string

        }

        public JsonResult UpdatePhoneById([FromBody] BuyerPhone buyerPhone)
        {
            bool flag = service1.updatePhone(buyerPhone.BuyerId, buyerPhone.OldNo, buyerPhone.NewNo);
            JsonData jsondata = new JsonData();
            if (flag)
            {
                jsondata["buyerPhone"] = buyerPhone.NewNo;
            }

            return Json(jsondata.ToJson());//这玩意是个string
        }
        public JsonResult UpdatePasswdById([FromBody] BuyerPasswd buyerPasswd)
        {
            bool flag = service1.updatePasswd(buyerPasswd.BuyerId, buyerPasswd.OldPasswd, buyerPasswd.NewPasswd);
            JsonData jsondata = new JsonData();
            if (flag)
            {
                jsondata["buyerPasswd"] = buyerPasswd.NewPasswd;
            }

            return Json(jsondata.ToJson());//这玩意是个string
        }
        public JsonResult DisplayBuyerInfo([FromBody] BuyerModel buyerModel)
        {
            Buyer buyer = service2.SearchByID(buyerModel.BuyerId);
            JsonData jsondata = new JsonData();
            if (buyer != null)
            {
                jsondata["buyerNickname"] = buyer.Nickname;
                jsondata["buyerPhone"] = buyer.Phone;
                jsondata["buyerGender"] = buyer.Gender;
                jsondata["buyerBirth"] = buyer.DateBirth.ToString();
                jsondata["buyerUrl"] = buyer.Url;
            }
            return Json(jsondata.ToJson());//这玩意是个string
        }

        [HttpPost]
        [Obsolete]
        public Buyer UpdateInfoById()
        {
            var date = Request;
            var files = Request.Form.Files;   //上传的图片
            var data = Request.Form;     //上传的信息
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            //通过id找到要更新的买家，准备更改后保存数据库
            Buyer beforeBuyer = service2.SearchByID(data["BuyerId"]);
            Buyer nowBuyer = beforeBuyer;
            //对其部分信息进行更新
            if(data["UpdatedBirth"]=="")
            {
                nowBuyer.DateBirth = null;
            }
            else
            {
                nowBuyer.DateBirth = DateTime.Parse(data["UpdatedBirth"]);
            }
            nowBuyer.Nickname = data["UpdatedNickname"];
            nowBuyer.Gender = int.Parse(data["UpdatedGender"]);
            //如果上传图片将执行此步骤，否则跳过
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)   //上传图片成功
                {
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    var exetent = Path.GetExtension(formFile.FileName); //文件后缀名
                    var folderPath = webRootPath + "/uploads/account/1";//+ Request.Cookies["shopName"].ToString();
                    if (!Directory.Exists(folderPath))
                    {
                        //新建对应的文件夹
                        Directory.CreateDirectory(folderPath);
                    }
                    string newFileName = System.Guid.NewGuid().ToString(); //随机生成新的文件名
                    var filePath = folderPath + "/" + newFileName + exetent; //newFileName;
                    var url = "/uploads/account/1/" + newFileName + exetent;    //存入数据库中实际的内容

                    nowBuyer.Url = url;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                }
            }
            service2.EditBuyer(beforeBuyer, nowBuyer);

            return nowBuyer;
        }

        /**************************** 买家收货地址 **********************/
        [HttpPost]
        public IActionResult DisplayReceiveInformation([FromBody] BuyerModel buyerModel)     // 查看买家所有收货地址
        {
            List<ReceiveInformation> receiveInformation = new List<ReceiveInformation>();
            receiveInformation = receiveInformationService.GetReceiveInformation(buyerModel.BuyerId);
            string str = JsonConvert.SerializeObject(receiveInformation);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }


        [HttpPost]
        public IActionResult AddReceiveInformation([FromBody] AddReceiveInformation addReceiveInformation)  // 添加收货地址
        {
            string receiveId = receiveInformationService.createReceiveInformation(addReceiveInformation.BuyerId, addReceiveInformation.ReceiverName, addReceiveInformation.Phone, addReceiveInformation.DetailAddr, addReceiveInformation.Tag);

            if (receiveId != null)
            {
                JsonData jsondata = new JsonData();
                jsondata["add"] = receiveId;
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["add"] = "NULL";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult DeleteReceiveInformation([FromBody] DeleteReceiveInformation deleteReceiveInformation)   // 删除收货地址
        {
            if (receiveInformationService.deleteReceiveInformation(deleteReceiveInformation.ReceiveId))
            {
                JsonData jsondata = new JsonData();
                jsondata["delete"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["delete"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult UpdateReceiveInformation([FromBody] UpdateReceiveInformation updateReceive)  // 更新收货地址
        {
            bool update = receiveInformationService.updateReceiveInformation(updateReceive.ReceivedId, updateReceive.ReceiverName, updateReceive.Phone, updateReceive.DetailAddr, updateReceive.Tag);

            if (update)
            {
                JsonData jsondata = new JsonData();
                jsondata["update"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["update"] = "NULL";
                return Json(jsondata.ToJson());
            }
        }

        /**************************** 买家收藏夹服务 ***********************************/
        [HttpPost]
        public IActionResult DisplayFavorites([FromBody] BuyerModel buyerModel)   // 查看关注商品
        {
            List<FavoriteProductView> favorites = new List<FavoriteProductView>();
            favorites = favoriteProductService.getFavoriteProduct(buyerModel.BuyerId);

            foreach (var favorite in favorites)
            {
                favorite.CommodityImg = "../.." + favorite.CommodityImg;
            }

            string str = JsonConvert.SerializeObject(favorites);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult AddFavoriteProduct([FromBody] AddOrDeleteFavorites addFavorite)  // 关注商品
        {
            if (favoriteProductService.addToFavorite(addFavorite.buyerid, addFavorite.commodityid))
            {
                JsonData jsondata = new JsonData();
                jsondata["addFavorite"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["addFavorite"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult CancelFavoriteProduct([FromBody] AddOrDeleteFavorites addFavorite)  // 取消关注商品
        {
            if (favoriteProductService.removeFromFavorite(addFavorite.buyerid, addFavorite.commodityid))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteFavorite"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteFavorite"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult CancelAllFavoriteProduct([FromBody] DeleteAllFavorites deleteAllFavorite)  // 清楚所有关注商品
        {
            if (favoriteProductService.removeAllFavorite(deleteAllFavorite.buyerid))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteAllFavorite"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteAllFavorite"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }


        /******************************* 买家关注店铺服务 ***************************/
        [HttpPost]
        public IActionResult DisplayFollowShops([FromBody] BuyerModel buyerModel)   // 查看关注店铺信息
        {
            List<FollowShopView> followShopViews = new List<FollowShopView>();
            followShopViews = followShopService.getFollowShops(buyerModel.BuyerId);

            foreach (var followShop in followShopViews)
            {
                followShop.Url = "../.." + followShop.Url;

                foreach (var commodity in followShop.commodityView)
                {
                    commodity.Url = "../.." + commodity.Url;
                }
            }

            string str = JsonConvert.SerializeObject(followShopViews);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }

        [HttpPost]
        public IActionResult AddFollowShop([FromBody] AddOrDeleteFollowShop add)  // 关注店铺
        {
            if (followShopService.addToFollowShop(Request.Cookies["buyerID"], add.shopid))
            {
                JsonData jsondata = new JsonData();
                jsondata["addFollowShop"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["addFollowShop"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult CancelFollowShop([FromBody] AddOrDeleteFollowShop deleteFollowShop)  // 取消关注店铺
        {
            if (followShopService.removeFollowShop(deleteFollowShop.buyerid, deleteFollowShop.shopid))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteFollowShop"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteFollowShop"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        [HttpPost]
        public IActionResult CancelAllFollowShop([FromBody] DeleteAllFollowShops deleteAllFollowShops)  // 清除所有关注店铺
        {
            if (followShopService.removeAllFollowShop(deleteAllFollowShops.buyerid))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteAllFollowShops"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteAllFollowShops"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }

        /************************ 买家优惠券 **********************/
        [HttpPost]
        public IActionResult DisplayCoupons([FromBody] LookCoupons lookCoupons)   // 查看买家优惠券
        {
            List<BuyerCouponView> coupons = new List<BuyerCouponView>();
            coupons = buyerCouponService.getBuyerCoupns(lookCoupons.BuyerId);
            string str = JsonConvert.SerializeObject(coupons);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }


        [HttpPost]
        public IActionResult UseCoupon([FromBody] UseCoupon useCoupon)  // 买家使用优惠券
        {
            if (buyerCouponService.deleteBuyerCoupon(useCoupon.CouponId))
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteCoupon"] = "SUCCESS";
                return Json(jsondata.ToJson());
            }
            else
            {
                JsonData jsondata = new JsonData();
                jsondata["deleteCoupon"] = "ERROR";
                return Json(jsondata.ToJson());
            }
        }


        /***************** 买家订单 ***************************/
        [HttpPost]
        public IActionResult DisplayOrders([FromBody] BuyerOrder buyerOrder)   // 查看买家订单
        {
            List<OrderInformationView> orders = new List<OrderInformationView>();
            orders = orderService.getOrderByBuyerId(buyerOrder.BuyerId);
            foreach (var order in orders)
            {
                foreach (var commodity in order.commodityList)
                {
                    commodity.Url = "../.." + commodity.Url;
                }
            }
            string str = JsonConvert.SerializeObject(orders);
            return new ContentResult { Content = str, ContentType = "application/json" };
        }
    }
}