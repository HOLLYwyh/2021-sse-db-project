let shortlg=new Vue({
    el: "#shortcutlg"
})
let which = 1;
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
            window.which=1;
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
            window.which=2;
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
            window.which=3;
            content.refresh();
        }
    }
})
//内容
let content = new Vue({
    el: '#content',
    data:
    {
        object:[
            ],
    },
    methods: {
        aClick(index) {
            $.ajax({
                type: "post",
                url: "/Commodity/SetCommodityID",
                async: false,
                contentType: "application/json",
                dataType: "json",
                data: JSON.stringify({ "ID": this.object[index].ID }), //请求类型
                success: function (result) {
                    console.log(result);
                    window.location.href = "/Commodity/details";
                }
            });
        },
        refresh() {
            console.log("which是");
            console.log(window.which);
            if (window.which == 1) {
                $.ajax({
                    type: "post",
                    url: "/Ranking/GetRankList",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ Type: which }), //请求类型
                    success: function (result) {
                        content.object = result;
                    }
                });
                for (let i = 0; i < 15; i++) {
                    content.object[i].description = content.object[i].description.substr(0, 50);
                }
            }
            else if (window.which == 2) {
                $.ajax({
                    type: "post",
                    url: "/Ranking/GetRankList",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ Type: which }), //请求类型
                    success: function (result) {
                        content.object = result;
                    }
                });
                for (let i = 0; i < 15; i++) {
                    content.object[i].description = content.object[i].description.substr(0, 50);
                }
            }
            else if (window.which == 3) {
                $.ajax({
                    type: "post",
                    url: "/Ranking/GetRankList",
                    async: false,
                    contentType: "application/json",
                    dataType: "json",
                    data: JSON.stringify({ Type: which }), //请求类型
                    success: function (result) {
                        content.object = result;
                    }
                });
                for (let i = 0; i < 15; i++) {
                    content.object[i].description = content.object[i].description.substr(0, 50);
                }
            }
            else alert("bug")
        }
    },
})

function getData() {
    console.log(window.which)
    $.ajax({
        type: "post",
        url: "/Ranking/GetRankList",
        async: false,
        contentType: "application/json",
        dataType: "json",
        data: JSON.stringify({ "Type": which }), //请求类型
        success: function (result) {
            content.object = result;
            console.log(result)
        },
    });
    for (let i = 0; i < 15; i++) {
        content.object[i].description = content.object[i].description.substr(0, 50);
    }
}
window.onload = getData();