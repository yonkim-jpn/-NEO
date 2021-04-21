import Vue from 'vue'
import VueGtag from 'vue-gtag'
import App from './App.vue'
import axios from 'axios'
// import VueAxios from 'vue-axios'
// import router from './router' //router
// import VueHead from 'vue-head'

Vue.use(VueGtag, {
  config: { id: 'G-KTY0ZK42S8' }
});



//グローバルコンポーネント
import Accordion from './components/accordion.vue'

Vue.config.productionTip = false
Vue.config.devtools = true;
Vue.prototype.$axios = axios;

Vue.component("Accordion", Accordion);

new Vue({
  // router,//router
  render: h => h(App),
}).$mount('#app')

