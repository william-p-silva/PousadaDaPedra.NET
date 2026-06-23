<script setup lang="ts">
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore';

const auth = loginUsuarioStore();
const user = auth.verificarAuth();

</script>

<template>
    <header class="flex justify-between py-2 px-6 bg-white  shadow-[inset_0_-8px_8px_-8px_rgba(0,0,0,0.4)]">
        <div class="flex flex-col justify-center items-center">
            <div class="bg-zinc-500 rounded-full w-22 h-22 "></div>
            <RouterLink to="/usuario" class="btn">{{ auth.usuarioLogado?.email ?? "Usuario" }}</RouterLink>
        </div>
        <nav class="flex gap-2 items-end">
            <RouterLink to="/login" class="btn btn-secundary">Login</RouterLink>
            <RouterLink to="/cadastro" class="btn btn-primary" >Cadastre-se</RouterLink>
            <RouterLink to="/criar/tarefa" class="btn btn-secundary" >Cadastrar Tarefa</RouterLink>

        </nav>
    </header>
</template>