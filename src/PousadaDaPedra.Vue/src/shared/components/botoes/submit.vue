<script setup lang="ts">
import { ArrowRight } from '@lucide/vue';
import LoadingBotton from '../loadingBotton.vue';

const props = defineProps<{
    isLoading: boolean,
}>();

</script>

<template>
    <div v-if="isLoading" class="btn btn-primary">
        <LoadingBotton class="h-6" />
    </div>
    <button v-else type="submit" class="btn btn-primary">
        <ArrowRight /> Entrar
    </button>
</template>