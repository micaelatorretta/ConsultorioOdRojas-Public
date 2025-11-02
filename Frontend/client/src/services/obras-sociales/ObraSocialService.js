import ApiService from '../ApiService';

const ObraSocialService = {
  getObrasSociales(paginationParams){
    return ApiService.post('/ObrasSociales/GetObraSocialPaged', paginationParams);
  },

  getObraSocialById(query) {
    return ApiService.post('/ObrasSociales/GetObraSocialById', query);
  },

  createObraSocial(obraSocialData) {
    return ApiService.post('/ObrasSociales/CreateObraSocial', obraSocialData);
  },

  updateObraSocial(obraSocialData) {
    return ApiService.post('/ObrasSociales/UpdateObraSocial', obraSocialData);
  },

  deleteObraSocial(obraSocialData) {
    return ApiService.post('/ObrasSociales/DeleteObraSocial',obraSocialData);
  }

};

export default ObraSocialService;
