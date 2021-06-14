Vue.component('dropdown-menu', {
    template: `
        <div>
            <button class="dropbtn"></button>
            <div class="dropdown-content">
                <a href="#">个人信息</a>
                <a href="#">设置</a>
                <a href="#">退出登录</a>
            </div>
        </div>
    `
})

//var dropdown = new Vue({ el: '#dropdown' });