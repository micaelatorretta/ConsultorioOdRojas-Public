<template>
  <v-autocomplete
    v-model="innerValue"
    v-model:search="search"
    :items="items"
    item-title="nombre"
    item-value="id"
    :label="label"
    return-object
    :loading="loading"
    :no-data-text="noDataText"
    variant="outlined"
    clearable
    :disabled="disabled"        
    :readonly="disabled"        
    hide-details="auto"
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
import { ref, watch, computed, onMounted, defineProps, defineEmits } from 'vue'
import ObraSocialService from '@/services/obras-sociales/ObraSocialService'
import { useSnackbar } from '@/composables/useSnackbar'

const props = defineProps({
  modelValue: { type: [Number, String, null], default: null },
  label: { type: String, default: 'Obra social' },
  noDataText: { type: String, default: 'Sin resultados' },
  debounceMs: { type: Number, default: 350 },
  pageSize: { type: Number, default: 20 },
  extraFilters: { type: Array, default: () => [] },
  hydrateOnMount: { type: Boolean, default: true },

  disabled: { type: Boolean, default: false }
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

const fetchItems = async () => {
  if (props.disabled) return      
  loading.value = true
  try {
    const response = await ObraSocialService.getObrasSociales({
      paginationParams: {
        pageNumber: 1,
        pageSize: 1000
      }
    })
    items.value = response.paginationData?.items?.$values || []
    hasLoadedOnce.value = true
    emit('loaded')
  } catch (err) {
    showSnackbar('Error al obtener las obras sociales', true)
    console.error('Error:', err)
  } finally {
    loading.value = false
  }
}

function onSearchChange() {
  if (props.disabled) return       
  clearTimeout(debounceTimer)
  debounceTimer = setTimeout(() => {
    fetchItems({ reset: true })
  }, props.debounceMs)
}

function ensureLoaded() {
  if (props.disabled) return       
  if (!hasLoadedOnce.value) fetchItems({ reset: true })
}

function reload() {
  if (props.disabled) return       
  fetchItems({ reset: true })
}

onMounted(async () => {
  if (props.disabled) return       
  if (props.modelValue && props.hydrateOnMount) {
    try {
      loading.value = true
      const data = await ObraSocialService.getObraSocialById(props.modelValue)
      const exists = items.value.some(x => x.id === data.id)
      if (!exists && data) items.value = [data, ...items.value]
      hasLoadedOnce.value = true
    } finally {
      loading.value = false
    }
  }
})

watch(() => props.modelValue, async (val, oldVal) => {
  if (props.disabled) return       
  if (val && val !== oldVal && !items.value.some(x => String(x.id) === String(val))) {
    try {
      const data = await ObraSocialService.getObraSocialById(val)
      if (data) items.value = [data, ...items.value]
    } catch {
      console.log('catch del watch en obras sociales')
    }
  }
})
</script>
