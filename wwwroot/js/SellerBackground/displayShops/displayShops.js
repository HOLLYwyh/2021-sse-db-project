//还没有定义实例

function displayshops(sellerID) {
    $.ajax({
        type: "post",
        url: "/SellerBackground/DisplayShopsForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "SellerID": sellerID }),
        success: function (result) {

            //console.log(result);

            if (result == null) {
                //alert("No Shops!");
                //console.log("null");
            }
            else {
                //赋值给指定实例的数组
            }
        }
    });
}