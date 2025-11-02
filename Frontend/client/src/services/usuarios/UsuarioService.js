import ApiService from '../ApiService';

const UsuarioService = {
  getUsuarios(paginationParams){
    return ApiService.post('/Usuarios/GetUsuarioPaged', paginationParams);
  },

  getUsuarioById(query) {
    return ApiService.post('/Usuarios/GetUsuarioById', query);
  },

  createUsuario(UsuarioData) {
        console.log(UsuarioData)
    return ApiService.post('/Usuarios/CreateUsuario', UsuarioData);
  },

  updateUsuario(UsuarioData) {
    console.log(UsuarioData)
    return ApiService.post('/Usuarios/UpdateUsuario', UsuarioData);
  },

  deleteUsuario(UsuarioData) {
    return ApiService.post('/Usuarios/DeleteUsuario',UsuarioData);
  }

  
};

export default UsuarioService;
