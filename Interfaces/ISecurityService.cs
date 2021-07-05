using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface ISecurityService
    {
        // 显示用户电话号码：不存在返回"null"
        public string displayPhone(string id);

        // 显示用户密码
        public string displayPasswd(string id);

        // 修改用户绑定的手机号码
        public Task<int> updatePhone(string id, string newPhone);

        // 修改用户密码
        public Task<int> updatePasswd(string id, string newPasswd);
    }
}
