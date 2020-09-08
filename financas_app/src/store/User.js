const User = {
  state: () => ({
    isFetchingUser: false,
    currentUser: null,
    userProfile: {
      displayName: null,
      email: null
    }
  }),
  mutations: {
    SET_FETCHING_USER (state, val) {
      state.isFetchingUser = val
    },
    SET_CURRENT_USER (state, val) {
      state.currentUser = val
    },
    SET_USER_PROFILE (state, val) {
      state.userProfile = val
    }
  },
  actions: {
    setCurrentUser ({ commit }, val) {
      commit('SET_CURRENT_USER', val)
    },
    setUserProfile ({ commit }, val) {
      commit('SET_USER_PROFILE', val)
    }
  }
}
export default User
