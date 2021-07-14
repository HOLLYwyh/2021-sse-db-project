let sh =new Vue({
    el: "#shop",
    data:
    {
        objectList: [],
        draw:false,
    }
})

function displayshops(sellerID) {
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

window.onload = displayshops("1");