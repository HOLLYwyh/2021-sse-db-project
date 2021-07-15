Vue.component('tabs', {
    template: `
    <el-tabs v-model="activeName" @tab-click="handleClick" >
        <el-tab-pane label="按关注时间" name="1"></el-tab-pane>
    </el-tabs >
    `,
    data: function () {
        return {
            activeName: "1",
        };
    },
    methods: {
        handleClick(tab, event) {
            console.log(tab, event);
        },

    }
})
Vue.component('index', {
    template: `
  <el-container direction="vertical">
    <h3>订单中心</h3>
    <el-link :underline="false" href="/Account/orders">历史订单</el-link>  

    <h3>我的钱包</h3>
    <el-link :underline="false" href="/Purchase/shoppingCart">购物车</el-link>  
    <el-link :underline="false" href="/Account/coupon">优惠券</el-link>  

    <h3>我的关注</h3>
    <el-link :underline="false" href="/Favorites/favorite">关注商品</el-link>  
    <el-link :underline="false" disabled>关注店铺</el-link>  

    <h3>账户设置</h3>
    <el-link :underline="false" href="/Account/personalInformation">个人信息</el-link>  
    <el-link :underline="false" href="/Account/security">账户安全</el-link>  
    <el-link :underline="false" href="/Account/address">收货管理</el-link>  

  </el-container>
`,
    data() {
        return {};
    },
    methods: {},
})

let app = new Vue({
    el: '#app',
    data() {
        return {
            list: [
                /*
                {
                    img: "https://idreamleaguesoccerkits.com/wp-content/uploads/2018/03/China-Logo-512x512-URL.png",
                    name: "英豪荟萃官方店铺",
                    date: "2020-05-01",
                    shop: "",
                },
                {
                    img: "https://idreamleaguesoccerkits.com/wp-content/uploads/2018/03/China-Logo-512x512-URL.png",
                    name: "英豪荟萃官方店铺",
                    date: "2020-05-01",
                    shop: "",
                },
                {
                    img: "https://idreamleaguesoccerkits.com/wp-content/uploads/2018/03/China-Logo-512x512-URL.png",
                    name: "英豪荟萃官方店铺",
                    date: "2020-05-01",
                    shop: "",
                },
                */
            ],
            id: "",
            input: '',
            currentDate: new Date(),
            rate: 5.0,
        };
    },
    methods: {
        getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        },
        removefollow(x) {
            this.$confirm("确定取消关注该店铺嘛?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            }).then(() => {
                $.ajax({
                    type: "post",
                    url: "/Account/CancelFollowShop",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        buyerid: this.id,               // 买家ID
                        shopid: x                 // 店铺ID
                    }),
                    success: function (result) {        // bool
                        DisplayFollowShops(this.id);
                    }
                })
            });

        },
        gotoshop(x) {
            $.ajax({
                url: "/Shop/SetShopID",
                type: "post",
                dataType: "json", //返回数据格式为json
                contentType: "application/json; charset=utf-8",
                async: false,
                data: JSON.stringify({ ID: x }),
                success: function (data) {//请求成功完成后要执行的方法
                    window.location = "/Shop/Shop"
                }
            })
        }

    },
    created() {
        this.id = this.getCookie("buyerID");
        console.log(this.id);
    }

})
window.onload = DisplayFollowShops(app.id);




function DisplayFollowShops() {               // 显示关注的所有店铺
    $.ajax({
        type: "post",
        url: "/Account/DisplayFollowShops",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ BuyerId: app.id, }),
        success: function (result) {
            // var object = eval("(" + result + ")");   //将json转换成对象
            console.log(result);
            app.list = result;
            console.log(app.list);
        }
    })
}
window.onload = DisplayFollowShops(app.id);


//function AddFollowShop(id) {                    // 关注店铺
//    $.ajax({
//        type: "post",
//        url: "/Account/AddFollowShop",
//        async: false,
//        contentType: "application/json",
//        dataType: "json",
//        data: JSON.stringify({
//            buyerid: app.id,             // 买家ID
//            shopid: app.id               // 店铺ID
//        }),
//        success: function (result) {       // bool
//            var jsonData = eval("(" + result + ")");   //将json转换成对象
//        }
//    })
//}

//function CancelFollowShop(id) {                  // 取消关注店铺
//    $.ajax({
//        type: "post",
//        url: "/Account/CancelFollowShop",
//        async: false,
//        contentType: "application/json",
//        dataType: "json",
//        data: JSON.stringify({
//            buyerid: app.id,               // 买家ID
//            shopid: app.id                 // 店铺ID
//        }),
//        success: function (result) {        // bool

//        }
//    })
//}


//function CancelAllFollowShop(id) {    // 清除所有关注店铺
//    $.ajax({
//        type: "post",
//        url: "/Account/CancelAllFollowShop",
//        async: false,
//        contentType: "application/json",
//        dataType: "json",
//        data: JSON.stringify({
//            buyerid: app.id            // 买家ID
//        }),
//        success: function (result) {      // bool
//            var jsonData = eval("(" + result + ")");   //将json转换成对象
//        }
//    })
//}


//function getCommodities() {    //渲染商品
//    $.ajax({
//        url: "/Search/GetCommodities",
//        type: "post",
//        dataType: "json", //返回数据格式为json
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        data: JSON.stringify({ Context: $("#searchContext").val() }),
//        success: function (data) {//请求成功完成后要执行的方法
//            console.log(commodity.goods);
//            commodity.goods = data
//            console.log(data);
//            console.log(commodity.goods);
//        }
//    })
//}

//function getCommodityType() {  //获取种类
//    $.ajax({
//        url: "/Search/GetCommodityType",
//        type: "get",
//        dataType: "json", //返回数据格式为json
//        async: false,
//        success: function (result) {//请求成功完成后要执行的方法
//            console.log(result)
//            var jsonData = eval("(" + result + ")");   //将json转换成对象
//            searchCategory.activeIndex = jsonData.type;
//            console.log(jsonData.type);
//        }
//    })
//}

//function setCommodDefault() {  //默认排序
//    $.ajax({
//        url: "/Search/SetSearchCommodityType",
//        type: "post",
//        dataType: "json", //返回数据格式为json
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        data: JSON.stringify({ Type: "0" }),
//        success: function (data) {//请求成功完成后要执行的方法
//            console.log("success");
//            window.location = "/Search/SearchCommodity"
//        }
//    })

//}

//function setCommodDesc() {  //价格降序排序
//    $.ajax({
//        url: "/Search/SetSearchCommodityType",
//        type: "post",
//        dataType: "json", //返回数据格式为json
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        data: JSON.stringify({ Type: "1" }),
//        success: function (data) {//请求成功完成后要执行的方法
//            console.log("success");
//            window.location = "/Search/SearchCommodity"
//        }
//    })

//}

//function setCommodAsc() {   //价格升序排序
//    $.ajax({
//        url: "/Search/SetSearchCommodityType",
//        type: "post",
//        dataType: "json", //返回数据格式为json
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        data: JSON.stringify({ Type: "2" }),
//        success: function (data) {//请求成功完成后要执行的方法
//            console.log("success");
//            window.location = "/Search/SearchCommodity"
//        }
//    })
//}

//function setCommodByAmount() {   //销量排序
//    $.ajax({
//        url: "/Search/SetSearchCommodityType",
//        type: "post",
//        dataType: "json", //返回数据格式为json
//        contentType: "application/json; charset=utf-8",
//        async: false,
//        data: JSON.stringify({ Type: "3" }),
//        success: function (data) {//请求成功完成后要执行的方法
//            console.log("success");
//            window.location = "/Search/SearchCommodity"
//        }
//    })
//}

//function start() {
//    getCommodities()
//    getCommodityType()
//}

// window.onload = start()
