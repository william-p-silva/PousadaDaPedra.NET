<script setup lang="ts">
import Loading from '@/shared/components/loading.vue';
import { loginUsuarioStore } from '../hooks/useLoginStore';
import Navegar from './navegar.vue';

const login = loginUsuarioStore();

async function submit() {
    const sucesso = await login.handleSubmit(login.usuarioLogin);
    if (sucesso) {
        login.router.push("/usuario")
    }
}
</script>



<template>
    <div v-if="login.isLoading" class="h-screen flex justify-center items-center">
        <Loading text="Cadastrando Usuário" />
    </div>
    <div v-else class="flex flex-col gap-6 justify-center items-center h-screen">
        <form class="flex flex-col gap-2 text-zinc-700" @submit.prevent="submit">

            <div class="flex flex-col items-center ">
                <h1 class="text-3xl font-bold text-zinc-700">Faço o login</h1>
                <p class="text-sm text-zinc-500">Efetue seu login na Pousada da Pedra :)</p>
            </div>

            <div>
                <span>Emal: </span>
                <input type="email" placeholder="email@teste" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="login.usuarioLogin.email">
            </div>
            <div>
                <span>Senha: </span>
                <input type="password" placeholder="*********" class="w-full p-1 rounded-lg border border-zinc-700"
                    v-model="login.usuarioLogin.senha">
            </div>
            <div>
                <input type="submit" value="Cadastrar"
                    class="w-full p-2 text-white cursor-pointer rounded-lg bg-zinc-700 hover:bg-zinc-800">
            </div>
        </form>
        <div v-if="login.error" class="-m-4 text-red-600/80">
            {{ login.error }}
        </div>

        <Navegar text="Cadastrar-se" rota="cadastro" />

    </div>
</template>