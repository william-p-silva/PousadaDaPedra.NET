<script setup lang="ts">
import Cadastro from '@/features/auth/components/cadastro.vue';
import Navegar from '@/features/auth/components/navegar.vue';

</script>


<template>
    <section class="flex flex-row-reverse">
        <article class="md:w-full h-screen bg-nights" />
        <div class="h-screen w-screen">
            <Cadastro :rota="true" />
        </div>
    </section>

</template>