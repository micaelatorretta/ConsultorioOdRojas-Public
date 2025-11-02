<template>
  <FullCalendar ref="calendarRef" :options="calendarOptions" />
</template>

<script setup>
/* eslint-disable */
import { ref } from 'vue'
import FullCalendar from '@fullcalendar/vue3'
import dayGridPlugin from '@fullcalendar/daygrid'
import timeGridPlugin from '@fullcalendar/timegrid'
import interactionPlugin from '@fullcalendar/interaction'
import esLocale from '@fullcalendar/core/locales/es'
import TurnoService from '@/services/turnos/TurnoService'
import { useSnackbar } from '@/composables/useSnackbar'

const props = defineProps({
  onAdd: { type: Function, required: true },
  onEdit: { type: Function, required: true }
})

const { showSnackbar } = useSnackbar?.() ?? { showSnackbar: () => {} }
const calendarRef = ref(null)

const toHHmm = (v) => {
  if (!v) return '00:00'
  if (v instanceof Date) return v.toTimeString().substring(0, 5)
  const s = String(v)
  if (s.includes('T')) return s.substring(11, 16)
  return s.substring(0, 5)
}

const mapTurnosToEvents = (arr) =>
  (arr || []).map(t => ({
    id: String(t.id ?? ''),
    title: t.descripcion ?? '(sin descripción)',
    start: `${t.fecha}T${toHHmm(t.horaInicio)}`,
    end: `${t.fecha}T${toHHmm(t.horaFin)}`,
    extendedProps: { raw: t }
  }))

const fetchAllTurnos = async () => {
  const resp = await TurnoService.getTurnos({
    paginationParams: { pageNumber: 1, pageSize: 2000 }
  })
  return resp.paginationData?.items?.$values || resp.items || []
}

const fetchTurnosByRange = async (startIso, endIso) => {
  const all = await fetchAllTurnos()
  const start = new Date(startIso)
  const end = new Date(endIso)
  return all.filter(t => {
    const s = new Date(`${t.fecha}T${toHHmm(t.horaInicio)}`)
    const e = new Date(`${t.fecha}T${toHHmm(t.horaFin)}`)
    return s < end && start < e
  })
}

const calendarOptions = ref({
  plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
  locale: esLocale,
  initialView: 'dayGridMonth',
  headerToolbar: { left: 'prev,next today', center: 'title', right: 'dayGridMonth,timeGridWeek,timeGridDay' },
  allDaySlot: false,
  slotMinTime: '07:00:00',
  slotMaxTime: '22:00:00',
  selectable: true,
  selectMirror: true,
  nowIndicator: true,
  navLinks: true,
  dayMaxEventRows: true,

  // fuente dinámica -> se vuelve a ejecutar en cada refetchEvents()
  events: async (fetchInfo, success, failure) => {
    try {
      const turnos = await fetchTurnosByRange(fetchInfo.startStr, fetchInfo.endStr)
      success(mapTurnosToEvents(turnos))
    } catch (e) {
      console.error(e)
      showSnackbar?.('No se pudieron cargar los turnos.', true)
      failure(e)
    }
  },

  dateClick: (info) => {
    const api = calendarRef.value?.getApi?.()
    if (api && api.view.type === 'dayGridMonth') {
      api.changeView('timeGridDay', info.dateStr)
    }
  },

  select: (info) => {
    const viewType = info.view?.type || ''
    if (!viewType.startsWith('timeGrid')) return
    const [fecha] = info.startStr.split('T')
    const horaInicio = info.startStr.substring(11, 16)
    const horaFin = info.endStr.substring(11, 16)
    props.onAdd({ descripcion: '', fecha, horaInicio, horaFin })
  },

  eventClick: (arg) => {
    const raw = arg.event.extendedProps?.raw
    if (raw) props.onEdit(raw)
  }
})

// Exponemos un método para que el padre refresque los eventos
const refetch = () => {
  calendarRef.value?.getApi?.().refetchEvents()
}
defineExpose({ refetch })
</script>

<style>
.fc .fc-scrollgrid { min-height: 70vh; }
</style>
