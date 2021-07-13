let shortcut1 = new Vue({ el: '#shortcut' });

const template = document.createElement("template");
template.innerHTML = `
<style>
.comoinfo_outerbox {
    position: absolute;
    left: 0;
    top: 0;
    right: 0;
    bottom: 0;
    width: 88vw;
    height: 49.5vw;
    background-color: #dddddd00;
    margin: auto;
    max-width: 1280px;
    max-height: 720px;
    min-width: 1080px;
    min-height: 607.5px;
}
.comoinfo_imgbox {
    float: left;
    position: absolute;
    left: 4%;
    width: 44%;
    height: 100%;
    background-color: rgba(64, 224, 208, 0);
    margin: 0px;
}
.comoinfo_mainimgbox {
    position: absolute;
    overflow: hidden;
    padding-bottom: 88%;
    left: 6%;
    top: 4%;
    width: 88%;
    height: 0;
    background-color: yellow;
    margin: 0px;
}
.comoinfo_mainimgbox img {
    width: 100%;
    height: 100%;
    position: absolute;
}
.comoinfo_imglistbox {
    position: absolute;
    left: 6%;
    top: 80%;
    height: 16%;
    width: 88%;
    background-color: rgba(127, 255, 212, 0);
    margin: 0px;
}
.comoinfo_infobox {
    float: left;
    position: absolute;
    top: 4%;
    left: 52%;
    width: 44%;
    height: 92%;
    background-color: #F0CEA0;
    margin: 0px;
    box-shadow:0 0 10px #000;
    border-radius: 2%;
}
.comoinfo_namebox {
    overflow: hidden;
    position: absolute;
    top: 2%;
    left: 2%;
    width: 96%;
    height: 18%;
    /*background-color: coral;*/
}
.comoinfo_namebox p {
    font-size: 38px;
    font-weight: bold;
    text-align: justify;
    margin: 0;
}
.comoinfo_introbox {
    position: absolute;
    top: 22%;
    left: 4%;
    width: 92%;
    height: 28%;
    /*background-color: cornflowerblue;*/
    overflow-y: scroll;
    box-shadow:inset 0px 10px 10px -10px rgb(99, 99, 99), inset 0px -10px 10px -10px rgb(99, 99, 99);
}
.comoinfo_introbox p {
    font-size: 16px;
    font-weight: normal;
    text-align: left;
    margin: 0;
    color: gray;
}
.comoinfo_pricebox {
    position: absolute;
    top: 52%;
    left: 0%;
    width: 100%;
    height: 8%;
    background-color: #c6cbcf;
}
.comoinfo_pricebox p {
    left: 5px;
    float: left;
    font-size: 36px;
    font-weight: bold;
    text-align: left;
    margin-left: 5px;
    margin-top: 0;
    color: red;
}
.comoinfo_amountbox {
    position: absolute;
    top: 62%;
    left: 2%;
    width: 96%;
    height: 18%;
    background-color: rgba(139, 0, 0, 0);
}
.comoinfo_buttonbox {
    position: absolute;
    top: 82%;
    left: 2%;
    width: 96%;
    height: 18%;
    background-color: rgba(71, 61, 139, 0);
}
.comoinfo_amountbutton {
    border: none;
    color: white;
    text-align: center;
    text-decoration: none;
    display: inline-block;
    font-size: 32px;
    transition-duration: 0.4s;
    cursor: pointer;
    background-color: rgb(168, 252, 99);
    color: black;
    border: 2px solid #555555;
}
.comoinfo_amountbutton:hover {
    background-color: #ffffff;
    color: rgb(0, 0, 0);
}
.comoinfo_amountminus {
    position: absolute;
    left: 16%;
    top: 50%;
    transform:translateY(-24px);
    width: 10%;
    height: 0px;
    padding-bottom: 10%;
    z-index: 1;
    line-height: 150%;
}
.comoinfo_amountplus {
    position: absolute;
    right: 48%;
    top: 50%;
    transform:translateY(-24px);
    width: 10%;
    height: 0px;
    padding-bottom: 10%;
    z-index: 1;
    line-height: 150%;
}
.comoinfo_amountshow {
    border: 2px solid #555555;
    position: absolute;
    z-index: 0;
    left: 26%;
    top: 50%;
    width: 16%;
    padding-bottom: 10%;
    transform:translateY(-24px);
    height: 0;
    text-align: center;
    font-size: 30px;
    background-color: white;
    line-height: 150%;
}
.comoinfo_buybutton {
    border: none;
    color: white;
    text-decoration: none;
    display: inline-block;
    cursor: pointer;
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;

    position: absolute;
    left: 10%;
    top: 20%;
    width: 32%;
    height: 50%;
    background-color: red;
    text-align: center;
    font-size: 28px;
    border: 2px solid #000000;
    line-height: 200%;
}
.comoinfo_buybutton:hover {
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
.comoinfo_addtocartbutton {
    border: none;
    color: white;
    text-decoration: none;
    display: inline-block;
    cursor: pointer;
    -webkit-transition-duration: 0.4s; /* Safari */
    transition-duration: 0.4s;

    position: absolute;
    right: 10%;
    top: 20%;
    width: 32%;
    height: 50%;
    background-color: rgb(33, 55, 255);
    text-align: center;
    font-size: 28px;
    border: 2px solid #000000;
    line-height: 200%;
}
.comoinfo_addtocartbutton:hover {
    box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24),0 17px 50px 0 rgba(0,0,0,0.19);
}
</style>
<div class="comoinfo_outerbox">
        <div class="comoinfo_imgbox">
            <div class="comoinfo_mainimgbox">
                <img src=""></img>
            </div>
            <div class="comoinfo_imglistbox">

            </div>
        </div>
        <div class="comoinfo_infobox">
            <div class="comoinfo_namebox">
                <p>商品名称商品名称商品名称商品名称商品名称</p>
            </div>
            <div class="comoinfo_introbox">
                <p>
                    长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试长文本测试
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍商品介绍
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                    accomodity introductionsafjddslkgahkghahgljdkghaccomodity introducasdas
                </p>
            </div>
            <div class="comoinfo_pricebox">
                <p style="color: gray; font-size: medium;"><br/>价格：</p>
                <p>¥</p>
                <p>168.00</p>
            </div>
            <div class="comoinfo_amountbox">
                <p style="color: gray; font-size: medium; font-weight: bold;"><br/><br/>商品数量：</p>
                <div class="comoinfo_amountbutton comoinfo_amountminus">
                    -
                </div>
                <div class="comoinfo_amountshow">1</div>
                <div class="comoinfo_amountbutton comoinfo_amountplus">
                    +
                </div>
            </div>
            <div class="comoinfo_buttonbox">
                <div class="comoinfo_buybutton">立即购买</div>
                <div class="comoinfo_addtocartbutton">加入购物车</div>
            </div>
        </div>
    </div>
`;

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

