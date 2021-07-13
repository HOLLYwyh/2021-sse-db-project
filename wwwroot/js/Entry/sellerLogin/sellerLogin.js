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
              type:'',
              
            }
        },
       
        methods: {
          onSubmit() {
                $.ajax({
                    type: "post",
                    url: "/Entry/SellerLogInForm",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ ID: $("#ID").val(), password: $("#password").val() }),
                    success: function (result) {
                        var jsonData = eval("(" + result + ")");   //将json转换成对象
                        if (jsonData.LogIn != "ERROR") {
                            window.location.href = "/SellerBackground/Home";
                        }
                        else {
                            alert("账号或密码错误!");
                        }
                    }
                });
          }
        }
})

