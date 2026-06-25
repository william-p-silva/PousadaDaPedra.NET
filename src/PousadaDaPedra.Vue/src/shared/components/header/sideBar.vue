<script setup lang="ts">
import { loginUsuarioStore } from '@/features/auth/hooks/useLoginStore';
import { useHeaderStore } from '@/shared/hooks/header/useHeaderStore';
import { LogIn, Mountain } from '@lucide/vue';
import { computed } from 'vue';
import { useRoute, useRouter } from 'vue-router';

const auth = loginUsuarioStore();
const user = auth.verificarAuth();
const useHeader = useHeaderStore();
const route = useRoute();
const router = useRouter();

async function handleLogout() {
    const result = await auth.logout();

    if (result) {
        router.push('/auth')
    }
}

const ITEM_HEIGHT = 48;
const GAP = 8;

const activeIndex = computed(() =>
    useHeader.navItems.findIndex(item => route.path.startsWith(item.to))
);

const sliderY = computed(() =>
    activeIndex.value * (ITEM_HEIGHT + GAP)
);

</script>

<template>
    <header :class="[
        'bg-nights h-screen z-50 shrink-0 fixed left-0 top-0 transition-all duration-300 ease-in-out overflow-hidden flex flex-col ',
        useHeader.hiddenSide ? 'w-20 px-2 py-6' : 'w-72 p-6'
    ]">
        <section
            :class="['flex items-center', useHeader.hiddenSide ? 'gap-0 justify-center' : 'gap-4 justify-start  ']">

            <div v-if="!useHeader.hiddenSide"
                :class="['w-12 h-12 rounded-2xl bg-pumpink flex justify-center items-center cursor-pointer']"
                @click="useHeader.hiddenSide = !useHeader.hiddenSide">
                <Mountain class="text-whiteSmoke" />
            </div>
            <div v-else @click="useHeader.hiddenSide = !useHeader.hiddenSide"
                class="w-12 h-12 rounded-2xl bg-pumpink flex items-center justify-center shrink-0 cursor-pointer">
                <span class="text-whiteSmoke font-bold text-xl uppercase">{{ auth.usuarioLogado?.nome[0]
                    }}</span>
            </div>
            <div
                :class="['overflow-hidden transition-all duration-300', useHeader.hiddenSide ? 'w-0 hidden' : 'w-auto opacity-100']">
                <h1 class="font-bold text-lg text-whiteSmoke whitespace-nowrap">Pousada da Pedra</h1>
                <p class="text-charcoal text-sm font-medium whitespace-nowrap">Gestão Operacional</p>
            </div>
        </section>

        <main :class="['mt-6 flex flex-col justify-center', useHeader.hiddenSide ? 'ml-3' : '']">
            <div class="mb-2">
                <h3
                    :class="['font-medium text-charcoal transition-all duration-300 whitespace-nowrap', useHeader.hiddenSide ? 'hidden' : 'opacity-100']">
                    Operação</h3>
            </div>
            <nav class="flex flex-col gap-2 -ml-3 text-whiteSmoke/60 transition-all ease-in-out  duration-300 ">
                <div class="absolute left-0 right-0 rounded-xl bg-charcoal transition-transform duration-300 ease-in-out pointer-events-none mx-2 "
                    :class="useHeader.hiddenSide ? '' : 'mr-6'" :style="{
                        height: `${ITEM_HEIGHT}px`,
                        transform: `translateY(${sliderY}px)`,
                        opacity: activeIndex >= 0 ? 1 : 0,
                    }" />
                <RouterLink v-for="item in useHeader.navItems" :key="item.to" :to="item.to"
                    class="relative z-10 flex gap-2 p-3 rounded-xl transition-colors duration-300 hover:bg-charcoal/50"
                    :class="route.path.startsWith(item.to) ? 'text-pumpink' : 'text-whiteSmoke/60'">
                    <p :class="useHeader.hiddenSide ? 'flex justify-center w-full' : ''">
                        <component :is="item.icon" />
                    </p>
                    <p :class="['text-whiteSmoke/60', useHeader.hiddenSide ? 'hidden' : '']">
                        {{ item.label }}
                    </p>
                </RouterLink>
            </nav>
        </main>
        <footer class="mt-auto pt-4 border-t border-charcoal flex items-center gap-4"
            :class="useHeader.hiddenSide ? 'justify-center' : 'justify-start'">

            <div class="flex px-2 justify-between items-center w-full" :class="useHeader.hiddenSide ? 'gap-0' : ''">

                <div class="flex justify-start items-center gap-4"
                    :class="[useHeader.hiddenSide ? 'hidden' : 'w-auto opacity-100']">
                    <div class="w-10 h-10 rounded-full bg-pumpink flex items-center justify-center shrink-0">
                        <span class="text-whiteSmoke font-bold text-xl uppercase">{{ auth.usuarioLogado?.nome[0]
                            }}</span>
                    </div>
                    <div :class="['overflow-hidden transition-all duration-300', useHeader.hiddenSide ? 'hidden' : '']">
                        <p class="text-whiteSmoke font-bold text-sm whitespace-nowrap">{{ auth.usuarioLogado?.nome ??
                            "Não authenticado" }}</p>
                        <p class="text-charcoal text-xs whitespace-nowrap">{{ auth.usuarioLogado?.cargo }}</p>
                    </div>
                </div>
                <button class="text-whiteSmoke/60 cursor-pointer transition-colors duration-300 hover:text-crimson"
                    :class="useHeader.hiddenSide ? 'w-full flex justify-center' : ''" @click="handleLogout">
                    <LogIn />
                </button>
            </div>


        </footer>
    </header>
</template>