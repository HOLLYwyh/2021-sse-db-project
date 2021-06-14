Vue.component('side-bar', {

    template: `
        <el-menu default-active="1" class="el-menu-vertical-demo"
            background-color="transparent" text-color="#8a8c93" active-text-color="#29b7cb">
            <el-menu-item index="1">
                <i class="el-icon-s-home"></i>
                <span slot="title">首页</span>
            </el-menu-item>
            <el-menu-item index="2">
                <i class="el-icon-s-order"></i>
                <span slot="title">订单</span>
            </el-menu-item>
            <el-menu-item index="3">
                <i class="el-icon-pie-chart"></i>
                <span slot="title">数据</span>
            </el-menu-item>
            <el-menu-item index="4">
                <i class="el-icon-s-ticket"></i>
                <span slot="title">活动</span>
            </el-menu-item>
            <el-menu-item index="5">
                <i class="el-icon-s-goods"></i>
                <span slot="title">商品</span>
            </el-menu-item>
            <el-submenu index="6">
                <template slot="title">
                    <i class="el-icon-s-tools"></i>
                    <span>管理</span>
                </template>
                <el-menu-item-group>
                    <el-menu-item index="6-1">
                        <i class="el-icon-user-solid"></i>
                        <span>登录</span>
                    </el-menu-item>
                    <el-menu-item index="6-2">
                        <i class="el-icon-circle-plus"></i>
                        <span>创建店铺</span>
                    </el-menu-item>
                    <el-menu-item index="6-3">
                        <i class="el-icon-s-shop"></i>
                        <span>切换店铺</span>
                    </el-menu-item>
                </el-menu-item-group>
            </el-submenu>
        </el-menu>
    `
})

var sidebar = new Vue({ el: '#nav' });