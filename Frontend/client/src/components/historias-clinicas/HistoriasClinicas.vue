<template>
  <v-container>
    <h1>Historia Clinica</h1>

    <PacienteSelector v-model="paciente" label="Paciente" />

    <v-alert v-if="!paciente" type="info" variant="tonal" class="mt-4">
      Seleccione un paciente para ver/editar la historia clinica.
    </v-alert>

    <HistoriaClinicaForm  v-if="paciente"
                          v-model="dialogVisible"
                          :historiaClinica="historiaClinica"
                          :paciente="paciente"
                          @save="saveHistoriaClinica" />

  </v-container>
</template>

<script>
/* eslint-disable */
import { ref, computed, watch } from 'vue';
import { useStore } from 'vuex';
import { useSnackbar } from '@/composables/useSnackbar';
import PacienteSelector from '../pacientes/PacienteSelector.vue';
import HistoriaClinicaForm from './HistoriaClinicaForm.vue';

export default {
  components: { PacienteSelector,HistoriaClinicaForm },
  setup() {
    const store = useStore();
    const { showSnackbar } = useSnackbar();

    const paciente = ref(null);

    const historiaClinica = computed(() => store.getters['historiasClinicas/historiaClinica']);
    const loading = computed(() => store.getters['historiasClinicas/loading']);

    const load = async () => {
      if (!paciente.value?.id && paciente.value !== 0) return;
      await store.dispatch('historiasClinicas/cargarPorPaciente', { pacienteId: paciente.value.id });
    };

    const saveHistoriaClinica = async (historiaClinica) => {
      //if (!paciente.value?.id && paciente.value !== 0) return;
      await store.dispatch('historiasClinicas/save', {historiaClinica: historiaClinica});
      showSnackbar('Historia clinica guardada');
      await load();
    };

    watch(paciente, () => { load(); });

    return {
      paciente, historiaClinica, loading,
      load, saveHistoriaClinica//,
      //unwrap
    };
  }
};
</script>
