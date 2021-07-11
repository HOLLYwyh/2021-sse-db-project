Vue.component('index', {
    template: `
  <el-container direction="vertical">
    <h3>订单中心</h3>
    <el-link :underline="false" href="https://">历史订单</el-link>  

    <h3>我的钱包</h3>
    <el-link :underline="false" href="https://">购物车</el-link>  
    <el-link :underline="false" href="https://">优惠券</el-link>  

    <h3>我的关注</h3>
    <el-link :underline="false" href="/Favorites/favorite">关注商品</el-link>  
    <el-link :underline="false" href="/Favorites/follow">关注店铺</el-link>  

    <h3>账户设置</h3>
    <el-link :underline="false" href="/Account/personalInformation">个人信息</el-link>  
    <el-link :underline="false" href="/Account/security">账户安全</el-link>  
    <el-link :underline="false" disabled>收货管理</el-link>  

  </el-container>
`,
    data() {
        return {};
    },
    methods: {},
})

new Vue({
    el:'#app',
    data() {
        return {
            options: [
                {
                    value: "家",
                    label: "家",
                },
                {
                    value: "学校",
                    label: "学校",
                },
                {
                    value: "公司",
                    label: "公司",
                },
            ],

            dialogFormVisible: false,
            editVisible: false,
            form: {
                NO: 0,
                name: "",
                address: "",
                phone: "",
                //value: "",
                tag: "",
            },
            total: 2,
            remove: 0,
            defaultADD: 0,
            formLabelWidth: "80px",
            info: [
                {
                    NO: 0,
                    name: "袁小金",
                    address: "上海市普陀区金沙江路 1518 弄",
                    phone: "12345678901",
                    tag: "家",
                    first: "默认",
                },
                {
                    NO: 1,
                    name: "袁小金",
                    address: "上海市嘉定区曹安公路4800号",
                    phone: "12345678901",
                    tag: "学校",
                    first: "",
                },
            ],
        };
    },
    methods: {
        addInfo() {
            if (!this.validate()) return;
            if (!this.form.tag) {
                this.form.tag = "地址" + (this.total + 1);
            }
            this.info.push({
                NO: this.total++,
                name: this.form.name,
                address: this.form.address,
                phone: this.form.phone,
                tag: this.form.tag,
            });
            this.clear();
        },
        edit(i, j) {
            this.editVisible = true;
            this.remove = j;
            this.form.NO = this.info[i].NO;
            this.form.name = this.info[i].name;
            this.form.address = this.info[i].address;
            this.form.phone = this.info[i].phone;
            this.form.tag = this.info[i].value;
        },
        deleteInfo() {
            this.$confirm("确定删除该地址信息吗?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            }).then(() => {
                if ((this.remove==this.defaultADD)) {
                    alert("请先将默认地址修改为其他！");
                    return false;
                }
                this.info.splice(this.remove, 1);
                this.total--;
                for (let o = 0; o < this.total; o++) {
                    info[o].NO = o;
                }
                this.clear();
            });
            this.editVisible = false;
        },
        renewInfo(i) {
            if (!this.validate()) return;

            if (!this.form.tag) {
                this.form.tag = "地址" + i;
            }
            this.info[i].name = this.form.name;
            this.info[i].address = this.form.address;
            this.info[i].phone = this.form.phone;
            this.info[i].tag = this.form.tag;
            this.clear();
        },
        clear() {
            this.editVisible = false;
            this.dialogFormVisible = false;
            this.form.tag = "";
            this.form.name = "";
            this.form.address = "";
            this.form.phone = "";
        },
        validate() {
            let x = this.form.name;
            if (x == null || x == "") {
                alert("请输入收货人姓名！");
                return false;
            }
            let y = this.form.address;
            let z = this.form.phone;
            if (y == null || y == "") {
                alert("请输入收货地址！");
                return false;
            }
            if (z.length != 11) {
                alert("请输入正确的手机号！");
                return false;
            }
            for (let i = 0; i < 11; i++) {
                if (z[i] < "0" || z[i] > "9") {
                    alert("请输入正确的手机号！");
                    return false;
                }
            }

            return true;
        },
        setDefault(i) {
            let x = this.defaultADD;
            this.info[i].first = "默认";
            this.info[x].first = "";
            this.defaultADD = i;
    },
}

})