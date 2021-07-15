const cart_item_template = document.createElement("template");
//右边栏
new Vue({
    el: "#naviRight"
})

//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

cart_item_template.innerHTML = `
<style>
.cart_comoitem {
    position: relative;
    height: 128px;
    width: 100%;
    left: 0;
    padding-bottom: 4px;
    border-bottom: 1px solid darkgrey;
}
.cart_comoitem div {
    float: left;
}
.cart_como_checkbox {
    position: absolute;
    left: 2%;
    top: 40%;
    background-color: rgba(139, 0, 139, 0);
}
.cart_como_img {
    position: absolute;
    left: 8%;
    width: 128px;
    height: 128px;
    overflow: hidden;
    background-color: rgba(189, 183, 107, 0);
}
.cart_como_img img {
    height: 128px;
    width: 128px;
}
.cart_como_name {
    text-align: left;
    line-height: 64px;
    font-size: 32px;
    position: absolute;
    left: 20%;
    height: 128px;
    width: 26%;
    background-color: rgba(0, 206, 209, 0);
}
.cart_como_perprice {
    text-align: center;
    line-height: 128px;
    font-size: 24px;
    position: absolute;
    left: 48%;
    height: 128px;
    width: 12%;
    background-color: rgba(255, 255, 255, 0);
}
.cart_como_amount {
    text-align: center;
    line-height: 128px;
    font-size: 24px;
    position: absolute;
    left: 60%;
    width: 12%;
    height: 128px;
    background-color: rgba(255, 255, 0, 0);
}
.cart_como_totprice {
    text-align: center;
    line-height: 128px;
    font-size: 24px;
    color: red;
    position: absolute;
    left: 72%;
    width: 12%;
    height: 128px;
    background-color: rgba(0, 38, 255, 0);
}
.cart_como_opt {
    text-align: center;
    font-size: 12px;
    position: absolute;
    right: 4%;
    top: 58px;
    width: fit-content;
    height: fit-content;
    background-color: rgba(255, 255, 0, 0);
}
.cart_como_opt:hover {
    text-decoration: underline;
    font-weight: bold;
    cursor: pointer;
}
</style>
<div class="cart_comoitem">
    <div class="cart_como_checkbox"><input type="checkbox" style="height: 16px; width: 16px;"></input></div>
    
    <div class="cart_como_img">
        <img src="../commoditydetails/1.png"></img>
    </div>

    <div class="cart_como_name">
        十五字十五字十五字十五字十五字
    </div>

    <div class="cart_como_perprice">
        123.00
    </div>

    <div class="cart_como_amount">
        1
    </div>

    <div class="cart_como_totprice">
        
    </div>

    <div class="cart_como_opt">
        删除
    </div>
</div>
`;

class CartItem extends HTMLElement {
    constructor(item_data) {
        super();

        this._shadowRoot = this.attachShadow({mode: 'closed'});
        this._shadowRoot.appendChild(cart_item_template.content.cloneNode(true));

        this.$commodityid = "";
        this.$name = this._shadowRoot.querySelector(".cart_como_name");
        this.$image = this._shadowRoot.querySelector(".cart_como_img img");
        this.$perprice = this._shadowRoot.querySelector(".cart_como_perprice");
        this.$amount = this._shadowRoot.querySelector(".cart_como_amount");
        this.$totprice = this._shadowRoot.querySelector(".cart_como_totprice");
        this.$delete = this._shadowRoot.querySelector(".cart_como_opt");
        this.$checkbox = this._shadowRoot.querySelector("input");

        this.$checkbox.addEventListener("click", () => {
            let cart = document.getElementsByTagName("cart-sum")[0];
            let delta = parseFloat(this.$totprice.innerHTML.toString());
            let price = parseFloat(cart.getAttribute("cartprice"));
            let num = parseInt(cart.getAttribute("cartnum"));
            
            if (this.$checkbox.checked == true) {
                num = num + 1;
                price = price + delta;
            }
            else {
                num = num - 1;
                price = price - delta;
            }
            
            cart.setAttribute("cartnum", num.toString());
            cart.setAttribute("cartprice", price.toFixed(2).toString());
        });

        this.$delete.addEventListener("click", () => {
            if (this.$checkbox.checked == true) {
            let cart = document.getElementsByTagName("cart-sum")[0];
            let delta = parseFloat(this.$totprice.innerHTML.toString());
            let price = parseFloat(cart.getAttribute("cartprice"));
            let num = parseInt(cart.getAttribute("cartnum"));
            num = num - 1;
            price = price - delta;
            cart.setAttribute("cartnum", num.toString());
            cart.setAttribute("cartprice", price.toFixed(2).toString());
            }
            $.ajax({
                type: "post",
                url: "/Purchase/DeleteCommodity",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ID:this.$commodityId}),
                success: function (result) {
                    console.log(result)
                }
            });

            this.parentNode.removeChild(this);
            //this.$commodityId;
        });
        this.$name.innerHTML = item_data.CommodityName;
        this.$amount.innerHTML = item_data.amount;
        this.$image.setAttribute("src", item_data.imgUrl);
        this.$totprice.innerHTML = item_data.Price;
        let price = parseFloat(this.$totprice.innerHTML.toString());
        let num = parseFloat(this.$amount.innerHTML.toString());
        this.$perprice.innerHTML = (price / num).toFixed(2).toString();
        this.$commodityId = item_data.commodityId;
    }

}

customElements.define("cart-item", CartItem);

const cart_sum_template = document.createElement("template");
cart_sum_template.innerHTML = `
<style>
.cart_buttonbox {
    position: relative;
    left: 0;
    top: 32px;
    width: 100%;
    height: 64px;
    background-color: rgb(156, 156, 156);
}
.cart_buy {
    color: white;
    border-radius: 8%;
    font-size: 32px;
    line-height: 64px;
    text-align: center;
    position: relative;
    left: 86%;
    top: 0;
    width: 128px;
    height: 64px;
    background-color: rgb(255, 145, 0);
    cursor: pointer;
}
.cart_buy:hover {
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
.cart_num {
    position: absolute;
    left: 40%;
    line-height: 64px;
    font-size: 16px;
}
.cart_num span {
    font-size: 32px;
    color: rgb(199, 0, 0);
}
.cart_totprice {
    position: absolute;
    left: 56%;
    line-height: 64px;
}
.cart_totprice span {
    font-size: 32px;
    color: rgb(199, 0, 0);
}
</style>
<div class="cart_buttonbox">
    <div class="cart_num">
        已选择商品数量：
        <span></span>
    </div>
    <div class="cart_totprice">
        总价（不含运费）：
        <span></span>
    </div>
    <div class="cart_buy">
        结算
    </div>
</div>
`;

class CartSum extends HTMLElement {
    constructor() {
        super();

        this._shadowRoot = this.attachShadow({mode: 'closed'});
        this._shadowRoot.appendChild(cart_sum_template.content.cloneNode(true));

        this.$cartComoNum = this._shadowRoot.querySelector(".cart_num span");
        this.$cartTotalPrice = this._shadowRoot.querySelector(".cart_totprice span");
        this.$buy = this._shadowRoot.querySelector(".cart_buy");

        this.$buy.addEventListener("click", () => {
            let items = document.querySelectorAll("cart-item");
            let len = items.length;
            let result = [];
            for (i = 0; i < len; i++) {
                if (items[i].$checkbox.checked == true) {
                    let tmp = { commodityId: items[i].$commodityId, amount: items[i].$amount.innerHTML.toString() };
                    result.push(tmp);
                }
            }
            $.ajax({
                url: "/Purchase/SetCartOrdedr",//json文件位置
                type: "post",
                contentType: "application/json",
                dataType: "json", //返回数据格式为json
                data: JSON.stringify({"cart":result}),
                success: function (data) {//请求成功完成后要执行的方法
                    var jsonData = eval("(" + data + ")");   //将json转换成对象
                    if (jsonData.result == "FALSE") {
                        alert("请先选择商品!!!!");
                    }
                    else {
                        window.location = "/Purchase/ConfirmOrder";
                    }
                }
            });
            
        });
    }

    get cartnum() {
        return this.getAttribute('cartnum');
    }
    set cartnum(value) {
        this.setAttribute('cartnum', value);
    }
    get cartprice() {
        return this.getAttribute('cartprice');
    }
    set cartprice(value) {
        this.setAttribute('cartprice', value);
    }
    static get observedAttributes() {
        return ['cartnum', 'cartprice'];
    }
    attributeChangedCallback(name, oldVal, newVal) {
        console.log(`Attribute: ${name} changed from ${oldVal} to ${newVal}`);
        this.render();
    }
    connectedCallback() {
        this.setAttribute("cartnum", "0");
        this.setAttribute("cartprice", "0.00");
    }
    render() {
        this.$cartComoNum.innerHTML = this.cartnum;
        this.$cartTotalPrice.innerHTML = this.cartprice;
    }
}

customElements.define("cart-sum", CartSum);
window.onload = function () {
    var dataArr;
    $.ajax({
        url: "/Purchase/GetCartDetail",//json文件位置

        type: "get",

        contentType: "application/json",
        dataType: "json", //返回数据格式为json
        //data: JSON.stringify({ "type": "1" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log("cart json get");
            dataArr = data;
            console.log(typeof (dataArr));
            //dataArr = JSON.parse(data);
            console.log(dataArr);
            let cart_item_list = document.getElementById("cart_itembox");
            console.log(cart_item_list);
            let len = dataArr.length;
            console.log(len);
            for (i = 0; i < len; i++) {
                //window.alert("1");
                cart_item_list.appendChild(new CartItem(data[i]));
                //console.log(data[i]);
            }
            cart_item_list.parentNode.appendChild(new CartSum());
        }
    });

}

