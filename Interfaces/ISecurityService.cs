using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface ISecurityService
    {
        // 显示用户电话号码：不存在返回null
        public string displayPhone(string buyerid);

        // 显示用户密码
        public string displayPasswd(string buyerid);

        // 修改用户绑定的手机号码
        public bool updatePhone(string buyerid, string oldPhone, string newPhone);

        // 修改用户密码
        public bool updatePasswd(string buyerid, string oldPasswd, string newPasswd);
    }
}
