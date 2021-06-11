var loginbox = new Vue({
    el: '.nav',
    data: {
        mouseOnAvator: false,

        mouseOnBox: false,
        appearance: false,
    },
    methods: {
        appear() {
            this.mouseOnAvator = true

            this.mouseOnBox = true
            this.appearance = true
            console.log('1')
        },
        disappear(a) {
            if (a == 'A') {
                this.mouseOnAvator = false
                console.log('2')
            }
            else if (a == 'B') {
                this.mouseOnBox = false
                console.log('3')
            }

            if (this.mouseOnBox == false && this.mouseOnAvator == false)
                this.appearance = false
        }
    }
})

let carousel1 = new Vue({ el: '#carousel1' });

let part1 = new Vue({ el: '#part1' });
let part2 = new Vue({ el: '#part2' });
let part3 = new Vue({ el: '#part3' });
let part4 = new Vue({ el: '#part4' });
let part5 = new Vue({ el: '#part5' });



