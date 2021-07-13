using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetMall.Models;
using InternetMall.DBContext;
using Internetmall.Interfaces;

namespace Internetmall.Services
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
