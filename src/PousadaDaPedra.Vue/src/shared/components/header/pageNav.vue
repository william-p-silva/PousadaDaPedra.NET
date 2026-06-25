<script setup lang="ts">
import { useHeaderStore } from '@/shared/hooks/header/useHeaderStore';
import { ArrowRight } from '@lucide/vue';
import { useRoute } from 'vue-router';


const useHeader = useHeaderStore();

const route = useRoute();


</script>



<template>
    <section class="flex border-b border-charcoal/30 shadow-[inset_0_-4px_4px_-4px] shadow-charcoal/30 p-5 -ml-10 justify-between">
        <article class="flex items-center gap-4">
            <h2 class="ml-10 text-lg text-charcoal/60 font-medium">
                Pousada da Pedra
            
            </h2>
            <ArrowRight class="text-charcoal/60" />
            <h2 class="text-lg text-charcoal font-medium">{{ route.name }}</h2>
        </article>
        <article>
            <input type="text" placeholder="oiiiiiiiiw">
        </article>
    </section>    
</template>