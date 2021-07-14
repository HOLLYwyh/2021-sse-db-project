let ord = new Vue({
    el: '#orders-list',
    data: {
        objectList: [],
        draw: false,
        shopID: ""
    }
})

function getShopID() {
    $.ajax({
        type: "get",
        url: "/SellerBackground/GetShopIDForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: null,
        success: function (result) {
            ord.shopID = result;      //获取后端存储的shopID信息
            console.log(result);
            console.log("NowShopID:");
            console.log(ord.shopID);
        }
    });
}

function displayorders(shopID) {
    $.ajax({
        type: "post",
        url: "/SellerBackground/DisplayOrdersForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "ShopID": shopID }),
        success: function (result) {
            console.log("allOrders:");
            console.log(result);
            ord.objectList = result;  //将订单信息给实例
            ord.draw = true;
        }
    });
}

window.onload = getShopID();
console.log(ord.shopID);
window.onload = displayorders(ord.shopID);
