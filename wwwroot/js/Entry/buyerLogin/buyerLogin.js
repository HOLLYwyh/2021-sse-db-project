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
        data: JSON.stringify({ ID: $("#ID").val(), password: $("#password").val() }),
        success: function (result) {
           alert("账号或密码错误");
        }
    });
}