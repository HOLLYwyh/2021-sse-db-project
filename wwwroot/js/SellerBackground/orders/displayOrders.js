let ord = new Vue({
    el: '#orders-list',
    data:{
        objectList: [],
        draw: false,
        shopID:""
    }
})

function getShopID() {
    $.ajax({
        type: "post",
        url: "/SellerBackground/GetShopIDForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        //data: JSON.stringify({ "ShopID": shopID }),
        success: function (result) {
            //let temp = JSON.stringify(result);
            //console.log(result);
            ord.shopID =result;
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
            //let temp = JSON.stringify(result);
            //console.log(result);

            ord.objectList = result;
            ord.draw = true;
        }
    });
}

window.onload = getShopID();
window.onload = displayorders(ord.shopID);
