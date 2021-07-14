
var content = new Vue({
    el: '#content',
    data: {
        tabPosition: 'left',
        activeName: 'first',

        form: {
            name: '',
            type: '',
            date1: '',
            date2: '',
            desc: ''
        },

        input: '',
        input2: '',
        input3: '',
        input4: '',
        tableData: [{
            ActivityId : '123',
            Name : '跳楼甩卖',
            Category : '满减',
            StartTime : '',
            EndTime : '',
            Description : 'sfddsf'
        }],
    },
    methods: {
        onSubmit() {
            console.log('submit!');

            let output=this.form

            $.ajax({
                url: "/AdminAction/ReleaseOneActivity",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify(output),//发送申请的活动的信息到后端
                success: function (data) {
                    console.log(data)
                }
            })

        },
        output() {
            console.log(this.form)
        },
        deleteRow(index, rows) {
            let id = this.tableData[Number(index)].ActivityId
            console.log(id)
            $.ajax({
                url: "/AdminAction/DeleteOneActivity",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify({ "ID": id }),  //发送要删除的活动的id到后端
                success: function (data) {
                    console.log(data)
                }
            })

            rows.splice(index, 1);
        },
        getAll() {
            let that
            $.ajax({
                url: "/AdminAction/DisplayAllActivities",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                //请求获取数据库内所有的活动信息
                success: function (data) {
                    that = data
                    console.log(that)
                }
            })
            this.tableData = that
        },
        getOne() {

            let id=this.input
            let that
            $.ajax({
                url: "/AdminAction/DisplayOneActivity",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify({ "ID": id }),       //发送要查询的活动的id到后端,返回该活动的所有信息
                success: function (data) {
                    console.log(data)
                    that = data
                    console.log(that)
                }
            })
            this.tableData=that
        },
        closeUser() {
            let id = this.input2

            $.ajax({
                url: "/AdminAction/DeleteBuyer",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify({ "ID": id }),//发送要封的人的id
                success: function (data) {
                    console.log(data)
                    alert("操作成功")
                }
            })
        },
        closeShop() {
            let id = this.input3

            $.ajax({
                url: "/AdminAction/DeleteShop",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify({ "ID": id }),//发送要封的店的id
                success: function (data) {
                    console.log(data)
                    alert("操作成功")
                }
            })
        },
        closeGoods() {
            let id = this.input4

            $.ajax({
                url: "/AdminAction/DeleteCommodity",
                type: "post",
                contentType: "application/json",
                async: false,
                dataType: "json",
                data: JSON.stringify({ "ID": id }),//发送要封的商品的id
                success: function (data) {
                    console.log(data)
                    alert("操作成功")
                }
            })
        },
    }
    
})