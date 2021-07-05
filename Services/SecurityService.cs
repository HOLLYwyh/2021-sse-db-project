using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    /// <summary>
    /// 为用户单独提供修改手机号码和密码的安全服务
    /// </summary>
    public class SecurityService : ISecurityService
    {
        private ModelContext _ctx;
        public SecurityService(ModelContext ctx)
        {
            _ctx = ctx;
        }

        // 显示用户绑定手机号
        public string displayPhone(string id)
        {
            // 检测用户是否存在，但应该没有必要，因为已经登录
            /*if(_ctx.Buyers.Any(e => e.BuyerId == id))
            */
            var buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == id);
            // 电话号码需要检测是否存在
            if (buyer.Phone != null)
                return buyer.Phone;
            else
                return "null";
        }

        // 显示用户密码
        public  string displayPasswd(string id)
        {
            var buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == id);
            return buyer.Passwd;
        }

        // 修改用户绑定的手机号码
        public Task<int> updatePhone(string id, string newPhone)
        {

            var buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == id);
            buyer.Passwd = newPhone;
           return _ctx.SaveChangesAsync();
        }

        // 修改用户密码
        public Task<int> updatePasswd(string id, string newPasswd)
        {
            var buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == id);
            buyer.Passwd = newPasswd;
            return _ctx.SaveChangesAsync();
        }
    }
}
