using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace Internetmall.Interfaces
{
    interface IAdministratorService
    {
        public bool SignUp(string phone, string nickName, string passwd);//注册

        public Administrator Login(string s, string passwd);//登录
    }
}
