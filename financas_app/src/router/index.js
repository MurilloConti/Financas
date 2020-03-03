import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Register from '../views/registro.vue'
import Dashboard from '../views/dashboard.vue'
import Acoes from '../views/acoes.vue'
import firebase from 'firebase'
Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
      const currentUser = firebase.auth().currentUser

      if (requiresAuth && !currentUser) {
        next()
      } else if (requiresAuth && currentUser) {
        next()
      } else {
        next()
      }
    }
  },
  {
    path: '/register',
    name: 'register',
    component: Register,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
      const currentUser = firebase.auth().currentUser

      if (requiresAuth && !currentUser) {
        next()
      } else if (requiresAuth && currentUser) {
        next()
      } else {
        next()
      }
    }
  },
  {
    path: '/acoes',
    name: 'acoes',
    component: Acoes,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
      const currentUser = firebase.auth().currentUser

      if (requiresAuth && !currentUser) {
        next()
      } else if (requiresAuth && currentUser) {
        next()
      } else {
        next()
      }
    }
  },
  { path: '*', redirect: Login }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
