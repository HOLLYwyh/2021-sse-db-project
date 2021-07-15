
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
    public class SellerService : ISellerService
    {
        private readonly ModelContext _context;
        Random rd = new Random();

        public int GetSellerCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int sellerCount = count.Sellercount + 1;
            count.Sellercount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return sellerCount;
        }

        public bool SignUp(string phone, string nickName, string passwd)//注册
        {
            //如果要注册的用户电话不存在，说明可以注册
            if (SellerExists(phone) == false)
            {
                var newSeller = new Seller();

                newSeller.Phone = phone;
                newSeller.Nickname = nickName;
                newSeller.Passwd = passwd;
                //为新用户随机生成一个用户ID
                newSeller.SellerId = GetSellerCount().ToString();
                //其他信息可以为空（初始即为空），用户后续添加即可

                Create(newSeller);

                return true;
            }
            //否则，不能注册已经存在的用户
            return false;
        }
        public Seller Login(string s, string passwd)
        {
            Seller seller;
            //暂时通过长度区分，有改进空间
            if (s.Length == 11)
                seller = SearchByPhone(s);
            else
                seller = SearchByID(s);

            if (seller == null)
            {
                Console.WriteLine("用户不存在");
            }
            else if (seller.Passwd != passwd)
            {
                Console.WriteLine("手机号或密码错误");
                seller = null;
            }
            return seller;
        }
        public Seller DisplaySeller(string s)
        {
            Seller seller;
            //暂时通过长度区分，有改进空间
            if (s.Length == 11)
                seller = SearchByPhone(s);
            else
                seller = SearchByID(s);

            if (seller == null)
            {
                Console.WriteLine("查看用户不存在");
            }
            return seller;
        }
        public Seller EditSeller(Seller before, Seller now)//修改个人信息，主码不允许修改！
        {
            string id = before.SellerId;

            var seller = Edit(id, now);

            return seller;
        }


        public SellerService(ModelContext context)
        {
            _context = context;
        }
        public List<Seller> Index()
        {
            return _context.Sellers.ToList();
        }
        public Seller SearchByPhone(string phone)
        {
            if (phone == null)
            {
                return null;
            }

            var seller = _context.Sellers
                .FirstOrDefault(m => m.Phone == phone);
            if (seller == null)
            {
                return null;
            }

            return seller;
        }
        public Seller SearchByID(string ID)
        {
            if (ID == null)
            {
                return null;
            }

            var seller = _context.Sellers
                .FirstOrDefault(m => m.SellerId == ID);
            if (seller == null)
            {
                return null;
            }

            return seller;
        }
        public void Create([Bind("SellerId,Phone,Passwd,Nickname,IdNumber")] Seller seller)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(seller);
            _context.SaveChanges();
            //return RedirectToAction(nameof(Index));
            //}
        }
        public Seller Edit(string id)
        {
            if (id == null)
            {
                return null;
            }

            var seller = _context.Sellers.Find(id);
            if (seller == null)
            {
                return null;
            }
            return seller;
        }
        public Seller Edit(string id, [Bind("SellerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Seller seller)
        {
            if (id != seller.SellerId)
            {
                return null;
            }

            //if (ModelState.IsValid)
            //{
            try
            {
                _context.Update(seller);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (SellerExists(seller.SellerId))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            //return RedirectToAction(nameof(Index));
            //}
            return seller;
        }
        public Seller Delete(string id)
        {
            if (id == null)
            {
                return null;
            }

            var seller = _context.Sellers
                .FirstOrDefault(m => m.SellerId == id);
            if (seller == null)
            {
                return null;
            }
            _context.Sellers.Remove(seller);
            _context.SaveChanges();
            return seller;
        }
        public bool SellerExists(string phone)
        {
            return _context.Sellers.Any(e => e.Phone == phone);
        }
    }
}
