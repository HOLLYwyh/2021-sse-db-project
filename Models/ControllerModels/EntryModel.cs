using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Models
{
    public class EntryLogInBuyer  //Entry需要的Controller
    {
        public string ID { get; set; }
        public string password { get; set; }
    }
    public class EntrySignUpBuyer
    {
        public string phoneNumber { get; set; }
        public string nickName { get; set; }
        public string password { get; set; }
    }
}
