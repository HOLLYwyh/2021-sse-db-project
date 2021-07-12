Vue.component('side-bar', {
    data: function () {
        return {
          active: 1, 
        }
    },
    methods:{
        handleClick(index){
            this.active = index;
            console.log(index);
            switch(index){
                case 1:
                    window.location.href = '/SellerBackground/Home';
                    break;
                case 2:
                    window.location.href = '/SellerBackground/Orders';
                    break;
                case 3:
                    window.location.href = '#';
                    break;
                case 4:
                    window.location.href = '#';
                    break;
                case 5:
                    window.location.href = '/SellerBackground/Goods';
                    break;
                case 6:
                    window.location.href = '/Entry/SellerLogIn';
                    break;
                case 7:
                    window.location.href = '/SellerBackground/ShopSignUp';
                    break;
                case 8:
                    window.location.href = '/SellerBackground/SwitchShop';
                    break;
                default:
                    window.location.href = '#';
                    break;
            }
        }
    },
    template: `
        <el-menu default-active="activeIndex" class="el-menu-vertical-demo"
            background-color="transparent" text-color="#8a8c93" active-text-color="#29b7cb">
            <el-menu-item index="1">
                <div v-on:click="handleClick(1)">
                    <i class="el-icon-s-home"></i>
                    <span slot="title">首页</span>
                </div>
            </el-menu-item>
            <el-menu-item index="2">
                <div v-on:click="handleClick(2)">
                    <i class="el-icon-s-order"></i>
                    <span slot="title">订单</span>
                </div>
            </el-menu-item>
            <el-menu-item index="3">
                <div v-on:click="handleClick(3)">
                    <i class="el-icon-pie-chart"></i>
                    <span slot="title">数据</span>
                </div>
            </el-menu-item>
            <el-menu-item index="4">
                <div v-on:click="handleClick(4)">
                    <i class="el-icon-s-ticket"></i>
                    <span slot="title">活动</span>
                </div>
            </el-menu-item>
            <el-menu-item index="5">
                <div v-on:click="handleClick(5)">
                    <i class="el-icon-s-goods"></i>
                    <span slot="title">商品</span>
                </div>
            </el-menu-item>
            <el-submenu index="9">
                <template slot="title">
                    <i class="el-icon-s-tools"></i>
                    <span>管理</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="6">
                        <div v-on:click="handleClick(6)">
                            <i class="el-icon-user-solid"></i>
                            <span>登录</span>
                        </div>
                    </el-menu-item>
                    <el-menu-item index="7">
                        <div v-on:click="handleClick(7)">
                            <i class="el-icon-circle-plus"></i>
                            <span>创建店铺</span>
                        </div>
                    </el-menu-item>
                    <el-menu-item index="8">
                        <div v-on:click="handleClick(8)">
                            <i class="el-icon-s-shop"></i>
                            <span>切换店铺</span>
                        </div>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
        </el-menu>
    `
})

var sidebar = new Vue({ el: '#nav' });