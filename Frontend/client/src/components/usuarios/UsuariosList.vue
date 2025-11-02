<template>
  <v-container>
    <h1>Usuarios</h1>
    <GenericDataTable
      :headers="headers"
      :items="usuarios"
      :loading="loading"
      :pagination="pagination"
      @updatePage="updatePage"
      @updateItemsPerPage="updateItemsPerPage"
      @addItem="agregarUsuario"
      @editItem="editarUsuario"
      @deleteItem="eliminarUsuario"
      @search="searchHandler"
      @updateSorting="sortHandler"
    />
  </v-container>

  <UsuarioFormDialog
    v-model="dialogVisible"
    :usuario="usuarioSeleccionado"
    :enabled="formEnabled"
    :isAdding="isAdding"

    @save="saveDialog"
    @close="closeDialog"
  />
</template>

<script>
/* eslint-disable */
import { ref, onMounted, computed } from 'vue';
import UsuarioService from '../../services/usuarios/UsuarioService';
import UsuarioFilterService from '../../services/usuarios/UsuarioFilterService';
import GenericDataTable from '../../components/commons/data-tables/GenericDataTable.vue';
import UsuarioFormDialog from './UsuarioFormDialog.vue';
import { DataTableAction } from '@/constants/DataTable/DataTableAction';
import { useDialog } from '@/utils/dialog/useDialog';
import { usePagination } from '@/utils/pagination/usePagination';
import { getSortDefinitions } from '@/utils/sort/sortHelper';
import { useSnackbar } from '@/composables/useSnackbar';
import { UsuarioRol } from '@/constants/Users/UsuarioRol';

export default {
  components: { GenericDataTable, UsuarioFormDialog },

  setup() {
    const { showSnackbar } = useSnackbar();
    const sortFields = ref([]);

    const usuarios = ref([]);
    const loading = ref(true);
    const isAdding = ref(false);

    // --- helpers ---
    const unwrap = (x) => (x && x.$values) ? x.$values : x;

    // Mapea el número a texto (para la tabla)
    function getRolText(rolNumber) {
      const entry = Object.entries(UsuarioRol).find(([, val]) => val === Number(rolNumber));
      return entry ? entry[0] : rolNumber;
    }

    // Convierte nombre de enum -> número (por si backend devuelve string)
    function rolToNumber(rol) {
      if (rol == null) return 0;
      if (typeof rol === 'number') return rol;
      // si viene "ADMIN", "SECRETARIA", etc.
      const asNum = UsuarioRol[rol];
      return typeof asNum === 'number' ? asNum : Number(rol) || 0;
    }

    // Normaliza el DTO del backend a la forma que espera el formulario
    function normalizeUsuario(dto) {
      if (!dto) return null;
      return {
        id:        dto.id ?? dto.Id ?? null,
        nombre:    dto.nombre ?? dto.Nombre ?? '',
        apellido:  dto.apellido ?? dto.Apellido ?? '',
        dni:       String(dto.dni ?? dto.Dni ?? '').trim(),
        email:     dto.email ?? dto.Email ?? '',
        rol:       rolToNumber(dto.rol ?? dto.Rol),
        username: dto.username,
        password: dto.password ?? '',
        changePassword: false,
        salt: dto.salt
        // Si el back manda colecciones u objetos anidados con $values, normalizalos acá si hiciera falta:
        // permisos: unwrap(dto.permisos)?.map(...) ?? []
      };
    }

    // === Fetch de usuarios (tabla) ===
    const fetchUsuarios = async (searchQuery = null) => {
      loading.value = true;
      try {
        const filters = UsuarioFilterService.getCommonFilters(searchQuery);
        const response = await UsuarioService.getUsuarios({
          paginationParams: {
            pageNumber: pagination.page,
            pageSize:   pagination.itemsPerPage,
            filters:    filters.length > 0 ? filters : undefined,
            sorts:      sortFields.value.length > 0 ? sortFields.value : undefined
          }
        });

        const rows = unwrap(response.paginationData?.items) || [];
        // Para la tabla mostramos el rol como TEXTO
        usuarios.value = rows.map(u => ({
          ...u,
          rol: getRolText(u.rol ?? u.Rol)
        }));
        pagination.totalItems = Number(response.paginationData?.totalCount) || 0;
      } catch (err) {
        showSnackbar('Error al obtener los usuarios', true);
        console.error('Error:', err);
      } finally {
        loading.value = false;
      }
    };

    // === Headers del DataTable ===
    const headers = computed(() => [
      { title: 'Nombre',   value: 'nombre',  sortable: true },
      { title: 'Apellido', value: 'apellido',sortable: true },
      { title: 'DNI',      value: 'dni',     sortable: true },
      { title: 'Email',    value: 'email',   sortable: true },
      { title: 'Rol',      value: 'rol',     sortable: true },
      { title: 'Acciones', value: 'acciones',sortable: false }
    ]);

    // === Pagination ===
    const { pagination, updatePage, updateItemsPerPage } = usePagination(fetchUsuarios);

    // === Crear objeto nuevo usuario ===
    const getNewUsuario = () => ({
      id: null,
      nombre: '',
      apellido: '',
      dni: '',
      email: '',
      rol: 0
    });

    const usuarioSeleccionado = ref(getNewUsuario());

    // === Dialog & Form Handling ===
    const { dialogVisible, formEnabled, formAction, openDialog, closeDialog } = useDialog();

    const agregarUsuario = () => {
      usuarioSeleccionado.value = getNewUsuario();
      isAdding.value = true;
      openDialog(DataTableAction.ADD);
    };

    const editarUsuario = async (row) => {
      try {
        loading.value = true;
        // Traer SIEMPRE por id para no usar la fila (que tiene 'rol' en texto)
        const resp = await UsuarioService.getUsuarioById({ id: row.id });
        const dto  = resp?.usuario ?? resp;       // soporta ambas formas
        const full = normalizeUsuario(dto);

        console.log(full);
        if (!full?.id) {
          showSnackbar('No se pudo cargar el usuario', true);
          return;
        }

        usuarioSeleccionado.value = full;         // ← ya normalizado (rol numérico)
        isAdding.value = false;
        openDialog(DataTableAction.EDIT);
      } catch (e) {
        console.error(e);
        showSnackbar('Error al cargar el usuario', true);
      } finally {
        loading.value = false;
      }
    };

    const eliminarUsuario = (row) => {
      // Para borrar alcanza con el id; podés mostrar confirmación si querés
      usuarioSeleccionado.value = { id: row.id, nombre: row.nombre, apellido: row.apellido };
      isAdding.value = false;
      openDialog(DataTableAction.DELETE);
    };

    const saveDialog = async (usuario) => {
        console.log('[Padre] saveDialog payload ->', usuario) // DEBUG
      try {
        switch (formAction.value) {
          case DataTableAction.ADD:
            await UsuarioService.createUsuario({ usuario });
            showSnackbar('Usuario agregado con éxito');
            break;
          case DataTableAction.EDIT:
            await UsuarioService.updateUsuario({ usuario });
            showSnackbar('Usuario actualizado con éxito');
            break;
          case DataTableAction.DELETE:
            await UsuarioService.deleteUsuario({ id: usuario.id });
            showSnackbar('Usuario eliminado con éxito');
            break;
          default:
            showSnackbar('Acción no definida', true);
        }
        closeDialog();
        fetchUsuarios();
      } catch (err) {
        const errorMessages =
          err?.response?.data?.Message ||
          Object.values(err?.response?.data?.Errors || {})
            .flatMap(error => unwrap(error))
            .join(', ') ||
          'Error en la operación';

        showSnackbar(errorMessages, true);
        console.error('Error:', err);
      }
    };

    const searchHandler = async (searchQueryObj) => {
      pagination.page = 1;
      const queryStr = searchQueryObj?.query;
      await fetchUsuarios(queryStr);
    };

    const sortHandler = (sortByFields) => {
      sortFields.value = getSortDefinitions(sortByFields);
      fetchUsuarios();
    };

    onMounted(() => { fetchUsuarios(); });

    return {
      usuarios,
      headers,
      loading,
      pagination,
      updatePage,
      updateItemsPerPage,
      agregarUsuario,
      editarUsuario,
      eliminarUsuario,
      usuarioSeleccionado,
      dialogVisible,
      saveDialog,
      closeDialog,
      formEnabled,
      searchHandler,
      sortHandler,
      isAdding
    };
  }
};

</script>