/*Vue.component('list', {
    props:{
        tag:String,
        list:[],
    },
    data: function () {
        return {
            tableData: [{
              date: '2016-05-02',
              name: '王小虎',
              address: '上海市普陀区金沙江路 1518 弄',
              tag: '家'
            }, {
              date: '2016-05-04',
              name: '王小虎',
              address: '上海市普陀区金沙江路 1517 弄',
              tag: '公司'
            }]
        }
    },
    methods: {
        resetDateFilter() {
          this.$refs.filterTable.clearFilter('date');
        },
        clearFilter() {
          this.$refs.filterTable.clearFilter();
        },
        formatter(row, column) {
          return row.address;
        },
        filterTag(value, row) {
          return row.tag === value;
        },
        filterHandler(value, row, column) {
          const property = column['property'];
          return row[property] === value;
        },
        drawTag(tag){
          if(tag==='DONE'){ //已完成
            return 'success';
          }else if(tag==='TO_BE_RECEIVE'){ //待收货
            return '';
          }else if(tag==='TO_BE_SHIP'){ //待发货
            return 'danger';
          }else if(tag==='TO_BE_PAY'){  //待付款
            return 'warning';
          }else{
            return 'info';
          }
        }
    },
    template: `

    `
})*/
var Main = new Vue({
  el:"#app",
  data() {
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
      showData:[{
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
  mounted(){
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
      this.showData.splice(0,this.showData.length);
      let index = 0;
      this.tableData.map((row) => {
        this.handleFilter(this.activeName, row);
        if(row.show === true){
          this.showData[index] = Object.assign({},row);   
          index++;
        }
      });
    }
  }
})
//var Ctor = Vue.extend(Main)
//new Ctor().$mount('#app')