
const Otimization = {
  state: () => ({
    lucroVendas: 0,
    GastosCompra: 0,
    totalOperacoes: 0
  }),
  mutations: {
    SET_LUCRO_VENDAS (state, val) {
      state.lucroVendas = val
    },
    SET_GASTOS_COMPRA (state, val) {
      state.GastosCompra = val
    },
    SET_TOTAL_OPERACOES (state, val) {
      state.totalOperacoes = val
    }
  },
  actions: {
    setLucroVendas ({ commit }, val) {
      commit('SET_LUCRO_VENDAS', val)
    },
    setGastosCompra ({ commit }, val) {
      commit('SET_GASTOS_COMPRA', val)
    },
    setTotalOperacoes ({ commit }, val) {
      commit('SET_TOTAL_OPERACOES', val)
    }
  }
}
export default Otimization
