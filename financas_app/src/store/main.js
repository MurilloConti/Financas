import Vue from 'vue'
import Vuex from 'vuex'
import Wallets from '../store/Wallet'
import User from './User'
import Movements from './Movement'
import Stocks from './Stocks'
import Otimization from './Otimization'
import router from './../router/index'
import { ConstructStockObject, ConstructWalletObject } from '../utils/firebaseUtils'
const fb = require('../firebaseConfig.js')
Vue.use(Vuex)

export const store = new Vuex.Store({
  modules: {
    User,
    Wallets,
    Movements,
    Stocks,
    Otimization
  }
})

fb.auth.onAuthStateChanged(user => {
  if (user) {
    const userInfo = {
      displayName: user.displayName,
      email: user.email
    }
    store.dispatch('setCurrentUser', user.uid)
    store.dispatch('setUserProfile', userInfo)

    fb.usuariosCollection.doc(store.state.User.currentUser).collection('Acoes')
      .onSnapshot(function (querySnapshot) {
        let arr = []
        querySnapshot.forEach(function (doc) {
          arr.push(ConstructStockObject(doc.data()))
        })
        store.dispatch('setStocks', arr)
      })

    fb.usuariosCollection.doc(store.state.User.currentUser).collection('Carteiras')
      .onSnapshot(function (querySnapshot) {
        let arr = []
        querySnapshot.forEach(function (doc) {
          arr.push(ConstructWalletObject(doc.data(), doc.id))
        })
        store.dispatch('setWallets', arr)
      })
  } else {
    // store.dispatch('clearData')
    router.push('/')
  }
})

export default store
