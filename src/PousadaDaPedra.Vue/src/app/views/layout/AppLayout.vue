<script setup lang="ts">
import { onMounted } from 'vue';
import Header from '@/shared/components/header.vue';
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore';

const authStore = loginUsuarioStore();

onMounted(async () => {
  await authStore.verificarAuth();
})
</script>

<template>
    <Header />
    <RouterView />
</template>

<style scoped></style>
