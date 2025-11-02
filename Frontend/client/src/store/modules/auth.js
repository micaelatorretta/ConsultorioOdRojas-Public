// src/store/modules/auth.js
const auth = {
   namespaced: true,
    state: () => ({
      currentUser: null,
    }),
    mutations: {
      setCurrentUser(state, user) {
        state.currentUser = user;
      },
      clearCurrentUser(state) {
        state.currentUser = null;
      },
    },
    actions: {
      login({ commit }, user) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        commit('setCurrentUser', user);
      },
      logout({ commit }) {
        localStorage.removeItem('currentUser');
        // Aquí puedes agregar la lógica de cierre de sesión
        commit('clearCurrentUser');
      },
      // Inicializa el usuario en el local storage
      initializeUser({ commit }) {
        const storedUser = localStorage.getItem('currentUser');
        if (storedUser) {
          commit('setCurrentUser', JSON.parse(storedUser));
        }
      }
    },
    getters: {
      isAuthenticated(state) {
        return !!state.currentUser;
      },
      currentUser(state) {
        return state.currentUser;
      },
    },
  };
  
  export default auth;