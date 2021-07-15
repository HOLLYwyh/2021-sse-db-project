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
    box-shadow: 0px 1px 2px 1px rgb(32, 32, 32);
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

class ShopItem extends HTMLElement {
    constructor(item_data) {
        super();

        this._shadowRoot = this.attachShadow({ mode: 'closed' });
        this._shadowRoot.appendChild(shop_item_template.content.cloneNode(true));

        this.$img = this._shadowRoot.querySelector(".shopinfo_itembox_img img");
        this.$name = this._shadowRoot.querySelector(".shopinfo_itembox_itemname");
        this.$price = this._shadowRoot.querySelector(".shopinfo_itembox_itemprice");
        this.$link = this._shadowRoot.querySelector(".shopinfo_itembox_iteminfo");
        /*
        this.$img.setAttribute("src", item_data.img_url);
        this.$name.innerHTML = item_data.name;
        this.$price.innerHTML = item_data.price;
        */
        this.$link.addEventListener("click", () => {
            //todo, click to jump to commodity detail page
        });

    }
}

customElements.define("shop-item", ShopItem);

window.onload = function () {

}