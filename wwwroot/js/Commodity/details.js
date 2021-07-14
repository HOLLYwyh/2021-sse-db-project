let shortcut1 = new Vue({ el: '#shortcut' });

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
        sendBuy() {
            var output = { number: this.number,id:this.input.id }

            $.ajax({
                type: "post",
                url: "",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ output }),
                success: function (result) {
                    alert("购买失败")
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