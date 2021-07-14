

Vue.component('shop', {
    props:["objlist","draw"],
    //mounted(){
    //    this.displayshops(this.getCookie("sellerID"));
    //    this.displayshops("1");
    //    console.log("1111111111111111");
    //    console.log(this.objlist)
    //},
    watch: {
        draw: function (curVal, oldVal) {
            if (curVal === true) {
                
            }
        }
    },
    methods: {
        drawType(type){
            if(type==='OFFICIAL_FLAGSHIP'){
                return '官方旗舰店';
            }else if(type==='PLATFORM_AUTH'){
                return '平台认证店';
            }else if(type==='INDIVIDUAL'){
                return '个人店铺';
            }else if(type==='BANNED_SHOP'){
                return '被封禁店铺';
            }else{
                return '未定义';
            }
        },
        handleClick(id){
            console.log(id);
           //this.displayorders(id);
            
        }
    },
    template: `
        <el-card>
            <div class="shop" v-for="item in objlist" >
                <img :src="item.img" />
                <div class="info">
                    <div class="name">{{item.shopName}}</div>
                    <div class="type">{{drawType(item.type)}}</div>
                    <div class="credit">{{item.creditScore}}</div>
                    <el-button class="btn" type="primary" size="mini" v-on:click="handleClick(item.shopID)">进入店铺</el-button>
                </div>
            </div>
        </el-card>
    `
})

