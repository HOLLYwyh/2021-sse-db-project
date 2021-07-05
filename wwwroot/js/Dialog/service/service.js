////// 左边栏的vue
////new Vue({
////    el: "#left-sidebar",
////    methods: {

////    },
////    data: {
////        contactsData: [{
////            contacts: 'HOLLYwyh'
////        },
////        { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' },
////            { contacts: 'lxhnb' }
////        ]
////    }

////});
const ws = require('nodejs-websocket');
const PORT = 3000
const TYPE_ENTER = 0
const TYPE_LEAVE = 1
const TYPE_MSG = 2

let count = 0;
const server = ws.createServer(connect => {
    console.log('有用户连接上来了');
    count++;
    connect.userName = `用户${count}`;
    broadcast({
        type: TYPE_ENTER,
        msg: `${connect.userName}进入了聊天室`,
        time: new Date().toLocaleTimeString(),
    });

    connect.on('text', data => {
        broadcast({
            type: TYPE_MSG,
            msg: `${connect.userName}: ${data}`,
            time: new Date().toLocaleTimeString(),
        });
    })

    connect.on('close', () => {
        count--;
        console.log('有用户连接断开了');
        broadcast({
            type: TYPE_MSG,
            msg: `${connect.userName}离开了聊天室`,
            time: new Date().toLocaleTimeString(),
        })
    })

    connect.on('error', () => {
        console.log('有用户连接异常');
    })
})

function broadcast(msg) {
    server.connections.forEach(item => {
        item.send(JSON.stringify(msg));
    })
}

server.listen(PORT, () => {
    console.log('web服务启动成功了，监听了端口' + PORT);
})
