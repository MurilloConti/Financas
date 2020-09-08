import { GetWalletFromFireStore } from '../utils/firebaseUtils'
import store from '../store/main'
const Wallets = {
  state: () => ({
    isFetchingWallet: false,
    wallets: [{
      nickName: null,
      corretagemTax: null,
      custodiaTax: null,
      equity: null,
      id: null,
      name: null
    }]
  }),
  mutations: {
    SET_FETCHING_WALLET (state, val) {
      state.isFetchingWallets = val
    },
    SET_WALLET (state, val) {
      state.wallets = val
    }
  },
  actions: {
    fetchWallets ({ commit }, val) {
      commit('SET_FETCHING_WALLET', true)
      GetWalletFromFireStore(store.state.User.currentUser).then((wallets) => {
        commit('SET_WALLET', wallets)
        commit('SET_FETCHING_WALLET', false)
      })
        .catch((e) => { commit('SET_FETCHING_WALLET', false) })
    },
    setWallets ({ commit }, val) {
      commit('SET_WALLET', val)
    }
  }
}
export default Wallets
