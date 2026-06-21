<script setup lang="ts">
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore'
import Tarefa from '@/features/tarefas/components/tarefa.vue'
import { useTarefaStore } from '@/features/tarefas/hooks/useTarefaStore'
import { onMounted } from 'vue'

const auth = loginUsuarioStore()
const tarefaStore = useTarefaStore();

onMounted(async () => {
    await auth.verificarAuth()
    await tarefaStore.listarTarefas();
})
</script>


<template>
    <section class="flex flex-wrap justify-center items-center gap-4 m-auto w-full p-4 ">
        <Tarefa v-for="tarefa in tarefaStore.tarefas" :tarefa="tarefa" :key="tarefa.id"/>
    </section>
</template>
