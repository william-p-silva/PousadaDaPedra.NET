<script setup lang="ts">
import { onMounted } from 'vue';
import { loginUsuarioStore } from './features/auth/hooks/useLoginStore';

const authStore = loginUsuarioStore();

onMounted(async () => {
  await authStore.verificarAuth();
})
</script>

<template>
  <main class="min-h-screen bg-zinc-100">
    <RouterView />
  </main>
</template>

<style scoped></style>
