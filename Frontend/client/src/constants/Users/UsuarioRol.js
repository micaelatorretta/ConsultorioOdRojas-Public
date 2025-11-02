/* eslint-disable */
export const UsuarioRol = {
    SECRETARIA: 0,
    ODONTOLOGA: 1,
    ADMIN: 2,
  };

export const UsuarioRolOptions = Object.entries(UsuarioRol)
  .filter(([key, val]) => typeof val === 'number') // Filtra entradas numéricas (por seguridad)
  .map(([key, value]) => ({
    value,
    text: key
  }));
  