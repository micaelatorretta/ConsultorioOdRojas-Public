<template>
  <!-- DIALOGO PARA CRUD DE Nomencladores -->
  <v-dialog v-model="dialogVisible" max-width="800">
    <v-card>
      <v-card-title>
        {{ localNomenclador.id ? 'Editar Nomenclador' : 'Nuevo Nomenclador' }}
      </v-card-title>

      <div class="row">
        <div class="col-md-4">
          <v-card-text>
            <v-text-field label="Código Nacional" 
                          v-model="localNomenclador.codigoNacional" 
                          :rules="[requiredRule]"
                        @input="localNomenclador.codigoNacional = localNomenclador.codigoNacional?.toUpperCase() || ''"
                          :disabled="!enabled" />
          </v-card-text>
        </div>

        <div class="col-md-6">
          <v-card-text>
            <v-text-field label="Descripción" 
                          :rules="[requiredRule]"
                          v-model="localNomenclador.descripcion" 
                          :disabled="!enabled" />
          </v-card-text>
        </div>

    
      </div>

      <div class="row">
        <div class="col-md-4">

          <v-card-text>
              <v-text-field
                label="Importe"
                v-model.number="localNomenclador.importe"   
                :rules="[requiredRule, decimalRule]"        
                :disabled="!enabled"
                type="number"       
                step="0.01"          
                min="0"/>
            </v-card-text>
        </div>
      </div>

      <v-card-text class="pb-0">
        <v-switch
          v-model="localNomenclador.requiereCara"
          :disabled="!enabled"
          color="primary"
          inset
          hide-details
          label="¿Requiere cara dental?"
        />
        <div class="text-caption text-medium-emphasis mt-1">
          Indica si la prestación se aplica a una cara específica del diente o a la pieza completa.<br />
          <strong>Activado</strong> se asocia a una cara dental (ej. obturación en cara Oclusal).<br />
          <strong>Desactivado</strong> se aplica a toda la pieza (ej. exodoncia, corona completa).
        </div>
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="saveNomenclador">Guardar</v-btn>
        <v-btn color="gray" @click="closeDialog">Cancelar</v-btn>
      </v-card-actions>
      
    </v-card>
  </v-dialog>
</template>

<script setup>
import { defineProps, defineEmits, computed, watch, ref } from 'vue';

const props = defineProps({
  modelValue: Boolean, // Para controlar el diálogo
  nomenclador: Object, // Para recibir el nomenclador actual
  enabled: Boolean // Para habilitar/deshabilitar campos del diálogo
});

const emit = defineEmits(['update:modelValue', 'save', 'close']);

// Computed para manejar el diálogo
const dialogVisible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
});

// Estado local para evitar modificar directamente `props.nomenclador`
const localNomenclador = ref({ ...props.nomenclador });

watch(() => props.nomenclador, (newNomenclador) => {
  localNomenclador.value = { ...newNomenclador }; // Copia el nomenclador recibido
}, { deep: true }); 

// Guardar nomenclador
const saveNomenclador = () => {
  emit('save', localNomenclador.value); // Emitir con los datos actualizados
};

// Cerrar el diálogo sin guardar
const closeDialog = () => {
  emit('close', localNomenclador.value);
};

// #region Reglas de validación de nomenclador

/**
 * Valida que el campo no esté vacío ni contenga solo espacios.
 *
 * Caso de exito: Aplicable a cualquier campo obligatorio.
 * Caso de error: Rechaza cadenas vacías o solo con espacios.
 *
 * @param {string} value - El valor del campo
 * @returns {true | string} - `true` si es válido, o mensaje de error si está vacío
 */
const requiredRule = (value) => {
  if (value === null || value === undefined) {
    return 'Este campo es obligatorio';
  }

  const stringValue = String(value).trim();

  return stringValue.length > 0 ? true : 'Este campo es obligatorio';
};

  /**
 * Valida que el valor sea un número decimal válido con hasta 2 decimales.
 *
 * Caso de exito: Acepta números enteros o decimales positivos (ej. 100, 100.5, 100.50).
 * Caso de error: Rechaza letras, símbolos no numéricos o más de 2 decimales.
 *
 * @param {string | number} value - El valor del campo
 * @returns {true | string} - `true` si es válido, o mensaje de error si no cumple el formato
 */
const decimalRule = (value) => {
  if (value === null || value === undefined || value === '') {
    return 'El importe es obligatorio';
  }

  const stringValue = String(value).trim();

  // Expresión regular: números enteros o decimales con hasta 2 decimales
  const regex = /^\d+(\.\d{1,2})?$/;

  return regex.test(stringValue)
    ? true
    : 'Debe ser un número con hasta 2 decimales';
};
// #endregion

</script>
