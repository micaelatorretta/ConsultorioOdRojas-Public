<template>
  <v-container>
    <div class="d-flex justify-space-between align-center mb-3">
      <h1 class="mb-0">Turnos</h1>

      <!-- Botón para abrir calendario -->
      <v-btn color="primary" prepend-icon="mdi-calendar" @click="calendarOpen = !calendarOpen">
        Calendario
      </v-btn>
    </div>

    <!-- Tabla de turnos -->
    <GenericDataTable
      :headers="headers"
      :items="turnos"
      :loading="loading"
      :pagination="pagination"
      @updatePage="updatePage"
      @updateItemsPerPage="updateItemsPerPage"
      @addItem="agregarTurno"
      @editItem="editarTurno"
      @deleteItem="eliminarTurno"
      @search="searchHandler"
      @updateSorting="sortHandler"
    />

    <!-- Drawer lateral con calendario -->
    <v-navigation-drawer
      v-model="calendarOpen"
      location="right"
      temporary
      :width="900"
      class="pa-4"
    >
      <div class="d-flex justify-space-between align-center mb-2">
        <div class="text-h6">Calendario de Turnos</div>
        <v-btn icon @click="calendarOpen = false"><v-icon>mdi-close</v-icon></v-btn>
      </div>

      <TurnosCalendar
        :independentFetch="true"
        :fetchByVisibleRange="true"
        :onAdd="handleCalendarAdd"
        ref="calendarRef" 
        :onEdit="editarTurnoDesdeCalendario"
      />
    </v-navigation-drawer>

    <!-- Dialogo -->
    <TurnoFormDialog
      v-model="dialogVisible"
      :turno="turnoSeleccionado"
      :enabled="formEnabled"
      :allowDelete="dialogAllowDelete"
      @save="saveDialog"
      @close="closeDialog"
      @delete="deleteDialog"
    />
  </v-container>
</template>

<script>
import { ref, onMounted, computed } from 'vue'
import TurnoService from '../../services/turnos/TurnoService'
import TurnoFilterService from '../../services/turnos/TurnoFilterService'
import GenericDataTable from '../../components/commons/data-tables/GenericDataTable.vue'
import TurnoFormDialog from './TurnoFormDialog.vue'
import TurnosCalendar from './TurnosCalendar.vue'
import { DataTableAction } from '@/constants/DataTable/DataTableAction'
import { useDialog } from '@/utils/dialog/useDialog'
import { usePagination } from '@/utils/pagination/usePagination'
import { getSortDefinitions } from '@/utils/sort/sortHelper'
import { useSnackbar } from '@/composables/useSnackbar'

export default {
  components: { GenericDataTable, TurnoFormDialog, TurnosCalendar },

  setup() {
    const { showSnackbar } = useSnackbar()
    const sortFields = ref([])
    const turnos = ref([])
    const loading = ref(true)
    const calendarOpen = ref(false)
    const dialogAllowDelete = ref(false)
    const calendarRef = ref(null)

    const fetchTurnos = async (searchQuery = null) => {
      loading.value = true
      try {
        const filters = TurnoFilterService.getCommonFilters(searchQuery)
        const response = await TurnoService.getTurnos({
          paginationParams: {
            pageNumber: pagination.page,
            pageSize: pagination.itemsPerPage,
            filters: filters.length > 0 ? filters : undefined,
            sorts: sortFields.value?.length ? sortFields.value : undefined
          }
        })
        const items = response.paginationData?.items?.$values || []
        turnos.value = items
        pagination.totalItems = Number(response.paginationData?.totalCount) || 0
      } catch (err) {
        showSnackbar('Error al obtener los turnos', true)
      } finally {
        loading.value = false
      }
    }

    const { pagination, updatePage, updateItemsPerPage } = usePagination(fetchTurnos)

    function normalizeTurno(dto) {
      if (!dto) return null;
      return {
        id:        dto.id ?? dto.Id,
        descripcion: dto.descripcion,
         fecha: dto.fecha,
      horaInicio: dto.horaInicio,
      horaFin: dto.horaFin,
      paciente: dto.paciente,
      pacienteId: dto.pacienteId
        // si tenés colecciones: unwrap(dto.algo)
      };
    }

    const getNewTurno = () => ({
      descripcion: '',
      fecha: new Date().toISOString().split('T')[0],
      horaInicio: '08:00',
      horaFin: '09:00',
      paciente: null,
      pacienteId: 0
    })

    const turnoSeleccionado = ref(getNewTurno())
    const { dialogVisible, formEnabled, formAction, openDialog, closeDialog } = useDialog()

    const headers = computed(() => [
      { title: 'Paciente', value: 'pacienteName', sortable: true },
      { title: 'Descripción', value: 'descripcion', sortable: true },
      { title: 'Fecha', value: 'fecha', sortable: true, isDate: true },
      { title: 'Hora Inicio', value: 'horaInicio', sortable: true },
      { title: 'Hora Fin', value: 'horaFin', sortable: true },
      { title: 'Acciones', value: 'acciones', sortable: false }
    ])

    const searchHandler = async (searchQueryObj) => {
      pagination.page = 1
      await fetchTurnos(searchQueryObj?.query)
    }

    const sortHandler = (sortByFields) => {
      sortFields.value = getSortDefinitions(sortByFields)
      fetchTurnos()
    }

    // Tabla (no muestra eliminar)
    const agregarTurno = () => {
      dialogAllowDelete.value = false
      turnoSeleccionado.value = getNewTurno()
      openDialog(DataTableAction.ADD)
    }

    // busca el turno del backend y lo carga completamente en turnoSeleccionado
    const cargarTurnoGetById = async (turnoId) => {
        // Traer el completo por id
        const resp = await TurnoService.getTurnoById({ id: turnoId });
        const dto  = resp?.turno ?? resp; // soporta ambas formas

        const full = normalizeTurno(dto);
        if (!full?.id) {
          showSnackbar('No se pudo cargar el turno', true);
          return;
        }

        turnoSeleccionado.value = full;
    }

    const editarTurno = async (row) => {
      dialogAllowDelete.value = false
      //turnoSeleccionado.value = { ...turno }

      cargarTurnoGetById(row.id);
       
      openDialog(DataTableAction.EDIT)
    }

    // Calendario (sí muestra eliminar)
    const handleCalendarAdd = (draft) => {
      dialogAllowDelete.value = true
      turnoSeleccionado.value = { ...draft }
      openDialog(DataTableAction.ADD)
    }

    const editarTurnoDesdeCalendario = (row) => {
      dialogAllowDelete.value = true
      cargarTurnoGetById(row.id);
       
      openDialog(DataTableAction.EDIT)
    }

    const eliminarTurno = (row) => {
      dialogAllowDelete.value = false

      cargarTurnoGetById(row.id);
      
      openDialog(DataTableAction.DELETE)
    }


    const saveDialog = async (turno) => {
      try {
        switch (formAction.value) {
          case DataTableAction.ADD:
            await TurnoService.createTurno({ turno })
            showSnackbar('Turno agregado con éxito')
            break
          case DataTableAction.EDIT:
            await TurnoService.updateTurno({ turno })
            showSnackbar('Turno actualizado con éxito')
            break
          case DataTableAction.DELETE:
            await TurnoService.deleteTurno({ id: turno.id })
            showSnackbar('Turno eliminado con éxito')
            break
        }
        closeDialog()
        fetchTurnos()
        calendarRef.value?.refetch()  
      } catch (err) {
        const msg = err.response?.data?.Message ||
          Object.values(err.response?.data?.Errors || {})
            .flatMap(e => e.$values)
            .join(', ') ||
          'Error en la operación'
        showSnackbar(msg, true)
      }
    }

    const deleteDialog = async (turno) => {
      try {
        await TurnoService.deleteTurno({ id: turno.id })
        showSnackbar('Turno eliminado con éxito')
        closeDialog()
        fetchTurnos()
        calendarRef.value?.refetch()  

      } catch (err) {
        const msg = err.response?.data?.Message ||
          Object.values(err.response?.data?.Errors || {})
            .flatMap(e => e.$values)
            .join(', ') ||
          'Error al eliminar'
        showSnackbar(msg, true)
      }
    }

    onMounted(() => fetchTurnos())

    return {
      turnos, loading, turnoSeleccionado,
      dialogVisible, formEnabled,
      headers, pagination,
      updatePage, updateItemsPerPage,
      agregarTurno, editarTurno, eliminarTurno,
      handleCalendarAdd, editarTurnoDesdeCalendario,
      saveDialog, deleteDialog, closeDialog,
      searchHandler, sortHandler,
      calendarOpen, dialogAllowDelete, 
      calendarRef
    }
  }
}
</script>
