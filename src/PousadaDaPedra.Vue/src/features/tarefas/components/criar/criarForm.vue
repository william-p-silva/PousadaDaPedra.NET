<script setup lang="ts">
import Select from '@/shared/components/inputs/select.vue';
import { useCriarTarefaStore } from '../../hooks/criar/useCriarStore';
import { computed, onMounted } from 'vue';
import MultiSelect from '@/shared/components/inputs/multiSelect.vue';
import Loading from '@/shared/components/loading.vue';

const cadastro = useCriarTarefaStore();

async function submit() {
    const result = await cadastro.handleSubmite();
    console.log("teste1")

    if (result) {
        alert("Cadastro realizado")
    }
}

onMounted(async () => {
    await cadastro.listarUsuarios();
});

const opcoesUsuarios = computed(() =>
    cadastro.usuarios.map(usuario => ({
        value: usuario.id,
        label: usuario.email
    }))
);

</script>

<template>
    <div v-if="cadastro.isLoadind">
        <Loading />
    </div>
    
    <form v-else @submit.prevent="submit" class="justify-center flex flex-col items-center mb-10">
        <div>
            <p></p>
        </div>
        <div class="flex flex-col items-center">
            <h1 class="text-3xl text-center font-bold text-zinc-700">
                Cadastre uma Nova Tarefa
            </h1>
        </div>
        <article class="flex flex-col gap-4 pt-6 ">
            <div>
                <span>Titulo: </span>
                <input type="text" placeholder="Titulo Teste" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="cadastro.tarefa.titulo">
            </div>
            <div class="">
                <span>Descrição:</span>
                <textarea placeholder="Descrição da tarefa" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="cadastro.tarefa.descricao"></textarea>
            </div>

            <div class="">
                <span>Prioridade:</span>
                <Select :options="cadastro.prioridades" v-model="cadastro.tarefa.prioridade"></Select>
            </div>

            <div class="">
                <span class="">Dificuldade:</span>
                <Select :options="cadastro.dificuldades" v-model="cadastro.tarefa.dificuldade"></Select>
            </div>


            <div class="">
                <span>Responsaveis:</span>
                <MultiSelect :options="opcoesUsuarios" v-model="cadastro.tarefa.responsaveis" />
            </div>
        </article>
        <div class="mt-6">
            <button type="submit" value="cadastrar" class="btn btn-primary">Cadastrar</button>
        </div>
        <div>
            <p>{{ cadastro.error }}</p>
        </div>
    </form>
</template>