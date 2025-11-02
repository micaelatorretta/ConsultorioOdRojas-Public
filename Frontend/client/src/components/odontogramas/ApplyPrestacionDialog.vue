<template>
  <v-dialog
    :model-value="modelValue"
    @update:model-value="$emit('update:modelValue', $event)"
    max-width="760"
  >
    <v-card>
      <v-card-title class="text-h6">
        {{ titulo }}
      </v-card-title>

      <v-card-text class="d-flex flex-column ga-4">

       <!-- Última prestación aplicada -->
        <v-alert v-if="current" type="info" variant="tonal" density="comfortable">
          <div class="text-caption">
            Última prestación aplicada:
            <strong>{{ nombrePrestacionActual }}</strong>
            • <span v-if="nombreObraSocialActual">{{ nombreObraSocialActual }}</span>
            • <span v-if="fechaActual">{{ fechaActual }}</span>
          </div>
        </v-alert>

        <!-- Fila de selectores (OS + Prestación) -->
        <div class="selectors-row d-flex flex-wrap ga-3">
          <div class="flex-1 min-w-0">
            <ObraSocialSelector
              v-model="obraSocial"
              label="Obra social"
            />
          </div>

          <div class="flex-1 min-w-0">
            <NomencladorSelector
              v-model="prestacion"
              label="Prestación"
              return-object
              :obra-social-id="obraSocial?.id || obraSocial"
              :disabled="!obraSocial"
              @loaded="onNomencladoresLoaded"
            />
          </div>
        </div>

        <!-- Color + Observación (resto del formulario) -->
        <div>
          <div class="text-caption mb-2">Color</div>
          <v-color-picker
            v-model="colorHex"
            mode="hexa"
            hide-inputs
            elevation="0"
            @update:model-value="_userChangedColor = true"
          />
        </div>

        <!-- <v-textarea v-model="observacion" label="Observación" rows="2" auto-grow /> -->
      </v-card-text>


      <v-card-actions>
        <v-spacer />
        <v-btn variant="text" @click="$emit('update:modelValue', false)">Cancelar</v-btn>

        <v-btn
          v-if="hasCurrent"
          variant="tonal"
          color="error"
          @click="onRemove"
        >
          Eliminar última
        </v-btn>

        <v-btn color="primary" :disabled="!prestacionId" @click="onSave">
          {{ hasCurrent ? 'Actualizar' : 'Aplicar' }}
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
/* eslint-disable */
import ObraSocialSelector from '../obras-sociales/ObraSocialSelector.vue';
import NomencladorSelector from '../nomencladores/NomencladorSelector.vue';

export default {
  components: { ObraSocialSelector, NomencladorSelector },
  props: {
    modelValue: { type: Boolean, default: false },
    numeroFdi:   { type: Number, required: true },
    cara:        { type: String, required: true },
    current:     { type: Object, default: null },
    odontogramaId: { type: Number, required: true }
  },
  emits: ['update:modelValue','save','remove'],
  data() {
    return {
      obraSocial: null,       // ← objeto o id
      prestacion: null,       // ← objeto completo
      colorHex: '#2196f3',
      observacion: '',
      _inicializado: false,
      _userChangedColor: false
    };
  },
  computed: {
    titulo() {
      return `${this.hasCurrent ? 'Editar' : 'Aplicar'} prestación • ${this.numeroFdi} – ${this.cara}`;
    },
    nombrePrestacionActual() {
      if (!this.hasCurrent) return '—';
      const n = this.current?.nomenclador ?? this.current?.Nomenclador;
      return n?.descripcion ?? n?.Descripcion ?? '—';
    },
    nombreObraSocialActual() {
      if (!this.hasCurrent) return '';
      const nom = this.current?.nomenclador ?? this.current?.Nomenclador;
      const os  = nom?.obraSocial ?? nom?.ObraSocial;
      return os?.nombre ?? os?.Nombre ?? '';
    },
    fechaActual() {
      const f =
        this.current?.fechaPrestacion ||
        this.current?.FechaPrestacion ||
        this.current?.createdDate ||
        this.current?.CreatedDate;
      return f ? new Date(f).toLocaleString() : '';
    },
    hasCurrent() { return !!(this.current && (this.current.id || this.current.Id)); },
    prestacionId() { return this.prestacion?.id ?? this.prestacion?.Id ?? null; }
  },
  watch: {
    modelValue(v) { if (v) this.prefill(); else this._inicializado = false; },
    // Si cambia la OS, limpiamos la prestación (y el selector recarga solo)
    obraSocial() { this.prestacion = null; },
    prestacion(nueva) {
      const sug = nueva?.colorHexSugerido || nueva?.ColorHexSugerido || null;
      if (sug && !this._userChangedColor) this.colorHex = sug;
    }
  },
  methods: {
   prefill() {
      if (this._inicializado) return;

      // reset limpio SIEMPRE
      this._userChangedColor = false;
      this.obraSocial  = null;
      this.prestacion  = null;
      this.colorHex    = '#2196f3';
      this.observacion = '';

      if (this.hasCurrent) {
        const nom = this.current?.nomenclador ?? this.current?.Nomenclador ?? null;
        this.prestacion = nom ?? null;

        this.colorHex =
          this.current?.colorHexadecimal ??
          this.current?.ColorHexadecimal ??
          nom?.colorHexSugerido ??
          nom?.ColorHexSugerido ??
          this.colorHex;

        this.observacion =
          this.current?.observacion ??
          this.current?.Observacion ??
          '';

        const os = nom?.obraSocial ?? nom?.ObraSocial ?? null;
        this.obraSocial = os?.id ?? os ?? null;
      }

      this._inicializado = true;
    },
    hasCurrent() {
      const c = this.current ?? null;
      const id =
        c?.id ??
        c?.Id ??
        c?.nomencladorId ??
        c?.NomencladorId ??
        c?.nomenclador?.id ??
        c?.Nomenclador?.Id;
      return !!id;
    },

    onNomencladoresLoaded() {
      // opcional: hook cuando el selector terminó de cargar
    },
    normalizeCara(c) { return c === 'P' ? 'L' : c; },
    onSave() {
      this.$emit('save', {
        prestacionId: this.prestacionId,  // ← solo el id hacia afuera
        colorHex: this.colorHex,
        observacion: (this.observacion || '').trim() || null
      });
      this._userChangedColor = true;
    },
    onRemove() {
      if (!this.hasCurrent) return;
      this.$emit('remove', {
        numeroPiezaDental: this.numeroFdi,
        caraDental: this.normalizeCara(this.cara),
        odontogramaId: this.odontogramaId
      });
    }
  }
};
</script>
<style>
.selectors-row > .flex-1 {
  flex: 1 1 0;      /* que repartan el ancho por igual */
}
.min-w-0 { min-width: 0; } /* evita overflow del v-autocomplete */

@media (max-width: 720px) {
  .selectors-row { flex-direction: column; }
}

</style>