<template>
  <v-dialog v-model="dialogVisible" max-width="720">
    <v-card>
      <v-card-title>
        {{ localTurno.id && enabled
            ? 'Editar Turno'
            : localTurno.id && !enabled
                ? 'Eliminar Turno'
                : 'Nuevo Turno' }}
      </v-card-title>

      <div class="row">
        <div class="col-md-6">
          <v-card-text>
            <PacienteSelector
              :disabled="!enabled"
              v-model="localTurno.paciente"
              label="Paciente"
            />
          </v-card-text>
        </div>
      </div>

      <div class="row">
        <div class="col-md-6">
          <v-card-text>
            <v-text-field
              label="Descripción"
              v-model="localTurno.descripcion"
              :disabled="!enabled"
            />
          </v-card-text>
        </div>
        <div class="col-md-6">
          <v-card-text>
            <v-text-field
              label="Fecha"
              v-model="localTurno.fecha"
              type="date"
              :disabled="!enabled"
            />
          </v-card-text>
        </div>
      </div>

      <div class="row">
        <div class="col-md-6">
          <v-card-text>
            <v-text-field
              label="Hora de Inicio"
              v-model="localTurno.horaInicio"
              type="time"
              :disabled="!enabled"
            />
          </v-card-text>
        </div>
        <div class="col-md-6">
          <v-card-text>
            <v-text-field
              label="Hora de Fin"
              v-model="localTurno.horaFin"
              type="time"
              :disabled="!enabled"
            />
          </v-card-text>
        </div>
      </div>

      <v-card-actions>
        <!-- Solo visible si se abre desde calendario -->
        <v-btn
          v-if="allowDelete && localTurno.id"
          color="red"
          variant="outlined"
          @click="confirmDelete = true"
        >
          Eliminar
        </v-btn>

        <v-spacer />
        <v-btn color="primary" @click="saveTurno">Guardar</v-btn>
        <v-btn color="gray" @click="closeDialog">Cancelar</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>

  <!-- Confirmación de borrado -->
  <v-dialog v-model="confirmDelete" max-width="420">
    <v-card>
      <v-card-title>Eliminar turno</v-card-title>
      <v-card-text>
        ¿Confirmás eliminar el turno
        <strong>{{ localTurno.descripcion || '(sin descripción)' }}</strong> del
        <strong>{{ localTurno.fecha }}</strong>?
      </v-card-text>
      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" @click="confirmDelete = false">Cancelar</v-btn>
        <v-btn color="red" @click="handleDelete">Eliminar</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { defineProps, defineEmits, computed, watch, ref } from 'vue'
import PacienteSelector from '../pacientes/PacienteSelector.vue'

const props = defineProps({
  modelValue: Boolean,
  turno: Object,
  enabled: Boolean,
  // 👇 Solo calendario habilita el botón eliminar
  allowDelete: { type: Boolean, default: false }
})

const emit = defineEmits(['update:modelValue', 'save', 'close', 'delete'])

const dialogVisible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
})

const localTurno = ref({ ...props.turno })
const confirmDelete = ref(false)

/** Sincroniza pacienteId cuando cambia el paciente (objeto o id) */
watch(
  () => localTurno.value.paciente,
  (p) => {
    const id =
      (p && typeof p === 'object')
        ? (p.id ?? p.Id ?? null)
        : (typeof p === 'number' ? p : null)
    localTurno.value.pacienteId = id
  },
  { immediate: true }
)

/** Normalización y sync cuando cambia el turno entrante */
watch(
  () => props.turno,
  (newTurno) => {
    if (!newTurno) return
    const t = { ...newTurno }

    // Si viene paciente pero no pacienteId, inferir
    if (t.paciente && (t.pacienteId == null)) {
      t.pacienteId = t.paciente.id ?? t.paciente.Id ?? null
    }

    // Fecha -> yyyy-MM-dd (para type="date")
    if (t.fecha && t.fecha.includes('/')) {
      const [d, m, y] = t.fecha.split('/')
      t.fecha = `${y}-${m.padStart(2, '0')}-${d.padStart(2, '0')}`
    }

    const fixTime = (v) => {
      if (!v) return ''
      if (v instanceof Date) return v.toTimeString().substring(0, 5)
      const s = String(v)
      if (s.includes('T')) return s.substring(11, 16) // ISO datetime
      return s.substring(0, 5) // HH:mm:ss -> HH:mm
    }

    t.horaInicio = fixTime(t.horaInicio)
    t.horaFin = fixTime(t.horaFin)
    localTurno.value = t
  },
  { deep: true, immediate: true }
)

const handleDelete = () => {
  emit('delete', localTurno.value)
  confirmDelete.value = false     // cierra confirmación
  dialogVisible.value = false     // cierra el diálogo principal
}

const saveTurno = () => {
  emit('save', { ...localTurno.value })
}

const closeDialog = () => {
  emit('close', localTurno.value)
}
</script>
