let ord = new Vue({
    el: '#home-list',
    data:{
        objectList: [],
        draw:false,
    }
})
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
            if (result == null) {
                alert("No orders!");
            }
            else {
                ord.objectList = result;
                ord.draw = true;
            }
        }
    });
}

window.onload = displayorders("1");
