import Vue from 'vue'
import Vuex from 'vuex'
const fb = require('../firebaseConfig.js')
Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    currentUser: null,
    userProfile: {},
    carteiraAcoes: {}
  },
  mutations: {
    setCurrentUser (state, val) {
      state.currentUser = val
    },
    setUserProfile (state, val) {
      state.userProfile = val
    },
    setCarteiraAcoes (state, val) {
      state.carteiraAcoes = val
    }
  },
  actions: {
    clearData ({ commit }) {
      commit('setCarteiraAcoes', 0)
    }
  },
  modules: {
  }
})

fb.auth.onAuthStateChanged(user => {
  if (user) {
    store.commit('setCurrentUser', user.uid)
  }
})
