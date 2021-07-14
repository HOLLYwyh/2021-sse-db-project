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
        public Good RenderOrderPageFromDetail(string commodityId, int amount);
        // 创建订单
        public bool createOrder(string buyerid, string commodityid, string receivedId,int amount);
        // 更新订单状态
        public bool updateOrderStatus(string orderid, int newStatus);
        // 更新订单中商品的状态
        public bool updateCommodityStatus(string commodityid, int newStatus);
        // 查看买家所有订单
        public string getOrderByBuyerId(string buyerid);
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
