Vue.component('index', {
    template: `
  <el-container direction="vertical">
    <h3>订单中心</h3>
    <el-link :underline="false" href="/Account/orders">历史订单</el-link>  

    <h3>我的钱包</h3>
    <el-link :underline="false" href="/Purchase/shoppingCart">购物车</el-link>  
    <el-link :underline="false" disabled>优惠券</el-link>  

    <h3>我的关注</h3>
    <el-link :underline="false" href="/Favorites/favorite">关注商品</el-link>  
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
            id:"",
            list: [
                /*
                {
                    price: "10",
                    date: "2021-7-1",
                    class:"店铺类",
                },
                {
                    price: "10",
                    date: "2021-7-1",
                    class: "店铺类",
                },
                {
                    price: "10",
                    date: "2021-7-1",
                    class: "店铺类",
                },
                {
                    price: "10",
                    date: "2021-7-1",
                    class: "店铺类",
                }
                */
            ],
        }
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
window.onload = DisplayCoupons(app.id);

function DisplayCoupons() {
    $.ajax({
        type: "post",
        url: "/Account/DisplayCoupons",
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
