/*只需要将objlist的内容进行修改，就能对页面进行重新渲染 */
Vue.component('list',{
    props: ["objlist","draw"],
    data(){
        return {
            pagesize: 5,
            currentpage: 1,
            activename: 'ALL',
            filtereddata: [],
            curpagedata: []
        }
    },
    template:
`
<el-card>
<el-tabs v-model="this.activename"
         v-on:tab-click="handleClick(this.activename)">
    <el-tab-pane label="全部" name="ALL"></el-tab-pane>
    <el-tab-pane label="待发货" name="TO_BE_SHIP"></el-tab-pane>
    <el-tab-pane label="待收货" name="TO_BE_RECEIVE"></el-tab-pane>
    <el-tab-pane label="待付款" name="TO_BE_PAY"></el-tab-pane>
    <el-tab-pane label="已完成" name="DONE"></el-tab-pane>
    <el-tab-pane label="待处理" name="OTHER"></el-tab-pane>
</el-tabs>
<el-table :data="this.curpagedata"  style="width: 100%">
    <el-table-column prop="buyerName" label="姓名" width="180">
    </el-table-column>
    <el-table-column prop="buyerPhone" label="电话" width="180">
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
    :current-page="this.currentpage"
    :page-sizes="[5, 10, 20, 50]"
    :page-size="this.pagesize" layout="total, sizes, prev, pager, next, jumper"
    :total="this.filtereddata.length">
    </el-pagination>
</div>
</el-card>
    `,
    watch:{
        draw: function (curVal, oldVal) {
            if (curVal === true) {
                this.handleClick('ALL');
            }
        }
    },
    methods: {
      formatter(row) {
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
          this.activename = activeName;
          this.filtereddata.splice(0, this.filtereddata.length);
          let index = 0;
          this.objlist.map((row) => {
              this.handleFilter(this.activename, row);
              if (row.show === true) {
                  this.filtereddata[index] = Object.assign({}, row);
                  index++;
              }
          });
          this.handleCurrentChange(1);
      },
      handleSizeChange(pageSize) { // 每页条数切换
          this.pagesize = pageSize;
          this.handleCurrentChange(this.currentpage);
      },
      handleCurrentChange(page) {//页码切换
          this.currentpage = page;
          this.currentChangePage(page);
      },
      //分页方法
      currentChangePage(currentPage) {
          let startIndex = (currentPage - 1) * this.pagesize;
          let endIndex = startIndex + this.pagesize - 1;
          this.curpagedata.splice(0, this.curpagedata.length);
          let index = 0;
          for (let i = startIndex; i <= endIndex; i++) {
              if (i < this.filtereddata.length) {
                  this.curpagedata[index] = Object.assign({}, this.filtereddata[i]);
                  index++;
              }
          }
      }
  }
})