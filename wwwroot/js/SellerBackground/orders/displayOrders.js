let ord = new Vue({
    el: '#orders-list',
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
            if (result==null) {
                alert("No orders!");
                console.log("null")
            }
            else {
                ord.objectList = result;
                ord.draw = true;
                console.log("draw1");
                console.log(ord.draw);
                /*console.log("!!!result");
                console.log(result);
                console.log("ord.objectList!!!!");
                console.log(ord.objectList)*/
                console.log("not null");
            }
            //console.log(result);
        }
    });
}


window.onload = displayorders("1");
