<script setup lang="ts">

</script>

<template>
    <div class="flex items-center justify-center gap-1">
        <span class="dot animation-delay-0"></span>
        <span class="dot animation-delay-1"></span>
        <span class="dot animation-delay-2"></span>
    </div>
</template>

<style scoped>
.dot {
    width: 6px;
    height: 6px;
    border-radius: 9999px;
    background: currentColor;
    animation: bounce 1s infinite ease-in-out;
}

.animation-delay-0 {
    animation-delay: 0s;
}

.animation-delay-1 {
    animation-delay: 0.15s;
}

.animation-delay-2 {
    animation-delay: 0.3s;
}

@keyframes bounce {

    0%,
    80%,
    100% {
        transform: scale(0.5);
        opacity: 0.5;
    }

    40% {
        transform: scale(1);
        opacity: 1;
    }
}
</style>