<template>
  <v-card>
    <v-card-title class="text-h6">
      Editar Historia Clínica
    </v-card-title>

    <v-card-text>
      <v-form ref="formRef" v-model="isValid" validate-on="input">
        <v-row dense>
          <!-- Intervenciones últimos 2 años -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.intervencionesUlt2Anios.si"
              :color="switchColor(localHistoriaClinica.intervencionesUlt2Anios.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.intervencionesUlt2Anios.si)">
                  Intervenciones últimos 2 años
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.intervencionesUlt2Anios.si"
              label="Detalle de intervenciones"
              v-model="localHistoriaClinica.intervencionesUlt2Anios.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.intervencionesUlt2Anios.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Tratamiento médico -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.tratamientoMedico.si"
              :color="switchColor(localHistoriaClinica.tratamientoMedico.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.tratamientoMedico.si)">
                  Tratamiento médico actual
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.tratamientoMedico.si"
              label="Detalle del tratamiento"
              v-model="localHistoriaClinica.tratamientoMedico.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.tratamientoMedico.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Alergias -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.alergias.si"
              :color="switchColor(localHistoriaClinica.alergias.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.alergias.si)">
                  ¿Tiene alergias?
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.alergias.si"
              label="Detalle de alergias"
              v-model="localHistoriaClinica.alergias.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.alergias.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Anestesias previas -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.anestesiasPrevias.si"
              :color="switchColor(localHistoriaClinica.anestesiasPrevias.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.anestesiasPrevias.si)">
                  ¿Tuvo anestesias previas?
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.anestesiasPrevias.si"
              label="Detalle de anestesias previas"
              v-model="localHistoriaClinica.anestesiasPrevias.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.anestesiasPrevias.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Sangrado excesivo -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.sangradoExcesivo.si"
              :color="switchColor(localHistoriaClinica.sangradoExcesivo.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.sangradoExcesivo.si)">
                  ¿Presenta sangrado excesivo?
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.sangradoExcesivo.si"
              label="Detalle"
              v-model="localHistoriaClinica.sangradoExcesivo.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.sangradoExcesivo.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Medicación actual -->
          <v-col cols="12" md="6">
            <v-switch
              v-model="localHistoriaClinica.medicacionActual.si"
              :color="switchColor(localHistoriaClinica.medicacionActual.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.medicacionActual.si)">
                  ¿Toma medicación actualmente?
                </span>
              </template>
            </v-switch>

            <v-textarea
              v-if="localHistoriaClinica.medicacionActual.si"
              label="Detalle de medicación"
              v-model="localHistoriaClinica.medicacionActual.detalle"
              :disabled="!enabled"
              :rules="[requiredIf(() => localHistoriaClinica.medicacionActual.si)]"
              auto-grow
              rows="2"
            />
          </v-col>

          <!-- Cansancio al caminar -->
          <v-col cols="12" md="4">
            <v-switch
              v-model="localHistoriaClinica.cansancioAlCaminar"
              :color="switchColor(localHistoriaClinica.cansancioAlCaminar)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.cansancioAlCaminar)">
                  ¿Siente cansancio al caminar?
                </span>
              </template>
            </v-switch>
          </v-col>

          <!-- Fuma -->
          <v-col cols="12" md="4">
            <v-switch
              v-model="localHistoriaClinica.fuma.tiene"
              :color="switchColor(localHistoriaClinica.fuma.tiene)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.fuma.tiene)">
                  ¿Fuma?
                </span>
              </template>
            </v-switch>

            <v-text-field
              v-if="localHistoriaClinica.fuma.tiene"
              label="Cantidad por día"
              v-model.number="localHistoriaClinica.fuma.cantidad"
              type="number"
              min="0"
              step="1"
              :disabled="!enabled"
              :rules="[nonNegativeNumberRule]"
            />
          </v-col>

          <!-- Bebe -->
          <v-col cols="12" md="4">
            <v-switch
              v-model="localHistoriaClinica.bebe.tiene"
              :color="switchColor(localHistoriaClinica.bebe.tiene)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.bebe.tiene)">
                  ¿Bebe alcohol?
                </span>
              </template>
            </v-switch>

            <v-text-field
              v-if="localHistoriaClinica.bebe.tiene"
              label="Unidades por semana"
              v-model.number="localHistoriaClinica.bebe.cantidad"
              type="number"
              min="0"
              step="1"
              :disabled="!enabled"
              :rules="[nonNegativeNumberRule]"
            />
          </v-col>

          <!-- Embarazo -->
          <v-col cols="12" md="4">
            <v-switch
              v-model="localHistoriaClinica.embarazo"
              :color="switchColor(localHistoriaClinica.embarazo)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.embarazo)">
                  ¿Embarazo?
                </span>
              </template>
            </v-switch>
          </v-col>

          <!-- Radiografías -->
          <v-col cols="12" md="8">
            <v-switch
              v-model="localHistoriaClinica.radiografias.si"
              :color="switchColor(localHistoriaClinica.radiografias.si)"
              :disabled="!enabled"
            >
              <template #label>
                <span :class="labelClass(localHistoriaClinica.radiografias.si)">
                  ¿Se tomaron radiografías?
                </span>
              </template>
            </v-switch>

            <v-text-field
              v-if="localHistoriaClinica.radiografias.si"
              label="Fecha de radiografía"
              v-model="localHistoriaClinica.radiografias.fecha"
              type="date"
              :disabled="!enabled"
              :rules="[notFutureDateRule]"
            />
          </v-col>

          <!-- Observaciones -->
          <v-col cols="12">
            <v-textarea
              label="Observaciones"
              v-model="localHistoriaClinica.observaciones"
              :disabled="!enabled"
              :rows="3"
              auto-grow
            />
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>

    <v-card-actions>
      <v-spacer />
      <v-btn variant="text" @click="closeDialog">Cancelar</v-btn>
      <v-btn color="primary" @click="saveHistoriaClinica">Guardar</v-btn>
    </v-card-actions>
  </v-card>
</template>

<script setup>
import { defineProps, defineEmits, watch, ref, computed } from 'vue';

const props = defineProps({
  paciente: { type: Object, default: null },
  modelValue: { type: Boolean, default: false },
  historiaClinica: { type: Object, default: null },
  enabled: { type: Boolean, default: true }
});

const emit = defineEmits(['update:modelValue', 'save', 'close']);

const enabled = computed(() => props.enabled ?? true);

const todayISO = () => new Date().toISOString().split('T')[0];

const getNewHistoriaClinica = () => ({
  intervencionesUlt2Anios: { si: false, detalle: '' },
  tratamientoMedico:      { si: false, detalle: '' },
  alergias:               { si: false, detalle: '' },
  anestesiasPrevias:      { si: false, detalle: '' },
  sangradoExcesivo:       { si: false, detalle: '' },
  medicacionActual:       { si: false, detalle: '' },
  cansancioAlCaminar: false,
  fuma: { tiene: false, cantidad: 0 },
  bebe: { tiene: false, cantidad: 0 },
  embarazo: false,
  radiografias: { si: false, fecha: todayISO() },
  paciente: props.paciente,
  pacienteId: props.paciente.id,
  observaciones: ''
});

// Estado local (evita mutar props directamente)
const localHistoriaClinica = ref({
  ...(props.historiaClinica ?? getNewHistoriaClinica())
});

// Sync si cambian props desde afuera
watch(
  () => props.historiaClinica,
  (nuevo) => {
    localHistoriaClinica.value = { ...(nuevo ?? getNewHistoriaClinica()) };
  },
  { deep: true, immediate: true }
);

// --- Validación ---
const formRef = ref();
const isValid = ref(false);

// Requerido si el predicado es true
const requiredIf = (predicate) => (val) =>
  predicate() ? (val && String(val).trim().length > 0 ? true : 'Campo requerido') : true;

const nonNegativeNumberRule = (val) =>
  (val === 0 || (!!val && !Number.isNaN(Number(val)) && Number(val) >= 0))
    ? true
    : 'Ingrese un número válido mayor o igual a 0';

const notFutureDateRule = (val) => {
  if (!val) return true;
  return val <= todayISO() ? true : 'La fecha no puede ser futura';
};

// --- Helpers de UI ---
// Color del switch: verde si true, gris si false
const switchColor = (val) => (val ? 'success' : 'grey');
const labelClass = (val) => (val ? 'text-success' : 'text-medium-emphasis');

// Guardar (valida antes de emitir)
const saveHistoriaClinica = async () => {
  const ok = await formRef.value?.validate();
  if (!ok?.valid) return;

  console.log(localHistoriaClinica.value)
  // structuredClone evita referencias compartidas
  emit('save', structuredClone(localHistoriaClinica.value));
  emit('update:modelValue', false);
};

// Cerrar sin guardar
const closeDialog = () => {
  emit('close');
  emit('update:modelValue', false);
};
</script>

<style scoped>
.text-success { color: rgb(var(--v-theme-success)) }
.text-medium-emphasis { color: rgba(var(--v-theme-on-surface), 0.6) }
</style>
