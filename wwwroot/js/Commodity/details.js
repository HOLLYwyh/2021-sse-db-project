let shortcut1 = new Vue({ el: '#shortcut' });
//右边栏
new Vue({
    el: "#naviRight"
})

function selectGoods(id) {
    console.log(id)
    $.ajax({
        url: "/Commodity/SetCommodityID",
        type: "post",
        contentType: "application/json",
        async: false,
        dataType: "json", //返回数据格式为json
        data: JSON.stringify({ "ID": id }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data)
            window.location = "/Commodity/Details"

        }
    })
}

let vm = new Vue({
    el: "#infox",
    data: {
        input:{
            code: "",
            commodityId: "1234",
            price: "222",
            storage:"",
            name: 'xbtfdgdgfgs',
            intro: "6666666sdfgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg",
            imgUrl: '../../Images/Home/index/a2.png',
            shopid: "",
            category:""
        },
        number: 1,
        goods: [],
        
    },
    methods: {
        plus() {
            this.number++;
        },
        minus() {
            if (this.number > 1) {
                this.number--;
            }
        },
        sendBuy() {   //购买
            var output = { ID: this.input.commodityId, Amount: this.number }
            $.ajax({
                type: "post",
                url: "/Purchase/SetCommodDetail",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify( output ),
                success: function (result) {
                    window.location = "/Purchase/ConfirmOrder"
                }
            });
        },
        addToCart() {  //添加购物车
            var output = { ID: this.input.commodityId, Amount: this.number }
            $.ajax({
                type: "post",
                url: "/Purchase/SetCommodDetail",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify(output),
                success: function (result) {
                }
            });
            $.ajax({
                type: "get",
                url: "/Commodity/AddToCart",
                async: false,
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    var jsonData = eval("(" + result + ")");   //将json转换成对象
                    if (jsonData.addToCart == "SUCCESS") {
                        alert("添加成功");
                    }
                    else {
                        alert("添加失败");
                    }
                }
            });
        },
        addToFav() {    //收藏商品
            $.ajax({
                type: "get",
                url: "/Commodity/AddToFavourite",
                async: false,
                contentType: "application/json",
                dataType: "json",
                success: function (result) {
                    var jsonData = eval("(" + result + ")");   //将json转换成对象
                    if (jsonData.addToFav == "SUCCESS") {
                        alert("添加成功");
                    }
                    else {
                        alert("添加失败");
                    }
                }
            });
        },
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        }
    },
    created() {
        var that;
        $.ajax({
            url: "/Commodity/GetMainCommodity",
            type: "get",
            dataType: "json", //返回数据格式为json
            async: false,
            success: function (result) {//请求成功完成后要执行的方法
                console.log(result);
                //var jsonData = eval("(" + result + ")");   //将json转换成对象
                that = result;
                //console.log(jsonData);
                //console.log(result);
            }
        })
        this.input = that;
        console.log(this.input)
        this.$forceUpdate();
    }
})




function getRcmd() {
    $.ajax({
        url: "/Commodity/GetRecomCommodity",
        type: "get",
        dataType: "json", //返回数据格式为json
        async: false,
        success: function (data) {//请求成功完成后要执行的方法
            console.log(133)
            console.log(data)
            vm.goods = data
            console.log(vm.goods)
        }
    })

}

window.onload=getRcmd()