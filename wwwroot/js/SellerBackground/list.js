/*只需要将origionData的内容进行修改，就能对页面进行重新渲染 */
Vue.component('list', {
  data: function () {
    return {
      pageSize: 5,
      currentPage: 1,
      activeName: 'ALL',
      origionData: [{
        id:'123456',
        date: '2016-05-02',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1518 弄',
        phone: '12234',
        condition: '待发货',
        tag: 'TO_BE_SHIP',
        show: true
      }, {
        id:'123457',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待发货',
        tag: 'TO_BE_SHIP',
        show: true
      }, {
        id:'123458',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待收货',
        tag: 'TO_BE_RECEIVE',
        show: true
      }, {
        id:'123459',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待收货',
        tag: 'TO_BE_RECEIVE',
        show: true
      }, {
        id:'123460',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '已完成',
        tag: 'DONE',
        show: true
      }, {
        id:'123461',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待付款',
        tag: 'TO_BE_PAY',
        show: true
      }, {
        id:'123462',
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待处理',
        tag: 'OTHER',
        show: true
      }],
      filteredData: [],
      curpageData:[]
    }
  },
  mounted() {
    this.handleClick('ALL');
  },
  methods: {
    deliver(row){
      //修改状态为待收货TO_BE_RECEIVE，更新数据库，再刷新页面
      //AJAX
      //此处还没有写
    },
    formatter(row) {
      return row.address;
    },
    drawTag(tag) {
      if (tag === 'DONE') { //已完成
        return 'success';
      } else if (tag === 'TO_BE_RECEIVE') { //待收货
        return '';
      } else if (tag === 'TO_BE_SHIP') { //待发货
        return 'danger';
      } else if (tag === 'TO_BE_PAY') {  //待付款
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
      this.handleCurrentChange(1);
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
          this.curpageData[index] = Object.assign({},this.filteredData[i]);
          index++;
        }
      }
    },
  },
  template: `
<el-card>
<el-tabs v-model="activeName" v-on:tab-click="handleClick(activeName)">
    <el-tab-pane label="全部" name="ALL"></el-tab-pane>
    <el-tab-pane label="待发货" name="TO_BE_SHIP"></el-tab-pane>
    <el-tab-pane label="待收货" name="TO_BE_RECEIVE"></el-tab-pane>
    <el-tab-pane label="待付款" name="TO_BE_PAY"></el-tab-pane>
    <el-tab-pane label="已完成" name="DONE"></el-tab-pane>
    <el-tab-pane label="待处理" name="OTHER"></el-tab-pane>
</el-tabs>
<el-table :data="curpageData"  style="width: 100%">
    <el-table-column prop="name" label="姓名" width="180">
    </el-table-column>
    <el-table-column prop="phone" label="电话" width="180">
    </el-table-column>
    <el-table-column prop="address" label="地址" :formatter="formatter">
    </el-table-column>
    <el-table-column prop="tag" label="标签" width="120">
        <template slot-scope="scope">
            <el-tag :type="drawTag(scope.row.tag)" disable-transitions>
                {{scope.row.condition}}</el-tag>
        </template>
    </el-table-column>
    <el-table-column prop="date" label="日期" sortable width="180">
    </el-table-column>
    <el-table-column label="发货" width="60">
      <template slot-scope="scope">
        <el-button type="primary" icon="el-icon-truck" circle 
                  v-if="scope.row.tag==='TO_BE_SHIP'"
                  v-on:click="deliver(scope.row)">
        </el-button>
      </template>
    </el-table-column>
</el-table>
<div class="paginationClass">
    <el-pagination
    v-on:size-change="handleSizeChange"
    v-on:current-change="handleCurrentChange" 
    :current-page="currentPage"
    :page-sizes="[5, 10, 20, 50]"
    :page-size="pageSize" layout="total, sizes, prev, pager, next, jumper"
    :total="filteredData.length">
    </el-pagination>
</div>
</el-card>
    `
})

let list = new Vue({ el: '#list' });
