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
    <el-link :underline="false" disabled>个人信息</el-link>  
    <el-link :underline="false" href="/Account/security">账户安全</el-link>  
    <el-link :underline="false" href="/Account/address">收货管理</el-link>  

  </el-container>
`,
    data() {
        return {};
    },
    methods: {},
})
Vue.component('promptbox', {
    template: `
    <div>
    <el-button type="danger" @click="dialogVisible = true">提交</el-button>
    <el-dialog
        title="提示"
    :visible.sync="dialogVisible"
        width="30%"
    :before-close="handleClose">
        <span>确定要提交并保存个人信息嘛~</span>
        <span slot="footer" class="dialog-footer">
            <el-button @click="dialogVisible = false">取 消</el-button>
        <el-button type="primary" @click="dialogVisible = false">确 定</el-button>
    </span >
    </el-dialog >
    </div>
`,
    data() {
        return {
            dialogVisible: false,
        };
    },
    methods: {
        handleClose(done) {
            this.$confirm("确认关闭？")
                .then((_) => {
                    done();
                })
                .catch((_) => { });
        },
    },
})


let app = new Vue({
    el: '#app',
    data() {
        return {
            avatar: "https://fuss10.elemecdn.com/e/5d/4a731a90594a4af544c0c25941171jpeg.jpeg",
            ID: "123456789",
            nickname: "HollyWYH",
            imageUrl: "",
            gender:0,
            pickerOptions: {
                disabledDate(time) {
                    return time.getTime() > Date.now();
                },
            },
            birthday: "",

        };
    },
    methods: {
        handleAvatarSuccess(res, file) {
            this.imageUrl = URL.createObjectURL(file.raw);
        },
        beforeAvatarUpload(file) {
            const isJPG = file.type === "image/jpeg";
            const isLt2M = file.size / 1024 / 1024 < 2;

            if (!isJPG) {
                this.$message.error("上传头像图片只能是 JPG 格式!");
            }
            if (!isLt2M) {
                this.$message.error("上传头像图片大小不能超过 2MB!");
            }
            return isJPG && isLt2M;
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
function display(id) {
    //console.log(id);
    $.ajax({
        type: "post",
        url: "/Account/DisplayBuyerInfo",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ BuyerId: id }),
        success: function (result) {
            var object = eval('(' + result + ')');//string类型转换成Json对象方法
            app.ID = object["buyerPhone"];
            app.nickname = object["buyerNickname"];
            app.gender = object["buyerGender"];
            app.birthday = object["buyerBirth"];
            console.log(object);

            //console.log(object["buyerPhone"]);
            //console.log(object["buyerPhone"]);
        }
    });
}
window.onload = display(app.id);

