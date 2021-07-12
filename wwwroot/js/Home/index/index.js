
let carousel1 = new Vue({ el: '#carousel1' });

let shortcut1 = new Vue({ el: '#shortcut' });

var rcmd = new Vue({
    el: '.rcmd_box',
    data: {
        mark: 0,
        goods:[]
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

function getRcmd() {
    $.ajax({
        url: "/Home/RcmdCommodity",
        type: "get",
        dataType: "json", //返回数据格式为json
        success: function (data) {//请求成功完成后要执行的方法
            rcmd.goods = data
            console.log("1")
        }
    })
    
}

function start() {
    getid()
    getRcmd()
}

window.onload = start()


