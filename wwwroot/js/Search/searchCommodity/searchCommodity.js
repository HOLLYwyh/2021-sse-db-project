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
    el:"#search-bar"
})

//翻页
new Vue({
    el: "#turn-to-page",
        methods: {
            handleSizeChange(val) {
                console.log(`每页 ${val} 条`);
            },
            handleCurrentChange(val) {
                console.log(`当前页: ${val}`);
            }
        },
        data:{
            currentPage1: 1,
        }
})


//商品结果列表
new Vue({
    el: "#commodity-list",
    data: {
        goods: [{
            "img": "../../Images/Home/index/a1.png",
            "intro": "1",
        },
            {
                "img": "../../Images/Home/index/a2.png",
                "intro": "2",
            }, {
                "img": "../../Images/Home/index/a3.png",
                "intro": "3",
            },
            {
                "img": "../../Images/Home/index/a2.png",
                "intro": "4",
            },
            {
                "img": "../../Images/Home/index/a3.png",
                "intro": "5",
            },
            {
                "img": "../../Images/Home/index/a1.png",
                "intro": "6",
            }, {
                "img": "../../Images/Home/index/a3.png",
                "intro": "7",
            }, {
                "img": "../../Images/Home/index/a1.png",
                "intro": "8",
            }, {
                "img": "../../Images/Home/index/a2.png",
                "intro": "9",
            }, {
                "img": "../../Images/Home/index/a1.png",
                "intro": "10",
                "shop": "金轮",
                "link": "https://www.baidu.com/"
            }, {
                "img": "../../Images/Home/index/a2.png",
                "intro": "11",
                "shop": "金轮",
                "link": "https://www.baidu.com/"
            }, {
                "img": "../../Images/Home/index/a3.png",
                "intro": "12",
                "shop": "金轮",
                "link": "https://www.baidu.com/"
            }     ],
        number: 0
    }
})

function update() {
    if (number > 1) {
        number = 0;
    }
    else {
        number++;
    }
}

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

function getSearchName() {
    $.ajax({
        url: "/Search/GetSearchName",
        type: "get",
        dataType: "json", //返回数据格式为json
        success: function (data) {//请求成功完成后要执行的方法
            var jsonData = eval("(" + data + ")");   //将json转换成对象
            data.input = jsonData.searchResult;
        }
    })
}

function getCommodities() {
    $.ajax({
        url: "/Search/GetCommodities",
        type: "get",
        dataType: "json", //返回数据格式为json
        data: JSON.stringify({ Context: $("#searchContext").val() }),
        success: function (data) {//请求成功完成后要执行的方法
            data.goods = data
        }
    })
}

function start() {
    //getSearchName()
    //getCommodities()
}

window.onload = start()