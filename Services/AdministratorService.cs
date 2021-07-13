using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetMall.Models;
using InternetMall.DBContext;
using InternetMall.Interfaces;

namespace InternetMall.Services
{
    public class AdministratorService : IAdministratorService
    {
        private ModelContext _context;

        public AdministratorService(ModelContext context)
        {
            _context = context;
        }
        public int GetAdministratorCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int adminCount = count.Administratorcount + 1;
            count.Administratorcount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return adminCount;
        }
        //没有注册的话，就不用这个函数了
        public bool SignUp(string phone, string nickName, string passwd)//注册
        {
            //如果要注册的用户电话不存在，说明可以注册
            if (AdministratorExists(phone) == false)
            {
                Administrator newAdmin = new Administrator();

                newAdmin.Phone = phone;
                newAdmin.Nickname = nickName;
                newAdmin.Passwd = passwd;
                //为新用户随机生成一个用户ID
                newAdmin.AdministratorId = GetAdministratorCount().ToString();
                //其他信息可以为空（初始即为空），用户后续添加即可

                Create(newAdmin);

                return true;
            }
            //否则，不能注册已经存在的用户
            return false;
        }
        public Administrator Login(string s, string passwd)//登录
        {
            Administrator admin;
            //暂时通过长度区分，有改进空间
            if (s.Length == 11)
                admin = SearchByPhone(s);
            else
                admin = SearchByID(s);

            if (admin == null)
            {
                Console.WriteLine("用户不存在");
            }
            else if (admin.Passwd != passwd)
            {
                Console.WriteLine("手机号或密码错误");
                admin = null;
            }
            return admin;
        }


        public bool AdministratorExists(string phone)
        {
            return _context.Administrators.Any(e => e.Phone == phone);
        }
        public void Create([Bind("AdministratorId,Phone,Passwd,Nickname,IdNumber,Name,Url")] Administrator admin)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(admin);
            _context.SaveChanges();
            //return RedirectToAction(nameof(Index));
            //}
        }
        public Administrator SearchByPhone(string phone)
        {
            if (phone == null)
            {
                return null;
            }

            var admin = _context.Administrators
                .FirstOrDefault(m => m.Phone == phone);
            if (admin == null)
            {
                return null;
            }

            return admin;
        }
        public Administrator SearchByID(string ID)
        {
            if (ID == null)
            {
                return null;
            }

            var admin = _context.Administrators
                .FirstOrDefault(m => m.AdministratorId == ID);
            if (admin == null)
            {
                return null;
            }

            return admin;
        }
    }
}
