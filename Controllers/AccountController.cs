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
            nowBuyer.Nickname = data["UpdatedNickname"];
            nowBuyer.Gender = int.Parse(data["UpdatedGender"]);
            nowBuyer.DateBirth = DateTime.Parse(data["UpdatedBirth"]);
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
    }
}