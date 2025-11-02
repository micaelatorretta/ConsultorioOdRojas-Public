
<template>
  <!-- DIALOGO PARA CRUD DE Obras Sociales -->
<v-dialog v-model="dialogVisible" max-width="400">
  <v-card>
    <v-card-title>
      {{ localObraSocial.id && enabled 
            ? 'Editar Obra Social' 
            : localObraSocial.id && !enabled 
                ? 'Eliminar Obra Social' 
                : 'Nueva Obra Social' }}
    </v-card-title>

    <v-card-text>
      <v-text-field
        label="Código"
        v-model="localObraSocial.codigo"
        :disabled="!enabled"
        @input="localObraSocial.codigo = localObraSocial.codigo?.toUpperCase() || ''"
      />
    </v-card-text>

    <v-card-text>
      <v-text-field label="Nombre" 
                    v-model="localObraSocial.nombre" 
                    :disabled="!enabled" />
    </v-card-text>
   

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="primary" @click="saveObraSocial">Guardar</v-btn>
      <v-btn color="gray" @click="closeDialog">Cancelar</v-btn>
    </v-card-actions>
    
  </v-card>
</v-dialog>
</template>


<script setup>
import { defineProps, defineEmits, computed, watch, ref } from 'vue';

const props = defineProps({
  modelValue: Boolean, // Para controlar el diálogo
  obraSocial: Object, // Para recibir el obraSocial actual
  enabled: Boolean // Para habilitar/deshabilitar campos del diálogo
});

const emit = defineEmits(['update:modelValue', 'save', 'close']);

// Computed para manejar el diálogo
const dialogVisible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
});

// Estado local para evitar modificar directamente `props.obraSocial`
const localObraSocial = ref({ ...props.obraSocial });

watch(() => props.obraSocial, (newObraSocial) => {
  localObraSocial.value = { ...newObraSocial }; // Copia el obraSocial recibido
}, { deep: true }); 

// Guardar obraSocial
const saveObraSocial = () => {
  emit('save', localObraSocial.value); // Emitir con los datos actualizados
};

// Cerrar el diálogo sin guardar
const closeDialog = () => {
    emit('close', localObraSocial.value);
};
</script>
