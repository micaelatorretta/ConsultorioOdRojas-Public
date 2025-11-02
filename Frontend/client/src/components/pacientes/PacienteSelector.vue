<template>
  <v-autocomplete
    v-model="innerValue"
    v-model:search="search"
    :items="items"
    :item-title="(item) => `${item.nombre} ${item.apellido}`"
    item-value="id"
    :label="label"
    return-object
    :loading="loading"
    :no-data-text="noDataText"
    variant="outlined"
    clearable
    hide-details="auto"
    @focus="ensureLoaded"
    @update:search="onSearchChange"
  >
    <!-- opcional: icono de recargar -->
    <template #append-inner>
      <v-btn
        icon
        size="small"
        variant="text"
        :disabled="loading"
        @click.stop="reload"
      >
        <v-icon>mdi-refresh</v-icon>
      </v-btn>
    </template>
  </v-autocomplete>
</template>

<script setup>
import { ref, watch, computed, onMounted,defineProps, defineEmits } from 'vue'
import PacienteService from '@/services/pacientes/PacienteService'
//import PacienteFilterService from '@/services/obras-sociales/PacienteFilterService'
import { useSnackbar } from '@/composables/useSnackbar'

/**
 * PROPS: podés agregar acá todo lo que quieras parametrizar desde el padre.
 */
const props = defineProps({
  /** v-model: id seleccionado */
  modelValue: { type: [Number, String, null], default: null },

  /** UI */
  label: { type: String, default: 'Paciente' },
  noDataText: { type: String, default: 'Sin resultados' },

  /** Búsqueda remota */
  debounceMs: { type: Number, default: 350 },
  pageSize: { type: Number, default: 20 },

  /** Filtros extra que quieras inyectar desde el padre (se agregan a los comunes) */
  extraFilters: { type: Array, default: () => [] },

  /** Hacer primer fetch al montar si ya hay modelValue */
  hydrateOnMount: { type: Boolean, default: true }
})

const emit = defineEmits(['update:modelValue', 'loaded'])

const { showSnackbar } = useSnackbar?.() ?? { showSnackbar: () => {} }

const innerValue = computed({
  get: () => props.modelValue,
  set: (val) => emit('update:modelValue', val)
})

const items = ref([])
const loading = ref(false)
const search = ref('')
//const page = ref(1)
//const total = ref(0)
const hasLoadedOnce = ref(false)

let debounceTimer = null

const sortFields = ref([]);

const fetchItems = async () => {
  loading.value = true;
  try {
   // console.log(searchQuery)
   // const filters = PacienteFilterService.getCommonFilters(searchQuery);

    const response = await PacienteService.getPacientes({
      paginationParams: {
        pageNumber: 1,
        pageSize: 1000,
        filters: undefined,
        sorts: sortFields.value?.length > 0 ? sortFields.value : undefined
      }
    });

    items.value = response.paginationData?.items?.$values || [];

    //pagination.totalItems = Number(response.paginationData?.totalCount) || 0;
  } catch (err) {
    showSnackbar('Error al obtener los pacientes', true);
    console.error('Error:', err);
  } finally {
    loading.value = false;
  }
};
  
function onSearchChange() {
  clearTimeout(debounceTimer)
  debounceTimer = setTimeout(() => {
    fetchItems({ reset: true })
  }, props.debounceMs)
}

function ensureLoaded() {
  if (!hasLoadedOnce.value) {
    fetchItems({ reset: true })
  }
}

function reload() {
  fetchItems({ reset: true })
}

// Hidratar valor inicial (si viene un id) para mostrar el nombre en el campo
onMounted(async () => {
  if (props.modelValue) {
    try {
      loading.value = true
      const data = await PacienteService.getById(props.modelValue)
      // Evita duplicados si luego llamamos a fetchItems
      const exists = items.value.some(x => x.id === data.id)
      if (!exists && data) items.value = [data, ...items.value]
    } catch (err) {
      // opcional mostrar snackbar
    } finally {
      loading.value = false
    }
  }
})

// Si el padre limpia el modelValue, no pasa nada especial;
// si lo cambia a otro id, podrías re-hidratar aquí si lo necesitás:
watch(() => props.modelValue, async (val, oldVal) => {
  if (val && val !== oldVal && !items.value.some(x => String(x.id) === String(val))) {
    try {
      const data = await PacienteService.getById(val)
      if (data) items.value = [data, ...items.value]
    } catch {
      console.log("catch del watch en obras sociales")
    }
  }
})
</script>
