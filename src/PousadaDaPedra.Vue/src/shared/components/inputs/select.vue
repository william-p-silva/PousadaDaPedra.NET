<script setup lang="ts">
interface Option {
    value: string | number
    label: string
}

defineProps<{
    options: Option[]
}>()

const model = defineModel<string | number>()
</script>

<template>
    <select
        v-model="model"
        class="w-full p-1 rounded-lg border border-zinc-700"
    >
        <option
            v-for="opt in options"
            :key="opt.value"
            :value="opt.value"
        >
            {{ opt.label }}
        </option>
    </select>
</template>