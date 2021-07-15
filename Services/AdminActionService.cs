using InternetMall.DBContext;
using InternetMall.Models;
using InternetMall.Models.BusinessEntity;
using InternetMall.Models.ControllerModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetMall.Services
{
    public class AdminActionService
    {
        private readonly ModelContext _context;

        public AdminActionService(ModelContext context)
        {
            _context = context;
        }

        /************************* 活动服务 *******************/
        // 发布活动
        public bool releaseActivity(string name, string category, DateTime startTime, DateTime endTime, string description, decimal? constrict, decimal? minus)
        {
            CreateIdCount createIdCount = new CreateIdCount(_context);
            string activityID = createIdCount.GetActivityCount();
            Activity activity = new Activity();

            activity.ActivityId = activityID;
            activity.Name = name;

            switch (category)
            {
                case "全场活动": activity.Category = 1; break;
                case "特类商品": activity.Category = 2; break;
                case "店铺活动": activity.Category = 3; break;
                case "单个商品": activity.Category = 4; break;
            }

            activity.StartTime = startTime;
            activity.EndTime = endTime;
            activity.Description = description;
            _context.Activities.Add(activity);   // 创建活动

            // 发布优惠券
            string couponID = createIdCount.GetCouponCount();
            Coupon coupon = new Coupon();
            coupon.CouponId = couponID;
            coupon.StartTime = startTime;
            coupon.EndTime = endTime;
            coupon.Threshold = constrict;
            coupon.Discount1 = minus;
            coupon.Discount2 = 0;
            switch (category)
            {
                case "全场活动": coupon.Category = 1; break;
                case "特类商品": coupon.Category = 2; break;
                case "店铺活动": coupon.Category = 3; break;
                case "单个商品": coupon.Category = 4; break;
            }
            coupon.ShopId = "1";
            coupon.ActivityId = activityID;
            _context.Coupons.Add(coupon);     // 添加优惠券
            if (_context.SaveChanges() < 0)   // 先注入优惠券，保证约束
                return false;

            // 给买家发布优惠券
            List<Buyer> buyers = _context.Buyers.Where(x => x.BuyerId != null).ToList();
            foreach (Buyer buyer in buyers)
            {
                BuyerCoupon buyerCoupon = new BuyerCoupon
                {
                    BuyerId = buyer.BuyerId,
                    CouponId = couponID,
                    Amount = 10
                };
                _context.BuyerCoupons.Add(buyerCoupon);     // 每个买家10张优惠券
            }

            // 给店铺发布优惠券
            CouponShop couponShop = new CouponShop
            {
                ShopId = "1",
                CouponId = couponID,
                Amount = 1000
            };
            _context.CouponShops.Add(couponShop);      // 每个店铺1000张优惠券

            if (_context.SaveChanges() > 0)
                return true;
            else
                return false;
        }

        // 查看指定活动
        public List<ActivityView> getOneActivity(string activityID)
        {
            if (activityID == "")
                return null;
            Activity activity = _context.Activities.Where(x => x.ActivityId == activityID).FirstOrDefault();

            List<ActivityView> activityViews = new List<ActivityView>();

            if (activity == null)
                return activityViews = null;

            ActivityView activityView = new ActivityView();
            activityView.ActivityId = activity.ActivityId;
            activityView.Name = activity.Name;
            activityView.StartTime = activity.StartTime;
            activityView.EndTime = activity.EndTime;
            switch (activity.Category)
            {
                case 1: activityView.Category = "全场活动"; break;
                case 2: activityView.Category = "特类商品"; break;
                case 3: activityView.Category = "店铺活动"; break;
                case 4: activityView.Category = "单个商品"; break;
            }
            activityView.Description = activity.Description;

            activityViews.Add(activityView);

            return activityViews;
        }

        // 查看所有活动
        public List<ActivityView> getActivities()
        {
            List<Activity> activities = _context.Activities.Where(x => x.ActivityId != null).ToList();
            List<ActivityView> activityViews = new List<ActivityView>();

            foreach (Activity activity in activities)
            {
                ActivityView activityView = new ActivityView();
                activityView.ActivityId = activity.ActivityId;
                activityView.Name = activity.Name;
                activityView.StartTime = activity.StartTime;
                activityView.EndTime = activity.EndTime;
                switch (activity.Category)
                {
                    case 1: activityView.Category = "全场活动"; break;
                    case 2: activityView.Category = "特类商品"; break;
                    case 3: activityView.Category = "店铺活动"; break;
                    case 4: activityView.Category = "单个商品"; break;
                }
                activityView.Description = activity.Description;

                activityViews.Add(activityView);
            }

            return activityViews;
        }

        // 删除活动
        public bool deleteOneActivity(string activityID)
        {
            Activity activity = _context.Activities.Where(x => x.ActivityId == activityID).FirstOrDefault();

            List<Coupon> coupons = _context.Coupons.Where(x => x.ActivityId == activityID).ToList();    // 活动关联的所有优惠券

            foreach (Coupon coupon in coupons)
            {
                // 删除店铺优惠券
                List<CouponShop> couponShops = _context.CouponShops.Where(x => x.CouponId == coupon.CouponId).ToList();
                foreach (CouponShop couponShop in couponShops)
                {
                    _context.CouponShops.Remove(couponShop);
                }

                // 删除买家优惠券
                List<BuyerCoupon> buyerCoupons = _context.BuyerCoupons.Where(x => x.CouponId == coupon.CouponId).ToList();
                foreach (BuyerCoupon buyerCoupon in buyerCoupons)
                {
                    _context.BuyerCoupons.Remove(buyerCoupon);
                }

                _context.SaveChanges();

                _context.Coupons.Remove(coupon);

                if (_context.SaveChanges() < 0)
                    return false;
            }

            if (activity != null)
            {
                _context.Activities.Remove(activity);
                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        /**************** 封禁用户 ************/
        public bool DeleteBuyer(string buyerId)
        {
            if (buyerId == "")
                return false;
            else
            {
                Buyer buyer = _context.Buyers.Where(x => x.BuyerId == buyerId).FirstOrDefault();
                buyer.Passwd = "000000";
                _context.Buyers.Update(buyer);

                if (_context.SaveChanges() > 0)
                    return true;
                else
                    return false;      
            }
        }

        public bool DeleteSeller(string sellerId)
        {
            if (sellerId == "")
                return false;
            else
            {
                if (_context.Sellers.Any(s => s.SellerId == sellerId))
                {
                    List<Shop> shopList = _context.Shops.Where(s => s.SellerId == sellerId).ToList();
                    foreach (Shop newShop in shopList)
                    {
                        string shopId = newShop.ShopId;
                        DeleteShop(shopId);
                    }
                    Seller newSeller = _context.Sellers.FirstOrDefault(s => s.SellerId == sellerId);
                    _context.Remove(newSeller);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return false;
            }

        }
            /**************** 下架商品 ***************/
        public bool DeleteCommodity(string commodityId)
        {
            if (commodityId == "")
                return false;
            else
            {
                if (_context.Commodities.Any(c => c.CommodityId == commodityId))
                {
                    Commodity newCommodity = _context.Commodities.FirstOrDefault(c => c.CommodityId == commodityId);
                    _context.Commodities.Remove(newCommodity);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }

        /***************** 封禁店铺 ***************/
        public bool DeleteShop(string shopId)
        {
            if (shopId == "")
                return false;
            else
            {
                if (_context.Shops.Any(s => s.ShopId == shopId))
                {
                    List<Commodity> commodityList = _context.Commodities.Where(c => c.ShopId == shopId).ToList();
                    foreach (Commodity newCommodity in commodityList)
                    {
                        string commodityId = newCommodity.CommodityId;
                        DeleteCommodity(commodityId);
                    }
                    Shop newShop = _context.Shops.FirstOrDefault(s => s.ShopId == shopId);
                    _context.Remove(newShop);
                    _context.SaveChanges();
                    return true;
                }
                else return false;
            }
        }
    }
}
