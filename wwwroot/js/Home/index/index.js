
let carousel1 = new Vue({ el: '#carousel1' });

let shortcut1 = new Vue({ el: '#shortcut' });

var rcmd = new Vue({
    el: '.rcmd_box',
    data: {
        mark: 0,
        goods:[]
        /*goods: [
            {
                img: '../../Images/Home/index/a1.png', intro: "泰国进口金枕鲜榴莲1,泰国进口金枕鲜榴莲,泰国进口金枕1", shop: '金轮', link:'https://www.baidu.com/'
            },
            {
                img: '../../Images/Home/index/a2.png', intro: '泰国进口金枕鲜榴莲2,泰国进口金枕鲜榴莲,泰国进口金枕2', shop: '金轮', link:''
            },
            {
                img: '../../Images/Home/index/a3.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕3', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a4.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕4', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a5.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕5', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a1.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕1', shop: '金轮', link: ''
            },
        ],*/
    }
})

let part1 = new Vue({ el: '#part1' });

let part2 = new Vue({ el: '#part2' });
let part3 = new Vue({ el: '#part3' });
let part4 = new Vue({ el: '#part4' });
let part5 = new Vue({ el: '#part5' });
let part6 = new Vue({ el: '#part6' });
let part7 = new Vue({ el: '#part7' });
let part8 = new Vue({ el: '#part8' });
let part9 = new Vue({ el: '#part9' });

let naviright = new Vue({ el: '#naviRight' })

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i].trim();
        if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
    }
    return "";
}

function getid() {
    let id = getCookie("buyerID")
    if (id) {
        document.getElementById("utility1").innerHTML = `<a href="/Entry/BuyerLogOut" >注销</a>`
    }
    return id
}

function getRcmd() {                            //获取推荐栏里的6个商品信息
    $.ajax({
        type: "post",
        url: "/Entry/SellerSignUpForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ getType: "rcmd" }), //请求类型
        success: function (result) {
               //需要获取一个goods数组
            rcmd.goods=result.goods 
        }
    });
}

function test() {
    $.ajax({
        url: "../../js/Home/index/test.json",//json文件位置
        type: "get",
        dataType: "json", //返回数据格式为json
        success: function (data) {//请求成功完成后要执行的方法
            
            console.log(data.goods)
            rcmd.goods=data.goods
        }
    })
    
}

function start() {
    getid()
    test()
}

window.onload = start()


