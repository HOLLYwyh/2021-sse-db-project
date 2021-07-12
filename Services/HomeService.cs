using Internetmall.Interfaces;
using Internetmall.Models;
using Internetmall.Models.BusinessEntity;
using InternetMall.DBContext;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internetmall.Services
{
    public class HomeService : IHomeService
    {
        static int GetRandomSeedbyGuid()
        {
            return new Guid().GetHashCode();
        }
        private readonly ModelContext _context;

        public HomeService(ModelContext context)
        {
            _context = context;
        }

        //按登录状态推荐商品
        public async Task<List<Good>> RecommendingCommodities(bool inFo = false, string buyerId = null)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> tempResultList = new List<Commodity>();
            List<Good> goods = new List<Good>();
            if (inFo == true)
            {
                int[] judge1 = new int[10];//对于十个商品大类进行权重标记的数组
                List<Order> orderList = await _context.Orders.Where(o => o.BuyerId == buyerId).OrderByDescending(c => c.OrdersDate).Include(o => o.OrdersCommodities).ToListAsync();//按时间对订单队列进行降序排序
                int count = 0;
                orderList.Sort();
                foreach (Order newOrder in orderList)//遍历该用户的最近的五个订单，若订单数少于五个则遍历所有订单
                {
                    count++;
                    if (count == 6)
                        break;
                    foreach (OrdersCommodity newOC in newOrder.OrdersCommodities)//遍历该订单中的所有商品
                    {
                        int commodityCotegory = (int)newOC.Commodity.Category;
                        judge1[commodityCotegory]++;//对于订单中遍历到的的商品种类，其权重加1
                    }
                }
                List<AddShoppingCart> shoppingCartList = await _context.AddShoppingCarts.Where(a => a.BuyerId == buyerId).ToListAsync();
                foreach (AddShoppingCart newShoppingCart in shoppingCartList)//遍历该用户购物车中的所有商品
                {
                    int commodityCotegory = (int)newShoppingCart.Commodity.Category;//对于订单中遍历到的的商品种类，其权重加2
                    judge1[commodityCotegory] += 2;
                }
                int[] judge2 = new int[6];
                for (int i = 1; i <= 6; i++)//找出权重最大的前六种商品种类，记录在judge2数组中
                {
                    int maxIndex = 1;
                    for (int j = 2; j < 10; j++)
                    {
                        if (judge1[j] > judge1[maxIndex])
                        {
                            maxIndex = j;
                        }
                    }
                    judge2[i] = judge1[maxIndex];
                    judge1[maxIndex] = 0;
                }
                for (int i = 1; i <= 6; i++)
                {
                    List<Commodity> commoditiesList = await _context.Commodities.Where(c => c.Category == judge2[i]).Include(c => c.Shop).Include(c => c.OrdersCommodities).ToListAsync();
                    int temp = random.Next(0, commoditiesList.Capacity-1);
                    tempResultList.Add(commoditiesList[temp]);
                }
            }
            else
            {
                List<Commodity> commoditiesList = await _context.Commodities.Include(c => c.Shop).Include(c => c.OrdersCommodities).ToListAsync();
                for (int i = 0; i < 6; i++)
                {
                    int temp1 = random.Next(0,15);
                    string temp2 = temp1.ToString();
                    tempResultList.Add(commoditiesList.FirstOrDefault(c => c.CommodityId == temp2));
                }
            }
            foreach (Commodity newCommodity in tempResultList)
            {
                Good newGood = new Good();
                newGood.img = newCommodity.Url;
                newGood.intro = newCommodity.Name;
                newGood.shop = newCommodity.Shop.Name;
                newGood.ID = newCommodity.Shop.ShopId;
                goods.Add(newGood);
            }
            return goods;
        }
        //按分区推荐商品
        public async Task<List<Good>> RecommendingZoneCommodities(int commodityCategory = -1)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> tempResultList = new List<Commodity>();
            List<Good> goods = new List<Good>();
            if (commodityCategory != -1)
            {
                List<Commodity> commoditiesList = await _context.Commodities.Where(c => c.Category == commodityCategory).Include(c => c.Shop).Include(c => c.OrdersCommodities).ToListAsync();
                for (int i = 0; i < 8; i++)
                {
                    int temp = random.Next(0, commoditiesList.Capacity - 1);
                    tempResultList.Add(commoditiesList[temp]);
                }
                foreach (Commodity newCommodity in tempResultList)
                {
                    Good newGood = new Good();
                    newGood.img = newCommodity.Url;
                    newGood.intro = newCommodity.Name;
                    newGood.shop = newCommodity.Shop.Name;
                    goods.Add(newGood);
                }
                return goods;
            }
            else return null;
        }
    }
}
