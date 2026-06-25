<script setup lang="ts">
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore'
import Tarefa from '@/features/tarefas/components/tarefa.vue'
import { useTarefaStore } from '@/features/tarefas/hooks/useTarefaStore'
import Loading from '@/shared/components/loading.vue'
import { onMounted } from 'vue'

const auth = loginUsuarioStore()
const tarefaStore = useTarefaStore();

onMounted(async () => {
    await auth.verificarAuth()
    await tarefaStore.listarTarefas();
})
</script>


<template>
    <section class="flex flex-wrap transition-all duration-300 ease-in-out justify-center items-center gap-4 m-auto w-full p-4 ">
        <div v-if="tarefaStore.isLoading || tarefaStore.tarefas === null"
            class="h-full flex justify-center items-center">
            <Loading text="Buscando Tarefas" />
        </div>
        <Tarefa v-else v-for="tarefa in tarefaStore.tarefas" :tarefa="tarefa" :key="tarefa.id" 
        class="transition-all duration-300 ease-in-out"
        />
    </section>
</template>
