Vue.component('shop', {
    data: function () {
        return {
            shopData: [{
                id: '123456',
                sellerId:'654321',
                name:'袁阿姨的中药铺',
                credit:23,
                type:'OFFICIAL_FLAGSHIP',
                picURL:'../../Images/SellerBackground/null.png'
            },
            {
                id: '123457',
                sellerId:'754321',
                name:'萌萌哒鸡蛋饼铺子',
                credit:66,
                type:'PLATFORM_AUTH',
                picURL:'../../Images/SellerBackground/null.png'
            },
            {
                id: '123457',
                sellerId:'754321',
                name:'萌萌哒鸡蛋饼铺子',
                credit:66,
                type:'PLATFORM_AUTH',
                picURL:'../../Images/SellerBackground/null.png'
            },
            {
                id: '123457',
                sellerId:'754321',
                name:'萌萌哒鸡蛋饼铺子',
                credit:66,
                type:'PLATFORM_AUTH',
                picURL:'../../Images/SellerBackground/null.png'
            }]
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
            
        }
    },
    template: `
        <el-card>
            <div class="shop" v-for="item in shopData" >
                <img :src="item.picURL" />
                <div class="info">
                    <div class="name">{{item.name}}</div>
                    <div class="type">{{drawType(item.type)}}</div>
                    <div class="credit">{{item.credit}}</div>
                    <el-button class="btn" type="primary" size="mini" v-on:click="handleClick(item.id)">进入店铺</el-button>
                </div>
            </div>
        </el-card>
    `
})

let shop = new Vue({ el: '#shop' });