<script setup lang="ts">
import Loading from '@/shared/components/loading.vue';
import { useTarefaStore } from '../hooks/useTarefaStore';
import { Status, type TarefaResponse } from '../types/tarefaType';
import { formatarData, formatarDificuldade, formatarPrioridade, formatarStatus } from '@/shared/hooks/formatar';
import { tarefaStyleHook } from '../hooks/useTarefaStyleStore';
import BotaoAcao from './botaoAcao.vue';


const tarefaStore = useTarefaStore();
const tarefaStyle = tarefaStyleHook();

const props = defineProps<{
    tarefa: TarefaResponse,
}>();

</script>

<template>

    <main class="p-4 bg-white flex flex-col justify-between w-66 min-h-112.5 text-sm rounded-lg shadow-md shadow-black/40 text-zinc-700">
        <header class="flex justify-between mb-4 items-center">
            <h1 class="font-bold text-xl text-gray-800 w-43 wrap-break-word ">{{ tarefa.titulo ?? "Titulo" }}</h1>
            <span :class="[tarefaStyle.getStyleDificuldade(tarefa.dificuldade), 'px-2 py-1 rounded-md font-semibold']">{{ formatarDificuldade(tarefa.dificuldade) }}</span>
        </header>
        <section class="flex flex-col gap-4">
            <article class="bg-zinc-100/90 p-2 rounded-md font-normal text-sm line-clamp-3 text-zinc-500">
                <p>{{ tarefa.descricao ?? "Descrição" }}</p>
            </article>
            <article class=" flex flex-col gap-2">
                <div class="flex justify-between items-center">
                    <h3 class="tracking-wider text-slate-500 text-[12px]">PRIORIDADE</h3>
                    <span :class="['font-semibold text-[12px]', tarefaStyle.getStylePrioridade(tarefa.prioridade)]">{{ formatarPrioridade(tarefa.prioridade) }}</span>
                </div>
                <div class="flex justify-between items-center">
                    <h3 class="tracking-wider text-slate-500 text-[12px]">STATUS</h3>
                    <span :class="[tarefaStyle.getStyleStatus(tarefa.status), 'font-semibold text-[12px] px-1 rounded-sm py-0.5']">{{ formatarStatus(tarefa.status) }}</span>
                </div>
            </article>
            <article class="flex flex-col gap-1">
                <p class="tracking-wider text-slate-500 text-[12px] uppercase">Responsáveis</p>
                <div class="bg-zinc-100/90 p-1 rounded-md font-bold text-[12px]">
                    <p v-for="responsavel of tarefa.responsaveis">{{ responsavel.email }}</p>
                </div>
            </article>
            <article class="flex flex-col justify-center items-center p-2 gap-4">
                <BotaoAcao :status="tarefa.status" :id="tarefa.id" />
            </article>
        </section>
        <footer class="flex flex-col justify-between border-t border-zinc-200 pt-4">
            <div class="flex justify-between">
                <div class="flex flex-col">
                    <p class="tracking-wider text-slate-500 text-[12px] uppercase">Início:</p>
                    <p class="text-zinc-800 ">{{ formatarData(tarefa.dataInicio) }}</p>
                </div>
                <div v-if="tarefa.status !== Status.Finalizada">
                    <p class="tracking-wider text-slate-500 text-[12px] uppercase text-right">Prazo:</p>
                    <p class="text-red-600 ">{{ formatarData(tarefa.prazo) }}</p>
                </div>
                <div v-else >
                        <p class="tracking-wider text-slate-500 text-[12px] uppercase text-right">Termino:</p>
                        <p class="text-red-600 ">{{ formatarData(tarefa.dataTermino) }}</p>
                </div>
            </div>
        </footer>
    </main>
</template>