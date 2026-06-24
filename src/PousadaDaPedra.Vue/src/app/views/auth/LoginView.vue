<script setup lang="ts">
import Login from '@/features/auth/components/login.vue';

</script>




<template>
        <section class="flex ">
                <article class="md:w-full h-screen bg-nights" />
                <div class="h-screen w-screen">
                        <Login :rota="true" />
                </div>
        </section>
</template>