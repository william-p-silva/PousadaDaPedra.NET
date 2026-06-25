<script setup lang="ts">
import { onMounted } from 'vue';
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore';
import SideBar from '@/shared/components/header/sideBar.vue';
import { useHeaderStore } from '@/shared/hooks/header/useHeaderStore';
import { ArrowLeft, Menu } from '@lucide/vue';
import PageNav from '@/shared/components/header/pageNav.vue';

const authStore = loginUsuarioStore();
const useHeader = useHeaderStore();

onMounted(async () => {
  await authStore.verificarAuth();
});
</script>

<template>
  <div class="h-full flex items-center bg-charcoal">
    <SideBar class="transition-all duration-500" />

    <!-- Botão hambúrguer — só aparece no mobile -->
    <button
      class="fixed top-4 left-4 z-50 md:hidden w-10 h-10 bg-nights rounded-xl flex items-center justify-center text-whiteSmoke cursor-pointer"
      @click="useHeader.isMobileOpen = !useHeader.isMobileOpen">
      <Menu />
    </button>

    <!-- Botão ArrowLeft — só aparece no desktop -->
    <div
      class="hidden md:flex fixed py-2 z-55 h-10 w-8 top-1/4 transition-all duration-600 bg-nights rounded-r-4xl text-charcoal cursor-pointer"
      :class="[
        useHeader.hiddenSide ? '' : useHeader.hiddenIconSide ? 'ml-0' : 'ml-72',
        useHeader.hiddenIconSide ? '' : 'ml-20',
      ]"
      @click="useHeader.hiddenIconSide = !useHeader.hiddenIconSide; useHeader.hiddenSide = !useHeader.hiddenSide">
      <ArrowLeft class="transition-all duration-500" :class="useHeader.hiddenIconSide ? 'rotate-180' : ''" />
    </div>
  </div>

  <!-- Conteúdo principal -->
  <section :class="[
    'flex-1 transition-all duration-300 ease-in-out',
    // Mobile: sem margem (sidebar é overlay)
    'ml-0',
    // Desktop: margem conforme estado da sidebar
    useHeader.hiddenSide && useHeader.hiddenIconSide
      ? 'md:ml-10'
      : useHeader.hiddenIconSide
        ? 'md:ml-10'
        : useHeader.hiddenSide
          ? 'md:ml-20'
          : 'md:ml-72',
  ]">
    <PageNav />
    <RouterView />
  </section>
</template>