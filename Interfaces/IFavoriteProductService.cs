using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    /// <summary>
    /// 收藏夹管理接口
    /// </summary>
    public interface IFavoriteProductService
    {
        //添加收藏夹
        public bool addToFavorite(string buyerid, string commodityid);
       
        // 从收藏夹中删除
        public Task removeFromFavorite(string buyerid, string commodityid);
       
        // 清除收藏夹
        public Task removeAllFavorite(string buyerid);
         
        // 查看收藏夹
        public IEnumerable<FavoriteProductView> GetFavoriteProduct(string buyerid);   
    }
}
