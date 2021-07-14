
function createshop() {
    $.ajax({
        type: "post",
        url: "/SellerBackground/ShopSignUpForm",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ SellerID: $("#sellerID").val(), Name: $("#name").val(), Category: $("#category").val(), Description: $("#description").val()}),
        success: function (result) {
            if (result === true) {
                window.location.href = "/SellerBackground/SwitchShop";
            }
            else {
                window.location.href = "/SellerBackground/ShopSignUp";
            }
        }
    });
}