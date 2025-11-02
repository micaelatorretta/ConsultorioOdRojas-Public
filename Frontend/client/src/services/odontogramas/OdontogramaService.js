import ApiService from '../ApiService';

const OdontogramaService = {
  getOdontogramas(paginationParams){
    return ApiService.post('/Odontogramas/GetOdontogramaPaged', paginationParams);
  },

  getOdontogramaById(query) {
    return ApiService.post('/Odontogramas/GetOdontogramaById', query);
  },

  createOdontograma(OdontogramaData) {
    return ApiService.post('/Odontogramas/CreateOdontograma', OdontogramaData);
  },

  updateOdontograma(OdontogramaData) {
    return ApiService.post('/Odontogramas/UpdateOdontograma', OdontogramaData);
  },

  deleteOdontograma(OdontogramaData) {
    return ApiService.post('/Odontogramas/DeleteOdontograma',OdontogramaData);
  },

  applyCara(applyCaraData){
    return ApiService.post('/Odontogramas/AplicarCaraOdontograma', applyCaraData);
  },

  eliminarPrestacion(eliminarPrestacionData){
    return ApiService.post('/Odontogramas/EliminarPrestacionOdontograma', eliminarPrestacionData);
  }
};

export default OdontogramaService;
