
let carousel1 = new Vue({ el: '#carousel1' });

let shortcut1 = new Vue({ el: '#shortcut' });

function selectGoods(id) {
    console.log(id)
    $.ajax({
        url: "/Commodity/SetCommodityID",
        type: "post",
        contentType: "application/json",
        async:false,
        dataType: "json", //返回数据格式为json
        data: JSON.stringify({ "ID": id }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data)
            window.location = "/Commodity/Details"

        }
    })
}

var rcmd = new Vue({
    el: '.rcmd_box',
    data: {
        mark: 0,
        goods:[]
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        }
    }
})

var part1 = new Vue({
    el: '#part1',
    data: {
        type: "服装",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part2 = new Vue({
    el: '#part2',
    data: {
        type: "电子设备",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part3 = new Vue({
    el: '#part3',
    data: {
        type: "书籍",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part4 = new Vue({
    el: '#part4',
    data: {
        type: "宠物",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part5 = new Vue({
    el: '#part5',
    data: {
        type: "运动",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part6 = new Vue({
    el: '#part6',
    data: {
        type: "食品",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part7 = new Vue({
    el: '#part7',
    data: {
        type: "家居",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part8 = new Vue({
    el: '#part8',
    data: {
        type: "美妆",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

var part9 = new Vue({
    el: '#part9',
    data: {
        type: "洗护",
        goods: [
        ],
        rank: [
        ],
        number: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10'],
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        },
        chooseAddressRank(thing, index) {
            console.log(thing.commodityId)
            selectGoods(thing.commodityId)
            return thing.commodityId
        },
    }
});

let naviright = new Vue({
    el: '#naviRight',
})

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
            console.log(data)
            rcmd.goods = data
        }
    })
    
}

function start() {
    getid()
    getRcmd()
    setGoods("服装")
    setRank("服装")
    setGoods("电子产品")
    setRank("电子产品")
    setGoods("书籍")
    setRank("书籍")
    setGoods("宠物")
    setRank("宠物")
    setGoods("运动")
    setRank("运动")
    setGoods("食品")
    setRank("食品")
    setGoods("家居")
    setRank("家居")
    setGoods("美妆")
    setRank("美妆")
    setGoods("洗护")
    setRank("洗护")
}
window.onload = start()


function setGoods(type) {
    if (type == "服装") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "1" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part1.goods = data
            }
        })
        return
    }
    else if (type == "电子产品") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "2" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part2.goods = data
            }
        })
        return
    }
    else if (type == "书籍") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "3" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part3.goods = data
            }
        })
        return
    }
    else if (type == "宠物") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "4" }),
            success: function (data) {//请求成功完成后要执行的方法

                console.log(data)
                console.log("12")
                part4.goods = data
            }
        })
        return
    }
    else if (type == "运动") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "5" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part5.goods = data
            }
        })
        return
    }
    else if (type == "食品") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "6" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part6.goods = data
            }
        })
        return
    }
    else if (type == "家居") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "7" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part7.goods = data
            }
        })
        return
    }
    else if (type == "美妆") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "8" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part8.goods = data
            }
        })
        return
    }
    else if (type == "洗护") {
        $.ajax({
            url: "/Home/RecmdZoneCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "9" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part9.goods = data
            }
        })
        return
    }
}

function setRank(type) {
    if (type == "服装") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "1" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part1.rank = data
            }
        })
        return
    }
    else if (type == "电子产品") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "2" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part2.rank = data
            }
        })
        return
    }
    else if (type == "书籍") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "3" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part3.rank = data
            }
        })
        return
    }
    else if (type == "宠物") {
        $.ajax({
            url: "/Home/RankCommodities",
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "4" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part4.rank = data
            }
        })
        return
    }
    else if (type == "运动") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "5" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part5.rank = data
            }
        })
        return
    }
    else if (type == "食品") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "6" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part6.rank = data
            }
        })
        return
    }
    else if (type == "家居") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "7" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part7.rank = data
            }
        })
        return
    }
    else if (type == "美妆") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "8" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part8.rank = data
            }
        })
        return
    }
    else if (type == "洗护") {
        $.ajax({
            url: "/Home/RankCommodities",//json文件位置
            type: "post",
            contentType: "application/json",
            dataType: "json", //返回数据格式为json
            data: JSON.stringify({ "type": "9" }),
            success: function (data) {//请求成功完成后要执行的方法
                console.log(data)
                console.log("12")
                part9.rank = data
            }
        })
        return
    }
}


function submitSearch() {
    $.ajax({
        url: "/Search/SetSearchName",//json文件位置
        type: "post",
        contentType: "application/json",
        dataType: "json", //返回数据格式为json
        data: JSON.stringify({ Context: $("#Context").val() }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data)
            window.location = "/Search/SearchCommodity";
        }
    })
}

