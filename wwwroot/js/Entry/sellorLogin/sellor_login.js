new Vue({el:'#shortcutsi'});


new Vue({
        el:'#formm',
        data:{
            form: {
              name: '',
              phone: '',
              date: '',
              pswd:'',
              pswd2:'',
              type:'',
              
            }
          
          },
       
        methods: {
          onSubmit() {
            console.log('submit!');
          }
        }
})

