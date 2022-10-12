import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import {BootstrapVue, IconsPlugin} from 'bootstrap-vue'
import PortalVue from 'portal-vue'
import Vuelidate from "vuelidate";

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue'



import Vuetify from 'vuetify'
//import 'vuetify/dist/vuetify.min.css'

Vue.use(Vuetify)

Vue.config.productionTip = false
Vue.use(PortalVue);
Vue.use(IconsPlugin);
Vue.use(BootstrapVue);

Vue.use(Vuelidate);


new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
