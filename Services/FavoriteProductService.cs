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
        public async Task removeFromFavorite(string buyerid, string commodityid)
        {
            FavoriteProduct favorite = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (favorite != null)
            {
                _context.FavoriteProducts.Remove(favorite);
                await _context.SaveChangesAsync();
            }
        }

        // 清除收藏夹
        public async Task removeAllFavorite(string buyerid)
        {
            var favorites = _context.FavoriteProducts.Where(x => x.BuyerId == buyerid);
            _context.FavoriteProducts.RemoveRange(favorites);
            await _context.SaveChangesAsync();
        }

        // 查看收藏夹
        public IEnumerable<FavoriteProductView> GetFavoriteProduct(string buyerid)
        {
            List<FavoriteProductView> favorites = new List<FavoriteProductView>();    //  收藏夹信息显示类 列表

            // 查询商品信息
            var query = from favoriteProduct in _context.FavoriteProducts
                        join comm in _context.Commodities on favoriteProduct.CommodityId equals comm.CommodityId
                        where favoriteProduct.BuyerId == buyerid
                        select new
                        {
                            BuyerId = favoriteProduct.BuyerId,
                            CommodityId = comm.CommodityId,
                            CommodityName = comm.Name,
                            DateCreated = favoriteProduct.DateCreated,
                            Price = (decimal)comm.Price
                        };

            foreach (var item in query)
            {
                FavoriteProductView favorite = new FavoriteProductView();

                favorite.BuyerId = item.BuyerId;
                favorite.CommodityId = item.CommodityId;
                favorite.CommodityName = item.CommodityName;
                favorite.DateCreated = (DateTime)item.DateCreated;
                favorite.Price = (decimal)item.Price;

                favorites.Add(favorite);
            }

            return favorites;
        }      
    }
}
