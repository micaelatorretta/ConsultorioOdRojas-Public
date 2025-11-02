<template>
  <v-container class="py-6">
    <h1>Respaldo de Información</h1>

    <v-row class="mt-4" dense>
      <!-- Generar backup -->
      <v-col cols="12" md="6">
        <v-card rounded="lg">
          <v-card-title class="text-h6">Generar backup</v-card-title>
          <v-card-text class="text-body-2">
            Se generará un archivo .rar/.zip con el respaldo de la base de datos y archivos relevantes.
          </v-card-text>
          <v-card-actions>
            <v-spacer />
            <v-btn
              color="primary"
              :loading="loadingBackup"
              :disabled="loadingBackup"
              @click="onGenerateBackup"
            >
              Generar backup
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>

      <!-- Restaurar backup -->
      <v-col cols="12" md="6">
        <v-card rounded="lg">
          <v-card-title class="text-h6">Restaurar desde archivo</v-card-title>
          <v-card-text>
           <v-file-input
                v-model="file"
                :multiple="false"
                label="Seleccionar backup (.rar/.zip)"
                accept=".rar,.zip,.7z"
                prepend-icon="mdi-archive"
                :disabled="loadingRestore"
                show-size
                clearable
                :rules="[fileRequiredRule, fileSizeRule, fileExtRule]"
              />
            <div class="text-caption text-medium-emphasis">
              Tamaño máx. recomendado: 1 GB
            </div>
          </v-card-text>
          <v-card-actions>
            <v-spacer />
            <v-btn
              color="error"
              variant="tonal"
              :disabled="!file || loadingRestore"
              :loading="loadingRestore"
              @click="onRestore"
            >
              Restaurar
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <!-- Snackbars -->
    <v-snackbar v-model="snackbar.show" :color="snackbar.error ? 'error' : 'success'">
      {{ snackbar.msg }}
    </v-snackbar>
  </v-container>
</template>

<script setup>
/* eslint-disable */
import { ref } from 'vue';
import RespaldosInformacionService from '@/services/respaldos-informacion/RespaldosInformacionService';

const loadingBackup = ref(false);
const loadingRestore = ref(false);
const file = ref(null);

const snackbar = ref({ show: false, msg: '', error: false });
const notify = (msg, error = false) => {
  snackbar.value = { show: true, msg, error };
};
const pickFile = (v) => Array.isArray(v) ? v[0] : v;

const fileRequiredRule = (v) => (pickFile(v) ? true : 'Seleccione un archivo');

const fileSizeRule = (v) => {
  const f = pickFile(v);
  if (!f) return true;
  const ok = f.size <= 1024 * 1024 * 1024; // 1 GB
  return ok || 'El archivo supera el tamaño máximo (1 GB).';
};

const fileExtRule = (v) => {
  const f = pickFile(v);
  if (!f) return true;
  const ext = f.name?.split('.').pop()?.toLowerCase();
  return ['rar', 'zip', '7z'].includes(ext) || 'Formato inválido. Use .rar, .zip o .7z';
};

// --- Descarga de backup ---
const onGenerateBackup = async () => {
  try {
    loadingBackup.value = true;

    const resp = await RespaldosInformacionService.createBackup();
    const data = resp?.data ?? resp;

    const url = data?.urlArchivoComprimido;
    //console.log(url)
    if (!url) throw new Error('No se recibió la URL del backup');

    // nombre a partir de la URL
    const filename = url.split('/').pop() || 'backup.zip';

    // dispara la descarga
    const a = document.createElement('a');
    a.href = url;
    a.download = filename; // algunos navegadores lo ignoran si es cross-origin
    document.body.appendChild(a);
    a.click();
    a.remove();
  } catch (e) {
    console.error(e);
    // mostrar snackbar si querés
  } finally {
    loadingBackup.value = false;
  }
};

// --- Restauración desde archivo ---
const onRestore = async () => {
  const f = pickFile(file.value);
  if (!f) return;

  try {
    loadingRestore.value = true;

    await RespaldosInformacionService.restoreBackup({
      file: f,
      onUploadProgress: (e) => {
        // opcional: barra de progreso con e.loaded / e.total
      },
    });

    notify('Restauración realizada correctamente.');
    file.value = null;
  } catch (err) {
    console.error(err);
    notify(parseApiError(err) || 'No se pudo restaurar el backup', true);
  } finally {
    loadingRestore.value = false;
  }
};

// Utilidad para mensajes de error
function parseApiError(e) {
  return (
    e?.response?.data?.title ||
    e?.response?.data?.message ||
    e?.response?.data?.detail ||
    e?.message
  );
}
</script>
