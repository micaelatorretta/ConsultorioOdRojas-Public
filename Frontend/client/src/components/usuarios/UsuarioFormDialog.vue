<template>
  <!-- DIALOGO PARA CRUD DE USUARIOS -->
  <v-dialog v-model="dialogVisible" max-width="800">
    <v-card>
      <v-card-title>
        {{ localUsuario.id ? 'Editar Usuario' : 'Nuevo Usuario' }}
      </v-card-title>

        <div class="row">
          <div class="col-md-4">
              <v-card-text>
                <v-text-field
                  label="Nombre"
                  v-model="localUsuario.nombre"
                  :rules="[requiredRule, onlyLettersRule]"
                  :disabled="!enabled"
                />
              </v-card-text>
          </div>

          <div class="col-md-4">
             <v-card-text>
                <v-text-field
                  label="Apellido"
                  v-model="localUsuario.apellido"
                  :rules="[requiredRule, onlyLettersRule]"
                  :disabled="!enabled"
                />
              </v-card-text>
          </div>

          <div class="col-md-4">
             
              <v-card-text>
                <v-text-field
                  label="DNI"
                  v-model="localUsuario.dni"
                  :disabled="!enabled"
                  :rules="[dniLengthRule, dniNumericRule]"
                  maxlength="8"
                  counter
                />
              </v-card-text>
          </div>
        </div>

        <div class="row">
          <div class="col-md-4">
              <v-card-text>
                <v-text-field
                  label="Email"
                  v-model="localUsuario.email"
                  :disabled="!enabled"
                  :rules="[requiredRule]"
                />
              </v-card-text>

          </div>

            <div class="col-md-4">
              <v-card-text>
                <v-text-field
                  label="Username"
                  v-model="localUsuario.username"
                  :disabled="!enabled"
                  :rules="[requiredRule]"
                />
              </v-card-text>

          </div>

          <div class="col-md-4">
            <v-card-text>
              <v-select
                label="Rol"
                v-model="localUsuario.rol"
                :items="roles"
                item-title="text"
                item-value="value"
                :disabled="!enabled"
                clearable
              />
            </v-card-text>
          </div>
        </div>

        <div class="row">

          <div class="col-md-12">
            <!-- Cambio de contraseña -->
              <v-card-text>
                <v-switch
                  v-if="localUsuario.id"
                  v-model="changePassword"
                  :disabled="!enabled"
                  label="Cambiar contraseña"
                  hide-details
                  inset
                />
              </v-card-text>
          </div>
      </div>
      <template v-if="showPasswordFields">
        <div class="row">

           <div class="col-md-6">
              <v-card-text>
                <v-text-field
                  :type="showPw ? 'text' : 'password'"
                  label="Contraseña"
                  v-model="password"
                  :disabled="!enabled"
                  :append-inner-icon="showPw ? 'mdi-eye-off' : 'mdi-eye'"
                  @click:append-inner="showPw = !showPw"
                  :rules="passwordRules"
                  autocomplete="new-password"
                />
              </v-card-text>

          </div>

           <div class="col-md-6">
              <v-card-text>
                <v-text-field
                  :type="showPw2 ? 'text' : 'password'"
                  label="Confirmar contraseña"
                  v-model="password2"
                  :disabled="!enabled"
                  :append-inner-icon="showPw2 ? 'mdi-eye-off' : 'mdi-eye'"
                  @click:append-inner="showPw2 = !showPw2"
                  :rules="[confirmPasswordRule]"
                  autocomplete="new-password"
                />
              </v-card-text>
          </div>
          </div>
         </template>


      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn color="primary" @click="onSaveClick">Guardar</v-btn>
        <v-btn color="gray" @click="closeDialog">Cancelar</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { defineProps, defineEmits, computed, watch, ref, toRaw } from 'vue'
import { UsuarioRolOptions } from '@/constants/Users/UsuarioRol'

const roles = UsuarioRolOptions

const props = defineProps({
  modelValue: Boolean, // Controla visibilidad del diálogo
  usuario: Object,     // Usuario recibido desde el padre
  enabled: Boolean,    // Habilitar/deshabilitar inputs
  isAdding: Boolean    // Indica si es alta o edición
})

const emit = defineEmits(['update:modelValue', 'save', 'close'])

// Estado del diálogo
const dialogVisible = computed({
  get: () => props.modelValue,
  set: (value) => emit('update:modelValue', value)
})

// Estado local (copia para evitar mutar props)
const localUsuario = ref({ ...props.usuario })

// Sincroniza cambios cuando cambia el usuario recibido
watch(
  () => props.usuario,
  (newUsuario) => {
    localUsuario.value = { ...newUsuario }
    password.value = ''
    password2.value = ''
    changePassword.value = false
  },
  { deep: true }
)

// Estados de contraseña
const password = ref('')
const password2 = ref('')
const showPw = ref(false)
const showPw2 = ref(false)
const changePassword = ref(false)

// Mostrar campos de contraseña
const showPasswordFields = computed(() => {
  return props.isAdding || (localUsuario.value?.id && changePassword.value)
})

// Normaliza el rol antes de guardar
function normalizeRol(rol) {
  return typeof rol === 'object' && rol !== null ? rol.value : rol
}

// === GUARDAR ===
const onSaveClick = () => {
  // Validar contraseñas si aplica
  if (showPasswordFields.value) {
    //const pwdError = passwordRules.map(r => r(password.value)).find(res => res !== true)
    //const confError = confirmPasswordRule(password2.value)
   // if (pwdError !== true || confError !== true) return
  }

  const usuarioParaEnviar = {
    ...toRaw(localUsuario.value), // Limpia proxies
    rol: normalizeRol(localUsuario.value.rol)
  }

  if (showPasswordFields.value && password.value?.length) {
    usuarioParaEnviar.password = password.value
    usuarioParaEnviar.changePassword = true;
  }

  console.log('[Dialog] emit save ->', usuarioParaEnviar)
  emit('save', usuarioParaEnviar)
  //emit('update:modelValue', false) // Cierra el diálogo
}

// === CERRAR ===
const closeDialog = () => {
  emit('close', toRaw(localUsuario.value))
  emit('update:modelValue', false)
}

// =====================
// Reglas de validación
// =====================
const dniLengthRule = (value) =>
  value && value.length === 8 ? true : 'El DNI debe tener exactamente 8 dígitos'

const dniNumericRule = (value) =>
  /^\d+$/.test(value) ? true : 'El DNI solo debe contener números'

const requiredRule = (value) =>
  value?.trim().length > 0 ? true : 'Este campo es obligatorio'

const onlyLettersRule = (value) =>
  /^[A-Za-zÁÉÍÓÚáéíóúÑñ\s]+$/.test(value)
    ? true
    : 'Solo se permiten letras y espacios'

// Validaciones de contraseña
const passwordRules = [
  (v) => showPasswordFields.value
    ? (v?.length >= 8 || 'Mínimo 8 caracteres')
    : true,
  (v) => showPasswordFields.value
    ? (/[A-Za-z]/.test(v) || 'Debe incluir letras')
    : true,
  (v) => showPasswordFields.value
    ? (/\d/.test(v) || 'Debe incluir números')
    : true
]

const confirmPasswordRule = (v) => {
  if (!showPasswordFields.value) return true
  return v === password.value ? true : 'Las contraseñas no coinciden'
}
</script>
