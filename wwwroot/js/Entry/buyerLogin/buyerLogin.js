//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

function login() {
    

    $.ajax({
        type: "post",
        url: "/Entry/BuyerLogInForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ phoneNumber: $("#ID").val(), password: $("#password").val() }),//也可以传"&username=sa&password=123456"
        success: function (result) {

            //if (document.cookie !== "") {
            //    //alert("成功~");
            //}
           /* else*/ {
                alert("登录失败");
            }
        }
    });
}