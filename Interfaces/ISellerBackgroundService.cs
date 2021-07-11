using InternetMall.Models;
using InternetMall.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Interfaces
{
    interface ISellerBackgroundService
    {
        public Task<List<Shop>> ChoseShop(string sellerID);
        public Task<List<Order>> DisplayOrder(string shopID);
        public Task<List<Order>> SearchOrder(string orderId = null, string commodityId = null, string commodityName = null, string recieverName = null,string receiverPhone=null, string sellerId = null);
    }
}
