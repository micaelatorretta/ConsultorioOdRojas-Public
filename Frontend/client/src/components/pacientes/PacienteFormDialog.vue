<template>
  <v-dialog v-model="dialogVisible" max-width="640">
    <v-card>
      <v-card-title>
        {{ localPaciente.id && enabled 
          ? 'Editar Paciente' 
          : localPaciente.id && !enabled 
              ? 'Eliminar Paciente' 
              : 'Nuevo Paciente' }}
      </v-card-title>

      <div class="row">
        <div class="col-md-6">
          <!-- Datos básicos -->
              <v-card-text>
                <v-text-field label="Nombre"
                              v-model="localPaciente.nombre"
                              :rules="[requiredRule, onlyLettersRule]"
                              :disabled="!enabled" />
              </v-card-text>

        </div>
        <div class="col-md-6">
            <v-card-text>
                <v-text-field label="Apellido"
                              v-model="localPaciente.apellido"
                              :rules="[requiredRule, onlyLettersRule]"
                              :disabled="!enabled" />
              </v-card-text>
        </div>
      </div>
   
      <div class="row">
        <div class="col-md-6">
          <v-card-text>
            <v-text-field label="DNI"
                          v-model="localPaciente.dni"
                          :rules="[dniLengthRule, dniNumericRule]"
                          maxlength="8"
                          counter
                          :disabled="!enabled" />
          </v-card-text>
        </div>
         <div class="col-md-6">

            <v-card-text>
              <ObraSocialSelector :disabled="!enabled"
                                  v-model="localPaciente.obraSocial"
                                  label="Obra social"/>
            </v-card-text>

        </div>
      </div>

      <!-- Historia Clínica (embebida) -->
      <v-divider class="my-2" />
      <v-expansion-panels multiple>
        <v-expansion-panel>
          <v-expansion-panel-title>
            Historia Clínica
          </v-expansion-panel-title>
          <v-expansion-panel-text>
            <HistoriaClinicaFormEmbedded
              v-model="localPaciente.historiaClinica"
              :enabled="enabled"
            />
          </v-expansion-panel-text>
        </v-expansion-panel>
      </v-expansion-panels>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="savePaciente">Guardar</v-btn>
        <v-btn color="gray" @click="closeDialog">Cancelar</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { defineProps, defineEmits, computed, watch, ref } from 'vue'
import ObraSocialSelector from '@/components/obras-sociales/ObraSocialSelector.vue'
import HistoriaClinicaFormEmbedded from '@/components/historias-clinicas/HistoriaClinicaFormEmbedded.vue'

const props = defineProps({
  modelValue: Boolean,
  paciente: Object,
  enabled: Boolean
})

const emit = defineEmits(['update:modelValue', 'save', 'close'])

const dialogVisible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
})

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

const localPaciente = ref({ ...props.paciente })

watch(() => props.paciente, (nuevo) => {
  const base = { ...nuevo }
  if (!base.historiaClinica) base.historiaClinica = getDefaultHC()
  localPaciente.value = base
}, { deep: true, immediate: true })

// ---- Helpers de normalización para payload ----
const toISO = (d) => {
  if (!d) return null
  const s = String(d)
  if (s.includes('/')) {
    const [day, month, year] = s.split('/')
    return `${year}-${month.padStart(2,'0')}-${day.padStart(2,'0')}`
  }
  return s.includes('T') ? s.split('T')[0] : s
}

const normalizeHistoriaClinica = (hc) => {
  if (!hc) return getDefaultHC()
  const out = structuredClone(hc)
  // normalizar fecha de radiografía si viene
  if (out.radiografias?.fecha) out.radiografias.fecha = toISO(out.radiografias.fecha)
  // normalizar cantidades a número
  if (out.fuma?.tiene && out.fuma?.cantidad != null) out.fuma.cantidad = Number(out.fuma.cantidad)
  if (out.bebe?.tiene && out.bebe?.cantidad != null) out.bebe.cantidad = Number(out.bebe.cantidad)
  return out
}

const savePaciente = () => {
  // obra social: puede ser objeto o id
  const os = localPaciente.value?.obraSocial
  const obraSocialId = (os && typeof os === 'object') ? (os.id ?? os.Id ?? null) : (os ?? null)

  const payload = {
    id: localPaciente.value.id,
    nombre: localPaciente.value.nombre?.trim() ?? '',
    apellido: localPaciente.value.apellido?.trim() ?? '',
    dni: localPaciente.value.dni?.trim() ?? '',
    obraSocialId,
    // 👇 anidamos toda la historia clínica en el DTO de paciente
    historiaClinica: normalizeHistoriaClinica(localPaciente.value.historiaClinica)
  }

  emit('save', payload)
}

const closeDialog = () => emit('close', localPaciente.value)

// --- Reglas de validación (igual que tenías) ---
const dniLengthRule = (value) => value && value.length === 8 ? true : 'El DNI debe tener exactamente 8 dígitos'
const dniNumericRule = (value) => (/^\d+$/).test(value) ? true : 'El DNI solo debe contener números'
const requiredRule = (value) => value?.trim().length > 0 ? true : 'Este campo es obligatorio'
const onlyLettersRule = (value) => /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$/.test(value) ? true : 'Solo se permiten letras y espacios'
</script>
