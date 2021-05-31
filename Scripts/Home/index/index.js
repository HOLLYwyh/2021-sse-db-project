
var vm = new Vue({
    el:'.carousel',
    data:{
        
        mark:0,
        img:[
                '../../../Images/Home/Index/a1.png',
                '../../../Images/Home/Index/a2.png',
                '../../../Images/Home/Index/a3.png',
                '../../../Images/Home/Index/a4.png',
                '../../../Images/Home/Index/a5.png'
        ],
        time:null
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
});



