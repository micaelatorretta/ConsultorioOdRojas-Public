<template>
  <v-container>
    <h1>Nomencladores</h1>

     <ObraSocialSelector
      v-model="obraSocial"
      label="Obra social"/>
  <!-- Mensaje cuando NO hay selección -->
    <v-alert
      v-if="!obraSocial"
      type="info"
      variant="tonal"
      class="mt-4"
    >
      Seleccione una obra social para ver los nomencladores.
    </v-alert>

      
    <GenericDataTable v-if="obraSocial"
      :headers="headers"
      :items="nomencladores"
      :loading="loading"
      :pagination="pagination"
      @updatePage="updatePage"
      @updateItemsPerPage="updateItemsPerPage"
      @addItem="agregarNomenclador"
      @editItem="editarNomenclador"
      @deleteItem="eliminarNomenclador"
      @search="searchHandler"
      @updateSorting="sortHandler"
    />
  </v-container>

  <NomencladorFormDialog
    v-model="dialogVisible"
    :nomenclador="nomencladorSeleccionado"
    :enabled="formEnabled"
    @save="saveDialog"
    @close="closeDialog"
  />
</template>

<script>
/* eslint-disable */
import { ref, onMounted, computed, watch } from 'vue';
import NomencladorService from '../../services/nomencladores/NomencladorService';
import NomencladorFilterService from '../../services/nomencladores/NomencladorFilterService';
import GenericDataTable from '../../components/commons/data-tables/GenericDataTable.vue';
import NomencladorFormDialog from './NomencladorFormDialog.vue';
import { DataTableAction } from '@/constants/DataTable/DataTableAction';
import { useDialog } from '@/utils/dialog/useDialog';
import { usePagination } from '@/utils/pagination/usePagination';
import { getSortDefinitions } from '@/utils/sort/sortHelper';
import { useSnackbar } from '@/composables/useSnackbar';
import ObraSocialSelector from '../obras-sociales/ObraSocialSelector.vue';
import { FilterOperator } from '../../constants/Filters/FilterOperator';

export default {
  components: {
    GenericDataTable,
    NomencladorFormDialog,
    ObraSocialSelector
  },

  setup() {
    const { showSnackbar } = useSnackbar();

    const sortFields = ref([]);

    const obraSocial = ref(null)

    const fetchNomencladores = async (searchQuery = null) => {
      loading.value = true;
      try {
         // Si NO hay OS seleccionada, no ir al backend y limpiar la tabla
        if (!obraSocial.value?.id && obraSocial.value !== 0) {
          nomencladores.value = []
          pagination.totalItems = 0
          loading.value = false
          return
        }

        console.log(obraSocial.value.id)

        const filters = NomencladorFilterService.getCommonFilters(searchQuery, obraSocial.value.id);

        const response = await NomencladorService.getNomencladores({
          paginationParams: {
            pageNumber: pagination.page,
            pageSize: pagination.itemsPerPage,
            filters: filters.length > 0 ? filters : undefined,
            sorts: sortFields.value?.length > 0 ? sortFields.value : undefined
          }
        });

        nomencladores.value = response.paginationData?.items?.$values || [];
        pagination.totalItems = Number(response.paginationData?.totalCount) || 0;
      } catch (err) {
        showSnackbar('Error al obtener los nomencladores', true);
        console.error('Error:', err);
      } finally {
        loading.value = false;
      }
    };

    const { pagination, updatePage, updateItemsPerPage } = usePagination(fetchNomencladores);

    const getNewNomenclador = () => {
      return {
        codigoNacional: '',
        descripcion: '',
        importe: 0,
        fecha: new Date().toISOString().split('T')[0],
        obraSocial: obraSocial
      };
    };

    const nomencladorSeleccionado = ref(getNewNomenclador());

    const { dialogVisible, formEnabled, formAction, openDialog, closeDialog } = useDialog();
    const nomencladores = ref([]);
    const loading = ref(true);

    const headers = computed(() => [
      { title: 'Código Nacional', value: 'codigoNacional', sortable: true },
      { title: 'Descripción', value: 'descripcion', sortable: true },
      { title: 'Importe', value: 'importe', sortable: true },
      { title: 'Acciones', value: 'acciones', sortable: false }
    ]);

    const searchHandler = async (searchQueryObj) => {
      pagination.page = 1;
      const queryStr = searchQueryObj?.query;
      await fetchNomencladores(queryStr);
    };

    const sortHandler = (sortByFields) => {
      sortFields.value = getSortDefinitions(sortByFields);
      fetchNomencladores();
    };

    const agregarNomenclador = () => {
      nomencladorSeleccionado.value = getNewNomenclador();
      openDialog(DataTableAction.ADD);
    };

    const editarNomenclador = (nomenclador) => {
      nomencladorSeleccionado.value = { ...nomenclador };
      openDialog(DataTableAction.EDIT);
    };

    const eliminarNomenclador = (nomenclador) => {
      nomencladorSeleccionado.value = { ...nomenclador };
      openDialog(DataTableAction.DELETE);
    };

    const saveDialog = async (nomenclador) => {
      try {
        switch (formAction.value) {
          case DataTableAction.ADD:
            console.log(nomenclador)
            
            await NomencladorService.createNomenclador({ nomenclador });
            showSnackbar('Nomenclador agregado con éxito');
            break;
          case DataTableAction.EDIT:
            await NomencladorService.updateNomenclador({ nomenclador });
            showSnackbar('Nomenclador actualizado con éxito');
            break;
          case DataTableAction.DELETE:
            await NomencladorService.deleteNomenclador({ id: nomenclador.id });
            showSnackbar('Nomenclador eliminado con éxito');
            break;
          default:
            showSnackbar('Acción no definida', true);
        }

        closeDialog();
        fetchNomencladores();
      } catch (err) {
        const errorMessages = err.response?.data?.Message ||
          Object.values(err.response?.data?.Errors || {})
            .flatMap(error => error.$values)
            .join(', ') ||
          'Error en la operación';

        showSnackbar(errorMessages, true);
        console.error('Error:', err);
      }
    };

    watch(obraSocial, () => {
      pagination.page = 1
      fetchNomencladores()
    })

    onMounted(() => {
      fetchNomencladores();
    });

    return {
      nomencladores,
      headers,
      loading,
      pagination,
      updatePage,
      updateItemsPerPage,
      agregarNomenclador,
      editarNomenclador,
      eliminarNomenclador,
      nomencladorSeleccionado,
      dialogVisible,
      saveDialog,
      closeDialog,
      formEnabled,
      searchHandler,
      sortHandler,
      obraSocial
    };
  }
};
</script>
