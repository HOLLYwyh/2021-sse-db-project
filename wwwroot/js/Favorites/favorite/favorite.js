Vue.component('tabs', {
    template: `
    <el-tabs v-model="activeName" @tab-click="handleClick" >
        <el-tab-pane label="按关注时间" name="1"></el-tab-pane>
        <el-tab-pane label="按购买次数" name="2"></el-tab-pane>
        <el-tab-pane label="按价格" name="3"></el-tab-pane>
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
Vue.component('radio', {
    template: `

    <el-radio-group v-model="radio">
        <el-radio :label="3">按关注时间</el-radio>
        <el-radio :label="6">按购买次数</el-radio>
        <el-radio :label="9">按价格</el-radio>
    <el-radio-group>
`,

    data() {
        return {
            radio: 3
        };
    },
})
Vue.component('pagination', {
    template: `
    <div class="block">
      <el-pagination
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        :current-page.sync="currentPage1"
        :page-size="100"
        layout="prev, pager, next, jumper"
        :total="1000"
      >
      </el-pagination>
    </div>
`,
    methods: {
        handleCurrentChange(val) {
            console.log(`当前页: ${val}`);
        },
    },
    data() {
        return {
            currentPage1: 1,
            currentPage2: 2,
            currentPage3: 3,
            currentPage4: 4,
        };
    },
})
Vue.component('index', {
    template: `
  <el-container direction="vertical">
    <h3>订单中心</h3>
    <el-link :underline="false" href="https://">历史订单</el-link>  

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
        list: [
            {
                img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                name: "好登西",
                date: "",
            },
            {
                img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                name: "好登西",
                date: "",
            },
            {
                img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                name: "好登西",
                date: "",
            },
            {
                img: "https://shadow.elemecdn.com/app/element/hamburger.9cf7b091-55e9-11e9-a976-7f4d0b07eef6.png",
                name: "好登西",
                date: "",
            },
        ];
        return {
            input: '',
            currentDate: new Date(),
            rate: 4.9,
        };
    },
    methods: {}

})
