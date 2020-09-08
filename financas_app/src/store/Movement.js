import { getOperationsFromStock } from '../utils/firebaseUtils'
import { store } from './index'
const Movements = {
  state: () => ({
    isFetchingMovement: false,
    movements: [
      {
        Code: null,
        Cost: null,
        Type: null,
        Qtd: null,
        dtOperation: null,
        idWallet: null
      }
    ]
  }),
  mutations: {
    SET_FETCHING_MOVEMENT (state, val) {
      state.isFetchingMovement = val
    },
    SET_MOVEMENTS (state, val) {
      state.movements = val
    }
  },
  actions: {
    setMovements ({ commit }, val) {
      if (val) {
        commit('SET_MOVEMENTS', val)
      }
    },
    fetchMovements ({ commit }) {
      commit('SET_FETCHING_MOVEMENT', true)
      getOperationsFromStock(store.state.user.currentUser).then((movements) => {
        commit('SET_MOVEMENTS', movements)
        commit('SET_FETCHING_MOVEMENT', false)
      })
        .catch((e) => { commit('SET_FETCHING_MOVEMENT', false) })
    }
  }
}
export default Movements
