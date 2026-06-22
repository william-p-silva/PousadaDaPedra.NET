<script setup lang="ts">
import Select from '@/shared/components/inputs/select.vue';
import { useCriarTarefaStore } from '../../hooks/criar/useCriarStore';
import { computed, onMounted } from 'vue';

const options = useCriarTarefaStore();

async function submit() {
    console.log(options.tarefa)
}

onMounted(async () => {
    await options.listarUsuarios();
});

const opcoesUsuarios = computed(() =>
    options.usuarios.map(usuario => ({
        value: usuario.id,
        label: usuario.email
    }))
);

</script>

<template>
    <form action="" class="justify-center flex flex-col items-center ">
        <div class="flex flex-col items-center">
            <h1 class="text-3xl text-center font-bold text-zinc-700">
                Cadastre uma Nova Tarefa
            </h1>
        </div>
        <article class="flex flex-col gap-4 pt-6 ">
            <div>
                <span>Titulo: </span>
                <input type="text" placeholder="Titulo Teste" class="w-full p-1 rounded-lg border border-zinc-700" v-model="options.tarefa.titulo">
            </div>
            <div class="">
                <span>Descrição:</span>
                <textarea 
                placeholder="Descrição da tarefa" 
                class="w-full p-1 rounded-lg border border-zinc-700"
                v-model="options.tarefa.descricao" ></textarea>
            </div>

            <div class="">
                <span>Prioridade:</span>
                <Select :options="options.prioridades" v-model="options.tarefa.prioridade" ></Select>
            </div>

            <div class="">
                <span class="">Dificuldade:</span>
                <Select :options="options.dificuldades" v-model="options.tarefa.dificuldade" ></Select>
            </div>


            <div class="">
                <span>Responsaveis:</span>
                <Select 
                :options="opcoesUsuarios" 
                v-model="options.tarefa.responsaveis"></Select>
            </div>
        </article>
        <div class="mt-6">
            <input @click="submit" value="cadastrar" class="btn btn-primary">
        </div>
    </form>
</template>