# 数据库课程设计项目

“英豪荟萃”网购平台

##  一、项目简介

​			网上购物系统具有强大的交互功能，可使商家和用户方便的传递信息，这种全新的交易方式实现了商家和用户，商家和商家的跨地域，无纸化信息交流。所以一个操作简单，交互方便，安全可靠的网上购物平台是很被需要的。

​			本项目是一个采用.net开发并以Oracle作为数据库开发的一个网络购物平台。

##  二、功能简介

​		系统功能性需求大体分为买家功能、卖家功能、管理员功能、系统功能。

- 买家功能

个人信息管理，消息系统（如联系客服），浏览商品，收藏关注商品或店铺，购买商品，查看订单等等。

- 卖家功能

个人信息管理，店铺管理，处理订单等。

- 管理员功能

个人信息管理，发布活动，下架商品等。

- 系统功能

维护订单状态以及显示商品。

##  三、小组成员

| 姓名                           | 学号    | 贡献度 |
| ------------------------------ | ------- | ------ |
| 吴英豪<sup>[Team Leader]</sup> | 1953608 | 100%   |
| 李晓恒                         | 1653014 | 100%   |
| 宁泰                           | 1952104 | 100%   |
| 高天宸                         | 1953068 | 100%   |
| 沈星宇                         | 1951576 | 100%   |
| 李林飞                         | 1951976 | 100%   |
| 王文炯                         | 1953281 | 100%   |
| 卢子昂                         | 1952214 | 100%   |
| 邵国诚                         | 1952111 | 100%   |
| 黄金坤                         | 1952211 | 100%   |

##  四、开发环境配置

### 一、Oracle数据库配置

#### 1. 云服务器配置数据库

由于是多人项目，需要多个人同时访问数据库，故本项目采用 **云服务器** 来部署Oracle，使用云服务器安装Docker并配置Oracle数据库可以参考一下的博客：

[docker 搭建Oracle-12C](https://www.freesion.com/article/9451481751/)

#### 2.本地配置数据库

本地安装并配置Oracle数据库的方法有很多，由于本项目未采用此方法，大家可以自行在网上搜索安装与配置步骤。

#### 3.数据库建表

本项目数据库一共需要18个表，有管理员、买家、卖家等，建表的SQL语句如下(Oracle语法)：

**这些表的设计已经满足第一范式和第三范式，可放心使用**

```sql
 -- 卖家ok
CREATE TABLE Seller (
        seller_id VARCHAR2(6) NOT NULL,
        phone CHAR(11) NOT NULL, 
        passwd VARCHAR2(18) NOT NULL,
        nickname VARCHAR2(30) NOT NULL,
        id_number CHAR(18) NOT NULL,
        seller_name VARCHAR2(30) NOT NULL,
        url varchar(255),
        CONSTRAINT pk_Seller PRIMARY KEY (seller_id)
        );
        
        
 -- 买家ok
CREATE TABLE Buyer (
        buyer_id VARCHAR2(6) NOT NULL, 
        phone Char(11) NOT NULL, 
        passwd VARCHAR2(18) NOT NULL,
        nickname VARCHAR2(30) NOT NULL,
        gender NUMBER(1) NOT NULL,
        date_birth DATE NOT NULL, 
        id_number CHAR(18) NOT NULL,
        url varchar(225),
        CONSTRAINT pk_Buyer PRIMARY KEY (buyer_id)
        );

 
-- 活动ok
CREATE TABLE Activity (
        activity_id VARCHAR2(6) NOT NULL,
        start_time DATE NOT NULL,
        end_time DATE NOT NULL, 
        name VARCHAR2(30) NOT NULL,
        category NUMBER(2) NOT NULL,
        description varchar2(200),
        CONSTRAINT pk_Activity PRIMARY KEY (activity_id)
        );


-- 店铺ok
CREATE TABLE Shop (
        shop_id VARCHAR2(6) NOT NULL,
        seller_id VARCHAR2(6) NOT NULL,
        name VARCHAR2(30) NOT NULL,
        credit_score  NUMBER(2) NOT NULL,
        category  NUMBER(2) NOT NULL,
        url varchar(255),
        Description varchar(1000),
        CONSTRAINT pk_Shop PRIMARY KEY (shop_id),
        CONSTRAINT fk_Shop FOREIGN KEY (seller_id)
                REFERENCES Seller(seller_id)
        );
        
     
-- 商品集ok
CREATE TABLE Commodity (
        commodity_id VARCHAR2(6) NOT NULL,
        price   NUMBER(11,2) NOT NULL,
        category  NUMBER(6) NOT NULL,
        storage  NUMBER(6) NOT NULL,
        name   VARCHAR2(30) NOT NULL,
        shop_id  VARCHAR2(6) NOT NULL,
        url varchar(255),
        soldnum number(10),
        Description varchar(1000),
        CONSTRAINT pk_Commodity PRIMARY KEY (commodity_id),
        CONSTRAINT fk_Commodity FOREIGN KEY (shop_id) 
                REFERENCES Shop(shop_id)
        );
   
-- 买家收获信息ok
CREATE TABLE ReceivedInformation (
        received_id VARCHAR2(6) NOT NULL,
        buyer_id VARCHAR2(6) NOT NULL, 
        phone CHAR(11) NOT NULL,
        receiver_name VARCHAR2(30) NOT NULL,
        country VARCHAR2(3) NOT NULL,
        province CHAR(3) NOT NULL,
        city CHAR(3) NOT NULL,
        district CHAR(3) NOT  NULL,
        detail_addr VARCHAR2(150) NOT NULL,
        tag varchar(10),
        CONSTRAINT pk_ReceivedInformation PRIMARY KEY (received_id),
        CONSTRAINT fk_ReceivedInformation FOREIGN KEY (buyer_id)
            REFERENCES Buyer(buyer_id)
        );
           
-- 订单ok
CREATE TABLE Orders (
        orders_id VARCHAR2(6) NOT NULL,
        buyer_id VARCHAR2(6) NOT NULL,
        received_id VARCHAR2(6) NOT NULL,
        shop_id VARCHAR2(6) NOT NULL,
        orders_date DATE NOT NULL,
        status  NUMBER(1) NOT NULL,
        Orderamount number(11,2),
        CONSTRAINT pk_Orders PRIMARY  KEY  (orders_id),
        CONSTRAINT fk1_Orders FOREIGN KEY(received_id)
                REFERENCES ReceivedInformation(received_id),
        CONSTRAINT fk2_Orders FOREIGN KEY (shop_id)
                 REFERENCES Shop(shop_id),
        CONSTRAINT fk3_Orders FOREIGN KEY (buyer_id)
                 REFERENCES Buyer(buyer_id)

        );


 -- 管理员ok
CREATE TABLE Administrator (
        administrator_id VARCHAR2(6) NOT NULL,
        phone CHAR(11) NOT NULL,
        passwd VARCHAR2(18) NOT NULL,
        nickname VARCHAR2(30) NOT NULL,
        id_number CHAR(18) NOT NULL,
        name VARCHAR2(30) NOT NULL,
        url varchar(255),
        CONSTRAINT pk_Admin PRIMARY KEY (administrator_id)
        );
 

-- 优惠券ok
CREATE TABLE Coupon (
        coupon_id VARCHAR2(6) NOT NULL,
        start_time DATE not NULL,
        end_time DATE NOT NULL,
        threshold  NUMBER(11,2) NOT NULL,
        discount_1  NUMBER(11,2) DEFAULT (0),
        discount_2  NUMBER(2,2) DEFAULT (0),
        category NUMBER(2) NOT NULL,
        shop_id VARCHAR2(6),
        activity_id VARCHAR2(6),
        CONSTRAINT pk_Coupon PRIMARY KEY (coupon_id),
        CONSTRAINT fk1_Coupon FOREIGN KEY(activity_id)
                REFERENCES Activity(activity_id),
        CONSTRAINT fk2_Coupon FOREIGN KEY (shop_id)
                 REFERENCES Shop(shop_id)
        );
        
-- 收藏商品    ok
 CREATE TABLE Favorite_Product (
        buyer_id VARCHAR(6) NOT NULL,
        commodity_id VARCHAR(6) NOT NULL,
        DateCreated date not null,
        CONSTRAINT pk_Favorite_Product PRIMARY KEY (buyer_id, commodity_id),
        CONSTRAINT fk1_Favorite_Product FOREIGN KEY(buyer_id)
                REFERENCES Buyer(buyer_id),
        CONSTRAINT fk2_Favorite_Product FOREIGN KEY (commodity_id)
                 REFERENCES Commodity(commodity_id)
        );

-- 加入购物车ok
CREATE TABLE Add_Shopping_Cart (
        buyer_id VARCHAR2(6) NOT NULL,
        commodity_id VARCHAR2(6) NOT  NULL,
        Quantity number(6),
        DateCreated date not null,
        CONSTRAINT pk_Shopping_Cart PRIMARY KEY (buyer_id, commodity_id),
        CONSTRAINT fk1_Add_Shopping_Cart FOREIGN KEY(buyer_id)
                REFERENCES Buyer(buyer_id),
        CONSTRAINT fk2_Add_Shopping_Cart FOREIGN KEY (commodity_id)
                 REFERENCES Commodity(commodity_id)
        );
        

 -- 关注店铺ok
CREATE TABLE Follow_Shop (
        buyer_id VARCHAR2(6) NOT NULL, 
        shop_id VARCHAR2(6) NOT NULL,
        DateCreated date not null,
        CONSTRAINT pk_Follow_Shop PRIMARY KEY (buyer_id, shop_id),
        CONSTRAINT fk1_Follow_Shop FOREIGN KEY(buyer_id)
                REFERENCES Buyer(buyer_id),
        CONSTRAINT fk2_Follow_Shop FOREIGN KEY (shop_id)
                 REFERENCES Shop(shop_id)
        );


-- 店铺关联优惠券ok
CREATE TABLE Coupon_Shop ( 
        coupon_id VARCHAR2(6) NOT NULL, 
        shop_id VARCHAR2(6) NOT NULL, 
        amount NUMBER(9) NOT NULL,
        CONSTRAINT pk_Coupon_Shop PRIMARY KEY (coupon_id, shop_id),
        CONSTRAINT fk1_Coupon_Shop FOREIGN KEY(coupon_id)
                REFERENCES Coupon(coupon_id),
        CONSTRAINT fk2_Coupon_Shop FOREIGN KEY (shop_id)
                 REFERENCES Shop(shop_id)
        );

-- 买家拥有优惠券
CREATE TABLE Buyer_Coupon ( 
        buyer_id VARCHAR2(6) NOT NULL, 
        coupon_id VARCHAR2(6) NOT NULL,
        amount NUMBER(3) NOT NULL,
        CONSTRAINT pk_Buyer_Coupon PRIMARY KEY (buyer_id, coupon_id),
        CONSTRAINT fk1_Buyer_Coupon FOREIGN KEY (buyer_id)
                REFERENCES Buyer(buyer_id),
        CONSTRAINT fk2_Buyer_Coupon FOREIGN KEY (coupon_id)
                 REFERENCES Coupon(coupon_id)
        );

/*新加了4个表*/
create table Counter(
        id varchar(10),
        buyercount number(10),
        sellercount number(10),
        administratorcount number(10),
        commoditycount number(10),
        shopcount number(10),
        ordercount number(10),

        couponcount number(10),
        activitycount number(10),
        receivedcount number(10),
        messagecount number(10),
        chatroomcount number(10),        

        CONSTRAINT pk_Counter PRIMARY KEY (id),
);

create table chatroom (
        chatroomid varchar(6),
        name varchar(50),
        type varchar(30),
        CONSTRAINT pk_chatroom PRIMARY KEY (chatroomid)
);


create table message(
        messageid varchar(6),
        chatroomid varchar(6),
        text varchar(500),
        timestamp date,
        username varchar(30),
        CONSTRAINT pk_message PRIMARY KEY (messageid),
        CONSTRAINT fk1_message FOREIGN KEY (chatroomid)
                REFERENCES chatroom(chatroomid)
);

create table chatuser(
        buyer_id varchar(6),
        seller_id varchar(6),
        chatroomid varchar(6),
        CONSTRAINT pk_chatuser PRIMARY KEY (buyer_id,seller_id,chatroomid),
        CONSTRAINT fk1_chatuser FOREIGN KEY (buyer_id)
                REFERENCES buyer(buyer_id),
        CONSTRAINT fk2_chatuser FOREIGN KEY (seller_id)
                REFERENCES seller(seller_id),
        CONSTRAINT fk3_chatuser FOREIGN KEY (chatroomid)
                REFERENCES chatroom(chatroomid)
        
)

```



###  二、.Net   环境配置

####  1. 建立.net Core MVC项目

本项目使用  .net Core 的MVC框架，故首先需要建立一个.net Core的MVC项目

(对应的.netFramework版本可以随意选择，不影响项目运行)

####  2. 连接数据库

在项目的配置文件appsettings.config中连接数据库

**（考虑到安全性问题，GitHub上为上传本项目的appsettings.config,大家可以自行配置本地的文件）**

在文件最后加入以下内容，汉字部分根据实际情况修改即可

```json
"AllowedHosts": "*",
  "ConnectionStrings": {
    "OracleDBContext": "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=数据库所在的ID地址)(PORT=端口))(CONNECT_DATA=(SERVICE_NAME=服务名)));User ID=用户名;Password=密码"
  }



```

####  3. 安装相关的NuGet包

- AWSSDK.Core    （JSON相关）  

- Microsoft.AspNetCore.Mvc.Razor.RuntimeCompilation  （这个好像是系统自带的？）  
- Microsoft.EntityFrameworkCore (数据库相关)  
- Oracle.EntityFrameworkCore (数据库相关)  
- Newtonsoft.Json(JSON相关)
- Alipay.AopSdk.Core(支付宝接口相关)

##  五、功能截屏展示

- 首页

  ![image-20210905165051817](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905165051817.png)

- 买家登录

  ![image-20210905165108213](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905165108213.png)

- 个人中心

  ![image-20210905185012668](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185012668.png)

- 购物车

  ![image-20210905185313613](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185313613.png)

- 商品详情

  ![image-20210905185201458](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185201458.png)

- 店铺详情

  ![image-20210905185123579](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185123579.png)

- 订单确认

  ![image-20210905185228068](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185228068.png)

- 搜索页面

  ![image-20210905185054887](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185054887.png)

- 卖家后台

  ![image-20210905185855091](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905185855091.png)

- 管理员中心

  ![image-20210905190245449](C:\Users\吴英豪\AppData\Roaming\Typora\typora-user-images\image-20210905190245449.png)













