
var content = new Vue({
    el: '#content',
    data:{
        activeName: 'first',
        tabPosition: 'left',
        form: {
            name: '',
            region: '',
            date1: '',
            time1:'',
            date2: '',
            time2:'',
            delivery: false,
            type: [],
            resource: '',
            desc: ''
        },
        tableData: [{
            id: '123',
            type: '满减',
            condition:200,
            minus:20
        }, {
                id: '123',
                type: '满减',
                condition: 200,
                minus: 20
            }],
    },
    methods: {
        onSubmit() {
            console.log('submit!');
        }
    }
    
})