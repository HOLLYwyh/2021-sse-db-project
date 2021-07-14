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
        public bool addToFollowShop(string buyerid, string shopid)
        {
            FollowShop followShop = _context.FollowShops.Where(x => x.BuyerId == buyerid && x.ShopId == shopid).FirstOrDefault();
            if (followShop == null)
            {
                followShop = new FollowShop { BuyerId = buyerid, ShopId = shopid, DateCreated = DateTime.Now };
                _context.FollowShops.Add(followShop);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else            // 已存在，不许添加
                return true;
        }

        // 取消关注
        public bool removeFollowShop(string buyerid, string shopid)
        {
            FollowShop followShop = _context.FollowShops.Where(x => x.BuyerId == buyerid && x.ShopId == shopid).FirstOrDefault();
            if (followShop != null)
            {
                _context.FollowShops.Remove(followShop);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        // 清除所有关注
        public bool removeAllFollowShop(string buyerid)
        {
            List<FollowShop> followShops = _context.FollowShops.Where(x => x.BuyerId == buyerid).ToList();

            _context.FollowShops.RemoveRange(followShops);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;

        }

        // 查看关注信息
        public List<FollowShopView> getFollowShops(string buyerid)
        {
            List<FollowShopView> followShopViews = new List<FollowShopView>();    //  返回买家关注显示列表        

            List<FollowShop> followShops = _context.FollowShops.Where(x => x.BuyerId == buyerid).ToList();  // 买家关注列表

            foreach (FollowShop followShop in followShops)
            {
                Shop shop = _context.Shops.Where(x => x.ShopId == followShop.ShopId).FirstOrDefault();    // 关注的店铺

                List<Commodity> shopAllCommodities = _context.Commodities.Where(x => x.ShopId == shop.ShopId).ToList(); // 店铺的所有商品

                List<Commodity> shopOrderCommodities = shopAllCommodities.OrderBy(x => x.Soldnum).ToList();   // 商品列表排序

                List<Commodity> shopCommodities = new List<Commodity>();

                int count = 0;
                foreach (Commodity commodity in shopOrderCommodities)
                {
                    shopCommodities.Add(commodity);
                    count++;
                    if (count == 4)
                        break;
                }

                List<CommodityView> commodityViews = new List<CommodityView>();
                foreach (var shopcommodity in shopCommodities)
                {
                    CommodityView commodityView = new CommodityView
                    {
                        CommodityId = shopcommodity.CommodityId,
                        ShopId = shopcommodity.ShopId,
                        Url = shopcommodity.Url,
                        Category = shopcommodity.Category,
                        Name = shopcommodity.Name,
                        Price = shopcommodity.Price,
                        Storage = shopcommodity.Storage,
                        Soldnum = shopcommodity.Soldnum,
                        Description = shopcommodity.Description
                    };
                    commodityViews.Add(commodityView);
                }

                FollowShopView followShopView = new FollowShopView
                {
                    BuyerId = followShop.BuyerId,
                    ShopId = followShop.ShopId,
                    DateCreated = followShop.DateCreated,
                    ShopName = shop.Name,
                    Url = shop.Url,
                    commodityView = commodityViews
                };

                followShopViews.Add(followShopView);
            }

            return followShopViews;
        }

    }
}
