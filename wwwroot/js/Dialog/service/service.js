const ws = require('nodejs-websocket');

const PORT = 3000
const TYPE_ENTER = 0
const TYPE_LEAVE = 1
const TYPE_MSG = 2

const server = ws.createServer(connect => {

    console.log('有用户连接上来了');
    //connect.userName = buyerNickname;
    broadcast({
        type: TYPE_ENTER,
        msg: `进入#联系客服#页面`,
        time: new Date().toLocaleTimeString(),
    });

    connect.on('text', data => {
        broadcast({
            type: TYPE_MSG,
            msg: `${data}`,
            time: new Date().toLocaleTimeString(),
        });
    })

    connect.on('close', () => {
        console.log('有用户连接断开了');
        broadcast({
            type: TYPE_LEAVE,
            msg: `断开了与客服的联系`,
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
