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

let shortcut1 = new Vue({ el: '#shortcut' });

var rcmd = new Vue({
    el: '.rcmd_box',
    data: {
        mark: 0,
        goods: [
            {
                img: '../../Images/Home/index/a1.png', intro: "泰国进口金枕鲜榴莲1,泰国进口金枕鲜榴莲,泰国进口金枕1", shop: '金轮', link:'https://www.baidu.com/'
            },
            {
                img: '../../Images/Home/index/a2.png', intro: '泰国进口金枕鲜榴莲2,泰国进口金枕鲜榴莲,泰国进口金枕2', shop: '金轮', link:''
            },
            {
                img: '../../Images/Home/index/a3.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕3', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a4.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕4', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a5.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕5', shop: '金轮', link: ''
            },
            {
                img: '../../Images/Home/index/a1.png', intro: '泰国进口金枕鲜榴莲,泰国进口金枕鲜榴莲,泰国进口金枕1', shop: '金轮', link: ''
            },
        ],
    }
})

let part1 = new Vue({ el: '#part1' });
let part2 = new Vue({ el: '#part2' });
let part3 = new Vue({ el: '#part3' });
let part4 = new Vue({ el: '#part4' });
let part5 = new Vue({ el: '#part5' });

let naviright = new Vue({ el: '#naviRight' })


