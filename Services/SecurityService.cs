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
        public string displayPhone(string buyerid)
        {
            // 检测用户是否存在，但应该没有必要，因为已经登录
            /*if(_ctx.Buyers.Any(e => e.BuyerId == id))
            */
            Buyer buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == buyerid);
            // 电话号码需要检测是否存在
            if (buyer.Phone != null)
                return buyer.Phone;
            else
                return null;
        }

        // 显示用户密码
        public string displayPasswd(string buyerid)
        {
            Buyer buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == buyerid);
            return buyer.Passwd;
        }

        // 修改用户绑定的手机号码
        public bool updatePhone(string buyerid, string oldPhone, string newPhone)
        {
            Buyer buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == buyerid);   

            if (buyer == null)
            {
                return false;
                throw new DllNotFoundException();
            }
            else
            {
                if (buyer.Phone == null || buyer.Phone == oldPhone)  // 比对电话号码
                {
                    buyer.Phone = newPhone;
                    _ctx.Buyers.Update(buyer);
                    _ctx.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }

        // 修改用户密码
        public bool updatePasswd(string buyerid, string oldPasswd, string newPasswd)
        {
            Buyer buyer = _ctx.Buyers.FirstOrDefault(x => x.BuyerId == buyerid);

            if (buyer == null)
            {
                return false;
                throw new DllNotFoundException();
            }
            else
            {
                if (buyer.Passwd == oldPasswd)  // 比对密码
                {
                    buyer.Passwd = newPasswd;
                    _ctx.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
        }
    }
}
