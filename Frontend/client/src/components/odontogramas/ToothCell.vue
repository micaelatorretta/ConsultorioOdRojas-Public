<template>
  <div class="relative d-inline-block tooth">
    <svg viewBox="0 0 100 120" class="tooth-svg">
      <!-- contorno -->
      <rect x="5" y="5" width="90" height="110" rx="8"
            :fill="presente ? 'white' : '#f4f4f4'"
            :stroke="presente ? 'currentColor' : '#bdbdbd'"
            :opacity="presente ? 1 : 0.7" />

      <!-- V (superior) -->
      <rect x="35" y="20" width="30" height="18"
            :fill="colorFor('V')" stroke="currentColor"
            @click="$emit('click-cara','V')" />

      <!-- M (izquierda) -->
      <rect x="15" y="46" width="16" height="28"
            :fill="colorFor('M')" stroke="currentColor"
            @click="$emit('click-cara','M')" />

      <!-- O (centro) -->
      <rect x="35" y="46" width="30" height="28"
            :fill="colorFor('O')" stroke="currentColor"
            @click="$emit('click-cara','O')" />

      <!-- D (derecha) -->
      <rect x="69" y="46" width="16" height="28"
            :fill="colorFor('D')" stroke="currentColor"
            @click="$emit('click-cara','D')" />

      <!-- Palatina / Lingual -->
      <rect x="35" y="78" width="30" height="18"
            :fill="colorFor(internaCode)" stroke="currentColor"
            @click="$emit('click-cara', internaCode)" />

      <!-- tachado si no está presente -->
      <g v-if="!presente">
        <line x1="12" y1="12" x2="88" y2="108" stroke="#c62828" stroke-width="2" stroke-dasharray="6 4"/>
        <line x1="88" y1="12" x2="12" y2="108" stroke="#c62828" stroke-width="2" stroke-dasharray="6 4"/>
      </g>

      <!-- número FDI -->
      <text x="50" y="110" text-anchor="middle" class="tooth-number">{{ numeroFdi }}</text>
    </svg>
  </div>
</template>

<script>
/* eslint-disable */
export default {
  props: {
    numeroFdi: { type: Number, required: true },
    presente:  { type: Boolean, default: true },
    caras:     { type: Array, default: () => [] }
  },
  computed: {
    internaCode() {
      const q = Math.floor((this.numeroFdi || 10) / 10);
      return [1,2,5,6].includes(q) ? 'P' : 'L';
    }
  },
  methods: {
    colorFor(code) {
      const m = this.caras.find(c => (c?.cara || '').toUpperCase() === code);
      return m?.colorHex || 'transparent';
    }
  }
};
</script>

<style scoped>
.tooth-svg {
  width: 60px;   /* antes 80px */
  height: 72px;  /* antes 96px */
  transform: scale(0.85); /* compacta todo el SVG */
  transform-origin: center;
}

.tooth-number {
  font-size: 9px; /* antes 12px */
  fill: #333;
}

svg rect { cursor: pointer; }
</style>
