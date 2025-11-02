import ApiService from '../ApiService';

const PacienteService = {
  getPacientes(paginationParams){
    return ApiService.post('/Pacientes/GetPacientePaged', paginationParams);
  },

  getPacienteById(query) {
    return ApiService.post('/Pacientes/GetPacienteById', query);
  },

  createPaciente(PacienteData) {
    return ApiService.post('/Pacientes/CreatePaciente', PacienteData);
  },

  updatePaciente(PacienteData) {
    return ApiService.post('/Pacientes/UpdatePaciente', PacienteData);
  },

  deletePaciente(PacienteData) {
    return ApiService.post('/Pacientes/DeletePaciente',PacienteData);
  }

  
};

export default PacienteService;
