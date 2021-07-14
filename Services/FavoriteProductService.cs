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
    /// 收藏夹管理中心
    /// </summary>
    public class FavoriteProductService : IFavoriteProductService
    {
        // 构造函数
        private readonly ModelContext _context;

        public FavoriteProductService(ModelContext context)
        {
            _context = context;
        }

        //添加收藏夹
        public bool addToFavorite(string buyerid, string commodityid)
        {
            FavoriteProduct favorite = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (favorite == null)
            {
                favorite = new FavoriteProduct { BuyerId = buyerid, CommodityId = commodityid, DateCreated = DateTime.Now };
                _context.FavoriteProducts.Add(favorite);

                if(_context.SaveChanges()>0)
                {
                    return true;
                }
                return false; 
            }
            else
            {
                return true;
            }
        }

        // 从收藏夹中删除
        public bool removeFromFavorite(string buyerid, string commodityid)
        {
            FavoriteProduct favorite = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (favorite != null)
            {
                _context.FavoriteProducts.Remove(favorite);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        // 清除所有收藏
        public bool removeAllFavorite(string buyerid)
        {
            List<FavoriteProduct> favorites = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid).ToList();
            _context.FavoriteProducts.RemoveRange(favorites);

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 查看收藏夹所有商品信息
        public List<FavoriteProductView> getFavoriteProduct(string buyerid)
        {
            List<FavoriteProductView> favoriteProductViews = new List<FavoriteProductView>();    // 收藏商品信息List     

            List<FavoriteProduct> favoriteProducts = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid).ToList();

            foreach (FavoriteProduct favorite in favoriteProducts)
            {
                Commodity commodity = _context.Commodities.Where(x => x.CommodityId == favorite.CommodityId).FirstOrDefault();

                Shop shop = _context.Shops.Where(x => x.ShopId == commodity.ShopId).FirstOrDefault();

                FavoriteProductView favoriteview = new FavoriteProductView
                {
                    BuyerId = favorite.BuyerId,
                    CommodityId = favorite.CommodityId,
                    CommodityImg = commodity.Url,
                    CommodityName = commodity.Name,
                    ShopName = shop.Name,
                    DateCreated = favorite.DateCreated,
                    Price = commodity.Price
                };

                favoriteProductViews.Add(favoriteview);
            }
            return favoriteProductViews;
        }
    }
}
