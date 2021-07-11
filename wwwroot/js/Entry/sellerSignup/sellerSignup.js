new Vue({el:'#shortcutsi'});


new Vue({
        el:'#formm',
        data:{
            form: {
              name: '',
              phone: '',
              date: '',
              pswd:'',
              pswd2:'',
            }
          
          },
       
        methods: {
            onSubmit() {
                //这里还需要做注册检测的判断
                $.ajax({
                    type: "post",
                    url: "/Entry/SellerSignUpForm",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ phoneNumber: $("#phoneNumber").val(), nickName: $("#nickName").val(), password: $("#password").val() }),
                    success: function (result) {
                        var jsonData = eval("(" + result + ")");   //将json转换成对象
                        if (jsonData.signUp != "ERROR") {
                            window.location.href = "/Entry/SellerLogIn";
                        }
                        else {
                            alert("此号码已被注册!");
                        }
                    }
                });
          }
        }
})

