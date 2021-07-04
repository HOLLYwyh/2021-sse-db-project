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
                        id
                    </div>
                    <div>
                        昵称
                    </div>
                    <div class="listli">
                        <a href="">用户中心</a>
                    </div>
                    <div class="listli">
                        我的订单
                    </div>
                    <div class="listli">
                        退出
                        <i class="fa fa-sign-out" aria-hidden="true"></i> 
                    </div>
                </div>
                
            </div>

            <div class="sidebaPersonage">
                <a href="www.baidu.com" class="topa">
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
                <a href="www.baidu.com" class="topa">
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
  `
})

