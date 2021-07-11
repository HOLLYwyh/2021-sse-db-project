Vue.component('upload', {
    data: function () {
        let checkPrice = (rule, value, callback) => {
            let val = Number(value);
            if (!value) {
                return callback(new Error('不能为空'));
            }
            if (isNaN(value)) {
                callback(new Error('请输入数字值'));
            } else {
                if ((val <= 0) || (val > 100000000000.00)) {
                    callback(new Error('大小在 0.00 到 100000000000.00'));
                } else {
                    callback();
                }
            }
        };
        let checkNum = (rule, value, callback) => {
            if (!value) {
                return callback(new Error('不能为空'));
            }
            if (!Number.isInteger(value)) {
                callback(new Error('请输入数字值'));
            } else {
                let val = Number(value);
                if ((val <= 0)||(val > 1000000)) {
                    callback(new Error('大小在 0 到 1000000'));
                } else {
                    callback();
                }
            }
        };
        return {
            origionData: [{
                discription: '大榴莲',
                price: '233',
                sale: '123',
                num: '456',
                category: 'FOOD',
                time: '2016-05-02',
                tag: 'ON_SALE',
                condition: '销售中',
                show: true
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
                        <el-button type="primary" @click="submitForm('ruleForm')">立即创建</el-button>
                        <el-button @click="resetForm('ruleForm')">重置</el-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
        </div>
    </el-card>
    `
})

let list = new Vue({ el: '#upload' });