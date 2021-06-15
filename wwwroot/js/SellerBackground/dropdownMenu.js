Vue.component('dropdown-menu', {
    props:{
        num:String,
        list:[],
    },
    data: function () {
        return {
            isShow: false,  //控制下拉菜单是否显示
        }
    },
    mounted: function () {
        document.addEventListener('click', (e) => {
            //console.log("e.target:"+e.target);
            //console.log("e.tartget.id:"+e.target.id);
            //console.log("typeof(e.tartget.id):"+typeof(e.target.id));
            //console.log("this.num:"+this.num);
            //console.log("typeof(this.num):"+typeof(this.num));

            if(e.target.id == this.num &&
              (e.target.className ==="dropbtn"||
               e.target.className === "sub-btn")){
                this.isShow = true;
            }else{
                this.isShow = false;
            }
        })
    },
    template: `
    <div class="dropdown">
        <div class="dropbtn">
            <button class="dropbtn" v-bind:id="num"></button>
        </div>
        <div class="dropdown-content" v-show="isShow">
            <div v-for="item in list">
                <button class="sub-btn">
                <i v-bind:class="item.icon"></i>
                <span class="sub-btn">{{item.text}}</span>
                </button>
            </div>
        </div>
    </div>
    </div>
    `
})

let avatar = new Vue({ el: '#avatar' });
let messageBox = new Vue({el:'#message-box'});