Vue.component('upload', {
    data: function () {
        return {
            data: [{
                name:'',
                category:'',
                detail: ''
            }],
            ruleForm: {
                name: '',
                category: '',
                condition: false,
                detail: '',
            },
            rules: {
                name: [
                    { required: true, message: '请输入店铺名称', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                category: [
                    { required: true, message: '请选择店铺类别', trigger: 'change' }
                ],
                detail: [
                    { required: true, message: '请填写店铺详情', trigger: 'blur' },
                    { min: 1, max: 200, message: '长度在 1 到 300 个字符', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    alert('submit!');
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
    <el-card>
        <div id="form">
        <el-form :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-row>
                <el-col :span="18">
                    <el-form-item label="店铺名称" prop="name">
                        <el-input v-model="ruleForm.name"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="店铺类别" prop="category">
                        <el-select v-model="ruleForm.category" placeholder="请选择店铺类别">
                            <el-option label="官方旗舰店" value="OFFICIAL_FLAGSHIP"></el-option>
                            <el-option label="平台认证店" value="PLATFORM_AUTH"></el-option>
                            <el-option label="个人店铺" value="INDIVIDUAL"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="20">
                    <el-form-item label="店铺详情" prop="detail">
                        <el-input type="textarea" :autosize="{ minRows: 10, maxRows: 11}" v-model="ruleForm.detail"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item>
                        <el-button type="primary" v-on:click="submitForm('ruleForm')">立即创建</el-button>
                        <el-button v-on:click="resetForm('ruleForm')">重置</el-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
        </div>
    </el-card>
    `
})

let list = new Vue({ el: '#upload' });