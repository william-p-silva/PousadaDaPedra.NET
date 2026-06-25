<script setup lang="ts">
import { onMounted } from 'vue';
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore';
import SideBar from '@/shared/components/header/sideBar.vue';
import { useHeaderStore } from '@/shared/hooks/header/useHeaderStore';

const authStore = loginUsuarioStore();

const useHeader = useHeaderStore();

onMounted(async () => {
  await authStore.verificarAuth();
})


</script>

<template>


  <SideBar />

  <section :class="['flex-1 transition-all duration-300 ease-in-out', useHeader.hiddenSide ? 'ml-20' : 'ml-72']">

    <RouterView />

  </section>
</template>
