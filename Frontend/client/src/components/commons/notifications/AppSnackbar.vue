<template>
   
  <v-snackbar 
    :model-value="modelValue"
    :show="modelValue"
    :color="color"
    timeout="3000"
    location="top right"

    class="custom-snackbar"
    style="font-weight: bold;"
    @update:modelValue="$emit('update:modelValue', $event)"
  >


    <template v-slot:default>
      <v-row>
        <v-col>
          {{ message }}
        </v-col>
      </v-row>
      <v-row justify="end">
        <v-btn
          color="white"
          variant="text"
          @click="$emit('update:modelValue', false)"
        >
          Cerrar
        </v-btn>
      </v-row>
    </template>
  </v-snackbar>
</template>

<script>
export default {
  props: {
    modelValue: Boolean, // <- Prop para v-model
    message: String,
    color: {
      type: String,
      default: "success"
    }
  }
};
</script>

<style>
/* .custom-snackbar {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
} */
</style>