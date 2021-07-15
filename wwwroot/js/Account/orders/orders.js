Vue.component('index', {
    template: `
  <el-container direction="vertical">
    <h3>订单中心</h3>
    <el-link :underline="false" disabled>历史订单</el-link>  

    <h3>我的钱包</h3>
    <el-link :underline="false" href="/Purchase/shoppingCart">购物车</el-link>  
    <el-link :underline="false" href="/Account/coupon">优惠券</el-link>  

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
            //pageSize: 5,
            //currentPage: 1,
            activeName: 'ALL',
            list: [
                /*{
                date: '2016-05-02',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1518 弄',
                phone: '12345678901',
                condition: '待发货',
                tag: 'TO_BE_SHIP',
                show: true
            }, {
                date: '2016-05-03',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '待发货',
                tag: 'TO_BE_SHIP',
                show: true
            }, {
                date: '2016-05-04',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '待收货',
                tag: 'TO_BE_RECEIVE',
                show: true
            }, {
                date: '2016-05-05',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '待收货',
                tag: 'TO_BE_RECEIVE',
                show: true
            }, {
                date: '2016-05-05',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '已完成',
                tag: 'DONE',
                show: true
            }, {
                date: '2016-05-05',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '待付款',
                tag: 'TO_BE_PAY',
                show: true
            }, {
                date: '2016-05-06',
                name: '王小虎',
                address: '上海市普陀区金沙江路 1517 弄',
                phone: '12345678901',
                condition: '待处理',
                tag: 'OTHER',
                show: true
            }*/
            ],
            filteredData: [],
            curpageData: []
        }
    },
    methods: {
        /*formatter(row) {
            return row.address;
        },*/
        drawTag(tag) {
            if (tag === '已完成') { //已完成
                return 'success';
            } else if (tag === '待收货') { //待收货
                return '';
            } else if (tag === '待发货') { //待发货
                return 'danger';
            } else if (tag === '待付款') {  //待付款
                return 'warning';
            } else {//OTHER
                return 'info';
            }
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
        /*handleFilter(value, row) {
            if (value === 'ALL') {
                row.show = true;
            } else {
                if (row.tag === value) {
                    row.show = true;
                } else {
                    row.show = false;
                }
            }
        },
        handleClick(activeName) {
            this.activeName = activeName;
            this.filteredData.splice(0, this.filteredData.length);
            let index = 0;
            this.list.map((row) => {
                this.handleFilter(this.activeName, row);
                if (row.show === true) {
                    this.filteredData[index] = Object.assign({}, row);
                    index++;
                }
            });
            //this.handleCurrentChange(1);
        },*/
        getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        },

        /*handleSizeChange(pageSize) { // 每页条数切换
            this.pageSize = pageSize;
            this.handleCurrentChange(this.currentPage);
        },
        handleCurrentChange(currentPage) {//页码切换
            this.currentPage = currentPage;
            this.currentChangePage(currentPage);
        },
        //分页方法
        currentChangePage(currentPage) {
            let startIndex = (currentPage - 1) * this.pageSize;
            let endIndex = startIndex + this.pageSize - 1;
            this.curpageData.splice(0, this.curpageData.length);
            let index = 0;
            for (let i = startIndex; i <= endIndex; i++) {
                if (i < this.filteredData.length) {
                    this.curpageData[index] = Object.assign({}, this.filteredData[i]);
                    index++;
                }
            }
        },*/

    },
    /*mounted() {
        this.handleClick('ALL');
    },*/
    created() {
        this.id = this.getCookie("buyerID");
        console.log(this.id);
    }

})
window.onload = DisplayOrders(app.id);

function DisplayOrders() {
    $.ajax({
        type: "post",
        url: "/Account/DisplayOrders",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ BuyerId: app.id, }),
        success: function (result) {
            // var object = eval("(" + result + ")");   //将json转换成对象
            console.log(result);
            app.list = result;
        }
    })
}
