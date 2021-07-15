//右边栏
new Vue({
	el: "#naviRight"
})
new Vue({ el: '#shortcut' });

var addressdatas = {
	addressdata: [
		{
			Buyer: null,
			BuyerId: "",
			City: "",
			Country: "",
			DetailAddr: "无收货地址，请先添加",
			Phone: "",
			Province: "",
			ReceivedId: "",
			ReceiverName: "",
			Tag: "",
			addressDefult: false,
			addressIsShow: false,
			isShowDefult: false,
		},
		
	
	]
}
var payment = {
	paymentdata:[
		{
			"name":"货到付款",
		},
		{
			"name":"在线支付",
		},
		{
			"name":"银行汇款",
		},
	]
}
var invoice = {
	invoicedata:[
		{
			"name":"不要发票",
		},
		{
			"name":"需要发票",
		},
	]
}
var Coupon = {
	Coupondata:[
		{
			"price":500,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":100,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":500,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":100,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
		{
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},
	]
}
var deliverymode = {
	deliverymodeData:[
		{
			"type":"自提",
			"name":"选择自提时，请与卖家协商取货地址。",
		},
		{
			"type":"物流",
			"name":"由卖家发货。",
		},
	]
}
var dialogs = [
				{id:1,name:'收货人'},
				{id:2,name:'所在地区'},
				{id:3,name:'详细地址'},
				{id:4,name:'手机号码'},
				{id:5,name:'收货人'},

]
var vm = new Vue({
	el: "#myVue",
	data: {
		 /*数据源*/
		dialogs: dialogs,

		

		shopTableDatas: [
			{
				
				img: "../../images/a1.png",
				intro: "大榴莲6666",
				shop: "美国",
				ID: "中国大陆",
				price: 100,
				Soldnum: 1,
				description: true,
				
			},
			
			
		],
		 moreAddressData:addressdatas.addressdata,//地址数据
		 paymentdatas:payment.paymentdata,//支付类型数据
		 deliverymodedatas:deliverymode.deliverymodeData,//配送类型数据
		invoicedatas: invoice.invoicedata,//发票类型数据


		/*Coupondatas: Coupon.Coupondata,*///优惠券数据


		 userBuyData:[],//用户购买数据
		
		/*默认选择标签*/
		 checkedAll:false, //全选状态
		 limitNum:1,//默认显示几个地址
		 currentIndex:0,//地址默认选择
		 paymentIndex:1,//支付类型默认选择
		 deliverymodeIndex:1,//配送类型默认选择
		 invoiceIndex:1,//发票类型默认选择
		 CouponIndex:-1,//优惠券默认选择
		 stopDelete:"",//定时器id(用于清空定时器)
		 activeName: '支付平台',
		
		/*关键字段初始化*/
		 moreaddressName:"",//收货人姓名
		 moreaddressCity:"",//收货人所在市
		 moreaddressArea:"",//收货人所在区
		 moreaddressMinarea:"",//收货人所在小区
		 couponPrice:0,//优惠券默认金额
		 goodNums:0,    //商品或者服务总数
		 totalMoney:0, //总价格
		 saveandremove:true,//收藏和取消收藏的状态
		 goodsNum:0,//商品的数量
		 serviceNum:0,//服务的数量

		Coupondatas:[ {
			CouponId : '2233',
			StartTime: '',
			EndTime: '',
			Threshold : '250',
			Discount : '20',
        },],

		/*{
			 Coupondatas
			"price":200,
			"time":"2017-08-30",
			"type":"[ 店铺类 ]",
			"types":"[ 店铺类 ]",
		},*/

		 form: {
			 	name:'',
	          	city:'',
				area:'',
				minarea:'',
				addressDefult:'',
				addressIsShow:'',
				phone:'',
				num:'',
				isShowDefult:''
        },
        newAddressShow:false,//新增收货地址显示
		dialogVisible: false,//编辑、新增对话状态
		edmitType:'',
		isTranShow:true,
	},
	mounted:function(){
		this.$nextTick(function(){
			this.initAddress();
		})
	},

	
	methods: {
		getAddress() {
			let that
			$.ajax({
				url: "/Purchase/GetReceiveInformation",
				type: "get",
				contentType: "application/json",
				async: false,
				dataType: "json", //返回数据格式为json
				success: function (data) {
					console.log(data)
					that = data
					console.log(that)
				}

			})
			if (that) {
			this.moreAddressData=that
            }
			
		},
		useQuan(index, price) {
			if (this.totalPrice >= this.Coupondatas[index].Threshold) {
				this.CouponIndex = index;
				this.couponPrice = price;
            }	
        },
		getQuan() {
			let that
			$.ajax({
				url: "/Purchase/GetCoupons",
				type: "get",
				contentType: "application/json",
				async: false,
				dataType: "json", //返回数据格式为json
				success: function (data) {
					console.log(data)
					that = data
					console.log(that)
				}

			})
			if (that) {
				this.Coupondatas = that
			}
        },
		submit() {
			let that = { TotalMoney: this.totalPrice, AddressID: this.filterAddress[currentIndex].ReceivedId }
			$.ajax({
				url: "",
				type: "post",
				contentType: "application/json",
				async: false,
				dataType: "json", //返回数据格式为json
				data: JSON.stringify(that),
				success: function (data) {
					console.log(data)
					
				}
			})
        },
		getGoods() {
			let that
			$.ajax({
				url: "/Purchase/GetCommodDetail",
				type: "get",
				contentType: "application/json",
				async: false,
				dataType: "json", //返回数据格式为json
				success: function (data) {
					console.log(data)
					that=data
				}
			})
			console.log(this.totalPrice)
			this.shopTableDatas=that
			
			
        },
		/*商品数量增加减少函数*/
		goodNum:function(item,way){
			if(way == 1){
				 item.num++;
				 vm.countTotalMoney()
			}else{
				if(item.num < 2){
					item.num =1;
				}else{
					item.num--;
					vm.countTotalMoney()
				}
				
			}
		},
		/*单选函数*/
		checkedRadioBtn:function(tabledatas){
			this.countTotalMoney()
			/*单选计算商品或服务数量*/
			if(tabledatas.type == "商品" && tabledatas.checked == true){
				this.goodsNum += 1;
			}else if(tabledatas.type == "服务" && tabledatas.checked == true){
				this.serviceNum += 1;
			}else if(tabledatas.type == "商品" && tabledatas.checked == false){
				this.goodsNum -= 1;
			}else if(tabledatas.type == "服务" && tabledatas.checked == false){
				this.serviceNum -= 1;
			}else{
				console.log("未知错误！")
			}
		},
		/*全选函数*/
		checkedAllBtn:function(checkedAll){
			var _this= this;
			/*全选计算商品或服务数量*/
			if(checkedAll == true){
				for(x in this.shopTableDatas){
					this.shopTableDatas[x].checked = true;
					if(this.shopTableDatas[x].type == "商品" ){
						_this.goodsNum += 1;
					}else if(this.shopTableDatas[x].type == "服务" ){
						_this.serviceNum += 1;
					}
				}
			}else{
				for(y in this.shopTableDatas){
					this.shopTableDatas[y].checked = false;
					this.goodsNum = 0;
					this.serviceNum = 0;
				}
			}
			vm.countTotalMoney();
		},
		/*删除单个选中函数*/
		deletegoods:function(index){
			console.log(index);
			this.shopTableDatas.splice(index, 1); 
			vm.countTotalMoney();
		},
		/*删除多个选中函数*/
		deleteSelectAll:function(){
			for(var i = this.shopTableDatas.length-1 ; i >= 0 ; i--){
				if(this.shopTableDatas[i].checked  == true){
					this.shopTableDatas.splice(i, 1);
				}
			}
			vm.countTotalMoney();
		},
		/*单个移到收藏*/
		movesSave:function(index){
			this.shopTableDatas.splice(index,1)
		},
		/*多个商品移动函数*/
		saveSelectAll:function(){
			for(var i = 0 ; i <= this.shopTableDatas.length ; i++){
				if(this.shopTableDatas[i].checked  == true){
					console.log(this.shopTableDatas[i])
					this.stopDelete = setTimeout(function(){
						vm.deleteSelectAll();
						clearInterval(this.stopDelete)
					},10);
				}
			}
		},
      /*计算商品总价函数*/
		countTotalMoney:function(){
			var _this = this;
			_this.totalMoney = 0;
			this.shopTableDatas.forEach(function(item,index){
				if(item.checked == true){
					_this.totalMoney += item.num*item.price
				}
			})
		},
		/*保存购买数据*/
		saveData:function(){
			var _this = this;
			_this.userBuyData.length = 0;
			this.shopTableDatas.forEach(function(item,index){
				if(item.checked == true){
					window.location.href='../CivilMilitaryIntegration/ShoppingCartAdress.html'
				}
			})	
		},
		/*删除收货地址函数*/
		deleAddress:function(index){
			this.moreAddressData.splice(index, 1); 
			
		},
		/*优惠券点击函数*/
		CouponIndexClcik:function(index,price){
			this.CouponIndex = index;
			this.couponPrice = price;
			
		},
		//初始化地址
		initAddress:function(){
			for(y in this.moreAddressData){
				if(this.moreAddressData[y].addressDefult == true){
					tem = this.moreAddressData[y];
     				index=y;
				}
			}
			this.moreAddressData.splice(index, 1)
            this.moreAddressData.unshift(tem)
		},
		//设置为默认收货地址
		defultAddress:function(item){
			var tem,index;
			for(x in this.moreAddressData){
				this.moreAddressData[x].isShowDefult = false;
			}
			item.isShowDefult = true;
			for(y in this.moreAddressData){
				if(this.moreAddressData[y].isShowDefult == true){
					tem = this.moreAddressData[y];
     				index=y;
				}
			}
			this.moreAddressData.splice(index, 1)
            this.moreAddressData.unshift(tem)
		},
		/*地址点击函数*/
		currentIndexClick:function(item,index){
			for(x in this.moreAddressData){
				this.moreAddressData[x].addressDefult = false;
			}
			item.addressDefult = true;
			this.currentIndex = index;
			this.moreaddressName = item.name;
			this.moreaddressCity = item.city;
			this.moreaddressArea = item.area;
			this.moreaddressMinarea = item.minarea;
		},
		//新增收货地址函数
		AddressShowClick:function(){
			//this.edmitType = '新增',
			//this.newAddressShow = true;
			window.location = "/Account/Address";
		},
		//新增收货地址和编辑收货地址  保存函数
		saveNewAdress:function(){
			if(this.edmitType == '新增'){
				if(this.form.isShowDefult == true){
					for(x in this.moreAddressData){
						this.moreAddressData[x].isShowDefult = false;
					}
					this.moreAddressData.unshift(this.form)
					this.clearEdmitAddress()
				}else{
					this.moreAddressData.push(this.form)
					this.clearEdmitAddress()
				}
				this.newAddressShow = false;
			}else if(this.edmitType == '修改'){
				if(this.form.isShowDefult == true){
					for(x in this.moreAddressData){
						this.moreAddressData[x].isShowDefult = false;
					}
					this.moreAddressData.splice(this.form.num,1,this.form);
					for(y in this.moreAddressData){
						if(this.moreAddressData[y].isShowDefult == true){
							tem = this.moreAddressData[y];
		     				index=y;
						}
					}
					this.moreAddressData.splice(index, 1)
		            this.moreAddressData.unshift(tem)
					this.clearEdmitAddress()
				}else{
					this.moreAddressData.splice(this.form.num,1,this.form)
					this.clearEdmitAddress()
				}
				this.newAddressShow = false;
				
			}
		},
		//编辑收货地址函数
		emitAddress:function(item,index){
			this.newAddressShow = true;
			this.edmitType = '修改';
			//把此行数据到对话框
			this.form.name = item.name
	        this.form.city = item.city
			this.form.area = item.area
			this.form.minarea = item.minarea
			this.form.phone = item.phone
			this.form.addressDefult = item.addressDefult
			this.form.addressIsShow = item.addressIsShow
			this.form.isShowDefult = item.isShowDefult
			this.form.num = index
			
			
		},
		/*鼠标移动函数显示：默认地址、编辑、删除*/	
		MouseOutList:function(moreaddressData){
			moreaddressData.addressIsShow = true;
		},
		MouseOverList:function(moreaddressData){
			moreaddressData.addressIsShow = false;
		},
		//点击更多地址函数
		moreAddress:function(){
			if(this.limitNum == 1){
				this.limitNum = this.moreAddressData.length;
				this.isTranShow = !this.isTranShow;
			}else{
				this.limitNum = 1;
				this.isTranShow = !this.isTranShow;
				var tem,index;
				for(y in this.moreAddressData){
					if(this.moreAddressData[y].addressDefult == true){
						tem = this.moreAddressData[y];
	     				index=y;
					}
				}
				this.moreAddressData.splice(index, 1)
	            this.moreAddressData.unshift(tem)
			}
		},
		//清空当前收货操作
		clearEdmitAddress:function(){
			this.form = {name:'',city:'',area:'',minarea:'',phone:'',addressDefult:'',addressIsShow:'',num:'',isShowDefult:''}
		},
		/*提示删除单个商品*/
		alertRadio:function(index){
        	this.$confirm('此操作将永久删除该商品, 是否继续?', '提示', {
          	confirmButtonText: '确定删除',
          	cancelButtonText: '取消',
         	type: 'warning'
       		}).then(() => {
	          	this.$message({
	            type: 'success',
	            message: '删除成功!',
	            callback : vm.deletegoods(index)
	         	});
        	}).catch(() => {
          		this.$message({
           		type: 'warning',
            	message: '已取消删除'
          });          
        });
      },
      /*提示多个删除函数*/
      alertMuch:function(){
        	this.$confirm('此操作将永久删除已选择商品或服务, 是否继续?', '提示', {
          	confirmButtonText: '确定删除',
          	cancelButtonText: '取消',
         	type: 'warning'
       		}).then(() => {
	          	this.$message({
	            type: 'success',
	            message: '删除成功!',
	            callback : vm.deleteSelectAll()
	         	});
        	}).catch(() => {
          		this.$message({
           		type: 'warning',
            	message: '已取消删除'
          });          
        });
      },
     
      /*提示单个商品移动到收藏函数*/
      alertmovesSavegoods:function(index){
        	this.$confirm('此操作将已选择商品或服务移到我的收藏, 是否继续?', '提示', {
          	confirmButtonText: '确定',
          	cancelButtonText: '取消',
         	type: 'warning'
       		}).then(() => {
	          	this.$message({
	            type: 'success',
            	message: '收藏成功!',
	            callback : vm.movesSave(index)
	         	});
        	}).catch(() => {
          		this.$message({
          		type: 'success',
	            message: '收藏成功!',
          });          
        });
      },
      /*提示收藏多个商品函数*/
      alertMuchgoods:function(){
        	this.$confirm('此操作将已选择商品或服务移到我的收藏, 是否继续?', '提示', {
          	confirmButtonText: '确定',
          	cancelButtonText: '取消',
         	type: 'warning'
       		}).then(() => {
	          	this.$message({
	            type: 'success',
            	message: '收藏成功!',
	            callback : vm.saveSelectAll()
	         	});
        	}).catch(() => {
          		this.$message({
          		type: 'success',
	            message: '收藏成功!',
          });          
        });
      },
      //提示删除收货地址函数
      deleAddressAlert:function(index) {
        this.$confirm('此操作将永久删除该地址, 是否继续?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          
        }).then(() => {
          this.$message({
            type: 'success',
            message: '删除成功!',
            callback:vm.deleAddress(index),
          });
        }).catch(() => {
          this.$message({
            type: 'info',
            message: '已取消删除'
          });          
        });
      },
      //对话框询问是否关闭
	},
	/*金额过滤器*/
	filters:{
		moneyFiler:function(value){
			
			return "￥"+value.toFixed(2);
		}
	},
	/*用于过滤缓存(用于过滤地址显示几个)*/
	computed:{
		filterAddress:function(){
			return this.moreAddressData.slice(0,this.limitNum)
		},
		totalPrice:function() {

			let p = 0
			for (let i in this.shopTableDatas) {
				p += vm.shopTableDatas[i].price * vm.shopTableDatas[i].Soldnum
			}
			p -= this.couponPrice
			return p
        }
	},
});

function start() {
	vm.getGoods()
	vm.getAddress()
	vm.getQuan() 
}

window.onload = start()