let shortcutlg=new Vue({
    el:"#shortcutlg"
})
let naviright = new Vue({ el: '#naviRight' })
let order=new Vue({
    el:'#order',
    data:{
        object:{
        orderNum:123456789,
        money:123,
        name:123456789,
        pay:123456789,
        method:123456789,
        time:"2021.7.12.11:59",}
    },
})
function getData(){
    console.log(order.object)
    $.ajax({
        type: "post",
        url: "",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "Type": order.object/*content.which*/ }), //请求类型
        success: function (result) {
            //var jsonData = eval("(" + result + ")");
            //content.object = jsonData;
            ordert.object = result;
        },
    });
}
