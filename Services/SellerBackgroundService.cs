using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;

namespace InternetMall.Services
{
    public class SellerBackgroundServices: ISellerBackgroundService
    {
        private readonly ModelContext _context;
        public SellerBackgroundServices(ModelContext context)
        {
            _context = context;
        }

        public async Task<List<Shop>> ChoseShop(string sellerId)//筛选店铺
        {
            if(sellerId==null)
            {
                return null;
            }
            var modelContext = _context.Shops.Where(s => s.SellerId == sellerId).Include(s => s.Seller);
            return await modelContext.ToListAsync();
        }

        public async Task<List<Order>> DisplayOrder(string shopId)//展示订单
        {
            if (shopId == null)
            {
                return null;
            }
            var modelContext = _context.Orders.Where(o => o.ShopId == shopId).Include(o => o.Buyer).Include(o => o.Received).Include(o => o.Shop);
            return await modelContext.ToListAsync();
        }

        public async Task<List<Order>> SearchOrder(string orderId, string commodityId, string commodityName, string recieverName,string recieverPhone, string buyerId)//搜索订单
        {
            List<Order> newList;
            var modelContext = await _context.Orders.Include(o => o.Buyer)
                                                    .Include(o => o.Received)
                                                    .Include(o => o.Shop).ToListAsync();
            newList = modelContext;
            foreach(Order newOrder in newList)
            {

                if (orderId != null)
                {
                    if (newOrder.OrdersId != orderId)
                        newList.Remove(newOrder);
                }
                else if(buyerId != null)
                {
                    if (newOrder.BuyerId != buyerId)
                        newList.Remove(newOrder);
                }
                else if(commodityId != null)
                {
                    foreach(OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if(newOrdersCommodity.CommodityId != commodityId)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if(commodityName !=null)
                {
                    foreach (OrdersCommodity newOrdersCommodity in newOrder.OrdersCommodities)
                    {
                        if (newOrdersCommodity.Commodity.Name.Contains(commodityName) == false)
                            newOrder.OrdersCommodities.Remove(newOrdersCommodity);
                    }
                }
                else if(recieverName !=null)
                {
                    if (newOrder.Received.ReceiverName != recieverName)
                        newList.Remove(newOrder);
                }
                else if (recieverPhone != null)
                {
                    if (newOrder.Received.Phone != recieverPhone)
                        newList.Remove(newOrder);
                }
            }
            return  newList;
        }
    }
}
