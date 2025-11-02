<!-- eslint-disable vue/multi-word-component-names -->

<template>
    <v-container>
      <v-form @submit.prevent="login">
        <v-text-field v-model="username" label="Username" required></v-text-field>
        <v-text-field v-model="password" label="Password" type="password" required></v-text-field>
        <v-btn type="submit" color="primary">Login</v-btn>
      </v-form>
    </v-container>
  </template>
  
  <script>
  import { ref/*, computed*/ } from 'vue';
  import { useStore } from 'vuex';
  import { useRouter } from 'vue-router';
  import	AuthService	from	'../../services/auth/AuthService';
  import { useSnackbar } from '@/composables/useSnackbar';

  export default {
    setup() {
      const store = useStore();
      const router = useRouter();
      const username = ref('');
      const password = ref('');
      const { showSnackbar } = useSnackbar();

      const login = async () => {
        // Aquí puedes agregar la lógica de autenticación
        const user = { username: username.value, password: password.value };
        try {
          const response = await AuthService.login({loginCredentials: user });
          console.log(response);
          // Guarda el usuario en el store
          store.dispatch('auth/login', response.usuario);

          // const currentUser = computed(() => store.getters['auth/currentUser']);
          // const isAuthenticated = computed(() => store.getters['auth/isAuthenticated']);
          // console.log(currentUser.value);
          // console.log(isAuthenticated.value);
          
          // Redirige a la pagina de inicio
          router.push('/');
        } catch (err) {
            //console.error(error);
            const errorMessages = err.response?.data?.Message || 
                        Object.values(err.response?.data?.Errors || {})
                              .flatMap(error => error.$values) // Aplanar los arrays de errores
                              .join(', ') || 
                        'Error en la operación';


          showSnackbar(errorMessages, true);
        }

      };
  
      return {
        username,
        password,
        login,
      };
    },
  };
  </script>