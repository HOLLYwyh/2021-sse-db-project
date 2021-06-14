Vue.component('dropdown-menu', {
    data: function () {
        return {
            isShow: false,
        }
    },

    mounted: function () {
        document.addEventListener('click', (e) => {
            if (e.target.className !== "dropbtn" &&
                e.target.className!=="sub-btn") {
                this.isShow = false;
            }     
            else{
                this.isShow = true;
            }
        })
    },
    methods: {

    },
    template: `
    <div class="dropdown">
        <div class="dropbtn">
            <button class="dropbtn"></button>
        </div>
        <div class="dropdown-content" v-show="isShow">
            <div>
                <button class="sub-btn">
                <i class="el-icon-user"></i>
                <span class="sub-btn">个人信息</span>
                </button>
            </div>
            <div>
                <button class="sub-btn">
                <i class="el-icon-setting"></i>
                <span class="sub-btn">设置</span>
                </button>
            </div>
            <div>
                <button class="sub-btn">
                <i class="el-icon-switch-button"></i>
                <span class="sub-btn">退出登录</span>
                </button>
            </div>
        </div>
    </div>
    </div>
    `
})

let avatar = new Vue({ el: '#avatar' });
