using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetMall.Interfaces
{
    interface IAdministratorService
    {
        public Administrator Login(string s, string passwd);//登录
    }
}
