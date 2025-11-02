import { createRouter, createWebHistory } from 'vue-router';
import store from '@/store';
import { UsuarioRol } from '@/constants/Users/UsuarioRol';

const routes = [
  { 
    path: '/', 
    name:'home', 
    component: () => import('@/components/turnos/TurnosList.vue'),
    meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
 },
 { 
  path: '/login', 
  name:'login', 
  component: () => import('@/components/auth/Login.vue')
},
{
  path: '/usuarios',
  name: 'usuarios',
  component: () => import('@/components/usuarios/UsuariosList.vue'), 
  meta: { requiresAuth: true, requiredRole: UsuarioRol.ADMIN  } 
},
  { 
    path: '/turnos',
    name:'turnos', 
    component: () => import('@/components/turnos/TurnosList.vue'), 
    meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
  }, 
  { 
    path: '/obras-sociales', 
    name:'obras-sociales', 
    component: () => import('@/components/obras-sociales/ObrasSocialesList.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
  },
  { 
    path: '/pacientes', 
    name:'pacientes', 
    component: () => import('@/components/pacientes/PacientesList.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
  },
  { 
    path: '/nomencladores', 
    name:'nomencladores', 
    component: () => import('@/components/nomencladores/NomencladoresList.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
  },
  { 
    path: '/odontogramas', 
    name:'odontogramas', 
    component: () => import('@/components/odontogramas/OdontogramasList.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.SECRETARIA  } 
  },
  { 
    path: '/historias-clinicas', 
    name:'historias-clinicas', 
    component: () => import('@/components/historias-clinicas/HistoriasClinicas.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.ODONTOLOGA  } 
  },
  { 
    path: '/respaldos-informacion', 
    name:'respaldos-informacion', 
    component: () => import('@/components/respaldos-informacion/RespaldosInformacion.vue'),
     meta: { requiresAuth: true, requiredRole: UsuarioRol.ADMIN  } 
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
  const isAuthenticated = store.getters['auth/isAuthenticated'];
  const currentUser = store.getters['auth/currentUser'];

  // Si la ruta requiere login
  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!isAuthenticated) {
      next({ path: '/login', query: { redirect: to.fullPath } });
      return;
    }

    // Si también requiere un rol específico
    const requiredRole = to.meta.requiredRole;
    // La condicion currentUser?.rol < requiredRole verifica segun el valor del enum por ejemplo,
    // Secretaria < Odontologa < Administrador 
    // En este caso, si el rol del usuario es menor al requerido, lo redirige
    if(requiredRole !== undefined && currentUser?.rol < requiredRole) {
      // Si el usuario tiene un rol menor al requerido, redirigí o mostrá error
      next({ path: '/' }); // o podés llevar a una ruta /unauthorized
      return;
    }
  }

  // Si ya está logueado e intenta ir al login, redirigí al home
  if (to.path === '/login' && isAuthenticated) {
    next({ path: '/' });
    return;
  }

  next();
});

export default router;