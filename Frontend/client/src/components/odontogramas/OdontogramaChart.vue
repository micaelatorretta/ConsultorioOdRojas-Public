<template>
  <div v-if="piezasSafe.length" class="odo">

    <!-- PERMANENTES SUPERIORES (izq / der) -->
    <section class="grid2">
      <div class="zone">
        <div class="labels top">
          <span v-for="n in [18,17,16,15,14,13,12,11]" :key="'lab'+n">{{ n }}</span>
        </div>
        <div class="teeth teeth8">
          <ToothCell v-for="p in fila([18,17,16,15,14,13,12,11])" :key="k(p)" v-bind="bind(p)" />
        </div>
      </div>

      <div class="zone">
        <div class="labels top">
          <span v-for="n in [21,22,23,24,25,26,27,28]" :key="'lab'+n">{{ n }}</span>
        </div>
        <div class="teeth teeth8">
          <ToothCell v-for="p in fila([21,22,23,24,25,26,27,28])" :key="k(p)" v-bind="bind(p)" />
        </div>
      </div>
    </section>

    <div class="midline"></div>

    <!-- TEMPORALES (centro) -->
    <section class="grid2">
      <div class="zone">
        <div class="labels top">
          <span v-for="n in [55,54,53,52,51]" :key="'lab'+n">{{ n }}</span>
        </div>
        <div class="teeth teeth5 temps">
          <ToothCell v-for="p in fila([55,54,53,52,51])" :key="k(p)" v-bind="bind(p)" />
        </div>
      </div>

      <div class="zone">
        <div class="labels top">
          <span v-for="n in [61,62,63,64,65]" :key="'lab'+n">{{ n }}</span>
        </div>
        <div class="teeth teeth5 temps">
          <ToothCell v-for="p in fila([61,62,63,64,65])" :key="k(p)" v-bind="bind(p)" />
        </div>
      </div>

      <div class="zone">
        <div class="teeth teeth5 temps">
          <ToothCell v-for="p in fila([85,84,83,82,81])" :key="k(p)" v-bind="bind(p)" />
        </div>
        <div class="labels bottom">
          <span v-for="n in [85,84,83,82,81]" :key="'lab'+n">{{ n }}</span>
        </div>
      </div>

      <div class="zone">
        <div class="teeth teeth5 temps">
          <ToothCell v-for="p in fila([71,72,73,74,75])" :key="k(p)" v-bind="bind(p)" />
        </div>
        <div class="labels bottom">
          <span v-for="n in [71,72,73,74,75]" :key="'lab'+n">{{ n }}</span>
        </div>
      </div>
    </section>

    <div class="midline"></div>

    <!-- PERMANENTES INFERIORES (izq / der) -->
    <section class="grid2">
      <div class="zone">
        <div class="teeth teeth8">
          <ToothCell v-for="p in fila([48,47,46,45,44,43,42,41])" :key="k(p)" v-bind="bind(p)" />
        </div>
        <div class="labels bottom">
          <span v-for="n in [48,47,46,45,44,43,42,41]" :key="'lab'+n">{{ n }}</span>
        </div>
      </div>

      <div class="zone">
        <div class="teeth teeth8">
          <ToothCell v-for="p in fila([31,32,33,34,35,36,37,38])" :key="k(p)" v-bind="bind(p)" />
        </div>
        <div class="labels bottom">
          <span v-for="n in [31,32,33,34,35,36,37,38]" :key="'lab'+n">{{ n }}</span>
        </div>
      </div>
    </section>

  </div>

  <v-alert v-else type="info" variant="tonal">No hay piezas para renderizar.</v-alert>
</template>

<script>
/* eslint-disable */
import ToothCell from './ToothCell.vue';

export default {
  components: { ToothCell },
  props: { piezas: { type: Array, default: () => [] } },
  emits: ['click-cara','toggle-presencia','context-cara'],
  computed: {
    piezasSafe() {
      const un = x => (x && x.$values) ? x.$values : (Array.isArray(x) ? x : []);
      return un(this.piezas).map(p => ({
        ...p,
        piezaDental: p.piezaDental || p.PiezaDental || {},
        // NO traduzco acá; lo hago en bind() para dejar el crudo disponible
        carasDentales: un(p.carasDentales || p.caras || p.Caras),
        presente: typeof p.presente === 'boolean' ? p.presente : true
      }));
    }
  },
  methods: {
    // --- helpers de traducción ---
    caraEnumToLetter(code) {
      // tu enum: 0=Mesial,1=Distal,2=Vestibular,3=Palatina,4=Oclusal,5=Lingual
      const map = { 0:'M', 1:'D', 2:'V', 3:'P', 4:'O', 5:'L' };
      return map[Number(code)] || (typeof code === 'string' ? code.toUpperCase() : 'O');
    },
    caraFromAny(c) {
      // si ya viene letra, úsala
      const s = (c?.cara || c?.Cara || '').toString().toUpperCase();
      if (s) return s;
      // si viene enum dentro de CaraDental
      const code = c?.caraDental?.caraDentaria ?? c?.CaraDental?.CaraDentaria;
      return this.caraEnumToLetter(code);
    },
    colorFromAny(c) {
      return (
        c?.colorHex || c?.ColorHex ||
        c?.colorHexadecimal || c?.ColorHexadecimal ||
        c?.nomenclador?.colorHexSugerido ||
        c?.Nomenclador?.ColorHexSugerido ||
        null
      );
    },

    fila(nums) {
      return nums
        .map(n => this.piezasSafe.find(p => p?.piezaDental?.numeroPieza === n))
        .filter(Boolean);
    },
    k(p){ return p?.id || p?.piezaDental?.numeroPieza; },

    // ==> AQUÍ la conversión a lo que espera ToothCell
    bind(p){
      const crudo = p?.carasDentales || p?.caras || [];
      const caras = crudo.map(c => ({
        cara: this.caraFromAny(c),
        colorHex: this.colorFromAny(c)
      }));
      return {
        numeroFdi: p?.piezaDental?.numeroPieza,
        presente: !!p?.presente,
        caras,
        onClickCara: (c) => this.$emit('click-cara', p?.piezaDental?.numeroPieza, c),
        onTogglePresencia: () => this.$emit('toggle-presencia', p),
        onContextCara: (c) => this.$emit('context-cara', p, c)
      };
    }
  }
};
</script>


<style scoped>
/* ===== TAMAÑOS COMPACTOS (todo cabe en pantalla) ===== */
.odo {
  --cell: 52px;     /* ancho de cada pieza */
  --gap:  10px;     /* separación entre piezas */
  --lab:  12px;     /* tamaño número FDI */
  max-width: 1280px;
  margin: 0 auto;
  padding: 8px 6px;
  display: grid;
  row-gap: 18px;
}

/* 2 columnas (izquierda/derecha) con un gap central consistente */
.grid2 {
  display: grid;
  grid-template-columns: 1fr 1fr;
  column-gap: 80px;           /* “línea media” */
  row-gap: 16px;
  justify-items: start;
}

/* cada zona (labels + dientes o dientes + labels) */
.zone { display: grid; row-gap: 6px; }

/* filas de dientes (8 o 5) */
.teeth { display: grid; column-gap: var(--gap); }
.teeth8 { grid-template-columns: repeat(8, var(--cell)); }
.teeth5 { grid-template-columns: repeat(5, var(--cell)); }

/* números FDI arriba/abajo */
.labels {
  display: grid;
  grid-auto-flow: column;
  grid-auto-columns: var(--cell);
  column-gap: var(--gap);
  font-size: var(--lab);
  color: #7a8b97;
  user-select: none;
  line-height: 1;
}
.labels.top    { justify-items: center; }
.labels.bottom { justify-items: center; }

/* temporales ligeramente más chicos */
.temps :deep(.tooth-svg) { transform: scale(.90); }

/* línea separadora horizontal entre grupos */
.midline {
  height: 1px; background: #c5c7c9; opacity: .7; margin: 2px 0 6px;
}

/* Si quieres aún más compacto en laptops pequeñas */
@media (max-width: 1366px) {
  .odo { --cell: 46px; --gap: 8px; --lab: 11px; }
  .grid2 { column-gap: 64px; }
}
</style>
