<script setup lang="ts">
import { onMounted } from 'vue';
import { loginUsuarioStore } from './features/auth/hooks/useLoginStore';
import Header from './shared/components/header.vue';

const authStore = loginUsuarioStore();

onMounted(async () => {
  await authStore.verificarAuth();
})
</script>

<template>
  <main class=" min-h-screen bg-whiteSmoke selection:bg-pumpink selection:text-whiteSmoke">
    <RouterView />
  </main>
</template>

<style scoped></style>
