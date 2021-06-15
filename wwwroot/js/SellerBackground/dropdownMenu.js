Vue.component('dropdown-menu', {
    props:{
        list:[],
    },
    data: function () {
        return {
            isShow: false,  //控制下拉菜单是否显示
        }
    },
    mounted: function () {
        document.addEventListener('click', (e) => {
            if (e.target.className !== "dropbtn" &&
                e.target.className!=="sub-btn") {
                this.isShow = false;
            }     
            else{
                this.isShow = true;
            }
        })
    },
    methods: {
        
    },
    template: `
    <div class="dropdown">
        <div class="dropbtn">
            <button class="dropbtn"></button>
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