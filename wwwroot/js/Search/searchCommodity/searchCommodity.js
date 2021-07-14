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
var commodity = new Vue({
    el: "#commodity-list",
    data: {
        goods: [
        ],
        number: 0
    },
    methods: {
        commodityDetial(id) {                 //进入商品详情
            console.log(id);
            $.ajax({
                url: "/Commodity/SetCommodityID",
                type: "post",
                dataType: "json", //返回数据格式为json
                contentType: "application/json; charset=utf-8",
                async: false,
                data: JSON.stringify({ ID:  id}),
                success: function (data) {//请求成功完成后要执行的方法
                    window.location = "/Commodity/Details"
                }
            })
        }
    }
})


//搜索分类菜单
var searchCategory = new Vue({
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


function getCommodities() {    //渲染商品
    $.ajax({
        url: "/Search/GetCommodities",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Context: $("#searchContext").val() }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(commodity.goods);
            commodity.goods = data
            console.log(data);
            console.log(commodity.goods);
        }
    })
}

function getCommodityType() {  //获取种类
    $.ajax({
        url: "/Search/GetCommodityType",
        type: "get",
        dataType: "json", //返回数据格式为json
        async: false,
        success: function (result) {//请求成功完成后要执行的方法
            console.log(result)
            var jsonData = eval("(" + result + ")");   //将json转换成对象
            searchCategory.activeIndex = jsonData.type;
            console.log(jsonData.type);
        }
    })
}

function setCommodDefault() {  //默认排序
    $.ajax({
        url: "/Search/SetSearchCommodityType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "0" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchCommodity"
        }
    })

}

function setCommodDesc() {  //价格降序排序
    $.ajax({
        url: "/Search/SetSearchCommodityType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "1" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchCommodity"
        }
    })

}

function setCommodAsc() {   //价格升序排序
    $.ajax({
        url: "/Search/SetSearchCommodityType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "2" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchCommodity"
        }
    })
}

function setCommodByAmount() {   //销量排序
    $.ajax({
        url: "/Search/SetSearchCommodityType",
        type: "post",
        dataType: "json", //返回数据格式为json
        contentType: "application/json; charset=utf-8",
        async: false,
        data: JSON.stringify({ Type: "3" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("success");
            window.location = "/Search/SearchCommodity"
        }
    })
}

function start() {
    getCommodities()
    getCommodityType()
}

window.onload = start()
