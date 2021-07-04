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


//测试卡片
new Vue({
    el: "#test",
    data: {
           currentDate: new Date()
    }
})