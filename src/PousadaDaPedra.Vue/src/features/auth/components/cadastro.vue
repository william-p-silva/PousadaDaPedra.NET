<script setup lang="ts">
import { CadastroUser } from '../hooks/cadastroStore';

const cadastro = CadastroUser();
</script>

<template>
    <div class="flex flex-col gap-6 justify-center items-center h-screen">
        <div class="flex flex-col items-center ">
            <h1 class="text-3xl font-bold text-zinc-700">Cadastre-se</h1>
            <p class="text-sm text-zinc-500">Efetue seu cadastro na Pousada da Pedra :)</p>
        </div>
        <form class="flex flex-col gap-2 text-zinc-700"
            @submit.prevent="(() => cadastro.handleSubmit(cadastro.cadastroUsuarioRequest))">
            <div>
                <span>Nome: </span>
                <input type="text" placeholder="Nome Teste" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="cadastro.cadastroUsuarioRequest.nome">
            </div>
            <div>
                <span>Emal: </span>
                <input type="email" placeholder="email@teste" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="cadastro.cadastroUsuarioRequest.email">
            </div>
            <div>
                <span>Senha: </span>
                <input type="password" placeholder="*********" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="cadastro.cadastroUsuarioRequest.senha">
            </div>
            <div>
                <input type="submit" value="Cadastrar"
                    class="w-full p-2 text-white cursor-pointer rounded-lg bg-zinc-700 hover:bg-zinc-800">
            </div>
        </form>

    </div>
</template>