<template>
  <v-form ref="formRef" v-model="isValid" validate-on="input">
    <v-row dense>
      <!-- Intervenciones últimos 2 años -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.intervencionesUlt2Anios.si"
          :color="switchColor(hc.intervencionesUlt2Anios.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.intervencionesUlt2Anios.si)">
              Intervenciones últimos 2 años
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.intervencionesUlt2Anios.si"
          label="Detalle de intervenciones"
          v-model="hc.intervencionesUlt2Anios.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.intervencionesUlt2Anios.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Tratamiento médico -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.tratamientoMedico.si"
          :color="switchColor(hc.tratamientoMedico.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.tratamientoMedico.si)">
              Tratamiento médico actual
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.tratamientoMedico.si"
          label="Detalle del tratamiento"
          v-model="hc.tratamientoMedico.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.tratamientoMedico.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Alergias -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.alergias.si"
          :color="switchColor(hc.alergias.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.alergias.si)">
              ¿Tiene alergias?
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.alergias.si"
          label="Detalle de alergias"
          v-model="hc.alergias.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.alergias.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Anestesias previas -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.anestesiasPrevias.si"
          :color="switchColor(hc.anestesiasPrevias.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.anestesiasPrevias.si)">
              ¿Tuvo anestesias previas?
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.anestesiasPrevias.si"
          label="Detalle de anestesias previas"
          v-model="hc.anestesiasPrevias.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.anestesiasPrevias.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Sangrado excesivo -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.sangradoExcesivo.si"
          :color="switchColor(hc.sangradoExcesivo.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.sangradoExcesivo.si)">
              ¿Presenta sangrado excesivo?
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.sangradoExcesivo.si"
          label="Detalle"
          v-model="hc.sangradoExcesivo.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.sangradoExcesivo.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Medicación actual -->
      <v-col cols="12" md="6">
        <v-switch
          v-model="hc.medicacionActual.si"
          :color="switchColor(hc.medicacionActual.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.medicacionActual.si)">
              ¿Toma medicación actualmente?
            </span>
          </template>
        </v-switch>

        <v-textarea
          v-if="hc.medicacionActual.si"
          label="Detalle de medicación"
          v-model="hc.medicacionActual.detalle"
          :disabled="!enabled"
          :rules="[requiredIf(() => hc.medicacionActual.si)]"
          auto-grow
          rows="2"
        />
      </v-col>

      <!-- Cansancio al caminar -->
      <v-col cols="12" md="4">
        <v-switch
          v-model="hc.cansancioAlCaminar"
          :color="switchColor(hc.cansancioAlCaminar)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.cansancioAlCaminar)">
              ¿Siente cansancio al caminar?
            </span>
          </template>
        </v-switch>
      </v-col>

      <!-- Fuma -->
      <v-col cols="12" md="4">
        <v-switch
          v-model="hc.fuma.tiene"
          :color="switchColor(hc.fuma.tiene)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.fuma.tiene)">
              ¿Fuma?
            </span>
          </template>
        </v-switch>

        <v-text-field
          v-if="hc.fuma.tiene"
          label="Cantidad por día"
          v-model.number="hc.fuma.cantidad"
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
          v-model="hc.bebe.tiene"
          :color="switchColor(hc.bebe.tiene)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.bebe.tiene)">
              ¿Bebe alcohol?
            </span>
          </template>
        </v-switch>

        <v-text-field
          v-if="hc.bebe.tiene"
          label="Unidades por semana"
          v-model.number="hc.bebe.cantidad"
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
          v-model="hc.embarazo"
          :color="switchColor(hc.embarazo)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.embarazo)">
              ¿Embarazo?
            </span>
          </template>
        </v-switch>
      </v-col>

      <!-- Radiografías -->
      <v-col cols="12" md="8">
        <v-switch
          v-model="hc.radiografias.si"
          :color="switchColor(hc.radiografias.si)"
          :disabled="!enabled"
        >
          <template #label>
            <span :class="labelClass(hc.radiografias.si)">
              ¿Se tomaron radiografías?
            </span>
          </template>
        </v-switch>

        <v-text-field
          v-if="hc.radiografias.si"
          label="Fecha de radiografía"
          v-model="hc.radiografias.fecha"
          type="date"
          :disabled="!enabled"
          :rules="[notFutureDateRule]"
        />
      </v-col>

      <!-- Observaciones -->
      <v-col cols="12">
        <v-textarea
          label="Observaciones"
          v-model="hc.observaciones"
          :disabled="!enabled"
          :rows="3"
          auto-grow
        />
      </v-col>
    </v-row>
  </v-form>
</template>

<script setup>
/* eslint-disable */
import { defineProps, defineEmits, ref, watch, nextTick,computed } from 'vue'

const props = defineProps({
  modelValue: { type: Object, default: null },
  enabled: { type: Boolean, default: true }
})
const emit = defineEmits(['update:modelValue'])

const getDefaultHC = () => ({
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
  radiografias: { si: false, fecha: new Date().toISOString().split('T')[0] },
  observaciones: ''
})

const hc = ref(props.modelValue ?? getDefaultHC())

//  Flag para distinguir si el cambio viene del padre (no re-emitir)
let syncingFromParent = false
const todayISO = () => new Date().toISOString().split('T')[0]

const formRef = ref()
const isValid = ref(false)

// 1) Cuando cambia el prop del padre, actualizá local SIN re-emitir
watch(
  () => props.modelValue,
  (nv) => {
    syncingFromParent = true
    hc.value = nv ? structuredClone(nv) : getDefaultHC()
    // desactivar la bandera en el próximo tick para permitir eventos de usuario
    nextTick(() => { syncingFromParent = false })
  },
  { immediate: true } // sin deep; no lo necesitás acá
)

// 2) Cuando cambia local por el usuario, emití al padre (si no viene del padre)
watch(
  hc,
  (val) => {
    if (syncingFromParent) return
    emit('update:modelValue', structuredClone(val))
  },
  { deep: true }
)

const requiredIf = (pred) => (v) => pred() ? (v && String(v).trim().length > 0 ? true : 'Campo requerido') : true
const nonNegativeNumberRule = (val) =>
  (val === 0 || (!!val && !Number.isNaN(Number(val)) && Number(val) >= 0))
    ? true
    : 'Ingrese un número válido mayor o igual a 0'
const notFutureDateRule = (val) => !val || val <= todayISO() || 'La fecha no puede ser futura'

const switchColor = (val) => (val ? 'success' : 'grey')
const labelClass = (val) => (val ? 'text-success' : 'text-medium-emphasis')

const enabled = computed(() => props.enabled)
</script>

<style scoped>
.text-success { color: rgb(var(--v-theme-success)) }
.text-medium-emphasis { color: rgba(var(--v-theme-on-surface), 0.6) }
</style>
