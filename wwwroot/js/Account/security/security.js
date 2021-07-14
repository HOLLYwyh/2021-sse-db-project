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
    <el-link :underline="false" disabled>账户安全</el-link>  
    <el-link :underline="false" href="/Account/address">收货管理</el-link>  

  </el-container>
`,
    data() {
        return {};
    },
    methods: {},
})
Vue.component('phonebut', {
    template: `
    <div>
        <el-button type="primary" round @click="visible1 = true">修改</el-button>

        <el-dialog title="手机号修改" :visible.sync="visible1" width="30%">

        <el-form :model="form1">
        <el-form-item label="旧手机号" :label-width="formLabelWidth">
        <el-input id="oldno" v-model="form1.oldno" ></el-input>
        </el-form-item>

        <el-form-item label="新手机号" :label-width="formLabelWidth">
        <el-input id="newno" v-model="form1.newno" ></el-input>
        </el-form-item>
        </el-form>
        <div slot="footer" class="dialog-footer">
        <el-button @click="visible1 = false">取 消</el-button>
        <el-button type="primary" @click="alterPhone()">确 定</el-button>
        </div>
        </el-dialog>
    </div>
`,
    data() {
        return {
            visible1: false,
            form1: {
                oldno: '',
                newno: '',
            },
            formLabelWidth: '85px'
        };
    },
    methods: {
        validatephone() {
            let x = this.form1.oldno;
            let y = this.form1.newno;
            if (x == null || x == "") {
                alert("请输入旧手机号码！");
                return false;
            }
            if (x !== app.phone) {
                alert("您输入的旧手机号码与账户绑定号码不一致");
                return false;
            }
            if (y.length != 11) {
                console.log(y)
                alert("请输入正确的新手机号！");
                return false;
            }

            //后端实现检查旧手机号码是否匹配，如正确更新该账户手机号码
            for (let i = 0; i < 11; i++) {
                if (y[i] < '0' || y[i] > '9') {
                    alert("请输入正确的新手机号！");
                    return false;
                }
            }
            if (x == y) {
                alert("新旧手机号不能相同！");
                return false;
            }
            return true;
        },
        alterPhone() {
            if (!this.validatephone()) {
                return false;
            }
            else {
                alert("手机号码修改成功！");
                this.visible1 = false;
                updatePhone(app.id);
                console.log(app.phone);
            }
        },
    }
})

Vue.component('passwordbut', {
    template: `
    <div>
    <el-button type="primary" round @click="Visible = true">修改</el-button>
    <el-dialog title="密码修改" :visible.sync="Visible" width="30%">
      <el-form :model="form">
        <el-form-item label="旧密码" :label-width="formLabelWidth">
          <el-input id="oldpasswd" v-model="form.oldpw" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item label="新密码" :label-width="formLabelWidth">
          <el-input v-model="form.newpw1" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item label="确认新密码" :label-width="formLabelWidth">
          <el-input id="newpasswd" v-model="form.newpw2" autocomplete="off"></el-input>
        </el-form-item>
      </el-form >
      <div slot="footer" class="dialog-footer">
        <el-button @click="Visible = false">取 消</el-button>
        <el-button type="primary" @click="alterpassword()">确 定</el-button>
      </div >
    </el-dialog >
    </div >

`,
    data() {
        return {
            Visible: false,
            form: {
                oldpw: "",
                newpw1: "",
                newpw2: "",
            },
            formLabelWidth: "85px",
        };
    },
    methods: {
        validatepassword() {
            let x = this.form.oldpw;
            if (x == null || x == "") {
                alert("请输入旧密码！");
                return false;
            }
            if (x !== app.password) {
                alert("密码输入错误！");
                return false;
            }

            let y = this.form.newpw1;
            let z = this.form.newpw2;
            if (y == null || y == "") {
                alert("请输入新密码！");
                return false;
            }
            /*
            //console.log(y);
            let pat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[^]{6, 18}$/;
            if (!pat.test(y)) {
                console.log(y)
                alert("密码至少包含小写字母、大写字母和数字,且长度为6-18位");
                return false;
            }*/
            /*if (y.length < 6 || y.length>18) {
                alert("密码至少包含小写字母、大写字母和数字,且长度为6-18位");
                return false;
            }

            //后端实现检查旧手机号码是否匹配，如正确更新该账户手机号码
            for (let i = 0; i < y.length; i++) {
                if (y[i] < '0' || y[i] > '9') {
                    alert("请输入正确的新手机号！");
                    return false;
                }
            }*/

            if (x == y) {
                alert("新旧密码不能相同！");
                return false;
            }
            if (z == null || z == "") {
                alert("请再次输入新密码！");
                return false;
            }
            if (z != y) {
                alert("两次输入密码不一致!");
                return false;
            }
            return true;
        },
        alterpassword() {
            if (!this.validatepassword()) {
                return false;
            } else {
                alert("密码修改成功！");
                this.Visible = false;
                updatePasswd(app.id);
            }
        },
    },

})

let app = new Vue({
    el: '#app',
    data() {
        return {
            phone: "12345678910",
            password: "Aa123456",
            pin: "",
            name: "",
            id: "",
        };
    },
    methods: {
        smile() {
            alert("该功能正在开发中哦~(*^_^*)");
        },
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
function details(id) {
    //console.log(id);
    $.ajax({
        type: "post",
        url: "/Account/GetPhonePasswdById",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ BuyerId: id }),
        success: function (result) {
            var object = eval('(' + result + ')');//string类型转换成Json对象方法
            app.phone = object["buyerPhone"];
            app.password = object["buyerPasswd"];
            //console.log(object["buyerPhone"]);
            //console.log(object["buyerPhone"]);
        }
    });
}
function updatePhone(id) {
    console.log(id);
    $.ajax({
        type: "post",
        url: "/Account/UpdatePhoneById",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ OldNo: $("#oldno").val(), NewNo: $("#newno").val(), BuyerId: id }),
        success: function (result) {
            var object = eval('(' + result + ')');//string类型转换成Json对象方法
            console.log($("#oldno").val() + $("#newno").val());
            app.phone = object["buyerPhone"];
            //console.log(object["buyerPhone"]);
            //console.log(object["buyerPhone"]);
        }
    });
}
function updatePasswd(id) {
    console.log(id);
    $.ajax({
        type: "post",
        url: "/Account/UpdatePasswdById",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ OldPasswd: $("#oldpasswd").val(), NewPasswd: $("#newpasswd").val(), BuyerId: id }),
        success: function (result) {
            var object = eval('(' + result + ')');//string类型转换成Json对象方法
            //console.log($("#oldpasswd").val() + $("#newpasswd").val());
            app.password = object["buyerPasswd"];
            //console.log(object["buyerPhone"]);
            //console.log(object["buyerPhone"]);
        }
    });
}

window.onload = details(app.id);



