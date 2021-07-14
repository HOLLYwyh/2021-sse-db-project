Vue.component('searchbar', {
    template: `
     <!--搜索框以相关部分-->
    <div class="search-bar rounded-b-lg bg-gray-200 shadow">
        <div class="huicui">
            <img src="/Images/Home/index/yinghaohuicui.png">
        </div>
        <div>
            <div>
                <select id="select" class="select rounded-l-lg border-opacity-0 text-blue-500 text-center">
                    <option>{{first}}</option>
                    <option>{{second}}</option>
                </select>
            </div>
            <div id="search" class="search">
                <el-input id="searchContext" placeholder="请输入搜索内容"
                          v-model="input"
                          clearable>
                </el-input>
            </div>
            <div id="search-btn" class="search-btn">
                <el-button icon="el-icon-search" @click="searchClick"></el-button>
            </div>
        </div>
        <div id="shopping-cart"class="shopping-cart">
            <el-button type="danger" @click="turnToCart">我的购物车</el-button>
        </div>
    </div>
    `,
    data: () => {
        return {
            input: "",
            first: (window.location.pathname === '/Search/SearchShop') ? "店铺" : "商品" ,
            second: (window.location.pathname === '/Search/SearchShop') ? "商品" : "店铺"
        }
    },
    methods: {
        searchClick() {    //根据选择类型搜索
            $.ajax({
                type: "post",
                url: "/Search/SetSearchName",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ Context: $("#searchContext").val()}),
                success: function (result) {
                    if ($('#select option:selected').text() === "店铺") {
                        window.location.href = "/Search/SearchShop";
                    }
                    else {
                        window.location.href = "/Search/SearchCommodity";
                    }
                }
            });
        },
        turnToCart() {
            window.location = "/Purchase/ShoppingCart"
        }
    },
    created() {
        var that;
        $.ajax({
            url: "/Search/GetSearchName",
            type: "get",
            dataType: "json", //返回数据格式为json
            async:false,
            success:  function (result) {//请求成功完成后要执行的方法
                var jsonData = eval("(" + result + ")");   //将json转换成对象
                that = jsonData.searchResult;
                console.log(jsonData.searchResult);
            }
        })
        this.input = that;
        this.$forceUpdate();
    }
})