Vue.component('searchbar', {
    template: `
     <!--搜索框以相关部分-->
    <div class="search-bar rounded-b-lg bg-gray-200 shadow">
        <div class="huicui">
            <img src="/Images/Home/index/yinghaohuicui.png">
        </div>
        <div>
            <div>
                <select class="select rounded-l-lg border-opacity-0 text-blue-500 text-center">
                    <option>{{first}}</option>
                    <option>{{second}}</option>
                </select>
            </div>
            <div id="search" class="search">
                <el-input placeholder="请输入搜索内容"
                          v-model="input"
                          clearable>
                </el-input>
            </div>
            <div id="search-btn" class="search-btn">
                <el-button icon="el-icon-search"></el-button>
            </div>
        </div>
        <div id="shopping-cart"class="shopping-cart">
            <el-button type="danger">我的购物车</el-button>
        </div>
    </div>
    `,
    data: () => {
        return {
            input: "",
            first: (window.location.pathname === '/Search/SearchShop') ? "店铺" : "商品" ,
            second: (window.location.pathname === '/Search/SearchShop') ? "商品" : "店铺"
        }
    }
})