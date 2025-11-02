export function validateDni(dni) {
  if (!/^\d{8}$/.test(dni)) {
    return 'El DNI debe contener exactamente 8 números';
  }
  return true;
}
