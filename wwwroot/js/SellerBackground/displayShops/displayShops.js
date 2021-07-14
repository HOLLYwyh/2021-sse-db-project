////let sh = new Vue({
////    el: '#list',    //某个组件或标签
////    data: {
////        objectList: [],
////        draw: false,
////    }
////})

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
                sh.objectList = result;//赋值给指定实例的数组
            }
        }
    });
}