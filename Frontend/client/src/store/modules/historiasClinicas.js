// src/store/modules/historiasClinicas.js
/* eslint-disable */
import HistoriaClinicaService from '../../services/historias-clinicas/HistoriaClinicaService';
import HistoriaClinicaFilterService from '../../services/historias-clinicas/HistoriaClinicaFilterService';

export default {
  namespaced: true,
  state: () => ({
    historiaClinica: null,
    loading: false,
    lista: [] // si querés listar historial del paciente
  }),
  getters: {
    historiaClinica: s => s.historiaClinica,
    loading: s => s.loading,
    lista: s => s.lista
  },
  mutations: {
    SET_LOADING(state, v) { state.loading = v; },
    SET_HISTORIA_CLINICA(state, v) { state.historiaClinica = v; },
    SET_LISTA(state, v) { state.lista = v || []; }
  },
  actions: {
    async save({ commit, dispatch }, historiaClinica) {
      commit('SET_LOADING', true);
      try {
        console.log(historiaClinica);

        const { data } = await HistoriaClinicaService.saveHistoriaClinica(historiaClinica);
        commit('SET_HISTORIA_CLINICA', data);
      } 
      finally { commit('SET_LOADING', false); }
    },

    async cargarPorPaciente({ commit }, { pacienteId }) {
    commit('SET_LOADING', true);
    try {
        const filters = HistoriaClinicaFilterService.getCommonFilters('', pacienteId);
        const data  = await HistoriaClinicaService.getHistoriasClinicas({
        paginationParams: {
            pageNumber: 1,
            pageSize: 1000,
            filters: filters.length ? filters : undefined
        }
        });
        const lista = data.paginationData?.items?.$values || [];

        // ahora traé el DETALLE con piezas/caras
        const det = await HistoriaClinicaService.getHistoriaClinicaById({ id: lista[0]?.id });

        console.log(det.historiaClinica)

        commit('SET_HISTORIA_CLINICA', det.historiaClinica);
    } finally {
        commit('SET_LOADING', false);
    }
    }
  }
};
