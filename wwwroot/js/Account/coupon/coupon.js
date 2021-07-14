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
            list: [
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
            ],
        }
    },
    methods: {

    }
})