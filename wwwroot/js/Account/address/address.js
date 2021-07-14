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

let app = new Vue({
    el: '#app',
    data() {
        return {
            id: '',
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
            total: 0,
            remove: 0,
            //defaultADD: 0,
            formLabelWidth: "80px",
            info: [
                /*{
                    NO: 0,
                    id: 0,
                    name: "袁小金",
                    address: "上海市普陀区金沙江路 1518 弄",
                    phone: "12345678901",
                    tag: "家",
                    first: "默认",
                },
                {
                    NO: 1,
                    id:0,
                    name: "袁小金",
                    address: "上海市嘉定区曹安公路4800号",
                    phone: "12345678901",
                    tag: "学校",
                    first: "",
                },*/
            ],
        };
    },
    methods: {
        addInfo() {
            if (!this.validate()) return;
            if (!this.form.tag) {
                alert("请输入标签！");
                return false;
            }
            /*this.info.push({
                NO: this.total++,
                name: this.form.name,
                address: this.form.address,
                phone: this.form.phone,
                tag: this.form.tag,
            });*/
            addReceiveInfo();
            this.clear();
        },
        edit(i) {
            this.editVisible = true;
            console.log(i);
            this.remove = this.info[i].ReceivedId;
            //this.form.NO = this.info[i].NO;
            this.form.name = this.info[i].ReceiverName;
            this.form.address = this.info[i].DetailAddr;
            this.form.phone = this.info[i].Phone;
            this.form.tag = this.info[i].Tag;
        },
        deleteInfo() {
            this.$confirm("确定删除该地址信息吗?", "提示", {
                confirmButtonText: "确定",
                cancelButtonText: "取消",
                type: "warning",
            }).then(() => {
                /*if ((this.remove == this.defaultADD)) {
                    alert("请先将默认地址修改为其他！");
                    return false;
                }
                this.info.splice(this.remove, 1);
                this.total--;
                for (let o = 0; o < this.total; o++) {
                    info[o].NO = o;
                }*/
                deleteReceiveInfo();
                this.clear();
            });
            this.editVisible = false;
        },
        renewInfo(i) {
            if (!this.validate()) return;
            if (!this.form.tag) {
                alert("请输入标签！");
                return false;
            }
            /*this.info[i].name = this.form.name;
            this.info[i].address = this.form.address;
            this.info[i].phone = this.form.phone;
            this.info[i].tag = this.form.tag;*/
            updateReceiveInfo();
            console.log(app.info);
            //displayReceiveInfo();
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
        /*setDefault(i) {
            let x = this.defaultADD;
            this.info[i].first = "默认";
            this.info[x].first = "";
            this.defaultADD = i;
        },*/
        getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }

    },
    created() {
        this.id = this.getCookie("buyerID")
    }
})


function addReceiveInfo() {    // 添加收货地址
    $.ajax({
        type: "post",
        url: "/Account/AddReceiveInformation",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            BuyerId: app.id,
            Phone: app.form.phone,
            ReceiverName: app.form.name,
            DetailAddr: app.form.address,
            Tag: app.form.tag
        }),
        success: function (result) {
            app.total = result.length;
            console.log(result);
            //var jsonData = eval("(" + result + ")");   //将json转换成对象
            displayReceiveInfo(app.id);
        }
    })
}

function displayReceiveInfo(id) {    // 显示收货地址
    $.ajax({
        type: "post",
        url: "/Account/DisplayReceiveInformation",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ BuyerId: id, }),
        success: function (result) {
            // var object = eval("(" + result + ")");   //将json转换成对象
            // console.log(result);
            app.info = result;
            console.log(app.info);
        }
    })
}
window.onload = displayReceiveInfo(app.id);


function updateReceiveInfo() {    // 更新收货地址
    $.ajax({
        type: "post",
        url: "/Account/UpdateReceiveInformation",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            ReceivedId: app.remove,
            BuyerId: app.id,
            Phone: app.form.phone,
            ReceiverName: app.form.name,
            DetailAddr: app.form.address,
            Tag: app.form.tag
        }),
        success: function (result) {
            var jsonData = eval("(" + result + ")");   //将json转换成对象
            displayReceiveInfo(app.id);
        }
    })
}


function deleteReceiveInfo(id) {    // 删除收货地址
    $.ajax({
        type: "post",
        url: "/Account/DeleteReceiveInformation",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({
            ReceiveId: app.remove,


        }),
        success: function (result) {
            var jsonData = eval("(" + result + ")");   //将json转换成对象
            displayReceiveInfo(app.id);
        }
    })
}

//注意！点击编辑按钮将对应数据放入表单中！