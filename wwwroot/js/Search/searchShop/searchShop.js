//右边栏
new Vue({
    el: "#naviRight"
})

//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

//搜索框
new Vue({
    el: "#search-bar"
})

//商品结果列表
var shop = new Vue({
    el: "#shop-list",
    data: {
        shops: [
        ],
        number: 0
    },
})

//搜索分类菜单
new Vue({
    el: "#search-category",
    data: {
        activeIndex: '1'
    },
    methods: {
        handleSelect(key, keyPath) {
            console.log(key, keyPath);
        }
    }
})

function setShopsDefault() {   //默认值返回店铺
    $.ajax({
        url: "/Search/SetSearchShopType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "0" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchShop"
        }
    })
}

function setShopsByCredit() { //按照商家信用返回店铺
    $.ajax({
        url: "/Search/SetSearchShopType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "1" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchShop"
        }
    })
}

function getShops() {    //渲染店铺
    //还需要写
    $.ajax({
        url: "/Search/GetShops",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Context: $("#searchContext").val() }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data)
            shop.shops = data
        }
    })
}

function start() {
    getShops()
}

window.onload = start()