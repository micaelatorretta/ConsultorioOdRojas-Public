import { UsuarioRol } from '@/constants/Users/UsuarioRol';
// Rutas comunes para secretaria, admin y odontologo
const rutasComunes = [
  { label: 'Turnos', path: '/turnos' },
  {
    label: 'Obras Sociales',
    // Ejemplo de dropdown:
    children: [
      { label: 'Obras Sociales', path: '/obras-sociales' },
      { label: 'Nomencladores', path: '/nomencladores' }
    ]
  }
];

// Rutas comunes para admin y odontologo
const rutasComunesAdminOdontologo = [
      { 
        label: 'Pacientes', 
         children: [
            { label: 'Pacientes', path: '/pacientes' },
            { label: 'Historias Clinicas', path: '/historias-clinicas' },
            { label: 'Odontogramas', path: '/odontogramas' }
          ]
       },
];

export const MenuItemsByRole = {


  [UsuarioRol.SECRETARIA]: [
    ...rutasComunes,
    // Agregar ruta unica de este rol de la siguiente manera: { label: 'Inicio', path: '/' }
  ],
  [UsuarioRol.ODONTOLOGA]: [
    ...rutasComunes,
    ...rutasComunesAdminOdontologo
    // Agregar ruta unica de este rol de la siguiente manera: { label: 'Inicio', path: '/' }

  ],
  [UsuarioRol.ADMIN]: [
        ...rutasComunes,
        ...rutasComunesAdminOdontologo,
        { label: 'Usuarios', path: '/usuarios' },
        { label: 'Respaldos de Información', path: '/respaldos-informacion' }
        // Agregar ruta unica de este rol de la siguiente manera:     { label: 'Admin Panel', path: '/admin-panel' },
  ],
};