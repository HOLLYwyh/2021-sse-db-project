Vue.component('shortcut',{
    template:`
    <div class="shortcut">
            <ul>
                <li>
                    <div class="col_EF4F00">
                        <div>
                            <a href="index.html">主界面</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_FFCC00">
                        <div>
                            <a href="http://">账户中心</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_9FCC48">
                        <div>
                            <a href="http://">加入英豪</a>
                        </div>
                    </div>
                </li>
                <li>
                    <div class="col_00A3E7">
                        <div>
                            <a href="http://">网站导航</a>
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