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


new Vue({
    el: '#app',
    data() {
        return {
            list: [
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
                },

            ],

            input: '',
            currentDate: new Date(),
            rate: 4.9,
        };
    },
    methods: {
        jumpto(x) {
            console.log(x);
        }
    }

})
