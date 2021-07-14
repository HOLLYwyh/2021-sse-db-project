using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using InternetMall.Models;

#nullable disable

namespace InternetMall.DBContext
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<AddShoppingCart> AddShoppingCarts { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<Buyer> Buyers { get; set; }
        public virtual DbSet<BuyerCoupon> BuyerCoupons { get; set; }
        public virtual DbSet<Chatroom> Chatrooms { get; set; }
        public virtual DbSet<Chatuser> Chatusers { get; set; }
        public virtual DbSet<Commodity> Commodities { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponShop> CouponShops { get; set; }
        public virtual DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public virtual DbSet<FollowShop> FollowShops { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrdersCommodity> OrdersCommodities { get; set; }
        public virtual DbSet<ReceiveInformation> ReceiveInformations { get; set; }
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ADMIN");

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("ACTIVITY");

                entity.Property(e => e.ActivityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ACTIVITY_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(2)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("DESCRIPTION");

                entity.Property(e => e.EndTime)
                    .HasColumnType("DATE")
                    .HasColumnName("END_TIME");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.StartTime)
                    .HasColumnType("DATE")
                    .HasColumnName("START_TIME");
            });

            modelBuilder.Entity<AddShoppingCart>(entity =>
            {
                entity.HasKey(e => new { e.BuyerId, e.CommodityId })
                    .HasName("SHOPPING_CART_PK");

                entity.ToTable("ADD_SHOPPING_CART");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.CommodityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COMMODITY_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_CREATED");

                entity.Property(e => e.Quantity)
                    .HasPrecision(6)
                    .HasColumnName("QUANTITY");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.AddShoppingCarts)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010024");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.AddShoppingCarts)
                    .HasForeignKey(d => d.CommodityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010025");
            });

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.ToTable("ADMINISTRATOR");

                entity.Property(e => e.AdministratorId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ADMINISTRATOR_ID");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("ID_NUMBER")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("PASSWD");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PHONE")
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.ToTable("BUYER");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.DateBirth)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_BIRTH");

                entity.Property(e => e.Gender)
                    .HasPrecision(1)
                    .HasColumnName("GENDER");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("ID_NUMBER")
                    .IsFixedLength(true);

                entity.Property(e => e.Nickname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("PASSWD");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PHONE")
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<BuyerCoupon>(entity =>
            {
                entity.HasKey(e => new { e.BuyerId, e.CouponId })
                    .HasName("BUYER_COUPON_PK");

                entity.ToTable("BUYER_COUPON");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.CouponId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COUPON_ID");

                entity.Property(e => e.Amount)
                    .HasPrecision(3)
                    .HasColumnName("AMOUNT");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.BuyerCoupons)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010048");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.BuyerCoupons)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010049");
            });

            modelBuilder.Entity<Chatroom>(entity =>
            {
                entity.ToTable("CHATROOM");

                entity.Property(e => e.Chatroomid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CHATROOMID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Type)
                    .HasColumnType("NUMBER(38)")
                    .HasColumnName("TYPE");
            });

            modelBuilder.Entity<Chatuser>(entity =>
            {
                entity.HasKey(e => new { e.Buyerid, e.Sellerid, e.Chatroomid })
                    .HasName("CHATUSER_PK");

                entity.ToTable("CHATUSER");

                entity.Property(e => e.Buyerid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYERID");

                entity.Property(e => e.Sellerid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SELLERID");

                entity.Property(e => e.Chatroomid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CHATROOMID");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Chatusers)
                    .HasForeignKey(d => d.Buyerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("BUYERFK");

                entity.HasOne(d => d.Chatroom)
                    .WithMany(p => p.Chatusers)
                    .HasForeignKey(d => d.Chatroomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ROOMFK");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Chatusers)
                    .HasForeignKey(d => d.Sellerid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SELLERFK");
            });

            modelBuilder.Entity<Commodity>(entity =>
            {
                entity.ToTable("COMMODITY");

                entity.Property(e => e.CommodityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COMMODITY_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(2)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Price)
                    .HasColumnType("NUMBER(11,2)")
                    .HasColumnName("PRICE");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.Soldnum)
                    .HasPrecision(10)
                    .HasColumnName("SOLDNUM")
                    .HasDefaultValueSql("0 ");

                entity.Property(e => e.Storage)
                    .HasPrecision(6)
                    .HasColumnName("STORAGE");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.Property(e => e.Description)
                   .HasMaxLength(1000)
                   .IsUnicode(false)
                   .HasColumnName("DESCRIPTION");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Commodities)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("SYS_C0010019");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("COUNTER");

                entity.Property(e => e.ID)
                   .HasMaxLength(10)
                   .IsUnicode(false)
                   .HasColumnName("ID");

                entity.Property(e => e.Buyercount)
                    .HasPrecision(10)
                    .HasColumnName("BUYERCOUNT");

                entity.Property(e => e.Sellercount)
                    .HasPrecision(10)
                    .HasColumnName("SELLERCOUNT");

                entity.Property(e => e.Administratorcount)
                    .HasPrecision(10)
                    .HasColumnName("ADMINISTRATORCOUNT");

                entity.Property(e => e.Commoditycount)
                    .HasPrecision(10)
                    .HasColumnName("COMMODITYCOUNT");

                entity.Property(e => e.Shopcount)
                    .HasPrecision(10)
                    .HasColumnName("SHOPCOUNT");

                entity.Property(e => e.Ordercount)
                   .HasPrecision(10)
                   .HasColumnName("ORDERCOUNT");

                entity.Property(e => e.Couponcount)
                   .HasPrecision(10)
                   .HasColumnName("COUPONCOUNT");

                entity.Property(e => e.Activitycount)
                   .HasPrecision(10)
                   .HasColumnName("ACTIVITYCOUNT");

                entity.Property(e => e.Receivedcount)
                   .HasPrecision(10)
                   .HasColumnName("RECEIVEDCOUNT");

                entity.Property(e => e.Messagecount)
                   .HasPrecision(10)
                   .HasColumnName("MESSAGECOUNT");

                entity.Property(e => e.Chatroomcount)
                   .HasPrecision(10)
                   .HasColumnName("CHATROOMCOUNT");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.ToTable("COUPON");

                entity.Property(e => e.CouponId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COUPON_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(2)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.CommodityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COMMODITY_ID");

                entity.Property(e => e.Discount1)
                    .HasColumnType("NUMBER(11,2)")
                    .HasColumnName("DISCOUNT_1");

                entity.Property(e => e.Discount2)
                    .HasColumnType("NUMBER(11,2)")
                    .HasColumnName("DISCOUNT_2");

                entity.Property(e => e.EndTime)
                    .HasColumnType("DATE")
                    .HasColumnName("END_TIME");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.StartTime)
                    .HasColumnType("DATE")
                    .HasColumnName("START_TIME");

                entity.Property(e => e.Threshold)
                    .HasColumnType("NUMBER(11,2)")
                    .HasColumnName("THRESHOLD");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.CommodityId)
                    .HasConstraintName("SYS_C0010043");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("SYS_C0010042");
            });

            modelBuilder.Entity<CouponShop>(entity =>
            {
                entity.HasKey(e => new { e.CouponId, e.ShopId })
                    .HasName("COUPON_SHOP_PK");

                entity.ToTable("COUPON_SHOP");

                entity.Property(e => e.CouponId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COUPON_ID");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.Amount)
                    .HasPrecision(9)
                    .HasColumnName("AMOUNT");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponShops)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010045");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.CouponShops)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010046");
            });

            modelBuilder.Entity<FavoriteProduct>(entity =>
            {
                entity.HasKey(e => new { e.BuyerId, e.CommodityId })
                    .HasName("PK");

                entity.ToTable("FAVORITE_PRODUCT");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.CommodityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COMMODITY_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_CREATED");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.FavoriteProducts)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010021");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.FavoriteProducts)
                    .HasForeignKey(d => d.CommodityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010022");
            });

            modelBuilder.Entity<FollowShop>(entity =>
            {
                entity.HasKey(e => new { e.ShopId, e.BuyerId })
                    .HasName("FOLLOW_SHOP_PK");

                entity.ToTable("FOLLOW_SHOP");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.DateCreated)
                    .HasColumnType("DATE")
                    .HasColumnName("DATE_CREATED");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.FollowShops)
                    .HasForeignKey(d => d.BuyerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010031");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.FollowShops)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010030");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("MESSAGE");

                entity.Property(e => e.Messageid)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGEID");

                entity.Property(e => e.Chatroomid)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("CHATROOMID");

                entity.Property(e => e.Text)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("TEXT");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("DATE")
                    .HasColumnName("TIMESTAMP");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.Chatroom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.Chatroomid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("MESSAGEFK");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrdersId)
                    .HasName("SYS_C0010034");

                entity.ToTable("ORDERS");

                entity.Property(e => e.OrdersId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ORDERS_ID");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.Orderamount)
                    .HasColumnType("NUMBER(11,2)")
                    .HasColumnName("ORDERAMOUNT");

                entity.Property(e => e.OrdersDate)
                    .HasColumnType("DATE")
                    .HasColumnName("ORDERS_DATE");

                entity.Property(e => e.ReceivedId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("RECEIVED_ID");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.Status)
                    .HasPrecision(1)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("SYS_C0010035");

                entity.HasOne(d => d.Received)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ReceivedId)
                    .HasConstraintName("SYS_C0010036");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ShopId)
                    .HasConstraintName("SYS_C0010037");
            });

            modelBuilder.Entity<OrdersCommodity>(entity =>
            {
                entity.HasKey(e => new { e.OrdersId, e.CommodityId })
                    .HasName("ORDERS_COMMODITY_PK");

                entity.ToTable("ORDERS_COMMODITY");

                entity.Property(e => e.OrdersId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("ORDERS_ID");

                entity.Property(e => e.CommodityId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("COMMODITY_ID");

                entity.Property(e => e.Status)
                    .HasPrecision(1)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.OrdersCommodities)
                    .HasForeignKey(d => d.CommodityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010040");

                entity.HasOne(d => d.Orders)
                    .WithMany(p => p.OrdersCommodities)
                    .HasForeignKey(d => d.OrdersId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SYS_C0010039");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER(6,0)")
                    .HasColumnName("AMOUNT");
            });

            modelBuilder.Entity<ReceiveInformation>(entity =>
            {
                entity.HasKey(e => e.ReceivedId)
                    .HasName("SYS_C0010032");

                entity.ToTable("RECEIVE_INFORMATION");

                entity.Property(e => e.ReceivedId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("RECEIVED_ID");

                entity.Property(e => e.BuyerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("BUYER_ID");

                entity.Property(e => e.City)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CITY")
                    .IsFixedLength(true);

                entity.Property(e => e.Country)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("COUNTRY");

                entity.Property(e => e.DetailAddr)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("DETAIL_ADDR");

                entity.Property(e => e.District)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("DISTRICT")
                    .IsFixedLength(true);

                entity.Property(e => e.Tag)
                   .HasMaxLength(10)
                   .IsUnicode(false)
                   .HasColumnName("TAG")
                   .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PHONE")
                    .IsFixedLength(true);

                entity.Property(e => e.Province)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("PROVINCE")
                    .IsFixedLength(true);

                entity.Property(e => e.ReceiverName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("RECEIVER_NAME");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.ReceiveInformations)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("SYS_C0010033");
            });

            modelBuilder.Entity<Seller>(entity =>
            {
                entity.ToTable("SELLER");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SELLER_ID");

                entity.Property(e => e.IdNumber)
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("ID_NUMBER")
                    .IsFixedLength(true);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.Nickname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NICKNAME");

                entity.Property(e => e.Passwd)
                    .IsRequired()
                    .HasMaxLength(18)
                    .IsUnicode(false)
                    .HasColumnName("PASSWD");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("PHONE")
                    .IsFixedLength(true);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("SHOP");

                entity.Property(e => e.ShopId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SHOP_ID");

                entity.Property(e => e.Category)
                    .HasPrecision(2)
                    .HasColumnName("CATEGORY");

                entity.Property(e => e.CreditScore)
                    .HasPrecision(2)
                    .HasColumnName("CREDIT_SCORE");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("NAME");

                entity.Property(e => e.SellerId)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("SELLER_ID");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("URL");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("SYS_C0010017");

                entity.Property(e => e.Description)
                   .HasMaxLength(1000)
                   .IsUnicode(false)
                   .HasColumnName("DESCRIPTION");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
