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
    <el-link :underline="false" disabled>关注商品</el-link>  
    <el-link :underline="false" href="/Favorites/follow">关注店铺</el-link>  

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
            id: "",
            list: [
                /*
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details:"/Home/Index", //该项为商品详情页url
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "2",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "3",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "4",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "5",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "6",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "7",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "8",
                },
                {
                    img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                    name: "英豪巨无霸",
                    date: "2020-05-01",
                    price: "9.9",
                    details: "9",
                },*/

            ],

            input: '',
            currentDate: new Date(),
            rate: 4.9,
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
        commodityDetial(id) {                 //进入商品详情
            console.log(id);
            $.ajax({
                url: "/Commodity/SetCommodityID",
                type: "post",
                dataType: "json", //返回数据格式为json
                contentType: "application/json; charset=utf-8",
                async: false,
                data: JSON.stringify({ ID: id }),
                success: function (data) {//请求成功完成后要执行的方法
                    window.location = "/Commodity/Details"
                }
            })
        },
        removefavo(x) {
            this.$confirm("确定取消收藏该商品嘛?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            }).then(() => {
                $.ajax({
                    type: "post",
                    url: "/Account/CancelFavoriteProduct",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        buyerid: this.id,           // 买家ID
                        commodityid: x              // 商品ID
                    }),
                    success: function (result) {        // bool
                        displayFavorites(this.id);
                    }
                })
            });
        },
        clearup() {
            this.$confirm("确定要清空收藏夹吗? 此操作不可撤销", "警告", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            }).then(() => {
                $.ajax({
                    type: "post",
                    url: "/Account/CancelAllFavoriteProduct",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({
                        buyerid: this.id            // 买家ID
                    }),
                    success: function (result) {      // bool
                        var jsonData = eval("(" + result + ")");   //将json转换成对象
                        displayFavorites(this.id);
                    }
                })
            });
        }
    },
    created() {
        this.id = this.getCookie("buyerID");
        //console.log(this.id);
    }


})
window.onload = displayFavorites(app.id);


function displayFavorites() {    // 显示收藏夹
    $.ajax({
        url: "/Account/DisplayFavorites",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json",
        async: false,
        data: JSON.stringify({ BuyerId: app.id }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data);
            /*let temp = data;
            let t = temp.DateCreated;
            let time = t.getFullYear() + '年' + (d.getMonth() + 1) + '月' + d.getDate() + '日' + d.getHours() + ':' + d.getMinutes() + ':' + d.getSeconds() : d);
            */
            app.list = data;

            console.log(app.list);
        }
    })
}

/*function deleteOneFavorite(id) {            // 删除一个收藏夹中的商品
    $.ajax({
        type: "post",
        url: "/Account/CancelFavoriteProduct",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            buyerid: app.id,                // 买家ID
            commodityid: app.id                  // 商品ID

        }),
        success: function (result) {        // bool

        }
    })
}


function deleteAllFavorites(id) {    // 删除收藏夹中所有商品
    $.ajax({
        type: "post",
        url: "/Account/CancelAllFavoriteProduct",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            buyerid: app.id            // 买家ID
        }),
        success: function (result) {      // bool
            var jsonData = eval("(" + result + ")");   //将json转换成对象
        }
    })
}*/

/*function deleteAllFavorites(id) {    // 删除收藏夹中所有商品
    $.ajax({
        type: "post",
        url: "/Account/CancelAllFavoriteProduct",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            buyerid: app.id            // 买家ID
        }),
        success: function (result) {      // bool
            var jsonData = eval("(" + result + ")");   //将json转换成对象
        }
    })
}*/

/*
function addFavorite(id) {                    // 添加到收藏夹
    $.ajax({
        type: "post",
        url: "/Account/AddFavoriteProduct",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            buyerid: app.id,                // 买家ID
            commodityid: app.id                  // 商品ID
        }),
        success: function (result) {        // bool
            var jsonData = eval("(" + result + ")");   //将json转换成对象
        }
    })
}
*/