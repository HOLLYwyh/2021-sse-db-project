using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
    public class EntryLogInBuyer  //买家登录
    {
        public string ID { get; set; }
        public string password { get; set; }
    }
    public class EntrySignUpBuyer   //买家注册
    {
        public string phoneNumber { get; set; }
        public string nickName { get; set; }
        public string password { get; set; }
    }

    public class EntryLogInSeller  //卖家登录
    {
        public string ID { get; set; }
        public string password { get; set; }
    }

    public class EntrySignUpSeller  //卖家注册
    {
        public string phoneNumber { get; set; }
        public string nickName { get; set; }
        public string password { get; set; }
    }

    public class EntryLogInAdmin   //管理员登录
    {
       public string ID { get; set; }
       public string password { get; set; }
    }
}
