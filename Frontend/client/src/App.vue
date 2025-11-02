<template>
  <v-app>

    <!-- Header -->
   <v-app-bar color="primary" class="px-6">
    <div class="app-brand d-flex align-center flex-shrink-0 mr-4">
      <v-img
        src="@/assets/tooth_315369.svg"
        alt="Logo"
        height="32"
        width="32"
        class="mr-2"
        contain
      />
      <span class="text-h6 text-no-wrap">Od. Rojas</span>
    </div>

      
      <!-- Opciones del menú a la izquierda -->
      <template v-if="isAuthenticated">
        <template v-for="item in menuItems" :key="item.label">
          <!-- Dropdown -->
          <v-menu v-if="item.children" offset-y>
            <template #activator="{ props }">
              <v-btn text v-bind="props">
                {{ item.label }}
                <v-icon end>mdi-menu-down</v-icon>
              </v-btn>
            </template>
            <v-list>
              <v-list-item
                v-for="subitem in item.children"
                :key="subitem.path"
                :to="subitem.path"
                link
                @click="$router.push(subitem.path)"
              >
                <v-list-item-title>{{ subitem.label }}</v-list-item-title>
              </v-list-item>
            </v-list>
          </v-menu>

          <!-- Botón normal -->
          <v-btn v-else :to="item.path" text>
            {{ item.label }}
          </v-btn>
        </template>

        <!-- Spacer para empujar el logout a la derecha -->
        <v-spacer />

        <!-- Botón cerrar sesión a la derecha -->
        <v-btn @click="cerrarSesion" text>Cerrar Sesión</v-btn>
      </template>

      <template v-else>
        <v-spacer />
        <v-btn to="/login" text>Iniciar Sesión</v-btn>
      </template>
    </v-app-bar>



    <!-- Contenido dinámico -->
    <v-main class="pa-4 mt-5">
      <router-view />

      <!-- Componente para las notificaciones -->
      <AppSnackbar
        :model-value="snackbar.show"
        :message="snackbar.message"
        :color="snackbar.color"
        @update:modelValue="snackbar.show = $event"
      />

    </v-main>

    <!-- Footer -->
    <!-- <v-footer color="grey darken-3" app>
      <v-container>
        <span class="white--text">© 2024 - Todos los derechos reservados</span>
      </v-container>
    </v-footer> -->
  </v-app>
</template>

<script>
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { computed } from 'vue';
import { MenuItemsByRole } from '@/constants/MenuItems/MenuItemsByRole';
import { useSnackbar } from '@/composables/useSnackbar';
import AppSnackbar from '@/components/commons/notifications/AppSnackbar.vue';

export default {
  name: 'App',
  components: {
    AppSnackbar
  },
  data: () => ({
    //
  }),
  setup() {
    const store = useStore();
    const router = useRouter();
    const { snackbar } = useSnackbar(); // componente para notificaciones

    const cerrarSesion = () => {
      store.dispatch('auth/logout');
      router.push('/login');
    };

    // Para mostrar/ocultar según login
    const isAuthenticated = computed(() => store.getters['auth/isAuthenticated']);

    const currentUser = computed(() => store.getters['auth/currentUser']);

    const menuItems = computed(() => {
      const role = currentUser.value?.rol;
      return MenuItemsByRole[role] || [];
    });

    return {
      cerrarSesion,
      isAuthenticated,
      menuItems,
      snackbar
    };
  }
}
</script>
