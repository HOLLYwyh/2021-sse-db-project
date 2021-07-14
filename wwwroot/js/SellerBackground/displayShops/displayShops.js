let sh =new Vue({
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
            console.log(result);
            sh.objectList = result;
            sh.draw = true;
        }
    });
}

window.onload = displayshops(getCookie("sellerID"));