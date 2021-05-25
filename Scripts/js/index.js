
var vm = new Vue({
    el:'.carousel',
    data:{
        
        mark:0,
        img:[
                '../../images/a1.png',
                '../../images/a2.png',
                '../../images/a3.png',
                '../../images/a4.png',
                '../../images/a5.png'
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


