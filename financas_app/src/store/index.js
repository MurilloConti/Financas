import Vue from 'vue'
import Vuex from 'vuex'
import { getMonthString, getDateFromSeconds } from '../utils/dateUtils'
import axios from 'axios'
import { API_URL, ALPHAVANTAGE_KEY } from '../constants/config'
const fb = require('../firebaseConfig.js')
Vue.use(Vuex)

export const store = new Vuex.Store({
  state: {
    isFetchingData: false,
    currentUser: null,
    userProfile: {},
    carteiraAcoes: [],
    rawAcoesData: [],
    carteiras: [],
    evolucaoPatrimonio: [],
    lucroVendas: 0,
    GastosCompra: 0,
    totalOperacoes: 0
  },
  mutations: {
    SET_FETCHING_DATA (state, val) {
      state.isFetchingData = val
    },
    SET_RAW_DATA (state, val) {
      state.rawAcoesData = val
    },
    SET_CURRENT_USER (state, val) {
      state.currentUser = val
    },
    SET_USER_PROFILE (state, val) {
      state.userProfile = val
    },
    SET_CARTEIRA_ACOES (state, val) {
      state.carteiraAcoes = val
    },
    SET_CARTEIORAS (state, val) {
      state.carteiras = val
    },
    SET_LUCRO_VENDAS (state, val) {
      state.lucroVendas = val
    },
    SET_GASTOS_COMPRA (state, val) {
      state.GastosCompra = val
    },
    SET_TOTAL_OPERACOES (state, val) {
      state.totalOperacoes = val
    },
    SET_EVOLUCAO_PATRIMONIO (state, val) {
      state.evolucaoPatrimonio = val
    },
    SET_ACAO_IN_CARTEIRA (state, val) {
      store.state.carteiraAcoes.forEach(element => {
        if (element.Code === val.Code) {
          element.Operacao = val.Operacao
        }
      })
    }
  },
  actions: {
    clearData ({ commit }) {
      commit('setCarteiras', 0)
      commit('setLucroVendas', 0)
      commit('setGastosCompra', 0)
    },
    setCurrentUser ({ commit }, val) {
      commit('SET_CURRENT_USER', val)
    },
    setUserProfile ({ commit }, val) {
      commit('SET_USER_PROFILE', val)
    },
    setLucroVendas ({ commit }, val) {
      commit('SET_LUCRO_VENDAS', val)
    },
    setGastosCompra ({ commit }, val) {
      commit('SET_GASTOS_COMPRA', val)
    },
    setTotalOperacoes ({ commit }, val) {
      commit('SET_TOTAL_OPERACOES', val)
    },
    setAcaoInCarteira ({ commit }, val) {
      commit('SET_ACAO_IN_CARTEIRA', val)
    },
    fetchAcoesCarteira ({ commit }, val) {
      if (val) {
        commit('SET_CARTEIRA_ACOES', val)
      } else {
        if (!store.state.carteiraAcoes) {
          fb.usuariosCollection.doc(store.state.currentUser).collection('Acoes')
            .get()
            .then(function (querySnapshot) {
              let arr = []
              querySnapshot.forEach(function (doc) {
                let acao = {
                  Code: doc.data().Cod,
                  Name: doc.data().Name,
                  Qtd: doc.data().Qtd,
                  Cost: doc.data().Cost,
                  Price: doc.data().Price,
                  dtUltAtualiza: getDateFromSeconds(doc.data().lastUpdate.seconds)
                }
                arr.push(acao)
              })
            })
        }
      }
    },
    fetchEvolPatrimonio ({ commit }) {
      commit('SET_FETCHING_DATA', true)
      let curYear = new Date().getFullYear()
      let start = new Date('01-01-' + (curYear))
      let end = new Date('01-01-' + (curYear + 1))
      fb.usuariosCollection.doc(store.state.currentUser).collection('Patrimonio')
        .where('date', '>=', start)
        .where('date', '<', end)
        .get()
        .then(function (querySnapshot) {
          let arr = []
          querySnapshot.forEach(function (doc) {
            let data = {
              mes: getMonthString(getDateFromSeconds(doc.data().date.seconds)),
              total: doc.data().total
            }
            arr.push(data)
          })
          commit('SET_EVOLUCAO_PATRIMONIO', arr)
        })
        .catch(function (error) {
          console.log('Error getting documents: ', error)
        })
    },
    fetchCarteiras ({ commit }, val) {
      commit('SET_FETCHING_DATA', true)
      if (val) {
        commit('SET_CARTEIORAS', val)
      } else {
        if (!store.state.carteiras) {
          fb.usuariosCollection.doc(store.state.currentUser).collection('Carteiras')
            .get()
            .then(function (querySnapshot) {
              let arr = []
              querySnapshot.forEach(function (doc) {
                let data = {
                  patrimony: doc.data().patrimony,
                  tax: doc.data().tax,
                  totalCost: doc.data().totalCost,
                  varTwelveMonths: doc.data().varTwelveMonths
                }
                arr.push(data)
              })
              commit('SET_CARTEIORAS', arr)
            })
            .catch(function (error) {
              console.log('Error getting documents: ', error)
              commit('SET_FETCHING_DATA', false)
            })
        }
      }
      commit('SET_FETCHING_DATA', false)
    },
    fetchRawAcoesData ({ commit }, acao) {
      commit('SET_FETCHING_DATA', true)
      if (acao.dtUltAtualiza.getTime() < new Date().getTime()) {
        if (!store.state.rawAcoesData.find(x => x.code === acao.Code)) {
          let url = API_URL + '?function=TIME_SERIES_DAILY_ADJUSTED&symbol=' + acao.Code + '&apikey=' + ALPHAVANTAGE_KEY
          let mainObj = {
            code: acao.Code,
            data: []
          }
          axios.get(url).then((response) => {
            let TimeSeries = response.data['Time Series (Daily)']
            Object.keys(TimeSeries).forEach(element => {
              let low = TimeSeries[element]['3. low']
              let open = TimeSeries[element]['1. open']
              let close = TimeSeries[element]['4. close']
              let high = TimeSeries[element]['2. high']
              let obj = {
                date: element,
                low: Number(low),
                open: Number(open),
                close: Number(close),
                high: Number(high)
              }
              mainObj.data.push(obj)
            })
            mainObj.data.push(['Data', 'low', 'open', 'close', 'high'])
            mainObj.data = mainObj.data.reverse()
            console.log(mainObj)
          })
            .catch(function (error) {
              console.log('ERROR: ', error)
            })
          let acoes = store.state.rawAcoesData
          acoes.push(mainObj)
          commit('SET_RAW_DATA', acoes)
          commit('SET_FETCHING_DATA', false)
        }
      }
    }
  },
  modules: {
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

    let curYear = new Date().getFullYear()
    let start = new Date('01-01-' + (curYear))
    let end = new Date('01-01-' + (curYear + 1))
    fb.usuariosCollection.doc(store.state.currentUser).collection('Patrimonio')
      .where('date', '>=', start)
      .where('date', '<', end)
      .onSnapshot(function (querySnapshot) {
        let arr = []
        console.log('CHAMOU Patrimonio.onSnapshot')
        querySnapshot.forEach(function (doc) {
          let acao = {
            Code: doc.data().Cod,
            Name: doc.data().Name,
            Qtd: doc.data().Qtd,
            Cost: doc.data().Cost,
            Price: doc.data().Price
          }
          arr.push(acao)
        })
        store.dispatch('fetchEvolPatrimonio', arr)
      })

    fb.usuariosCollection.doc(store.state.currentUser).collection('Acoes')
      .onSnapshot(function (querySnapshot) {
        let arr = []
        console.log('CHAMOU Acoes.onSnapshot')
        querySnapshot.forEach(function (doc) {
          let acao = {
            Code: doc.data().Cod,
            Name: doc.data().Name,
            Qtd: doc.data().Qtd,
            Cost: doc.data().Cost,
            Price: doc.data().Price,
            dtUltAtualiza: getDateFromSeconds(doc.data().lastUpdate.seconds)
          }
          arr.push(acao)
        })
        store.dispatch('fetchAcoesCarteira', arr)
      })

    fb.usuariosCollection.doc(store.state.currentUser).collection('Carteiras')
      .onSnapshot(function (querySnapshot) {
        let arr = []
        console.log('CHAMOU Carteiras.onSnapshot')
        querySnapshot.forEach(function (doc) {
          let data = {
            patrimony: doc.data().patrimony,
            tax: doc.data().tax,
            totalCost: doc.data().totalCost,
            varTwelveMonths: doc.data().varTwelveMonths
          }
          arr.push(data)
        })
        store.dispatch('fetchCarteiras', arr)
      })
  }
})
