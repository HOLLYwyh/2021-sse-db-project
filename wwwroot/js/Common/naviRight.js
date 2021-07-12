//使用该组件需先引入
//<link rel="stylesheet" href="https://www.jq22.com/jquery/font-awesome.4.7.0.css">
//<script src="https://unpkg.com/element-ui/lib/index.js"></script>



Vue.component('naviright', {
    template: `
  <div class="qyzSidebar">
    <div class="sidebar">
        <!--购物车点击展开-->
        
        
        <!--点击菜单 -->
        <div class="sidebaRmenu">
            <!--个人-->
            <div class="sidebaPersonage">
                <i class="fa fa-user-o fa-lg user"></i>
                <div class="ttbox">
                    
                    <div>
                    
                    <el-avatar icon="el-icon-user-solid"></el-avatar>
                    
                    </div>
                    <div>
                        id:{{this.id}}
                    </div>
                    <div>
                        {{this.name}}
                    </div>
                    <div class="listli">
                        <a href="/Account/PersonalInformation"><div>用户中心</div></a>
                    </div>
                    <div class="listli">
                        我的订单
                    </div>
                    <div class="listli">
                        <a href="/Entry/BuyerLogOut">退出登录</a>
                        <i class="fa fa-sign-out" aria-hidden="true"></i> 
                    </div>
                </div>
                
            </div>

            <div class="sidebaPersonage">
                <a href="/Favorites/Follow" class="topa">
                    <i class="fa fa-heart-o fa-lg user"></i>
                    <em class="tab-text">关注</em>
                </a>
            </div>
            
            <!--购物车-->
            <div class="sidebaPersonage ">
                <a href="www.baidu.com" class="topa">
                    <i class="fa fa-shopping-cart fa-lg user"></i>
                    <em class="tab-text">购物车</em>
                </a>
            </div>
            <!--收藏-->
            <div class="sidebaPersonage ">
                <a href="/Favorites/favorite" class="topa">
                    <i class="fa fa-diamond fa-lg user"></i>
                    <em class="tab-text">收藏</em>
                </a>
            </div>
            <div class="sidebaPersonage">
                <a href="www.baidu.com" class="topa">
                    <i class="fa fa-book fa-lg user"></i>
                    <em class="tab-text">优惠券</em>
                </a>
            </div>
        </div>

    </div>
</div>
  `,

 data: function () {
        return {
            id:'',
            name: '昵称',
            img:''
        }
    },
    mounted() {
        //将Vue方法传到全局对象window中
        window.setid = this.setid;
        window.setName = this.setName
    },
    methods: {
        
        setid(idx) {
            console.log("22")
            this.id=idx
        },
        setName(namex) {
            this.name = namex
        },
        getCookie(cname) {
            var name = cname + "=";
            var ca = document.cookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
    },
    created(){
            let name = this.getCookie("buyerNickName")
            let id = this.getCookie("buyerID")
            console.log(name)
            console.log("11")
            console.log(decodeURI(name))
            if (name) {
                this.setid(decodeURI(id))
                this.setName(decodeURI(name))
            
        }
    }
})

