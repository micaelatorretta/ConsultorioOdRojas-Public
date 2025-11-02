import ApiService from '../ApiService';

const NomencladorService = {
  getNomencladores(paginationParams){
    return ApiService.post('/Prestaciones/GetNomencladorPaged', paginationParams);
  },

  getNomencladorById(query) {
    return ApiService.post('/Prestaciones/GetNomencladorById', query);
  },

  createNomenclador(NomencladorData) {
    return ApiService.post('/Prestaciones/CreateNomenclador', NomencladorData);
  },

  updateNomenclador(NomencladorData) {
    return ApiService.post('/Prestaciones/UpdateNomenclador', NomencladorData);
  },

  deleteNomenclador(NomencladorData) {
    return ApiService.post('/Prestaciones/DeleteNomenclador',NomencladorData);
  }

  
};

export default NomencladorService;
