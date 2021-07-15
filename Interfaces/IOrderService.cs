using Internetmall.Models.BusinessEntity;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    /// <summary>
    /// 订单处理接口
    /// </summary>
    public interface IOrderService
    {
        //返回用户所有优惠券
        public List<CouponView> GetCoupons(string buyerId);
        // 根据用户Id返回其所有收货人信息
        public List<ReceiveInformation> GetReceiveInformation(string buyerId);
        public List<Good> RenderOrderPageFromDetail(string commodityId, int amount);
        // 从商品详情页创建订单
        public bool CreateOrderFromDetail(string buyerid, string commodityid, string receivedId,int amount,int price);
        // 从购物车创建订单
        public List<Good> RenderOrderPageFromCart(Cart newCart, string buyerId);
        // 更新订单状态
        public bool updateOrderStatus(string orderid, int newStatus);
        // 更新订单中商品的状态
        public bool updateCommodityStatus(string commodityid, int newStatus);
        // 查看买家所有订单
        public List<OrderInformationView> getOrderByBuyerId(string buyerid);

        // 根据状态查看买家订单
        public List<OrderView> getOrderByStatus(string buyerid, int status);
        // 查看订单详情
        public OrderDetailView getOrderByStatus(string orderid);
        // 删除订单
        public bool removeOrder(string buyerid, string commodityid);
        // 删除所有订单
        public bool removeAllOrder(string buyerid);
    }
}
