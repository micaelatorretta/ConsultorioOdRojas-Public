import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify'
import { loadFonts } from './plugins/webfontloader'
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css';
import BootstrapVue3 from 'bootstrap-vue-3';
import store from './store'

// Inicializa el usuario en el local storage
store.dispatch('auth/initializeUser');

loadFonts()

createApp(App)
  .use(router)
  .use(vuetify)
  .use(BootstrapVue3)
  .use(store)
  .mount('#app')
