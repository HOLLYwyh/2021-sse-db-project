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
        data: JSON.stringify({ commodityId: $("#commodityId").val() }),
        success: function (result) {
            console.log($("#commodityId").val());
            //var jsonData = eval("(" + result + ")");  //将json转换成对象
            console.log(result);//目前得到了对应商品的所有属性，再把属性展示到前端即可
            console.log(eval("(" + result + ")"));
            
        }
    });
}

