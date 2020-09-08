import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import CreateUser from '../views/CreateUser.vue'
import Dashboard from '../views/dashboard.vue'
import CreateWallet from '../views/CreateWallet.vue'
import OverviewStocks from '../views/overviewAcoes/overview.vue'
import otimizaCarteira from '../views/otimizaCarteira.vue'
import AddEquity from '../views/AddEquity.vue'
import firebase from 'firebase'
Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: '/createwallet',
    name: 'CreateWallet',
    component: CreateWallet,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  {
    path: '/dashboard',
    name: 'dashboard',
    component: Dashboard,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  {
    path: '/otimizaCarteira',
    name: 'otimizaCarteira',
    component: otimizaCarteira,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  {
    path: '/register',
    name: 'register',
    component: CreateUser,
    meta: {
      requiresAuth: false
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  {
    path: '/addAtivos',
    name: 'addAtivos',
    component: AddEquity,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  {
    path: '/acoes',
    name: 'OverviewStocks',
    component: OverviewStocks,
    meta: {
      requiresAuth: true
    },
    beforeEnter: (to, from, next) => {
      validateUser(to, next)
    }
  },
  { path: '*', redirect: Login }
]

const validateUser = (to, next) => {
  const requiresAuth = to.matched.some(x => x.meta.requiresAuth)
  const currentUser = firebase.auth().currentUser
  if (requiresAuth && !currentUser) {
    next('/')
  } else if (requiresAuth && currentUser) {
    next()
  } else {
    next()
  }
}

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
