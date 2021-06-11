Vue.component('carousel',{
    template:  `<div class="carousel"  @mouseenter="enter" @mouseleave="leave" >
                <transition-group tag="ul" name="image"
                enter-active-class="animate__animated animate__fadeIn"
                leave-active-class="animate__animated animate__fadeOut"
                >
                <li v-for='(image,index) in img' :key='index' v-show="index === mark" style="position:absolute">
                <a href="javascript:;">
                        <img :src="image">
                </a>
                </li>
                </transition-group>
                <div class="bullet">
                <span v-for="(item,index) in img.length" :class="{'active':index === mark}"
                @click="change(index)" :key="index"></span>
                </div>
                
                </div>`,
    data:function () {
        return {
            
            mark:0,
            img:[
                    '../../../Images/Home/index/a1.png',
                    '../../../Images/Home/index/a2.png',
                    '../../../Images/Home/index/a3.png',
                    '../../../Images/Home/index/a4.png',
                    '../../../Images/Home/index/a5.png'
            ],
            time:null
            }
        },
    methods:{   
        change(i){
            this.mark = i;
        },
        prev(){
            this.mark--;
            if(this.mark === -1){
                this.mark = 4;
                return
            }
        },
        next(){
            this.mark++;
            if(this.mark === 5){
                this.mark = 0;
                return
            }
        },
        autoPlay(){
            this.mark++;
            if(this.mark === 5){
                this.mark = 0;
                return
            }
        },
        play(){
            this.time = setInterval(this.autoPlay,3000);
        },
        enter(){
            console.log('enter')
            clearInterval(this.time);
        },
        leave(){
            console.log('leave')
            this.play();
        }
        
    },
    created(){
        this.play()
    }
})