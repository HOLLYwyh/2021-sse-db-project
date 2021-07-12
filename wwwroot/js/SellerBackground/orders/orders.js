Vue.component('search', {
    data: function () {
        let checkCommodity = (rule, value, callback) => {
            if (this.ruleForm.commodityTag !== '') {
                if (!value) {
                    return callback(new Error('不能为空'));
                }
                if (this.ruleForm.commodityTag === 'commodityName') {
                    if ((value.length <= 0) || (value.length > 30)) {
                        callback(new Error('字符数在 1 到 30 个'));
                    } else {
                        callback();
                    }
                } else if (this.ruleForm.commodityTag === 'commodityId') {
                    let val = Number(value);
                    if ((val <= 0) || (val > 1000000)) {
                        callback(new Error('位数在 1 到 6 位'));
                    } else {
                        callback();
                    }
                }
            }
        };
        let checkReceiver = (rule, value, callback) => {
            if (this.ruleForm.receiverTag !== '') {
                if (!value) {
                    return callback(new Error('不能为空'));
                }
                if (this.ruleForm.receiverTag === 'commodityName') {
                    if ((value.length <= 0) || (value.length > 30)) {
                        callback(new Error('字符数在 1 到 30 个'));
                    } else {
                        callback();
                    }
                } else if (this.ruleForm.commodityTag === 'commodityId') {
                    let val = Number(value);
                    if ((val <= 0) || (val > 1000000)) {
                        callback(new Error('位数在 1 到 6 位'));
                    } else {
                        callback();
                    }
                }
            }
        };
        return {
            pickerOptions: {
                shortcuts: [{
                    text: '最近一周',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近一个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                        picker.$emit('pick', [start, end]);
                    }
                }, {
                    text: '最近三个月',
                    onClick(picker) {
                        const end = new Date();
                        const start = new Date();
                        start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                        picker.$emit('pick', [start, end]);
                    }
                }]
            },
            time: '',   //[ "2021-08-03T16:00:00.000Z", "2021-08-18T16:00:00.000Z" ]
            ruleForm: {
                id: '',
                commodityTag: '',
                commodity: '',
                receiverTag: '',
                receiver: '',
            },
            rules: {
                id: [
                    { min: 1, max: 6, message: '长度在 1 到 6 个字符', trigger: 'blur' }
                ],
                commodity: [
                    { validator: checkCommodity, trigger: 'blur' },
                ],
                receiver: [
                    { validator: checkReceiver, trigger: 'blur' }
                ]
            }
        }
    },
    methods: {
        submitForm() {
            //前后端交互
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
                            <el-select v-model="ruleForm.commodityTag" slot="prepend" placeholder="请选择">
                                <el-option label="商品名称" value="commodityName"></el-option>
                                <el-option label="商品id" value="commodityId"></el-option>
                            </el-select>
                        </el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="收货信息：" prop="receiver">
                        <el-input placeholder="请输入内容" v-model="ruleForm.receiver" class="input-with-select">
                            <el-select v-model="ruleForm.receiverTag" slot="prepend" placeholder="请选择">
                                <el-option label="收货人姓名" value="receiverName"></el-option>
                                <el-option label="收货人手机号" value="recerverPhone"></el-option>
                                <el-option label="下单人账号" value="buyerId"></el-option>
                            </el-select>
                        </el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="订单id：" prop="id">
                        <el-input v-model="ruleForm.id"></el-input>
                    </el-form-item>
                </el-col>
            </el-row>
            <el-row>
                <el-col :span="24">
                    <el-form-item label="时间：" prop="time">
                    <el-date-picker
                        v-model="time"
                        type="daterange"
                        align="right"
                        unlink-panels
                        range-separator="至"
                        start-placeholder="开始日期"
                        end-placeholder="结束日期"
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