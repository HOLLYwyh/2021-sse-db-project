using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    /// <summary>
    /// 关注店铺管理中心
    /// </summary>
    public class FollowShopService : IFollowShopService
    {
        // 构造函数
        private readonly ModelContext _context;

        public FollowShopService(ModelContext context)
        {
            _context = context;
        }

        //添加关注
        public void addToFollowShop(string buyerid, string shopid)
        {
            FollowShop followShop = _context.FollowShops.Where(x => x.BuyerId == buyerid && x.ShopId == shopid).FirstOrDefault();
            if (followShop == null)
            {
                followShop = new FollowShop { BuyerId = buyerid, ShopId = shopid,  DateCreated = DateTime.Now };
                _context.FollowShops.Add(followShop);
            }

            _context.SaveChanges();
        }

        // 取消关注
        public async Task removeFollowShop(string buyerid, string shopid)
        {
            FollowShop followShop = _context.FollowShops.Where(x => x.BuyerId == buyerid && x.ShopId == shopid).FirstOrDefault();
            if (followShop != null)
            {
                _context.FollowShops.Remove(followShop);
                await _context.SaveChangesAsync();
            }
        }

        // 查看关注信息
        public IEnumerable<FollowShopView> GetCartProduct(string buyerid)
        {
            List<FollowShopView> followShops = new List<FollowShopView>();    //  关注信息显示类 列表

            // 查询商品信息
            var query = from followShop in _context.FollowShops
                        join buyer in _context.Buyers on followShop.BuyerId equals buyer.BuyerId
                        join shop in _context.Shops on followShop.ShopId equals shop.ShopId
                        where followShop.BuyerId == buyerid
                        select new
                        {
                            BuyerId = buyer.BuyerId,
                            ShopId = shop.ShopId,
                            ShopName = shop.Name,       
                            DateCreated = followShop.DateCreated
                        };

            foreach (var item in query)
            {
                FollowShopView followShopView = new FollowShopView();

                followShopView.BuyerId = item.BuyerId;
                followShopView.ShopId = item.ShopId;
                followShopView.DateCreated = (DateTime)item.DateCreated;
                followShopView.ShopName = item.ShopName;
                

                followShops.Add(followShopView);
            }

            return followShops;
        }
    }
}
