// src/store/index.js
import { createStore } from 'vuex';
import auth from './modules/auth';
import odontograma from './modules/odontograma';
import historiasClinicas from './modules/historiasClinicas';

const store = createStore({
  modules: {
    auth, 
    odontograma,
    historiasClinicas
  },
});

export default store;