let shortlg=new Vue({
    el: "#shortcutlg"
})
//排行榜分类
let hot=new Vue({
    el:'#hot',
    data:{
        isActive:true,
        checkf24:false,
    },
    methods:{
        divClick:function(){
            this.isActive=true;
            this.checkf24=false;
            search.isActive=true;
            search.checkf25=false;
            praise.isActive=true;
            praise.checkf26=false;  
            content.which=1;
            content.refresh();
        }
        
    }
})

let search=new Vue({
    el:'#search',
    data:{
        isActive:true,
        checkf25:false,
    },
    methods:{
        divClick:function(){
            this.isActive=false;
            this.checkf25=true;
            hot.isActive=false;
            hot.checkf24=true;
            praise.isActive=true;
            praise.checkf26=false;
            content.which=2;
            content.refresh();
        },
       
        
    }
})
let praise=new Vue({
    el:'#praise',
    data:{
        isActive:true,
        checkf26:false,
    },
    methods:{
        divClick:function(){
            this.isActive=false;
            this.checkf26=true;
            hot.isActive=false;
            hot.checkf24=true;
            search.isActive=true;
            search.checkf25=false;
            content.which=3;
            content.refresh();
        }
    }
})
//内容
let content=new Vue({
    el:'#content',
    data:
    {
        which: 1,
        object:
        [
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,}, 
        {   id:"132",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},  
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
         {  id:"123",
             pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
         {  id:"123",
             pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {  id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {  id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        {   id:"123",
            pic: '../../Images/Ranking/1.png',
            name:"歪比歪比",
            description:"歪比八卜",
            price:999,},
        ], 
    },
    methods:{
        aClick(index){
            return this.object[index].id;
        },
        refresh(){
            if(this.which==1){
               alert("1")
            }
            else if(this.which==2)
            {
                alert("2")
            }
            else if(this.which==3)
            {
                alert("3")
            }
            else alert("bug")
        }
    }
})