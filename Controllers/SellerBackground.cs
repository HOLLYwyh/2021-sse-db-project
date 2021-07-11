using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace InternetMall.Controllers
{
    public class SellerBackgroundController : Controller
    {
        //必要的成员变量与构造函数
        [Obsolete]
        private readonly IHostingEnvironment _hostingEnvironment;

        [Obsolete]
        public SellerBackgroundController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        //显示页面
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Goods()
        {
            return View();
        }
        public IActionResult ShopSignUp()
        {
            return View();
        }

        //前后端交互
       [HttpPost]
       [Obsolete]
        public async Task<IActionResult> UploadCommodity()      //上传商品
       {
            var date = Request;
            var files = Request.Form.Files;   //上传的图片
            var data = Request.Form.Keys;     //上传的信息
            long size = files.Sum(f => f.Length);
            string webRootPath = _hostingEnvironment.WebRootPath;
            string contentRootPath = _hostingEnvironment.ContentRootPath;
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    long fileSize = formFile.Length; //获得文件大小，以字节为单位

                    var exetent = Path.GetExtension(formFile.FileName); //文件后缀名
                    string newFileName = System.Guid.NewGuid().ToString(); //随机生成新的文件名
                    var filePath = webRootPath + "/uploads/" + newFileName+ exetent; //newFileName;
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size });
        }
    }
}
