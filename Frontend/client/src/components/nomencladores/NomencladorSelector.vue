<template>
  <v-autocomplete
    v-model="innerValue"
    v-model:search="search"
    :items="items"
    item-title="descripcion"
    item-value="id"
    :label="label"
    return-object
    :loading="loading"
    :no-data-text="noDataText"
    variant="outlined"
    clearable
    hide-details="auto"
    :disabled="disabled"
    @focus="ensureLoaded"
    @update:search="onSearchChange"
  >
    <template #append-inner>
      <v-btn
        icon
        size="small"
        variant="text"
        :disabled="loading || disabled"
        @click.stop="reload"
      >
        <v-icon>mdi-refresh</v-icon>
      </v-btn>
    </template>
  </v-autocomplete>
</template>

<script setup>
/* eslint-disable */
import { ref, watch, computed, onMounted, defineProps, defineEmits } from 'vue'
import NomencladorService from '@/services/nomencladores/NomencladorService'
import NomencladorFilterService from '@/services/nomencladores/NomencladorFilterService' // ← si lo tenés
import { useSnackbar } from '@/composables/useSnackbar'

const props = defineProps({
  modelValue: { type: [Object, null], default: null },   // return-object
  label: { type: String, default: 'Nomenclador' },
  noDataText: { type: String, default: 'Sin resultados' },
  debounceMs: { type: Number, default: 350 },
  pageSize: { type: Number, default: 20 },
  extraFilters: { type: Array, default: () => [] },
  hydrateOnMount: { type: Boolean, default: true },
  obraSocialId: { type: [Number, String, null], default: null }, // ← NUEVO
  disabled: { type: Boolean, default: false }                     // ← NUEVO
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
const hasLoadedOnce = ref(false)
let debounceTimer = null

const buildFilters = () => {
  // Si tenés tu helper centralizado:
  if (NomencladorFilterService?.getCommonFilters) {
    console.log("servicio de filtros")
    const base = NomencladorFilterService.getCommonFilters(search.value, props.obraSocialId)
    return [...(base || []), ...(props.extraFilters || [])]
  }
  // // Fallback simple si no existe el service de filtros:
  // const filters = []
  // if (props.obraSocialId != null) {
  //   filters.push({ field: 'ObraSocialId', operator: 'eq', value: props.obraSocialId })
  // }
  // if (search.value?.trim()) {
  //   filters.push({ field: 'Descripcion', operator: 'contains', value: search.value.trim() })
  // }
  return [...filters, ...(props.extraFilters || [])]
}

const fetchItems = async ({ reset = false } = {}) => {
  if (props.disabled) { items.value = []; return }
  if (!props.obraSocialId) { // requerimos OS para listar
    items.value = []
    hasLoadedOnce.value = false
    return
  }

  loading.value = true
  try {
    const response = await NomencladorService.getNomencladores({
      paginationParams: {
        pageNumber: 1,
        pageSize: 1000,
        filters: buildFilters()
      }
    })
    items.value = response.paginationData?.items?.$values || []
    hasLoadedOnce.value = true
    emit('loaded')
  } catch (err) {
    showSnackbar('Error al obtener los nomencladores', true)
    console.error('Error:', err)
  } finally {
    loading.value = false
  }
}

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

function reload() { fetchItems({ reset: true }) }

// Re-hidratar seleccionado (si viene desde fuera) – opcional
onMounted(async () => {
  if (props.hydrateOnMount && props.modelValue && props.modelValue.id) {
    try {
      loading.value = true
      // Si querés re-hidratar por id, usá tu endpoint de detalle
      const data = await NomencladorService.getById(props.modelValue.id)
      const exists = items.value.some(x => x.id === data.id)
      if (!exists && data) items.value = [data, ...items.value]
    } catch {}
    finally { loading.value = false }
  }
})

// Si cambia la obra social, reseteamos y recargamos
watch(() => props.obraSocialId, (n) => {
  items.value = []
  if (!n) {
    innerValue.value = null
    hasLoadedOnce.value = false
    return
  }
  // si ya estaba abierto o hay foco, cargá
  fetchItems({ reset: true })
})
</script>
