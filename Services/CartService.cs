using InternetMall.DBContext;
using InternetMall.Interfaces;
using InternetMall.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetMall.Models.BusinessEntity;

namespace InternetMall.Services
{
    /// <summary>
    /// 购物车管理中心
    /// </summary>
    public class CartService : ICartService
    {
        // 构造函数
        private readonly ModelContext _context;

        public CartService(ModelContext context)
        {
            _context = context;
        }

        //添加购物车
        public void addToCart(string buyerid, string commodityid)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (cart == null)
            {
                cart = new AddShoppingCart { BuyerId = buyerid, CommodityId = commodityid, Quantity = 1, DateCreated = DateTime.Now };
                _context.AddShoppingCarts.Add(cart);
            }

            else
                cart.Quantity++;

            _context.SaveChanges();
        }

        // 从购物车中删除
        public async Task RemoveFromCart(string buyerid, string commodityid)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x=>x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if(cart != null)
            {
                _context.AddShoppingCarts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        // 清除购物车
        public async Task RemoveAllCart(string buyerid)
        {
            var carts = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid);
            _context.AddShoppingCarts.RemoveRange(carts);
            await _context.SaveChangesAsync();
        }

        // 查看购物车
        public IEnumerable<CartView> GetCartProduct(string buyerid)
        {
            List<CartView> shopCart = new List<CartView>();    //  购物车信息显示类 列表


            // var shopCart = _context.AddShoppingCarts.Where(a => a.BuyerId == buyerid).Include(a => a.Buyer).Include(a=>a.Commodity).ThenInclude(c=>c.Shop).ToList();
            
            // 查询商品信息
            var query = from cart in _context.AddShoppingCarts
                        join buyer in _context.Buyers on cart.BuyerId equals buyer.BuyerId
                        join comm in _context.Commodities on cart.CommodityId equals comm.CommodityId
                        join shop in _context.Shops on buyer.BuyerId equals shop.SellerId
                        where cart.BuyerId == buyerid         //@李林飞
                        select new
                        {
                            BuyerId = buyer.BuyerId,
                            CommodityId = cart.CommodityId,
                            CommodityName = comm.Name,
                            ShopName = shop.Name,
                            Quantity = cart.Quantity,
                            DateCreated = cart.DateCreated,
                            Price = comm.Price
                        };           

            foreach (var item in query)
            {
                CartView cartview = new CartView();

                cartview.BuyerId = item.BuyerId;
                cartview.CommodityId = item.CommodityId;
                cartview.CommodityName = item.CommodityName;
                cartview.DateCreated = (DateTime)item.DateCreated;
                cartview.ShopName = item.ShopName;
                cartview.Quantity = item.Quantity;
                cartview.Price = (decimal)item.Price;

                shopCart.Add(cartview);
            }

            return shopCart;       
        }

        
    }
}

