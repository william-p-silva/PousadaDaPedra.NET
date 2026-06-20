<script setup lang="ts">
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore'
import { onMounted } from 'vue'

const auth = loginUsuarioStore()

console.log("COMPONENTE CARREGOU")

onMounted(async () => {
    console.log("ONMOUNTED INICIO")

    await auth.verificarAuth()

    console.log("ONMOUNTED FIM")
    console.log(auth.usuarioLogado)
})
</script>


<template>
    <h1 v-if="auth.usuarioLogado">
        Olá usuario {{ auth.usuarioLogado.nome }}
    </h1>

    <h1 v-else>
        Carregando...
    </h1>
</template>
