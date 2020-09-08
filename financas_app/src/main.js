import Vue from 'vue'
import App from './App.vue'
import router from './router'
import store from './store/main'
import axios from 'axios'
import { BootstrapVue } from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
const fb = require('./firebaseConfig.js')
Vue.config.productionTip = false
Vue.use(BootstrapVue)

let app
fb.auth.onAuthStateChanged(user => {
  if (!app) {
    app = new Vue({
      el: '#app',
      router,
      store,
      axios,
      render: h => h(App)
    })
  }
})
