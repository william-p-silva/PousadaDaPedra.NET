<script setup lang="ts">
defineProps<{
    rota: string,
    text: string,
}>()
</script>

<template>
 <div class="flex items-center gap-2 text-sm">
        <RouterLink
            to="/"
            class="font-medium text-zinc-500 hover:text-zinc-700 transition-colors duration-200"
        >
            Voltar
        </RouterLink>

        <span class="text-zinc-400">|</span>

        <RouterLink
            :to="rota"
            class="font-medium text-zinc-700 hover:text-zinc-900 hover:underline transition-colors duration-200"
        >
            {{ text }}
        </RouterLink>
    </div>
</template>