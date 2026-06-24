<script setup lang="ts">
import Cadastro from '@/features/auth/components/cadastro.vue';
import Login from '@/features/auth/components/login.vue';
import { useAuthStore } from '@/features/auth/hooks/authStore';
import { computed } from 'vue';

const authStore = useAuthStore();

const isCadastro = computed(() => authStore.isCadastro);
</script>

<template>
  <section class="relative w-full h-screen overflow-hidden flex">

    <!-- Painel escuro: desliza da esquerda para a direita quando muda para cadastro -->
    <div
      :class="[
        'absolute top-0 md:w-1/2 h-full bg-nights z-0',
        'transition-transform duration-700 ease-[cubic-bezier(0.77,0,0.175,1)]',
        isCadastro ? 'translate-x-full' : 'translate-x-0'
      ]"
    />

    <!-- Painel de formulário: desliza por CIMA do painel escuro (z-10) -->
    <div
      :class="[
        'absolute top-0 w-full md:w-1/2 h-full bg-whiteSmoke z-10',
        'transition-transform duration-700 ease-[cubic-bezier(0.77,0,0.175,1)]',
        isCadastro ? 'md:translate-x-0 md:left-0' : 'md:translate-x-full md:left-0'
      ]"
    >
      <!-- Transition do Vue para trocar entre Login e Cadastro sem flash -->
      <Transition name="form-fade" mode="out-in">
        <Cadastro v-if="isCadastro" :rota="false" :key="'cadastro'" />
        <Login v-else :rota="false" :key="'login'" />
      </Transition>
    </div>

  </section>
</template>

<style scoped>
/* Fade suave ao trocar entre Login e Cadastro */
.form-fade-enter-active,
.form-fade-leave-active {
  transition: opacity 200ms ease, transform 200ms ease;
}
.form-fade-enter-from {
  opacity: 0;
  transform: translateY(8px);
}
.form-fade-leave-to {
  opacity: 0;
  transform: translateY(-8px);
}
</style>