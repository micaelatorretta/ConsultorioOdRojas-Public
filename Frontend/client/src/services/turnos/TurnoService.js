import ApiService from '../ApiService';

const TurnoService = {
  getTurnos(paginationParams){
    return ApiService.post('/Turnos/GetTurnoPaged', paginationParams);
  },

  getTurnoById(query) {
    return ApiService.post('/Turnos/GetTurnoById', query);
  },

  createTurno(turnoData) {
    return ApiService.post('/Turnos/CreateTurno', turnoData);
  },

  updateTurno(turnoData) {
    return ApiService.post('/Turnos/UpdateTurno', turnoData);
  },

  deleteTurno(turnoData) {
    return ApiService.post('/Turnos/DeleteTurno',turnoData);
  }

  
};

export default TurnoService;
