

let sh = new Vue({
    el: "#shop",
    data:
    {
        objectList: [],
        draw:false,
    }
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

function setifshop(ifshop) {
    $.ajax({
        type: "post",
        url: "/SellerBackground/SetIfShopForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "IfShop": ifshop }),
        success: {}
    })
}


function displayshops(sellerID) {
    console.log("sellerID:");
    console.log(sellerID);
    $.ajax({
        type: "post",
        url: "/SellerBackground/DisplayShopsForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "SellerID": sellerID }),
        success: function (result) {
            console.log("result:");
            console.log(result);
            if (result === null || result === "" || typeof (result) === "undefined") {
                setifshop("1"); //设置“无店铺”信息
                if (window.location.href != "https://localhost:44393/SellerBackground/ShopSignUp") {
                    window.location.href = "/SellerBackground/ShopSignUp";
                }
            }
            else {
                sh.objectList = result;
                sh.draw = true;
                setifshop("1");  //设置“有店铺”信息
                if (window.location.href != "https://localhost:44393/SellerBackground/SwitchShop") {
                    window.location.href = "/SellerBackground/SwitchShop";
                }
            }
        },
        error: function (result) {
            setifshop("0");      //设置“无店铺”信息
            if (window.location.href != "https://localhost:44393/sellerbackground/shopsignup") {
                window.location.href = "/sellerbackground/shopsignup";
            }
        }
    });
}

window.onload = displayshops(getCookie("sellerID"));