import { ref } from 'vue';
import { DataTableAction } from '@/constants/DataTable/DataTableAction';

export function useDialog() {
  const dialogVisible = ref(false);
  const formEnabled = ref(true);
  const formAction = ref(DataTableAction.UNDEFINED);

  const openDialog = (action) => {
    formAction.value = action;
    dialogVisible.value = true;
    formEnabled.value = action !== DataTableAction.DELETE;
  };

  const closeDialog = () => {
    dialogVisible.value = false;
  };

  return {
    dialogVisible,
    formEnabled,
    formAction,
    openDialog,
    closeDialog
  };
}