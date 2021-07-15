using InternetMall.DBContext;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    /// <summary>
    /// 买家优惠券
    /// </summary>
    public class BuyerCouponService
    {
        private readonly ModelContext _context;

        public BuyerCouponService(ModelContext context)
        {
            _context = context;
        }


        // 查看买家优惠券
        public List<BuyerCouponView> getBuyerCoupns(string buyerid)
        {
            List<BuyerCoupon> buyerCoupons = _context.BuyerCoupons.Where(x => x.BuyerId == buyerid).ToList();
            List<BuyerCouponView> buyerCouponViews = new List<BuyerCouponView>();

            foreach (BuyerCoupon buyerCoupon in buyerCoupons)
            {
                Coupon coupon = _context.Coupons.Where(x => x.CouponId == buyerCoupon.CouponId).FirstOrDefault();
                BuyerCouponView buyerCouponView = new BuyerCouponView();

                buyerCouponView.CouponId = coupon.CouponId;
                buyerCouponView.ActivityId = coupon.ActivityId;
                buyerCouponView.ShopId = coupon.ShopId;
                buyerCouponView.StartTime = coupon.StartTime;
                buyerCouponView.EndTime = coupon.EndTime;
                buyerCouponView.Threshold = coupon.Threshold;
                buyerCouponView.Discount1 = coupon.Discount1;
                switch (coupon.Category)
                {
                    case 1: buyerCouponView.Category = "全场活动"; break;
                    case 2: buyerCouponView.Category = "特类商品"; break;
                    case 3: buyerCouponView.Category = "店铺活动"; break;
                    case 4: buyerCouponView.Category = "单个商品"; break;
                }

                buyerCouponViews.Add(buyerCouponView);
            }
            return buyerCouponViews;
        }

        // 使用优惠券
        public bool deleteBuyerCoupon(string couponid)
        {
            BuyerCoupon buyerCoupon = _context.BuyerCoupons.Where(x => x.CouponId == couponid).FirstOrDefault();
            if (buyerCoupon.Amount > 0)
            {
                buyerCoupon.Amount--;
                _context.BuyerCoupons.Update(buyerCoupon);
            }
            else
            {
                _context.BuyerCoupons.Remove(buyerCoupon);
            }

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }



       
    }
}
