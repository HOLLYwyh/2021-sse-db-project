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
        private ModelContext _context;
        public SecurityService(ModelContext ctx)
        {
            _context = ctx;
        }

        // 返回Buyer所有信息
        public Buyer getBuyInformation(string buyerid)
        {
            Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerid).FirstOrDefault();
            if (buyer != null)
                return buyer;
            else
                return null;
        }

        // 显示用户绑定手机号
        public string displayPhone(string buyerid)
        {
            // 检测用户是否存在，但应该没有必要，因为已经登录
            /*if(_ctx.Buyers.Any(e => e.BuyerId == id))
            */
            Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerid).FirstOrDefault();
            // 电话号码需要检测是否存在
            if (buyer != null && buyer.Phone != null)
                return buyer.Phone;
            else
                return null;
        }

        // 显示用户密码
        public string displayPasswd(string buyerid)
        {
            Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerid).FirstOrDefault();
            if (buyer != null && buyer.Passwd != null)
                return buyer.Passwd;
            else
                return null;
        }

        // 修改用户绑定的手机号码
        public bool updatePhone(string buyerid, string oldPhone, string newPhone)
        {
            Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerid).FirstOrDefault();

            if (buyer != null)
            {
                if (buyer.Phone == null || buyer.Phone == oldPhone)   // 电话号码比对
                {
                    buyer.Phone = newPhone;
                    _context.Buyers.Update(buyer);

                    if (_context.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
                else      // 电话号码比对错误
                    return false;
            }
            else
                return false; // 该用户不存在
        }

        // 修改用户密码
        public bool updatePasswd(string buyerid, string oldPasswd, string newPasswd)
        {
            Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerid).FirstOrDefault();

            if (buyer != null)
            {
                if (buyer.Passwd == null || buyer.Passwd == oldPasswd)   // 电话号码比对
                {
                    buyer.Passwd = newPasswd;
                    _context.Buyers.Update(buyer);

                    if (_context.SaveChanges() > 0)
                        return true;
                    else
                        return false;
                }
                else  // 密码比对错误
                    return false;
            }
            else
                return false; // 该用户不存在
        }
    }
}
