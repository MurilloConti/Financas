import { getStocksFromWallet } from '../utils/firebaseUtils'
import router from './../router/index'
import { store } from './index'

const Stocks = {
  state: () => ({
    isFetchingStocks: false,
    stocks: [{
      Code: null,
      Name: null,
      Qtd: null,
      Cost: null,
      Price: null,
      dtOperation: null,
      idWallet: null,
      Operacao: null,
      idealPerc: null
    }]
  }),
  mutations: {
    SET_FETCHING_STOCKS (state, val) {
      state.isFetchingStocks = val
    },
    SET_STOCKS (state, val) {
      state.stocks = val
    },
    SET_ACAO_IN_CARTEIRA (state, val) {
      state.stocks.forEach(element => {
        if (element.Code === val.Code) {
          element.Operacao = val.Operacao
        }
      })
    }
  },
  actions: {
    setStocks ({ commit }, val) {
      if (val) {
        commit('SET_STOCKS', val)
      }
    },
    fetchStocks ({ commit }, val) {
      commit('SET_FETCHING_STOCKS', true)
      getStocksFromWallet(store.state.currentUser).then((stocks) => {
        commit('SET_CARTEIRA_ACOES', stocks)
        if (stocks.length === 0) {
          if (store.state.wallets.length > 0) {
            router.push('addAtivos')
          } else {
            router.push('CreateWallet')
          }
        }
      })
    },
    setAcaoInCarteira ({ commit }, val) {
      commit('SET_ACAO_IN_CARTEIRA', val)
    }
  }
}
export default Stocks
