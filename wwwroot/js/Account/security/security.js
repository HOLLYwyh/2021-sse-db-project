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
        <el-input v-model="form1.oldno" ></el-input>
        </el-form-item>

        <el-form-item label="新手机号" :label-width="formLabelWidth">
        <el-input v-model="form1.newno" ></el-input>
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
            if (y.length != 11) {
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
          <el-input v-model="form.oldpw" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item label="新密码" :label-width="formLabelWidth">
          <el-input v-model="form.newpw1" autocomplete="off"></el-input>
        </el-form-item>

        <el-form-item label="确认新密码" :label-width="formLabelWidth">
          <el-input v-model="form.newpw2" autocomplete="off"></el-input>
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
            //后端实现检查旧密码是否正确，如正确更新密码，密码格式检查放在前端
            let y = this.form.newpw1;
            let z = this.form.newpw2;
            if (y == null || y == "") {
                alert("请输入新密码！");
                return false;
            }

            let pat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[^]{6, 18}$/;
            if (!pat.test(y)) {
                alert("密码至少包含小写字母、大写字母和数字,且长度为6-18位");
                return false;
            }
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
            }
        },
    },

})



new Vue({
    el: '#app',
    data() {
        return {
            phone: "12345678910",
            password: "Aa123456",
            pin: "",
            name: "",
        };
    },
    methods: {
    },

})
