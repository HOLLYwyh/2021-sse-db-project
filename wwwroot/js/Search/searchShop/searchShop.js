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
new Vue({
    el: "#shop-list"
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
    data: {
        currentPage1: 1,
    }
})

function getShops() {   //默认值返回店铺

}

function getShopsByCredit() { //按照商家信用返回店铺

}