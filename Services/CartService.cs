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
        public bool addToCart(string buyerid, string commodityid,int number)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (cart == null)
            {
                cart = new AddShoppingCart { BuyerId = buyerid, CommodityId = commodityid, Quantity = number, DateCreated = DateTime.Now };
                _context.AddShoppingCarts.Add(cart);
            }

            else
            {
                cart.Quantity+= number;
                _context.AddShoppingCarts.Update(cart);
            }

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 从购物车中删除
        public bool RemoveFromCart(string buyerid, string commodityid)
        {
            AddShoppingCart cart = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid && x.CommodityId == commodityid).FirstOrDefault();
            if (cart != null)
            {
                _context.AddShoppingCarts.Remove(cart);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        // 清除购物车
        public bool RemoveAllCart(string buyerid)
        {
            List<AddShoppingCart> carts = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid).ToList();
            _context.AddShoppingCarts.RemoveRange(carts);
            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 渲染购物车里的商品
        public List<CartView> GetCartProduct(string buyerid)
        {
            List<CartView> shopCarts = new List<CartView>();    //  购物车信息显示类 列表        

            List<AddShoppingCart> carts = _context.AddShoppingCarts.Where(x => x.BuyerId == buyerid).ToList();

            foreach (AddShoppingCart cart in carts)
            {
                Commodity commodity = _context.Commodities.Where(x => x.CommodityId == cart.CommodityId).FirstOrDefault();

                Shop shop = _context.Shops.Where(x => x.ShopId == commodity.ShopId).FirstOrDefault();

                CartView cartview = new CartView
                {
                    errorCode = 0,
                    BuyerId = cart.BuyerId,
                    commodityId = cart.CommodityId,
                    CommodityName = commodity.Name,
                    DateCreated = cart.DateCreated,
                    ShopName = shop.Name,
                    shopId = shop.ShopId,
                    imgUrl = commodity.Url,
                    amount = cart.Quantity,
                    Price = commodity.Price * cart.Quantity
                };

                shopCarts.Add(cartview);
            }
            return shopCarts;
        }
    }
}

