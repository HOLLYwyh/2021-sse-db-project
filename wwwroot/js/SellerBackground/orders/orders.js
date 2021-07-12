Vue.component('search', {
    data: function () {
        return {
            pickerOptions: {
                disabledDate(time) {
                    return time.getTime() > Date.now();
                },
                shortcuts: [{
                    text: '今天',
                    onClick(picker) {
                        picker.$emit('pick', new Date());
                    }
                }, {
                    text: '昨天',
                    onClick(picker) {
                        const date = new Date();
                        date.setTime(date.getTime() - 3600 * 1000 * 24);
                        picker.$emit('pick', date);
                    }
                }, {
                    text: '一周前',
                    onClick(picker) {
                        const date = new Date();
                        date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', date);
                    }
                }]
            },
            ruleForm: {
                id: '',
                commodity: '',
                receiver: '',
                startTime: '',
                endTime: ''
            },
            rules: {
                id: [
                    { min: 1, max: 6, message: '长度在 1 到 6 个字符', trigger: 'blur' }
                ],
                commodity: [
                    { min: 1, max: 6, message: '长度在 1 到 6 个字符', trigger: 'blur' }
                ],
            }
        }
    },
    methods: {
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    //通过AJAX的方式向后端传送数据
                    var formData = new FormData();
                    formData.append("name", $("#picName").val());                //商品名称
                    formData.append("price", $("#price").val());                //商品价格
                    formData.append("storage", $("#storage").val());            //商品库存
                    formData.append("category", $("#category").val());          //商品类别
                    formData.append("file", this.upFile);
                    formData.append("description", $("#description").val());    //商品描述
                    formData.append("onSale", $("#onSale").val());              //商品是否上架

                    $.ajax({
                        url: '/SellerBackground/UploadCommodity',
                        type: 'POST',
                        data: formData,
                        async: false,
                        cache: false,
                        contentType: false,
                        processData: false,
                        success: function (returndata) {
                            alert("成功");
                        },
                        error: function (returndata) {
                            alert("失败");
                        }
                    });
                } else {
                    console.log('error submit!!');
                    return false;
                }
            });
        },
        resetForm(formName) {
            this.$refs[formName].resetFields();
        },
    },
    template: `
    <el-card class="search-card">
    <el-form method="post" enctype="multipart/form-data" id="uploadForm" :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-row>
                <el-col :span="24">
                    <el-form-item label="商品信息：" prop="commodity">
                        <el-input placeholder="请输入内容" v-model="ruleForm.commodity" class="input-with-select">
                            <el-select v-model="ruleForm.commodity" slot="prepend" placeholder="请选择">
                                <el-option label="商品名称" value="1"></el-option>
                                <el-option label="商品id" value="2"></el-option>
                            </el-select>
                        </el-input>
                    </el-form-item>
                </el-col>
            </el-cow>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="收货信息：" prop="receiver">
                        <el-input placeholder="请输入内容" v-model="ruleForm.receiver" class="input-with-select">
                            <el-select v-model="ruleForm.receiver" slot="prepend" placeholder="请选择">
                                <el-option label="收货人姓名" value="1"></el-option>
                                <el-option label="收货人手机号" value="2"></el-option>
                                <el-option label="下单人账号" value="3"></el-option>
                            </el-select>
                        </el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="订单id：" prop="id">
                        <el-input id="description" v-model="ruleForm.id"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="10">
                <el-form-item label="时间：" prop="startTime">
                    <el-date-picker
                        v-model="ruleForm.startTime"
                        align="right"
                        type="date"
                        placeholder="选择开始日期"
                        :picker-options="pickerOptions">
                    </el-date-picker>
                </el-form-item>
                </el-col>
                <el-col :span="12">
                <el-form-item label="至" prop="endTime">
                    <el-date-picker
                        v-model="ruleForm.endTime"
                        align="right"
                        type="date"
                        placeholder="选择结束日期"
                        :picker-options="pickerOptions">
                    </el-date-picker>
                </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item>
                        <el-button style="margin-left:100px" class="btn" type="primary" v-on:click="submitForm('ruleForm')">搜索</el-button>
                        <el-button class="btn" v-on:click="resetForm('ruleForm')">重置</el-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
    </el-card>
    `
})

let search = new Vue({ el: '#search' });