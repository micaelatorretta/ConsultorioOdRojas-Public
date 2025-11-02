import ApiService from '../ApiService';

const AuthService = {
  login(loginCredentials) {
    console.log(loginCredentials);
    return ApiService.post('/Auth/Login', loginCredentials);
  },
};

export default AuthService;
