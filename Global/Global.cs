using Internetmall.Models.BusinessEntity;
using InternetMall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall
{
    public static class Global
    {
        //搜索部分
        public static string GSearchName = "";      //搜索时搜索名称
        public static string GCommodityType = "0";  //商品种类
        public static string GShopType = "0";       //店铺种类
        //商品详情部分
        public static string GCommodityID = "2";    //商品ID
        public static int   GCommodityNum = 1;      //商品详情页面购买数量
        //店铺详情部分
        public static string GShopId = "1";          //店铺ID
        //购买部分                                           
        public static int GConfirmOrderType = 1;    //购买类别  1-从商品详情页购买，2-从购物车购买
        //购物车部分
        public static Cart GCart;          //购物车中选中的物品
        //创建订单部分
        public static List<Good> GGoods;    //购物车中购买的商品
    }
}
