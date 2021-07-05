/*只需要将tableData的内容进行修改，就能对页面进行重新渲染 */
Vue.component('list', {
  data: function () {
    return {
      activeName: 'ALL',
      tableData: [{
        date: '2016-05-02',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1518 弄',
        phone: '12234',
        condition: '待发货',
        tag: 'TO_BE_SHIP',
        show: true
      }, {
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待发货',
        tag: 'TO_BE_SHIP',
        show: true
      }, {
        date: '2016-05-04',
        name: '王小虎',
        address: '上海市普陀区金沙江路 1517 弄',
        phone: '12234',
        condition: '待收货',
        tag: 'TO_BE_RECEIVE',
        show: true
      }],
      showData: [{
        date: '',
        name: '',
        address: '',
        phone: '',
        condition: '',
        tag: '',
        show: false
      }]
    }
  },
  mounted() {
    this.handleClick('ALL');
  },
  methods: {
    formatter(row, column) {
      return row.address;
    },
    drawTag(tag) {
      if (tag === 'DONE') { //已完成
        return 'success';
      } else if (tag === 'TO_BE_RECEIVE') { //待收货
        return '';
      } else if (tag === 'TO_BE_SHIP') { //代发货
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
      this.showData.splice(0, this.showData.length);
      let index = 0;
      this.tableData.map((row) => {
        this.handleFilter(this.activeName, row);
        if (row.show === true) {
          this.showData[index] = Object.assign({}, row);
          index++;
        }
      });
    }
  },
  template: `
<div>
<el-tabs v-model="activeName" @tab-click="handleClick(activeName)">
    <el-tab-pane label="全部" name="ALL"></el-tab-pane>
    <el-tab-pane label="待发货" name="TO_BE_SHIP"></el-tab-pane>
    <el-tab-pane label="待收货" name="TO_BE_RECEIVE"></el-tab-pane>
    <el-tab-pane label="待付款" name="TO_BE_PAY"></el-tab-pane>
    <el-tab-pane label="已完成" name="DONE"></el-tab-pane>
    <el-tab-pane label="待处理" name="OTHER"></el-tab-pane>
</el-tabs>
<el-table :data="showData"  style="width: 100%">
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
</el-table>
</div>
    `
})

let list = new Vue({ el: '#list' });