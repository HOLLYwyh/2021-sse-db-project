
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
            desc: '',
            constrict: '',
            minus: '',
            
        },
        rules: {
            name: [
                { required: true, message: '请输入活动名称', trigger: 'blur' },
                { min: 3, max: 10, message: '长度在 3 到 10 个字符', trigger: 'blur' }
            ],
            type: [
                { required: true, message: '请选择活动区域', trigger: 'change' }
            ],
            date1: [
                { type: 'date', required: true, message: '请选择日期', trigger: 'change' }
            ],
            date2: [
                { type: 'date', required: true, message: '请选择时间', trigger: 'change' }
            ],
            desc: [
                { required: true, message: '请输入活动名称', trigger: 'blur'}
            ],
            constrict: [
                { required: true, message: '请输入数字', trigger: 'blur' },
                { type: 'number', message: '必须为数字值' }
            ],
            minus: [
                { required: true, message: '请输入数字', trigger: 'blur' },
                { type: 'number', message: '必须为数字值' }
            ]

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
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    
                    this.onSubmit()
                    alert('发布成功!');
                    this.$refs['form'].resetFields();
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
        
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
            if (this.input) {

            
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
                this.tableData = that

            }
        },
        closeUser() {
            if (this.input2) {
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
            }
        },
        closeShop() {
            if (this.input3) {
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
            }
        },
        closeGoods() {
            if (this.input4) {
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
            }
        },
    }
    
})