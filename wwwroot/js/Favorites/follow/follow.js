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
    <el-link :underline="false" href="https://">购物车</el-link>  
    <el-link :underline="false" href="https://">优惠券</el-link>  

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

new Vue({
    el: '#app',
    data() {
        return {
            list: [
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
            ],

            input: '',
            currentDate: new Date(),
            rate: 4.9,
        };
    },
    methods: {}

})