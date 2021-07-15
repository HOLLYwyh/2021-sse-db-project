Vue.component('shortcut',{
    template:`
    <div class="shortcut">
            <ul>
                <li>
                    <div class="col_EF4F00">
                        <div>
                            <a href="/Home/index">首页</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_FFCC00">
                        <div>
                            <a href="/Account/orders">个人中心</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_9FCC48">
                        <div>
                            <a href="/Purchase/ShoppingCart">购物车</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_00A3E7">
                        <div>
                            <a href="/SellerBackground/Home">英豪店铺</a>
                        </div>
                    </div>
                </li>
            </ul>
    </div>
    `,
    data:function(){
        return{
            cut:0
        }
    },
})