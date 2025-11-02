<template>
  <v-container>
    <h1>Odontograma</h1>

    <PacienteSelector v-model="paciente" label="Paciente" />

    <v-alert v-if="!paciente" type="info" variant="tonal" class="mt-4">
      Seleccione un paciente para ver/editar el odontograma.
    </v-alert>
<!-- 
    <div v-if="paciente" class="d-flex align-center ga-2 my-4">
      <v-btn :loading="loading" @click="load()">Refrescar</v-btn>
      <v-spacer />
      <v-chip v-if="odontograma" color="primary" variant="tonal">
        Paciente #{{ odontograma.pacienteId }} • {{ fechaLegible }}
      </v-chip>
    </div> -->

    <!-- EMPTY STATE -->
    <v-sheet
      v-if="paciente && !odontograma && !loading"
      class="pa-6 text-center"
      color="grey-lighten-4" rounded="lg"
    >
      <div class="text-h6 mb-2">Este paciente no tiene odontogramas aún</div>
      <div class="text-body-2 mb-4">
        Creá uno nuevo para registrar prestaciones y presencia de piezas.
      </div>
      <v-btn color="primary" @click="crear">Crear odontograma</v-btn>
    </v-sheet>

    <!-- ALERTA: abre un diálogo al hacer click -->
    <v-alert
      v-if="!detalleHC && paciente"
      type="warning"
      variant="tonal"
      class="my-4 cursor-pointer"
      prominent>
      <div class="d-flex align-center w-100">
        <div class="text-body-1 text-truncate">
          El paciente no tiene Historia Clinica cargada.
        </div>
        <v-spacer />
      </div>
    </v-alert>
  

   <!-- Botón para ver la historia clinica en detalle -->
    <v-btn  v-if="detalleHC && paciente" size="small" @click="dialogHC = true" variant="text">Ver Historia Clinica</v-btn>

    <!-- DIÁLOGO: muestra el detalle multilínea -->
    <v-dialog v-model="dialogHC" max-width="720">
      <v-card>
        <v-card-title class="text-h6">
          Historia Clínica – Advertencias
        </v-card-title>

        <v-card-text>
          <div v-html="detalleHCFormateado"></div>
        </v-card-text>

        <v-card-actions>
          <v-spacer />
          <v-btn @click="dialogHC = false">Cerrar</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <OdontogramaChart
      v-if="odontograma && paciente"
      :piezas="unwrap(odontograma?.piezasDentales)"
      @click-cara="onClickCara"
      @toggle-presencia="onTogglePresencia"
      @context-cara="onContextCara"
    />

    <ApplyPrestacionDialog
  v-if="selected"
  v-model="dialogVisible"
  :numeroFdi="selected.numeroFdi"
  :cara="selected.cara"
  :current="selected.current"
  :odontograma-id="odontograma?.id"          
  @save="onSavePrestacion"
  @remove="onRemovePrestacion"              
/>
  </v-container>
</template>

<script>
/* eslint-disable */
import { ref, computed, watch,onBeforeUnmount } from 'vue';
import { useStore } from 'vuex';
import { useSnackbar } from '@/composables/useSnackbar';
import PacienteSelector from '../pacientes/PacienteSelector.vue';
import OdontogramaChart from './OdontogramaChart.vue';
import ApplyPrestacionDialog from './ApplyPrestacionDialog.vue';
import { onBeforeRouteLeave, onBeforeRouteUpdate } from 'vue-router';
import HistoriaClinicaFilterService from '@/services/historias-clinicas/HistoriaClinicaFilterService';

export default {
  components: { PacienteSelector, OdontogramaChart, ApplyPrestacionDialog },
  setup() {
    const store = useStore();
    const { showSnackbar } = useSnackbar();

     // Limpia al salir de la ruta
    onBeforeRouteLeave(() => {
      store.dispatch('odontograma/reset');
    });

    // Si la misma vista se reutiliza y solo cambian params/query:
    onBeforeRouteUpdate(() => {
      store.dispatch('odontograma/reset');
    });

    // Seguridad adicional: al desmontar el componente
    onBeforeUnmount(() => {
      store.dispatch('odontograma/reset');
    });
    
    const paciente = ref(null);
    const dialogVisible = ref(false);
    // selected: { numeroFdi, cara, current? }
    const selected = ref(null);
    const dialogHC = ref(false);

    const unwrap = (x) => (Array.isArray(x) ? x : (x && x.$values) ? x.$values : []);

    const detalleHC = computed(() => store.getters['odontograma/detalleHistoriaClinica']);// Historia Clinica detalle
    const odontograma = computed(() => store.getters['odontograma/odontograma']);
    const loading = computed(() => store.getters['odontograma/loading']);
    const fechaLegible = computed(() => {
      const f = odontograma.value?.fecha;
      return f ? new Date(f).toLocaleDateString() : '';
    });

    
    //  Formatear cada línea en negrita + <hr> entre ellas
    const detalleHCFormateado = computed(() => {
      if (!detalleHC.value) return '';
      
      const lineas = detalleHC.value.split('\n')
        .map(line => {
          const [titulo, ...resto] = line.split(':');
          if (resto.length === 0) return `<p>${titulo}</p>`;
          return `<p><b>${titulo.trim()}:</b> ${resto.join(':').trim()}</p>`;
        });

      // Agregar <hr> entre cada bloque
      return lineas.join('<hr>');
    });

    const load = async () => {
      if (!paciente.value?.id && paciente.value !== 0) return;
      await store.dispatch('odontograma/cargarPorPaciente', { pacienteId: paciente.value.id });
    };

    const crear = async (clonarUltimo = true) => {
      if (!paciente.value?.id && paciente.value !== 0) return;
      await store.dispatch('odontograma/crearNuevo', {
        paciente: paciente.value,
        clonarUltimo
      });
      showSnackbar('Odontograma creado');
      await load();
    };

    // enum -> letra
    const mapEnumToLetter = (code) => {
      const map = { 0:'M', 1:'D', 2:'V', 3:'P', 4:'O', 5:'L' };
      return map[Number(code)] || 'O';
    };
    const caraToLetter = (c) =>
      (c?.cara || c?.Cara)
        ? String(c?.cara || c?.Cara).toUpperCase()
        : mapEnumToLetter(c?.caraDental?.caraDentaria ?? c?.CaraDental?.CaraDentaria);

    // abrir diálogo con la última prestación (si existe)
    const onClickCara = (numeroFdi, cara) => {
      const piezas = unwrap(odontograma.value?.piezasDentales);
      const pieza = piezas.find(p => p?.piezaDental?.numeroPieza === numeroFdi);
      if (!pieza) return;

      const presente = (typeof pieza.presente === 'boolean') ? pieza.presente : true;
      if (!presente) { showSnackbar('La pieza está marcada como ausente.', true); return; }

      const caras = unwrap(pieza?.carasDentales || pieza?.caras);

      const esPrestacion = (c) => !!(c?.id || c?.Id || c?.nomencladorId || c?.NomencladorId || c?.nomenclador || c?.Nomenclador);
      //const current = [...caras].reverse().find(c => caraToLetter(c) === cara) || null;
      const current = [...caras]
        .reverse()
        .find(c => caraToLetter(c) === cara && esPrestacion(c)) || null;


      selected.value = { numeroFdi, cara, current };
      dialogVisible.value = true;
    };

    // crear nueva "versión" (aplicar/actualizar)
    const onSavePrestacion = async ({ prestacionId, colorHex, observacion }) => {
      try {
        console.log("onSavePrestacion");
        await store.dispatch('odontograma/aplicarCara', {
          numeroPiezaDental: selected.value.numeroFdi,
          caraDental: selected.value.cara,
          nomencladorId: prestacionId,
          colorHexadecimal: colorHex,
          observacion: observacion ?? null
        });
        showSnackbar(selected.value.current ? 'Prestación actualizada' : 'Prestación aplicada');
        dialogVisible.value = false;
        await load();
      } catch (e) {
        console.error(e);
        showSnackbar('No se pudo aplicar la prestación', true);
      }
    };

  // helper
  const normalizeCara = (c) => (c === 'P' ? 'L' : c);

  // deshacer última desde el diálogo (payload ya viene normalizado desde el dialog)
  const onRemovePrestacion = async (payload) => {
    try {
      console.log(payload);
      await store.dispatch('odontograma/eliminarPrestacion', payload);
      showSnackbar('Última prestación eliminada');
      dialogVisible.value = false;
      await load();
    } catch (e) {
      console.error(e);
      showSnackbar('No se pudo eliminar la prestación', true);
    }
  };

  // click derecho: borrar última directamente (construimos payload)
  const onContextCara = async (pieza, cara) => {
    const caras = unwrap(pieza?.carasDentales || pieza?.caras);
    const ult = [...caras].reverse().find(c => caraToLetter(c) === cara);
    if (!ult) return;
    if (!confirm(`Eliminar última prestación en ${pieza.piezaDental.numeroPieza}–${cara}?`)) return;

    await onRemovePrestacion({
      numeroPiezaDental: pieza.piezaDental.numeroPieza,
      caraDental: normalizeCara(cara),
      odontogramaId: odontograma.value?.id
    });
  };


    // toggle presencia
    const onTogglePresencia = async (pieza) => {
      try {
        const nuevo = !pieza.presente;
        const motivo = nuevo ? null : prompt('Motivo de ausencia (opcional):');
        await store.dispatch('odontograma/setPresencia', {
          piezaId: pieza.id, presente: nuevo, motivoAusencia: motivo || null
        });
        showSnackbar(nuevo ? 'Pieza marcada como presente' : 'Pieza marcada como ausente');
        await load();
      } catch (e) {
        console.error(e);
        showSnackbar('No se pudo actualizar la presencia', true);
      }
    };

    watch(paciente, () => { load(); });
    watch(dialogVisible, (v) => {
      if (!v) selected.value = null;
    });

    return {
      paciente, odontograma, loading, fechaLegible,
      dialogVisible, selected,
      load, crear,
      onClickCara, onSavePrestacion, onRemovePrestacion,
      onTogglePresencia, onContextCara,
      unwrap,
      detalleHC, dialogHC, detalleHCFormateado
    };
  }
};
</script>

<style>
/* Trunca a 3 líneas cuando no está expandido */
.line-clamp-3 {
  display: -webkit-box;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.cursor-pointer { cursor: pointer; }
.whitespace-preline { white-space: pre-line; } /* respeta \n del backend */

</style>