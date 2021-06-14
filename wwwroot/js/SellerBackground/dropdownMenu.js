Vue.component('dropdown-menu', {
    template: `
    <div class="dropdown">
        <button class="dropbtn"></button>
        <div class="dropdown-content">
            <a href="#">个人信息</a>
            <a href="#">设置</a>
            <a href="#">退出登录</a>
        </div>
    </div>
    `
})

let menu = new Vue({ el: '#menu' });