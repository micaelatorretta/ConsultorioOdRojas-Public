// src/store/modules/odontograma.js
/* eslint-disable */
import OdontogramaService from '../../services/odontogramas/OdontogramaService';
import OdontogramaFilterService from '../../services/odontogramas/OdontogramaFilterService';

const getDefaultState = () => ({
  odontograma: null,
  detalleHistoriaClinica: '',
  loading: false,
  lista: []
});

export default {
  namespaced: true,
  state: () => ({
    odontograma: null,
    detalleHistoriaClinica: '',
    loading: false,
    lista: [] // si querés listar historial del paciente
  }),
  getters: {
    odontograma: s => s.odontograma,
    detalleHistoriaClinica: s => s.detalleHistoriaClinica,
    loading: s => s.loading,
    lista: s => s.lista
  },
  mutations: {
    SET_LOADING(state, v) { state.loading = v; },
    SET_ODONTOGRAMA(state, v) { state.odontograma = v; },
    SET_DETALLE_HISTORIA_CLINICA(state, v) { state.detalleHistoriaClinica = v; },
    SET_LISTA(state, v) { state.lista = v || []; },
    RESET_STATE(state) { Object.assign(state, getDefaultState()); } 
  },
  actions: {
    reset({ commit }) { commit('RESET_STATE'); },
    async crearNuevo({ commit, dispatch }, { paciente, clonarUltimo = true}) {
      commit('SET_LOADING', true);
      try {

        const { data } = await OdontogramaService.createOdontograma({ odontograma: { paciente } });
        commit('SET_ODONTOGRAMA', data);
      } 
      finally { commit('SET_LOADING', false); }
    },

    async cargarPorPaciente({ commit }, { pacienteId }) {
    commit('SET_LOADING', true);
    try {
        const filters = OdontogramaFilterService.getCommonFilters('', pacienteId);
        const data  = await OdontogramaService.getOdontogramas({
        paginationParams: {
            pageNumber: 1,
            pageSize: 1000,
            filters: filters.length ? filters : undefined
        }
        });

        const lista = data.paginationData?.items?.$values || [];
        
        //console.log(data.detalleHistoriaClinica);

        commit('SET_DETALLE_HISTORIA_CLINICA', data.detalleHistoriaClinica);
      
        // ahora traé el DETALLE con piezas/caras
        const det = await OdontogramaService.getOdontogramaById({ id: lista[0]?.id });

        commit('SET_ODONTOGRAMA', det?.odontograma);
    } finally {
        commit('SET_LOADING', false);
    }
    },
    async aplicarCara({ dispatch, state }, { numeroPiezaDental, caraDental, nomencladorId, colorHexadecimal, observacion }) {
      await OdontogramaService.applyCara({
        odontogramaId: state.odontograma.id,
        numeroPiezaDental, 
        caraDental,
        nomencladorId, 
        colorHexadecimal, 
        observacion
      });
      await dispatch('refrescar');
    },
    async eliminarPrestacion({ dispatch }, payload) {
      await OdontogramaService.eliminarPrestacion(payload);
      await dispatch('refrescar');
    },
    async setPresencia({ dispatch }, { piezaId, presente, motivoAusencia }) {
      await OdontogramaService.setPresencia({ piezaId, presente, motivoAusencia });
      await dispatch('refrescar');
    },
    async refrescar({ state, dispatch }) {
      if (state?.odontograma?.pacienteId) {
        await dispatch('cargarPorPaciente', { pacienteId: state.odontograma.pacienteId });
      }
    }
  }
};
