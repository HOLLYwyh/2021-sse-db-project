Vue.component('search', {
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
            ruleForm: {
                name: '',
                price: '',
                num: '',
                category: '',
                condition: false,
                detail: '',
                picture: '',
            },
            rules: {
                name: [
                    { required: true, message: '请输入商品名称', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                price: [
                    { required: true, message: '请输入商品价格', trigger: 'blur' },
                    { validator: checkPrice, trigger: 'blur' },
                ],
                num: [
                    { required: true, message: '请输入商品库存' },
                    { validator: checkNum, trigger: 'blur' },
                ],
                category: [
                    { required: true, message: '请选择商品类别', trigger: 'change' }
                ],
                detail: [
                    { required: true, message: '请填写商品详情', trigger: 'blur' },
                    { min: 1, max: 200, message: '长度在 1 到 300 个字符', trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        isFloat(n) {
            return /^-?\d*\.\d+$/.test(n);
        },
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
    <el-card>
    <div style="margin-top: 15px;">
        <el-input placeholder="请输入内容" v-model="input3" class="input-with-select">
            <el-select v-model="select" slot="prepend" placeholder="请选择">
                <el-option label="餐厅名" value="1"></el-option>
                <el-option label="订单号" value="2"></el-option>
                <el-option label="用户电话" value="3"></el-option>
            </el-select>
            <el-button slot="append" icon="el-icon-search"></el-button>
        </el-input>
    </div>

    <el-form method="post" enctype="multipart/form-data" id="uploadForm" :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-row>
                <el-col :span="6">
                    <el-form-item label="订单搜索" prop="name">
                        <div style="margin-top: 15px;">
                            <el-input placeholder="请输入内容" v-model="input3" class="input-with-select">
                                <el-select v-model="select" slot="prepend" placeholder="请选择">
                                    <el-option label="餐厅名" value="1"></el-option>
                                    <el-option label="订单号" value="2"></el-option>
                                    <el-option label="用户电话" value="3"></el-option>
                                </el-select>
                                <el-button slot="append" icon="el-icon-search"></el-button>
                            </el-input>
                        </div>
                    </el-form-item>
                </el-col>
                <el-col :span="6">
                    <el-form-item label="商品价格" prop="price">
                        <el-input id="price" v-model="ruleForm.price"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="6">
                    <el-form-item label="商品库存" prop="num">
                        <el-input id="storage" v-model.number="ruleForm.num"></el-input>
                    </el-form-item>
                </el-col>
                <el-col :span="6">
                    <el-form-item label="商品类别" prop="category">
                        <el-select id="category" v-model="ruleForm.category" placeholder="请选择商品类别">
                            <el-option label="服装" value="CLOTHING"></el-option>
                            <el-option label="电子产品" value="ELECTRONICS"></el-option>
                            <el-option label="书籍" value="BOOKS"></el-option>
                            <el-option label="宠物" value="PETS"></el-option>
                            <el-option label="运动" value="SPORTS"></el-option>
                            <el-option label="食品" value="FOOD"></el-option>
                            <el-option label="家居" value="HOME"></el-option>
                            <el-option label="美妆" value="BEAUTY"></el-option>
                            <el-option label="洗护" value="BODYCARE"></el-option>
                        </el-select>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="12">
                    <el-form-item  label="上传图片" prop="picture">
                        <el-upload class="upload-demo" drag action="" limit=1
                             :auto-upload="false" :on-change="handleChange">
                            <i class="el-icon-upload"></i>
                            <div class="el-upload__text">将文件拖到此处，或<em>点击上传</em></div>
                            <div class="el-upload__tip" slot="tip">只能上传jpg/png文件，且不超过500kb</div>
                        </el-upload>
                    </el-form-item>
                </el-col>
                <el-col :span="12">
                    <el-form-item label="商品详情" prop="detail">
                        <el-input id="description" type="textarea" :autosize="{ minRows: 10, maxRows: 11}" v-model="ruleForm.detail"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="12">
                    <el-form-item label="是否上架" prop="condition">
                        <el-switch id="onSale" v-model="ruleForm.condition"></el-switch>
                    </el-form-item>
                </el-col>
                <el-col :span="12">
                    <el-form-item style="float: right">
                        <el-button type="primary" v-on:click="submitForm('ruleForm')">立即创建</el-button>
                        <el-button v-on:click="resetForm('ruleForm')">重置</el-button>
                    </el-form-item>
                </el-col>
            </el-row>
        </el-form>
    </el-card>
    `
})

let search = new Vue({ el: '#search' });