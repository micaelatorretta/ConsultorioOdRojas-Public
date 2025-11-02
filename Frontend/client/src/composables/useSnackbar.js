import { ref } from 'vue';

const snackbar = ref({
  show: false,
  message: '',
  color: 'success'
});


export function useSnackbar() {
/**
 * Muestra un mensaje en un popup que desaparece al cabo de unos segundos
 * @param message 
 * @param isError 
 */
  const showSnackbar = (message, isError = false) => {
    snackbar.value.message = message;
    snackbar.value.color = isError ? 'error' : 'success';
    snackbar.value.show = true;
  };

  return {
    snackbar,
    showSnackbar
  };
}
