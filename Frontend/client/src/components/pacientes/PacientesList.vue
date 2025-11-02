<template>
  <v-container>
    <h1>Pacientes</h1>
    <GenericDataTable
      :headers="headers"
      :items="pacientes"
      :loading="loading"
      :pagination="pagination"
      @updatePage="updatePage"
      @updateItemsPerPage="updateItemsPerPage"
      @addItem="agregarPaciente"
      @editItem="editarPaciente"
      @deleteItem="eliminarPaciente"
      @search="searchHandler"
      @updateSorting="sortHandler"
    />
  </v-container>

  <PacienteFormDialog
    v-model="dialogVisible"
    :paciente="pacienteSeleccionado"
    :enabled="formEnabled"
    @save="saveDialog"
    @close="closeDialog"
  />
</template>

<script>
import { ref, onMounted, computed } from 'vue';
import PacienteService from '../../services/pacientes/PacienteService';
import PacienteFilterService from '../../services/pacientes/PacienteFilterService';
import GenericDataTable from '../../components/commons/data-tables/GenericDataTable.vue';
import PacienteFormDialog from './PacienteFormDialog.vue';
import { DataTableAction } from '@/constants/DataTable/DataTableAction';
import { useDialog } from '@/utils/dialog/useDialog';
import { usePagination } from '@/utils/pagination/usePagination';
import { getSortDefinitions } from '@/utils/sort/sortHelper';
import { useSnackbar } from '@/composables/useSnackbar';

export default {
  components: {
    GenericDataTable,
    PacienteFormDialog
  },

  setup() {
    const { showSnackbar } = useSnackbar();

    const sortFields = ref([]);

    const fetchPacientes = async (searchQuery = null) => {
      loading.value = true;
      try {
        const filters = PacienteFilterService.getCommonFilters(searchQuery);

        const response = await PacienteService.getPacientes({
          paginationParams: {
            pageNumber: pagination.page,
            pageSize: pagination.itemsPerPage,
            filters: filters.length > 0 ? filters : undefined,
            sorts: sortFields.value?.length > 0 ? sortFields.value : undefined
          }
        });

        pacientes.value = response.paginationData?.items?.$values || [];
        pagination.totalItems = Number(response.paginationData?.totalCount) || 0;
      } catch (err) {
        showSnackbar('Error al obtener los pacientes', true);
        console.error('Error:', err);
      } finally {
        loading.value = false;
      }
    };

    const { pagination, updatePage, updateItemsPerPage } = usePagination(fetchPacientes);

    const getNewPaciente = () => {
      return {
        descripcion: '',
        fecha: new Date().toISOString().split('T')[0],
      };
    };

    // helpers arriba de setup()
    //const unwrap = (x) => (x && x.$values) ? x.$values : x;

    function normalizePaciente(dto) {
      if (!dto) return null;
      return {
        id:        dto.id ?? dto.Id,
        nombre:    dto.nombre ?? dto.Nombre ?? '',
        apellido:  dto.apellido ?? dto.Apellido ?? '',
        dni:       dto.dni ?? dto.Dni ?? '',
        obraSocial: dto.obraSocial ?? null,
        obraSocialId: dto.obraSocial?.id ?? null,
        historiaClinica: dto.historiaClinica ?? null,
        // si tenés colecciones: unwrap(dto.algo)
      };
    }

    const pacienteSeleccionado = ref(getNewPaciente());

    const { dialogVisible, formEnabled, formAction, openDialog, closeDialog } = useDialog();
    const pacientes = ref([]);
    const loading = ref(true);

    const headers = computed(() => [
      { title: 'Nombre', value: 'nombre', sortable: true },
      { title: 'Apellido', value: 'apellido', sortable: true },
      { title: 'DNI', value: 'dni', sortable: true },
      { title: 'Acciones', value: 'acciones', sortable: false }
    ]);

    const searchHandler = async (searchQueryObj) => {
      pagination.page = 1;
      const queryStr = searchQueryObj?.query;
      await fetchPacientes(queryStr);
    };

    const sortHandler = (sortByFields) => {
      sortFields.value = getSortDefinitions(sortByFields);
      fetchPacientes();
    };

    const agregarPaciente = () => {
      pacienteSeleccionado.value = getNewPaciente();
      openDialog(DataTableAction.ADD);
    };

    const editarPaciente = async (row) => {
      try {
        loading.value = true; // opcional: bloquea la tabla mientras trae
        // Traer el completo por id
        const resp = await PacienteService.getPacienteById({ id: row.id });
        const dto  = resp?.paciente ?? resp; // soporta ambas formas

        const full = normalizePaciente(dto);
        if (!full?.id) {
          showSnackbar('No se pudo cargar el paciente', true);
          return;
        }

        pacienteSeleccionado.value = full;
        openDialog(DataTableAction.EDIT); // recién ahora abrimos el diálogo
      } catch (err) {
        console.error(err);
        const msg = err?.response?.data?.Message || 'Error al cargar el paciente';
        showSnackbar(msg, true);
      } finally {
        loading.value = false;
      }
    };


    const eliminarPaciente = (paciente) => {
      pacienteSeleccionado.value = { ...paciente };
      openDialog(DataTableAction.DELETE);
    };

    const saveDialog = async (paciente) => {
      try {
        switch (formAction.value) {
          case DataTableAction.ADD:
            await PacienteService.createPaciente({ paciente });
            showSnackbar('Paciente agregado con éxito');
            break;
          case DataTableAction.EDIT:
            await PacienteService.updatePaciente({ paciente });
            showSnackbar('Paciente actualizado con éxito');
            break;
          case DataTableAction.DELETE:
            await PacienteService.deletePaciente({ id: paciente.id });
            showSnackbar('Paciente eliminado con éxito');
            break;
          default:
            showSnackbar('Acción no definida', true);
        }

        closeDialog();
        fetchPacientes();
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

    onMounted(() => {
      fetchPacientes();
    });

    return {
      pacientes,
      headers,
      loading,
      pagination,
      updatePage,
      updateItemsPerPage,
      agregarPaciente,
      editarPaciente,
      eliminarPaciente,
      pacienteSeleccionado,
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
