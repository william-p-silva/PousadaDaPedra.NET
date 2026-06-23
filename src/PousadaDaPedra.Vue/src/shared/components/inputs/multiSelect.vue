<script setup lang="ts">
interface Option {
    value: string | number
    label: string
}

defineProps<{
    options: Option[]
}>()

const model = defineModel<(string | number)[]>({
    default: []
})

function onChange(event: Event) {
    const target = event.target as HTMLSelectElement

    model.value = Array.from(target.selectedOptions).map(
        option => Number(option.value)
    )
}
</script>

<template>
<select
    multiple
    :value="model"
    @change="onChange"
    class="w-full p-2 rounded-lg border border-zinc-700 min-h-32"
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