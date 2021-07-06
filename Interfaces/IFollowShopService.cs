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
        public void addToFollowShop(string buyerid, string shopid);

        // 取消关注
        public Task removeFollowShop(string buyerid, string shopid);

        // 查看关注信息
        public IEnumerable<FollowShopView> GetCartProduct(string buyerid);
    }
}
