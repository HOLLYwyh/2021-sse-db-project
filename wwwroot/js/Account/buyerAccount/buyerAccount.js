//账户中心页不同界面切换
let which = 0;
function set(which) {
    let accountHome = document.getElementById('account-home');
    let accountInfo = document.getElementById('account-info');
    let accountSafe = document.getElementById('account-safe');
    let historyOrders = document.getElementById('history-orders');
    let cart = document.getElementById('cart');
    let coupon = document.getElementById('coupon');
    let bookmark = document.getElementById('bookmark');
    let folShops = document.getElementById('fol-shops');
    let folGoods = document.getElementById('fol-goods');
    let addressManagement = document.getElementById('address-management');
    accountHome.style.display = 'none';
    accountInfo.style.display = 'none';
    accountSafe.style.display = 'none';
    historyOrders.style.display = 'none';
    cart.style.display = 'none';
    coupon.style.display = 'none';
    bookmark.style.display = 'none';
    folShops.style.display = 'none';
    folGoods.style.display = 'none';
    addressManagement.style.display = 'none';
    switch (which) {
        case 0: accountHome.style.display = 'flex'; break;
        case 1: accountInfo.style.display = 'flex'; break;
        case 2: accountSafe.style.display = 'flex'; break;
        case 3: historyOrders.style.display = 'flex'; break;
        case 4: cart.style.display = 'flex'; break;
        case 5: coupon.style.display = 'flex'; break;
        case 6: bookmark.style.display = 'flex'; break;
        case 7: folShops.style.display = 'flex'; break;
        case 8: folGoods.style.display = 'flex'; break;
        case 9: addressManagement.style.display = 'flex'; break;
    }

}

const INFO = document.querySelector('#self-info');
INFO.addEventListener('click', function () { which = 1; set(which); });

const SECURITY = document.querySelector('#ac-security');
SECURITY.addEventListener('click', function () { which = 2; set(which); });

const HISTORY = document.querySelector('#history_orders');
HISTORY.addEventListener('click', function () { which = 3; set(which); });

const CART = document.querySelector('#shoppingcart');
CART.addEventListener('click', function () { which = 4; set(which); });

const COUPON = document.querySelector('#coupons');
COUPON.addEventListener('click', function () { which = 5; set(which); });

const BOOKMARK = document.querySelector('#bookmarks');
BOOKMARK.addEventListener('click', function () { which = 6; set(which); });

const SHOP = document.querySelector('#follow-shops');
SHOP.addEventListener('click', function () { which = 7; set(which); });

const GOOD = document.querySelector('#follow-goods');
GOOD.addEventListener('click', function () { which = 8; set(which); });

const ADDRESS = document.querySelector('#address-manage');
ADDRESS.addEventListener('click', function () { which = 9; set(which); });

//复制ID或昵称
function copyID() {
    let id = document.getElementById("id").innerText;
    let iD = document.getElementById("iD");
    iD.value = id;
    iD.select();
    document.execCommand("copy");
    alert("您的账号已复制到剪贴板！");
}
const COPIEDID = document.querySelector('#copy1');
COPIEDID.addEventListener('click', copyID);//这不对啊？

function copyNN() {
    let nickname = document.getElementById("nick-name_input");
    nickname.select();
    document.execCommand("copy");
    alert("您的昵称已复制到剪贴板！");
}
const COPIEDNN = document.querySelector('#copy2');
COPIEDNN.addEventListener('click', copyNN);

//账户安全界面修改按钮
