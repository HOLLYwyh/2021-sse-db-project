//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

function details() {

    $.ajax({
        type: "post",
        url: "/Detail/GetCommodityDetails",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ commodityName: $("#commodityName").val() }),
        success: function (result) {
            var jsonData = eval("(" + result + ")");  //将json转换成对象
            console.log(jsonData);//目前得到了对应商品的所有属性，再把属性展示到前端即可
            
        }
    });
}

