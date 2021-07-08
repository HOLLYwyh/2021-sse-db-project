using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetMall.Interfaces
{
    public interface ISellerService
    {
        public bool SignUp(string phone, string nickName, string passwd);//注册

        public Seller Login(string s, string passwd);

        public Seller DisplaySeller(string s);

        public Seller EditSeller(Seller before, Seller now);//修改个人信息，主码不允许修改！

        public List<Seller> Index();

        public Seller SearchByPhone(string phone);

        public Seller SearchByID(string ID);


        public void Create([Bind("SellerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Seller seller);


        public Seller Edit(string id);


        public Seller Edit(string id, [Bind("SellerId,Phone,Passwd,Nickname,Gender,DateBirth,IdNumber")] Seller seller);


        public Seller Delete(string id);


        public bool SellerExists(string phone);
    }
}
