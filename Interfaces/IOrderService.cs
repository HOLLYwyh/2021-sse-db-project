using InternetMall.Models;
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
        // 根据买家id查看所有订单
        public Task<Order> getOrderByBuyerid(string buyerid);

        // 创建订单
        public void createOrder(string buyerid, string commodityid);

        // 删除订单
        public Task removeOrder(string buyerid, string commodityid);

        // 删除所有订单
        public Task removeAllOrder(string buyerid);    
    }
}
