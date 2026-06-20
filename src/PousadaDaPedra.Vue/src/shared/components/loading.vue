<script setup lang="ts">
defineProps<{
  text?: string;
}>();
</script>

<template>
  <div class="flex flex-col items-center justify-center gap-3 p-4">
    <div class="spinner"></div>

    <p
      v-if="text"
      class="text-sm text-gray-600"
    >
      {{ text }}
    </p>
  </div>
</template>

<style scoped>
.spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #e5e7eb;
  border-top: 4px solid #2563eb;
  border-radius: 50%;
  animation: spin 0.8s linear infinite;
}

@keyframes spin {
  from {
    transform: rotate(0deg);
  }

  to {
    transform: rotate(360deg);
  }
}
</style>