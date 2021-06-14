Vue.component('dropdown-menu', {
    props:{

    },
    template: `
    <div class="dropdown">
        <button class="dropbtn"></button>
        <div class="dropdown-content">
            <div class="sub-btn">
                <button>
                <i class="el-icon-user"></i>
                <span>个人信息</span>
                </button>
            </div>
            <div class="sub-btn">
                <button>
                <i class="el-icon-setting"></i>
                <span>设置</span>
                </button>
            </div>
            <div class="sub-btn">
                <button>
                <i class="el-icon-switch-button"></i>
                <span>退出登录</span>
                </button>
            </div>
        </div>
    </div>
    `
})

let avatar = new Vue({ el: '#avatar' });
