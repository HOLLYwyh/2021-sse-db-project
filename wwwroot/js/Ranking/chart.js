let shortlg=new Vue({
    el: "#shortcutlg"
})

//右边栏
new Vue({
    el: "#naviRight"
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
let content = new Vue({
    el: '#content',
    data:
    {
        which: 1,
        object:
            [
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "132",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
                {
                    ID: "123",
                    img: '../../Images/Ranking/1.png',
                    intro: "歪比歪比",
                    shop: "金轮",
                    description: "歪比八卜",
                    price: 999.9,
                },
            ],
    },
    methods: {
        aClick(index) {
            return this.object[index].ID;
        },
    }
})


function refresh() {
    if (content.which == 1) {
        $.ajax({
            type: "post",
            url: "",
            async: false,
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ "ID": which }), //请求类型
            success: function (result) {
                var jsonData = eval("(" + result + ")");
                content.object = jsonData;
            }
        });
    }
    else if (content.which == 2) {
        $.ajax({
            type: "post",
            url: "",
            async: false,
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ "ID": which }), //请求类型
            success: function (result) {
                var jsonData = eval("(" + result + ")");
                content.object = jsonData;
            }
        });
    }
    else if (content.which == 3) {
        $.ajax({
            type: "post",
            url: "",
            async: false,
            contentType: "application/json",
            dataType: "json",
            data: JSON.stringify({ "ID": which }), //请求类型
            success: function (result) {
                var jsonData = eval("(" + result + ")");
                content.object = jsonData;
            }
        });
    }
    else alert("bug")
}


function getData() {
    $.ajax({
        type: "post",
        url: "",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "ID": which }), //请求类型
        success: function (result) {
            var jsonData = eval("(" + result + ")");
            content.object = jsonData;
        }
    });
}
window.onload = getData();