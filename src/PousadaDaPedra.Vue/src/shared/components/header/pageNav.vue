<script setup lang="ts">
import { useHeaderStore } from '@/shared/hooks/header/useHeaderStore';
import { ArrowRight, Bell, ChevronRight, Plus, Search } from '@lucide/vue';
import { useRoute } from 'vue-router';


const useHeader = useHeaderStore();

const route = useRoute();


</script>



<template>
    <section
        class="border-b border-charcoal/30 shadow-[inset_0_-4px_4px_-4px] shadow-charcoal/30 p-3 -ml-10 justify-between  lg:flex hidden">
        <article class="flex items-center gap-2 ">
            <div class="flex gap-2 items-center">
                <h2 class="ml-10 text-lg text-charcoal/60 font-medium">
                    Pousada da Pedra
                </h2>
                <ChevronRight class="text-charcoal/60" />
            </div>
            <h2 class="text-lg text-charcoal font-medium">{{ route.name }}</h2>
        </article>
        <article class="flex items-center gap-2  ">
            <div class="inpt inpt-primary p-2 w-65">
                <Search class="text-charcoal/50 " />
                <input type="text" placeholder="Buscar Tarefas, Pessoas ... " class=" outline-none">
            </div>
            <div class="rounded-2xl bg-white flex justify-center items-center p-2.5 border border-charcoal/30">
                <Bell class="h-5 text-charcoal/80" />
            </div>
            <RouterLink to="/criar/tarefa" class="btn btn-primary p-2 flex items-center" ">
                <div  class="flex items-center">
                <Plus class="h-4" /> Tarefa
                </div>
            </RouterLink>
        </article>
    </section>
    <section
        class="border-b border-charcoal/30 shadow-[inset_0_-4px_4px_-4px] shadow-charcoal/30 p-3 justify-center  flex lg:hidden w-full ">

        <article class="flex items-center gap-2 flex-col sm:flex-row ">
            <div class="flex gap-2">
                <div class="inpt inpt-primary p-2 w-65">
                    <Search class="text-charcoal/50 " />
                    <input type="text" placeholder="Buscar Tarefas, Pessoas ... " class=" outline-none">
                </div>
                <div class="rounded-2xl bg-white flex justify-center items-center p-2.5 border border-charcoal/30">
                    <Bell class="h-5 text-charcoal/80" />
                </div>
            </div>
            <RouterLink to="/criar/tarefa" class="btn btn-primary p-2 flex items-center" ">
                <div  class="flex items-center">
                <Plus class="h-4" /> Tarefa
                </div>
            </RouterLink>
        </article>
    </section>
</template>