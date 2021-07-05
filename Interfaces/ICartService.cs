using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    /// <summary>
    /// 购物车管理接口
    /// </summary>
    public interface ICartService
    {
        //添加购物车
        public void addToCart(string buyerid, string commodityid);

        // 从购物车中删除
        public Task RemoveFromCart(string buyerid, string commodityid);
       
        // 清除购物车
        public Task RemoveAllCart(string buyerid);
        
        // 查看购物车
        public IEnumerable<CartView> GetCartProduct(string buyerid);
       
    }
}
