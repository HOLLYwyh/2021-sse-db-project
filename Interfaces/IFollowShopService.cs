using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    /// <summary>
    /// 关注店铺接口
    /// </summary>
    public interface IFollowShopService
    {
        //添加关注
        public bool addToFollowShop(string buyerid, string shopid);

        // 取消关注
        public bool removeFollowShop(string buyerid, string shopid);

        // 清除所有关注
        public bool removeAllFollowShop(string buyerid);

        // 查看关注信息
        public List<FollowShopView> getCartProduct(string buyerid);
    }
}
