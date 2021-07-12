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
            byte[] bytes = new byte[4];
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        private readonly ModelContext _context;

        public HomeService(ModelContext context)
        {
            _context = context;
        }

        //按登录状态推荐商品
        public  List<Good> RecommendingCommodities(bool inFo = false, string buyerId = null)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> tempResultList = new List<Commodity>();
            List<Good> goods = new List<Good>();
            if (inFo == true)
            {
                int[] judge1 = new int[10];//对于十个商品大类进行权重标记的数组
                List<Order> orderList =  _context.Orders.Where(o => o.BuyerId == buyerId).OrderByDescending(c => c.OrdersDate).Include(o => o.OrdersCommodities).ToList();//按时间对订单队列进行降序排序
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
                List<AddShoppingCart> shoppingCartList =  _context.AddShoppingCarts.Where(a => a.BuyerId == buyerId).ToList();
                foreach (AddShoppingCart newShoppingCart in shoppingCartList)//遍历该用户购物车中的所有商品
                {
                    int commodityCotegory = (int)newShoppingCart.Commodity.Category;//对于订单中遍历到的的商品种类，其权重加2
                    judge1[commodityCotegory] += 2;
                }
                int[] judge2 = new int[7];
                for (int i = 1; i <= 6; i++)//找出权重最大的前六种商品种类，记录在judge2数组中
                {
                    int maxIndex = i;
                    for (int j = 2; j < 10; j++)
                    {
                        if (judge1[j] > judge1[maxIndex])
                        {
                            maxIndex = j;
                        }
                    }
                    judge2[i] = maxIndex;
                    judge1[maxIndex] = -1;//0;
                }
                for (int i = 1; i <= 6; i++)
                {
                    List<Commodity> commoditiesList =  _context.Commodities.Where(c => c.Category == judge2[i]).Include(c => c.Shop).Include(c => c.OrdersCommodities).ToList();
                    int temp = random.Next(0, commoditiesList.Count-1);
                    tempResultList.Add(commoditiesList[temp]);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    int randCategory = random.Next(1, 9);
                    List<Commodity> commoditiesList =  _context.Commodities.Where(c => c.Category == randCategory).Include(c => c.Shop).Include(c => c.OrdersCommodities).ToList();
                    int temp = random.Next(0, commoditiesList.Count - 1);
                    tempResultList.Add(commoditiesList[temp]);
                }
            }
            foreach (Commodity newCommodity in tempResultList)
            {
                Good newGood = new Good();
                newGood.img = newCommodity.Url;
                newGood.intro = newCommodity.Name;
                newGood.shop = newCommodity.Shop.Name;
                newGood.ID = newCommodity.CommodityId;
                goods.Add(newGood);
            }
            return goods;
        }
        //按分区推荐商品
        public  List<Good> RecommendingZoneCommodities(int commodityCategory = -1)
        {
            Random random = new Random(GetRandomSeedbyGuid());
            List<Commodity> tempResultList = new List<Commodity>();
            List<Good> goods = new List<Good>();
            if (commodityCategory != -1)
            {
                List<Commodity> commoditiesList =  _context.Commodities.Where(c => c.Category == commodityCategory).Include(c => c.Shop).Include(c => c.OrdersCommodities).ToList();
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
                    newGood.ID = newCommodity.CommodityId;
                    goods.Add(newGood);
                }
                return goods;
            }
            else return null;
        }
    }
}
