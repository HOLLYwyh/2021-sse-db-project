//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

function validateForm() {
    var x = document.forms["myForm"]["phoneNumber"].value;
    if (x == null || x == "") {
        alert("需要输入手机号码");
        return false;
    }
    for (let i = 0; i < 11; i++) {
        if (x[i] < '0' || x[i] > '9') {
            alert("请输入正确的手机号");
            return false;
        }
    }
    if (x.length != 11) {
        alert("请输入正确的手机号");
        return false;
    }

    x = document.forms["myForm"]["nickName"].value;
    if (x == null || x == "") {
        alert("需要输入昵称");
        return false;
    }
    x = document.forms["myForm"]["password"].value;
    if (x == null || x == "") {
        alert("需要输入密码");
        return false;
    }
    var pat = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[^]{6,18}$/;
    if (!pat.test(x)) {
        alert("密码至少包含小写字母、大写字母和数字且长度为6-18位");
        return false;
    }

    let y = document.forms["myForm"]["confirmPassword"].value;
    if (y == null || y == "") {
        alert("需要再次输入密码");
        return false;
    }
    if (y != x) {
        alert("两次输入密码不一致");
        return false;
    }
    return true;
}


function signup() {
    if (!validateForm()) {
        /*alert("注册失败");*/
        return false;
    }

    $.ajax({
        type: "post",
        url: "/Entry/Test",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ phoneNumber: $("#phoneNumber").val(), password: $("#password").val() }),//也可以传"&username=sa&password=123456"
        success: function (result) {

            //if (document.cookie !== "") {
            //    //alert("成功~");
            //}
           /* else*/ {
                alert("该号码已被注册");
            }
        }
    });
}

