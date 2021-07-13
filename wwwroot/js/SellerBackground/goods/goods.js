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
            pageSize: 10,
            currentPage: 1,
            activeName: 'ALL',
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
            }, {
                discription: '网球',
                price: '123',
                sale: '456',
                num: '0',
                category: 'SPORTS',
                time: '2016-05-02',
                tag: 'SOLD_OUT',
                condition: '已售完',
                show: true
            }, {
                discription: '充电宝',
                price: '123',
                sale: '456',
                num: '789',
                category: 'ELECTRONICS',
                time: '2016-05-02',
                tag: 'OFF_THE_SHELF',
                condition: '已下架',
                show: true
            }],
            filteredData: [],
            curpageData: [],
            upFile: '', //上传的图片
            ruleForm: {
                name: '',
                price: '',
                num: '',
                category: '',
                condition: false,
                detail: '',
                picture:'',
            },
            rules: {
                name: [
                    { required: true, message: '请输入商品名称', trigger: 'blur' },
                    { min: 1, max: 30, message: '长度在 1 到 30 个字符', trigger: 'blur' }
                ],
                price: [
                    { required: true, message: '请输入商品价格', trigger: 'blur' },
                    { validator: checkPrice, trigger: 'blur'  },
                ],
                num: [
                    { required: true, message: '请输入商品库存' },
                    { validator: checkNum, trigger: 'blur'  },
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
    mounted() {
        this.handleClick('ALL');
    },
    methods: {
        isFloat(n){
            return /^-?\d*\.\d+$/.test(n);
        },
        submitForm(formName) {
            this.$refs[formName].validate((valid) => {
                if (valid) {
                    //通过AJAX的方式向后端传送数据
                    var formData = new FormData();
                    formData.append("name",$("#name").val());                //商品名称
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
                            window.location = "/SellerBackground/Goods";
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
        drawTag(tag) {
            if (tag === 'ON_SALE') { //出售中
                return 'success';
            } else if (tag === 'OFF_THE_SHELF') { //已下架
                return '';
            } else if (tag === 'SOLD_OUT') {  //已售完
                return 'warning';
            } else {//OTHER
                return 'info';
            }
        },
        handleFilter(value, row) {
            if (value === 'ALL') {
                row.show = true;
            } else {
                if (row.tag === value) {
                    row.show = true;
                } else {
                    row.show = false;
                }
            }
        },
        handleClick(activeName) {
            console.log("!");
            this.activeName = activeName;
            this.filteredData.splice(0, this.filteredData.length);
            let index = 0;
            this.origionData.map((row) => {
                this.handleFilter(this.activeName, row);
                if (row.show === true) {
                    this.filteredData[index] = Object.assign({}, row);
                    index++;
                }
            });
            this.currentChangePage(1);
        },
        handleSizeChange(pageSize) { // 每页条数切换
            this.pageSize = pageSize;
            this.handleCurrentChange(this.currentPage);
        },
        handleCurrentChange(currentPage) {//页码切换
            this.currentPage = currentPage;
            this.currentChangePage(currentPage);
        },
        //分页方法
        currentChangePage(currentPage) {
            let startIndex = (currentPage - 1) * this.pageSize;
            let endIndex = startIndex + this.pageSize - 1;
            this.curpageData.splice(0, this.curpageData.length);
            let index = 0;
            for (let i = startIndex; i <= endIndex; i++) {
                if (i < this.filteredData.length) {
                    this.curpageData[index] = Object.assign({}, this.filteredData[i]);
                    index++;
                }
            }
        },
        //上传文件
        handleChange(file, fileList) {
            console.log(file, fileList)
            this.upFile = file.raw
            console.log(this.upFile)
        },
    },
    template: `
    <div>
    <el-card>
        <el-form method="post" enctype="multipart/form-data" id="uploadForm" :model="ruleForm" :rules="rules" ref="ruleForm" label-width="100px" class="demo-ruleForm">
            <el-row>
                <el-col :span="6">
                    <el-form-item label="商品名称" prop="name">
                        <el-input id="name" v-model="ruleForm.name"></el-input>
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
                        <el-upload class="upload-demo" drag action=""
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
    </br>
    <el-card>
    <el-tabs v-model="activeName" v-on:tab-click="handleClick(activeName)">
        <el-tab-pane label="全部" name="ALL"></el-tab-pane>
        <el-tab-pane label="出售中" name="ON_SALE"></el-tab-pane>
        <el-tab-pane label="已售完" name="SOLD_OUT"></el-tab-pane>
        <el-tab-pane label="已下架" name="OFF_THE_SHELF"></el-tab-pane>
    </el-tabs>
    <el-table :data="curpageData"  style="width: 100%">
        <el-table-column prop="discription" label="商品描述">
        </el-table-column>
        <el-table-column prop="price" label="价格" width="100" sortable>
        </el-table-column>
        <el-table-column prop="sale" label="销量" width="100" sortable>
        </el-table-column>
        <el-table-column prop="num" label="库存" width="100" sortable>
        </el-table-column>
        <el-table-column prop="category" label="分类" width="150">
        </el-table-column>
        <el-table-column prop="time" label="创建时间" sortable width="150">
        </el-table-column>
        <el-table-column prop="tag" label="状态" width="120">
            <template slot-scope="scope">
                <el-tag :type="drawTag(scope.row.tag)" disable-transitions>
                    {{scope.row.condition}}</el-tag>
            </template>
        </el-table-column>
        <el-table-column width="150">
            <el-button type="primary" icon="el-icon-edit" circle></el-button>
            <el-button type="danger" icon="el-icon-delete" circle></el-button>
        </el-table-column>
    </el-table>
    <div class="paginationClass">
        <el-pagination
        v-on:size-change="handleSizeChange"
        v-on:current-change="handleCurrentChange" 
        :current-page="currentPage"
        :page-sizes="[10, 20, 50, 100]"
        :page-size="pageSize" layout="total, sizes, prev, pager, next, jumper"
        :total="filteredData.length">
        </el-pagination>
    </div>
    </el-card>
    </div>
    `
})

let list = new Vue({ el: '#upload' });