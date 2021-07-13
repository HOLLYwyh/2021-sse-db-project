let shortcut1 = new Vue({ el: '#shortcut' });

let vm = new Vue({
    el: "#infox",
    data: {
        input:{
            code: "",
            id: "1234",
            price: "222",
            storage:"",
            name: 'xbtfdgdgfgs',
            intro: "6666666sdfgggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg",
            img: '../../Images/Home/index/a2.png',
            shopid: "",
            category:""
        },
        number: 1,
        
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
        }
    }
    /*created() {
        var that;
        $.ajax({
            url: "",
            type: "get",
            dataType: "json", //返回数据格式为json
            async: false,
            success: function (result) {//请求成功完成后要执行的方法
                var jsonData = eval("(" + result + ")");   //将json转换成对象
                that = jsonData.searchResult;
                console.log(jsonData.searchResult);
            }
        })
        this.input = that;
        this.$forceUpdate();
    }*/
})

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

var rcmd = new Vue({
    el: '#rcmd_box',
    data: {
        mark: 0,
        goods: []
    },
    methods: {
        chooseAddress(thing, index) {
            console.log(thing.ID)
            selectGoods(thing.ID)
            return thing.ID
        }
    }
})

class CommodityDetails extends HTMLElement {
    constructor() {
        super();

        this._shadowRoot = this.attachShadow({mode: 'open'});
        this._shadowRoot.appendChild(template.content.cloneNode(true));

        this.$name = this._shadowRoot.querySelector(".comoinfo_namebox p");
        this.$intro = this._shadowRoot.querySelector(".comoinfo_introbox p");
        this.$price = this._shadowRoot.querySelectorAll(".comoinfo_pricebox p")[2];
        this.$amount = this._shadowRoot.querySelector(".comoinfo_amountbox .comoinfo_amountshow");
        this.$minus = this._shadowRoot.querySelector(".comoinfo_amountbox .comoinfo_amountminus");
        this.$plus = this._shadowRoot.querySelector(".comoinfo_amountbox .comoinfo_amountplus");
        this.$buy = this._shadowRoot.querySelector(".comoinfo_buttonbox .comoinfo_buybutton");
        this.$cart = this._shadowRoot.querySelector(".comoinfo_buttonbox .comoinfo_addtocartbutton");
        this.$img = this._shadowRoot.querySelector(".comoinfo_mainimgbox img");

        this.$minus.addEventListener("click", () => {
            let como_num = parseInt(this.$amount.innerHTML.toString());
            if (como_num > 1) {
                como_num = como_num - 1;
            }
            this.$amount.innerHTML = como_num.toString();
        });

        this.$plus.addEventListener("click", () => {
            let como_num = parseInt(this.$amount.innerHTML.toString());
            como_num = como_num + 1;
            this.$amount.innerHTML = como_num.toString();
        });
        
        this.$buy.addEventListener("click", () => {
            //todo
        });

        this.$cart.addEventListener('click', () => {
            //todo
        });
        this.render();
    }

    static get observedAttributes() {
        return ['commodity_name', 'commodity_price', 'commodity_intro', 'commodity_imgurl'];
    }
    attributeChangedCallback(name, oldVal, newVal) {
        this[name] = newVal;
        this.render();
    }

    render() {
        this.$name.innerHTML = "哈哈哈";
        //this.$intro.innerHTML = "开心商品";
        this.$price.innerHTML = "交个朋友998";
        this.$amount.innerHTML = "1";
        this.$img.setAttribute("src", "");
    }
}

customElements.define('commodity-details', CommodityDetails);



window.onload = function () {
    let cd = document.createElement("commodity-details");
    //cd.outerHTML = "<commodity-details></commodity-details>";
    //cd.innerHTML = "<commodity-details></commodity-details>";
	document.body.appendChild(cd);
}

