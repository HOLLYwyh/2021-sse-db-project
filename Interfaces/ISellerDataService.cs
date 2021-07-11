using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    public interface ISellerDataService
    {
        // 店铺关注数量
        public Task<List<ShopFavoriteNum>> getShopFavotitesNum(string sellerid);       

        // 本月盈利
        public int getMonthProfit(string sellerid);       

        // 待发货总数
        public Task<List<ShopToBeShipNum>> getToBeShipNum(string sellerid);       

        // 退款售后订单总数       
        public Task<List<ShopCanceledOrderNum>> getCanceledOrderNum(string sellerid);     
    }
}
