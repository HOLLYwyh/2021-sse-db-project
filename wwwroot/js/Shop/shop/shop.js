//右边栏
new Vue({
    el: "#naviRight"
})

//渲染导航栏
new Vue({
    el: "#shortcutlg"
})

const shop_item_template = document.createElement("template");
shop_item_template.innerHTML = `
<style>
.shopinfo_itembox {
    float: left;
    position: relative;
    height: 144px;
    width: 144px;
    background-color: rgba(0, 0, 255, 0);
    overflow: hidden;
    z-index: 1;
    margin-left: 4.5%;
}
.shopinfo_itembox_img {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    width: 100px;
    height: 100px;
    border-radius: 100%;
    /*border: 2px solid gray;*/
    box-shadow: 0px -1px 8px 1px rgb(255, 255, 255);
    pointer-events: none;
}
.shopinfo_itembox_img img{
    height: 100px;
    width: 100px;
    /*border: 1px solid rgb(94, 94, 94);*/
    border-radius: 100%;
    box-shadow: 0px 60px 16px -16px rgb(94, 94, 94);
}
.shopinfo_itembox_light {
    background-color: white;
    border: transparent;
    border-radius: 100%;
    position: absolute;
    width: 16px;
    height: 16px;
    top: 0;
    left: 62px;
    transform: translateY(-8px);
    box-shadow: 0px 0px 20px 20px rgba(255, 255, 255, 0.96);
}
.shopinfo_itembox_iteminfo {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    width: 100px;
    height: 100px;
    border-radius: 100%;
    background-color: rgba(255, 255, 255, 0);
    z-index: -1;
    overflow: hidden;
}
.shopinfo_itembox_itemname {
    overflow: hidden;
    position: absolute;
    width: 100%;
    height: 42%;
    top: 24%;
    text-align: center;
    font-size: 14px;
    font-weight: bold;
    color: rgb(255, 255, 255);
}
.shopinfo_itembox_itemprice {
    position: absolute;
    width: 100%;
    height: 16%;
    top: 72%;
    text-align: center;
    font-size: 12px;
    font-weight: bold;
    color: red;
}
.shopinfo_itembox_iteminfo:hover {
    background-color: rgba(128, 128, 128, 0.6);
    z-index: 2;
    cursor: pointer;
}
</style>
<div class="shopinfo_itembox">
    <div class="shopinfo_itembox_light"></div>
    <div class="shopinfo_itembox_iteminfo">
        <div class="shopinfo_itembox_itemname">商品名称商品名称商品名称商品名称</div>
        <div class="shopinfo_itembox_itemprice">商品价格</div>
    </div>
    <div class="shopinfo_itembox_img">
        <img src="./7-6.PNG"></img>
    </div>
</div>
`;
//let tmp = { name: "数据库算法导论操作系统计算机系统结构", price: "998.00", img_url: "" };
class ShopItem extends HTMLElement {
    constructor(item_data) {
        super();
        //item_data = tmp;
        this._shadowRoot = this.attachShadow({ mode: 'closed' });
        this._shadowRoot.appendChild(shop_item_template.content.cloneNode(true));

        this.$img = this._shadowRoot.querySelector(".shopinfo_itembox_img img");
        this.$name = this._shadowRoot.querySelector(".shopinfo_itembox_itemname");
        this.$price = this._shadowRoot.querySelector(".shopinfo_itembox_itemprice");
        this.$link = this._shadowRoot.querySelector(".shopinfo_itembox_iteminfo");
        this.$img.setAttribute("src", item_data.Url);
        this.$name.innerHTML = item_data.Name;
        this.$price.innerHTML = item_data.Price;
        this.$comid = item_data.CommodityId;
        this.$link.addEventListener("click", () => {
            //todo, click to jump to commodity detail page
            $.ajax({
                url: "/Commodity/SetCommodityID",
                type: "post",
                dataType: "json", //返回数据格式为json
                contentType: "application/json; charset=utf-8",
                async: false,
                data: JSON.stringify({ ID: this.$comid }),
                success: function (data) {//请求成功完成后要执行的方法
                    window.location = "/Commodity/Details"
                }
            })
        });

    }
}

customElements.define("shop-item", ShopItem);

const shop_title_template = document.createElement("template");
shop_title_template.innerHTML = `
<style>
.shopinfo_titlebox {
    position: absolute;
    left: 0;
    top: 0;
    width: 100%;
    height: 128px;
    background-color: #7a7a6c;
    font-family: cursive;
}
.followme_button {
    position: absolute;
    left: 75%;
    height: 24px;
    bottom: 8px;
    text-align: center;
    line-height: 24px;
    font-size: 20px;
    width: fit-content;
    padding: 4px 4px;
    color: #d9be3f;
    transition: 0.5s;
    letter-spacing: 4px;
    cursor: pointer;
    overflow: hidden;
}
.followme_button:hover {
    background-color: #d9be3f;
    color: #050801;
    box-shadow: 0 0 5px #d9be3f,
                0 0 25px #d9be3f,
                0 0 50px #d9be3f,
                0 0 200px #d9be3f;
}
.followme_button div {
    position: absolute;
}
.followme_button div:nth-child(1){
    width: 100%;
    height: 2px;
    top: 0;
    left: -100%;
    background: linear-gradient(to right,transparent,#d9be3f);
    animation: animate1 1s linear infinite;
}
.followme_button div:nth-child(2){
    width: 2px;
    height: 100%;
    top: -100%;
    right: 0;
    background: linear-gradient(to bottom,transparent,#d9be3f);
    animation: animate2 1s linear infinite;
    animation-delay: 0.25s;
}
.followme_button div:nth-child(3){
    width: 100%;
    height: 2px;
    bottom: 0;
    right: -100%;
    background: linear-gradient(to left,transparent,#d9be3f);
    animation: animate3 1s linear infinite;
    animation-delay: 0.5s;
}
.followme_button div:nth-child(4){
    width: 2px;
    height: 100%;
    bottom: -100%;
    left: 0;
    background: linear-gradient(to top,transparent,#d9be3f);
    animation: animate4 1s linear infinite;
    animation-delay: 0.75s;
}
@keyframes animate1 {
    0% {
      left: -100%;
    }
    50%,100% {
      left: 100%;
    }
}
@keyframes animate2 {
    0% {
      top: -100%;
    }
    50%,100% {
      top: 100%;
    }
}
@keyframes animate3 {
    0% {
      right: -100%;
    }
    50%,100% {
      right: 100%;
    }
}
@keyframes animate4 {
    0% {
      bottom: -100%;
    }
    50%,100% {
      bottom: 100%;
    }
}
.shopinfo_name {
    position: absolute;
    left: 25%;
    top: 0;
    width: 50%;
    height: 128px;
    font-size: 64px;
    color: rgb(0, 0, 0);
    font-weight: bold;
    text-align: center;
    line-height: 194px;
    font-family: cursive;
    z-index: 1;
    text-shadow: 0px 2px 8px white;
}
</style>
<div class="shopinfo_titlebox" style="background-color: #1a1515;">
	<div class="followme_button">
		<div></div>
		<div></div>
		<div></div>
		<div></div>
		关注
	</div>
	<div class="shopinfo_name">
		英豪荟萃官方店铺
	</div>
</div>
`;

class ShopTitle extends HTMLElement {
    constructor(shop_data) {
        super();
        //let tmp = { name: "英豪荟萃官方旗舰店", shopId: "1" };
        //shop_data = tmp;
        this._shadowRoot = this.attachShadow({ mode: 'closed' });
        this._shadowRoot.appendChild(shop_title_template.content.cloneNode(true));
        this.$shopId = shop_data.shopID;
        this.$followme = this._shadowRoot.querySelector(".followme_button");
        this.$name = this._shadowRoot.querySelector(".shopinfo_name");
        this.$name.innerHTML = shop_data.shopName;
        this.$followme.addEventListener("click", () => {
            //todo click to follow this shop
            $.ajax({
                type: "post",
                url: "/Account/AddFollowShop",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({
                    buyerid: this.$shopId,               // 买家ID
                    shopid: this.$shopId                 // 店铺ID
                }),
                success: function (result) {        // bool
                    let ret = result.search("SUCCESS");
                    console.log(ret);
                    if (ret != -1) {
                        alert("关注成功");
                    }
                    else {
                        alert("关注失败");
                    }
                }
            })
        });
    }
}

customElements.define("shop-title", ShopTitle);

const shop_display_template = document.createElement("template");
shop_display_template.innerHTML = `
<style>
.shopinfo_displaybox {
    position: absolute;
    left: 0;
    top: 176px;
    height: 640px;
    width: 100%;
    background-color: rgba(173, 216, 230, 0);
}
.shopinfo_linebox {
    position: relative;
    width: 100%;
    height: 160px;
    /*background-color: #1a1515;*/
    perspective:100px;
    overflow: hidden;
    background-image: linear-gradient(#1a1515, white);
}
.shopinfo_linebox_bottomboard_dark {
    position: absolute;
    width: 100%;
    bottom: 0;
    height: 16px;
    background-color: #6e4d2d;
    z-index: 1;
}
.shopinfo_linebox_bottomboard_light {
    position: absolute;
    left: 20px;
    width: 1240px;
    bottom: 16px;
    height: 48px;
    background-color: #eed5b4;
    /*background-color: #fcedbc;*/
    transform-style: preserve-3d;
    transform-origin:50% 50%;
    transform:rotateX(8deg);
    z-index: 1;
}
.shopinfo_itembox {
    float: left;
    position: relative;
    height: 144px;
    width: 144px;
    background-color: rgba(0, 0, 255, 0);
    overflow: hidden;
    z-index: 1;
    margin-left: 4.5%;
}
.shopinfo_itembox_img {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    width: 100px;
    height: 100px;
    border-radius: 100%;
    /*border: 2px solid gray;*/
    box-shadow: 0px 1px 2px 1px rgb(32, 32, 32);
    pointer-events: none;
}
.shopinfo_itembox_img img{
    height: 100px;
    width: 100px;
    /*border: 1px solid rgb(94, 94, 94);*/
    border-radius: 100%;
    box-shadow: 0px 64px 10px -16px rgb(94, 94, 94);
}
.shopinfo_itembox_light {
    background-color: white;
    border: transparent;
    border-radius: 100%;
    position: absolute;
    width: 16px;
    height: 16px;
    top: 0;
    left: 60px;
    transform: translateY(-8px);
    box-shadow: 0px 0px 20px 20px rgba(255, 255, 255, 0.96);
}
.shopinfo_itembox_iteminfo {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    margin: auto;
    width: 100px;
    height: 100px;
    border-radius: 100%;
    background-color: rgba(255, 255, 255, 0);
    z-index: -1;
    overflow: hidden;
}
.shopinfo_itembox_itemname {
    overflow: hidden;
    position: absolute;
    width: 100%;
    height: 42%;
    top: 24%;
    text-align: center;
    font-size: 14px;
    font-weight: bold;
    color: rgb(255, 255, 255);
}
.shopinfo_itembox_itemprice {
    position: absolute;
    width: 100%;
    height: 16%;
    top: 72%;
    text-align: center;
    font-size: 12px;
    font-weight: bold;
    color: red;
}
.shopinfo_itembox_iteminfo:hover {
    background-color: rgba(128, 128, 128, 0.6);
    z-index: 3;
    cursor: pointer;
}
.qiangjiao1 {
    position: absolute;
    left: 40px;
    height: 100%;
    width: 2px;
    top: 0;
    background-color: #75695c;
    z-index: 1;
}
.qiangjiao2 {
    position: absolute;
    right: 40px;
    height: 100%;
    width: 2px;
    top: 0;
    background-color: #75695c;
    z-index: 1;
}
</style>
<div class="shopinfo_displaybox">
	<div id="line1" class="shopinfo_linebox">
		<div class="qiangjiao1"></div>
		<div class="qiangjiao2"></div>
		<div class="shopinfo_linebox_bottomboard_light"></div>
		<div class="shopinfo_linebox_bottomboard_dark"></div>
	</div>
	<div id="line2" class="shopinfo_linebox">
		<div class="qiangjiao1"></div>
		<div class="qiangjiao2"></div>
		<div class="shopinfo_linebox_bottomboard_light"></div>
		<div class="shopinfo_linebox_bottomboard_dark"></div>
	</div>
	<div id="line3" class="shopinfo_linebox">
		<div class="qiangjiao1"></div>
		<div class="qiangjiao2"></div>
		<div class="shopinfo_linebox_bottomboard_light"></div>
		<div class="shopinfo_linebox_bottomboard_dark"></div>
	</div>
	<div id="line4" class="shopinfo_linebox">
		<div class="qiangjiao1"></div>
		<div class="qiangjiao2"></div>
		<div class="shopinfo_linebox_bottomboard_light"></div>
		<div class="shopinfo_linebox_bottomboard_dark"></div>
	</div>
</div>
`;

class ShopDisplay extends HTMLElement {
    constructor() {
        super();

        this._shadowRoot = this.attachShadow({ mode: 'closed' });
        this._shadowRoot.appendChild(shop_display_template.content.cloneNode(true));

        this.$borders = this._shadowRoot.querySelectorAll(".shopinfo_linebox");
    }
}

customElements.define("shop-display", ShopDisplay);

window.onload = function () {
    $.ajax({
        url: "/Shop/GetShop",//json文件位置
        type: "get",
        contentType: "application/json",
        dataType: "json", //返回数据格式为json
        //data: JSON.stringify({ "type": "1" }),
        success: function (data) {//请求成功完成后要执行的方法
            console.log(data);
            //var jsonData = eval("(" + data + ")");   //将json转换成对象
            let jsonData = data;
            let shopInfo = jsonData.Shop;
            let shop_main = document.getElementById("shop_main");
            shop_main.appendChild(new ShopTitle(shopInfo));
            let items = jsonData.CommodityViews;//shop's commodities
            let len = items.length;
            let cupboard = document.querySelector("shop-display");
            for (i = 0; i < 4; i++) {
                for (j = 0; j < 6 && i * 6 + j < len; j++) {
                    cupboard.$borders[i].appendChild(new ShopItem(items[i * 6 + j]));
                    //console.log(cupboard.$borders[i])
                }
            }
        }
    });
}