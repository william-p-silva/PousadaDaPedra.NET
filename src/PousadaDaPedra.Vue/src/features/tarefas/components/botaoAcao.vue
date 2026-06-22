<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { acaoBotaoTarefaStore } from '../hooks/useTarefaBotaoStore';
import type { Status } from '../types/tarefaType';

const props = defineProps<{
    status: Status,
    id: number,
}>();
const botao = acaoBotaoTarefaStore(props.status);
const prazo = ref('');

function executarAcao() {
    const dataPrazo = new Date(prazo.value);
    botao.action(props.id, dataPrazo);
}
</script>



<template>
    <button :class="[botao.style, 'btn btn-primary w-full']" @click="executarAcao">
        {{ botao.text }}
    </button>
    <div class="flex flex-col" v-if="botao.text !== 'Finalizar'">
        <span>Prazo para finalizar:</span>
        <input v-model="prazo" type="date" class="p-1 border border-zinc-400 rounded-lg cursor-pointer">
    </div>
</template>