using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetMall.Interfaces
{
    public interface IBuyerService
    {
        public bool SignUp(string phone, string nickName, string passwd);//注册

        public Buyer Login(string s, string passwd);//登录

        public Buyer DisplayBuyer(string s);//展示买家个人信息

        public Buyer EditBuyer(Buyer before, Buyer now);//修改个人信息，主码不允许修改！

        /*以下函数为工具函数*/
        public List<Buyer> Index();

        public Buyer SearchByPhone(string phone);

        public Buyer SearchByID(string ID);

        public void Create([Bind("BuyerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Buyer buyer);

        public Buyer Edit(string id);

        public Buyer Edit(string id, [Bind("BuyerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Buyer buyer);

        public Buyer Delete(string id);

        public bool BuyerExists(string phone);

    }
}