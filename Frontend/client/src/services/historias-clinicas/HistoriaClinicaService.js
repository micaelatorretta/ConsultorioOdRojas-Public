import ApiService from '../ApiService';

const HistoriaClinicaService = {
  getHistoriasClinicas(paginationParams){
    return ApiService.post('/HistoriasClinicas/GetHistoriaClinicaPaged', paginationParams);
  },

  getHistoriaClinicaById(query) {
    return ApiService.post('/HistoriasClinicas/GetHistoriaClinicaById', query);
  },

  saveHistoriaClinica(HistoriaClinicaData) {
    return ApiService.post('/HistoriasClinicas/SaveHistoriaClinica', HistoriaClinicaData);
  },

  updateHistoriaClinica(HistoriaClinicaData) {
    return ApiService.post('/HistoriasClinicas/UpdateHistoriaClinica', HistoriaClinicaData);
  },

  deleteHistoriaClinica(HistoriaClinicaData) {
    return ApiService.post('/HistoriasClinicas/DeleteHistoriaClinica',HistoriaClinicaData);
  },

};

export default HistoriaClinicaService;
