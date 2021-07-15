using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternetMall.Models;
using InternetMall.Interfaces;
using InternetMall.DBContext;

namespace InternetMall.Services
{
    public class BuyerService:IBuyerService
    {
        private ModelContext _context;      
        public int GetBuyerCount()
        {
            var count = _context.Counters.FirstOrDefault(m => m.ID == "0");
            int buyerCount = count.Buyercount + 1;
            count.Buyercount += 1;
            _context.Counters.Update(count);
            _context.SaveChanges();
            return buyerCount;
        }
        public bool SignUp(string phone, string nickName, string passwd)//注册
        {
            //如果要注册的用户电话不存在，说明可以注册
            if (BuyerExists(phone) == false)
            {
                var newBuyer = new Buyer();

                newBuyer.Phone = phone;
                newBuyer.Nickname = nickName;
                newBuyer.Passwd = passwd;
                newBuyer.Gender = 0;
                //为新用户随机生成一个用户ID
                newBuyer.BuyerId = GetBuyerCount().ToString();
                //其他信息可以为空（初始即为空），用户后续添加即可

                Create(newBuyer);

                return true;
            }
            //否则，不能注册已经存在的用户
            return false;
        }
        public Buyer Login(string s, string passwd)
        {
            Buyer buyer;
            //暂时通过长度区分，有改进空间
            if (s.Length == 11)
                buyer = SearchByPhone(s);
            else          
                buyer = SearchByID(s);
 
            if (buyer == null)
            {
                Console.WriteLine("用户不存在");
            }
            else if (buyer.Passwd != passwd)
            {
                Console.WriteLine("手机号或密码错误");
                buyer = null;
            }
            return buyer;
        }   
        public Buyer DisplayBuyer(string s)
        {
            Buyer buyer;
            //暂时通过长度区分，有改进空间
            if (s.Length == 11)
                buyer = SearchByPhone(s);
            else
                buyer = SearchByID(s);

            if (buyer == null)
            {
                Console.WriteLine("查看用户不存在");
            }          
            return buyer;
        }
        public Buyer EditBuyer(Buyer before, Buyer now)//修改个人信息，主码不允许修改！
        {
            string id = before.BuyerId;

            var buyer = Edit(id, now);

            return buyer;
        }


        public BuyerService(ModelContext context)
        {
            _context = context;
        }
        public List<Buyer> Index()
        {
            return _context.Buyers.ToList();
        }
        public Buyer SearchByPhone(string phone)
        {
            if (phone == null)
            {
                return null;
            }

            var buyer = _context.Buyers
                .FirstOrDefault(m => m.Phone == phone);
            if (buyer == null)
            {
                return null;
            }

            return buyer;
        }
        public Buyer SearchByID(string ID)
        {
            if (ID == null)
            {
                return null;
            }

            var buyer = _context.Buyers
                .FirstOrDefault(m => m.BuyerId == ID);
            if (buyer == null)
            {
                return null;
            }

            return buyer;
        }
        public void Create([Bind("BuyerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Buyer buyer)
        {
            //if (ModelState.IsValid)
            //{
            _context.Add(buyer);
            _context.SaveChanges();
                //return RedirectToAction(nameof(Index));
            //}
        }
        public Buyer Edit(string id)
        {
            if (id == null)
            {
                return null;
            }

            var buyer = _context.Buyers.Find(id);
            if (buyer == null)
            {
                return null;
            }
            return buyer;
        }
        public Buyer Edit(string id, [Bind("BuyerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Buyer buyer)
        {
            if (id != buyer.BuyerId)
            {
                return null;
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(buyer);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyerExists(buyer.BuyerId))
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
            return buyer;
        }
        public Buyer Delete(string id)
        {
            if (id == null)
            {
                return null;
            }

            var buyer = _context.Buyers
                .FirstOrDefault(m => m.BuyerId == id);
            if (buyer == null)
            {
                return null;
            }
            _context.Buyers.Remove(buyer);
            _context.SaveChanges();
            return buyer;
        }
        public bool BuyerExists(string phone)
        {
            return _context.Buyers.Any(e => e.Phone == phone);
        }
    }
}
