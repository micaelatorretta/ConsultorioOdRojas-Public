<template>
  <v-container>
    <!-- Barra superior con botón + y buscador -->
    <v-row align="center" justify="space-between">
      <v-col cols="auto">
        <v-btn icon color="primary" @click="emit('addItem')">
          <v-icon>mdi-plus</v-icon>
          <span v-if="placeholderAddButton" class="ml-2">{{ placeholderAddButton }}</span>
        </v-btn>
      </v-col>

      <v-col cols="auto">
        <v-text-field
          v-model="searchQuery"
          label="Buscar..."
          dense
          outlined
          clearable
          @input="emitSearch"
          style="width: 500px"
        />
      </v-col>
    </v-row>

    <v-data-table-server
      :headers="headers"
      :items="displayItems"           
      :loading="loading"
      :items-length="localPagination.totalItems"
      :v-if="headers.length > 0 && items"
      :items-per-page="localPagination.itemsPerPage"
      class="elevation-1"
      no-data-text="No hay datos disponibles"
      :hide-default-footer="true"
      @update:sort-by="updateSortBy"
    >
      <template v-slot:item.acciones="{ item }">
        <v-btn color="primary" icon @click="emit('editItem', item)">
          <v-icon>mdi-pencil</v-icon>
        </v-btn>
        <v-btn color="red" icon @click="emit('deleteItem', item)">
          <v-icon>mdi-delete</v-icon>
        </v-btn>
      </template>
    </v-data-table-server>

    <!-- Contenedor de paginación -->
    <div class="pagination-container">
      <v-select
        v-model="localPagination.itemsPerPage"
        :items="[5, 10, 15, 20]"
        label="Items por página"
        dense
        outlined
        class="items-per-page"
        @update:model-value="emit('updateItemsPerPage', localPagination.itemsPerPage)"
      />
      <v-pagination
        v-if="localPagination.totalItems > 0"
        v-model="localPagination.page"
        :length="Math.ceil(localPagination.totalItems / localPagination.itemsPerPage)"
        @update:model-value="emit('updatePage', localPagination.page)"
      />
    </div>
  </v-container>
</template>

<script setup>
import { defineProps, defineEmits, reactive, watch, ref, computed } from 'vue';

const props = defineProps({
  headers: Array,
  items: Array,
  loading: Boolean,
  pagination: Object,
  placeholderAddButton: String,
  /** Si true, intenta formatear cualquier campo con fecha ISO a dd/MM/aaaa */
  autoFormatDates: { type: Boolean, default: true },
});

/* ---------- Utils de paginación ---------- */
const localPagination = reactive({ ...props.pagination });
const emit = defineEmits([
  'updatePage',
  'updateItemsPerPage',
  'addItem',
  'editItem',
  'deleteItem',
  'search',
  'updateSorting',
]);

watch(
  () => props.pagination,
  (newVal) => Object.assign(localPagination, newVal),
  { deep: true }
);

/* ---------- Búsqueda ---------- */
const searchQuery = ref('');
const emitSearch = () => {
  emit('search', { query: searchQuery.value });
};

/* ---------- Ordenamiento ---------- */
const updateSortBy = (newSortBy) => {
  emit('updateSorting', newSortBy);
};

/* ---------- Helpers de formato de fecha ---------- */
/** Detecta strings tipo 'YYYY-MM-DD' o 'YYYY-MM-DDTHH:mm:ss...' */
const isIsoDate = (val) => {
  if (typeof val !== 'string') return false;
  // yyyy-mm-dd
  if (/^\d{4}-\d{2}-\d{2}$/.test(val)) return true;
  // yyyy-mm-ddThh:mm[:ss[.SSS]]Z?
  if (/^\d{4}-\d{2}-\d{2}T/.test(val)) return true;
  return false;
};

const formatIsoDateToDMY = (val) => {
  if (!val) return '';
  // Si viene con T, me quedo con la parte de fecha
  const onlyDate = String(val).split('T')[0];
  const [y, m, d] = onlyDate.split('-');
  if (!y || !m || !d) return val; // fallback
  return `${d.padStart(2, '0')}/${m.padStart(2, '0')}/${y}`;
};

/* ---------- Helpers para paths anidados ---------- */
const getByPath = (obj, path) => {
  if (!obj || !path) return undefined;
  return path.split('.').reduce((acc, key) => (acc ? acc[key] : undefined), obj);
};

const setByPath = (obj, path, value) => {
  if (!obj || !path) return;
  const parts = path.split('.');
  const last = parts.pop();
  let cur = obj;
  for (const p of parts) {
    if (cur[p] == null || typeof cur[p] !== 'object') cur[p] = {};
    cur = cur[p];
  }
  cur[last] = value;
};

/* ---------- Items formateados para mostrar ---------- */
const displayItems = computed(() => {
  const src = Array.isArray(props.items) ? props.items : [];
  if (!props.autoFormatDates || !Array.isArray(props.headers)) return src;

  // clon superficial de cada item y aplicación de formato SOLO a columnas visibles
  return src.map((row) => {
    const clone = structuredClone ? structuredClone(row) : JSON.parse(JSON.stringify(row));
    for (const h of props.headers) {
      const key = h?.value;
      if (!key || key === 'acciones') continue;

      const val = getByPath(clone, key);
      if (isIsoDate(val)) {
        setByPath(clone, key, formatIsoDateToDMY(val));
      }
    }
    return clone;
  });
});
</script>
