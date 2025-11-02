
<template>
  <v-container>
    <h1>Obras Sociales</h1>
    <GenericDataTable
      :headers="headers"
      :items="obrasSociales"
      :loading="loading"
      :pagination="pagination"
      @updatePage="updatePage"
      @updateItemsPerPage="updateItemsPerPage"
      @addItem="agregarObraSocial"
      @editItem="editarObraSocial"
      @deleteItem="eliminarObraSocial"
      @search="searchHandler"
      @updateSorting="sortHandler"
    />
  </v-container>

  <ObraSocialFormDialog
    v-model="dialogVisible"
    :obraSocial="obraSocialSeleccionada"
    :enabled="formEnabled"
    @save="saveDialog"
    @close="closeDialog"
  />


</template>


<script>
import { ref, onMounted, computed } from 'vue';
import ObraSocialService from '../../services/obras-sociales/ObraSocialService';
import ObraSocialFilterService from '../../services/obras-sociales/ObraSocialFilterService';
import GenericDataTable from '../../components/commons/data-tables/GenericDataTable.vue';
import ObraSocialFormDialog from './ObraSocialFormDialog.vue';
import { DataTableAction } from '@/constants/DataTable/DataTableAction';
import { useDialog } from '@/utils/dialog/useDialog';
import { usePagination } from '@/utils/pagination/usePagination';
import { getSortDefinitions } from '@/utils/sort/sortHelper';
import { useSnackbar } from '@/composables/useSnackbar';

export default {
  components: {
    GenericDataTable,
    ObraSocialFormDialog
  },

  setup() {
    const { showSnackbar } = useSnackbar();
    
    const sortFields = ref([]);

    // Se define el metodo para buscar en la API porque de esto depende la paginacion.
    const fetchObrasSociales = async (searchQuery = null) => {
      loading.value = true;
      try {

        const filters = ObraSocialFilterService.getCommonFilters(searchQuery); // Obtener filtros para la busqueda.

        const response = await ObraSocialService.getObrasSociales({
          paginationParams: {
            pageNumber: pagination.page,
            pageSize: pagination.itemsPerPage,
            filters: filters.length > 0 ? filters : undefined,
            sorts: sortFields.value != null && sortFields.value.length > 0 ? sortFields.value : undefined
          }
        });

        obrasSociales.value = response.paginationData?.items?.$values || [];
        pagination.totalItems = Number(response.paginationData?.totalCount) || 0;
      } catch (err) {
        showSnackbar('Error al obtener los Obras Sociales', true);
        console.error('Error:', err);
      } finally {
        loading.value = false;
      }
    };

    // Se le debe pasar por parametro la funcion para buscar las obras sociales cuando actualiza las paginas.
    const { pagination, updatePage, updateItemsPerPage } = usePagination(fetchObrasSociales);

     /**
     * Devuelve un objeto ObraSocial vacío
     */
     const getNewObraSocial = () => {
      return { id: 0, nombre: '', codigo: '' };
    };

    // Se utiliza para mantener el estado actual de una instancia en el crud de obras sociales
    const obraSocialSeleccionada = ref(getNewObraSocial());

    // Variables del dialogo
    const { dialogVisible, formEnabled, formAction, openDialog, closeDialog } = useDialog();
    
    const obrasSociales = ref([]);
    const loading = ref(true);

    // Definir los header del datatable aca
    const headers = computed(() => [
      { title: 'Código', value: 'codigo', sortable: true },
      { title: 'Nombre', value: 'nombre', sortable: true },
      { title: 'Acciones', value: 'acciones', sortable: false }
    ]);

     // Buscar obras sociales por medio del input de busqueda.
     const searchHandler = async (searchQueryObj) => {
      pagination.page = 1; // 🔹 Resetear a la primera página al filtrar
      const queryStr = searchQueryObj?.query; // Convertir a string y limpiar espacios
      await fetchObrasSociales(queryStr);
    };

    // Handler para el evento de ordenamiento en el datatable
    const sortHandler = async (sortByFields) => {
      sortFields.value = getSortDefinitions(sortByFields);

      await fetchObrasSociales();
    };
   
    const agregarObraSocial = () => {
      obraSocialSeleccionada.value = getNewObraSocial();
      openDialog(DataTableAction.ADD);     
    };

    const editarObraSocial = (obraSocial) => {
      obraSocialSeleccionada.value = { ...obraSocial };
      console.log(obraSocial);
      openDialog(DataTableAction.EDIT);
    };

    const eliminarObraSocial = (obraSocial) => {
      obraSocialSeleccionada.value = { ...obraSocial };
            console.log(obraSocial);

      openDialog(DataTableAction.DELETE);
    };

    const saveDialog = async (obraSocial) => {
      try {
        switch (formAction.value) {
          case DataTableAction.ADD:
            await ObraSocialService.createObraSocial({ obraSocial });
            showSnackbar('Obra Social creada con éxito');
            break;
          case DataTableAction.EDIT:
            await ObraSocialService.updateObraSocial({ obraSocial });
            showSnackbar('Obra Social actualizada con éxito');
            break;
          case DataTableAction.DELETE:
            await ObraSocialService.deleteObraSocial({ id: obraSocial.id  });
            showSnackbar('Obra Social eliminada con éxito');
            break;
          default:
            showSnackbar('Acción no definida', true);
        }

        closeDialog();
        fetchObrasSociales();

      } catch (err) {
       const errorMessages = err.response?.data?.Message || 
                      Object.values(err.response?.data?.Errors || {})
                            .flatMap(error => error.$values) // Aplanar los arrays de errores
                            .join(', ') || 
                      'Error en la operación';


        showSnackbar(errorMessages, true);
        console.error('Error:', err);
      } 

    };

    // Ciclo de vida onMounted
    onMounted(async () => {
      await fetchObrasSociales();
    });

    return {
      obrasSociales,
      headers,
      loading,
      pagination,
      updatePage,
      updateItemsPerPage,
      agregarObraSocial,
      editarObraSocial,
      eliminarObraSocial,
      obraSocialSeleccionada,
      dialogVisible,
      saveDialog,
      closeDialog,
      formEnabled,
      searchHandler,
      sortHandler
    };
  }
};
</script>
