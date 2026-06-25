<script setup lang="ts">
import { loginUsuarioStore } from '../hooks/useLoginStore';
import { Eye, EyeOff, Lock, Mail } from '@lucide/vue';
import { ref } from 'vue';
import Submit from '@/shared/components/botoes/submit.vue';
import { useAuthStore } from '../hooks/authStore';

const login = loginUsuarioStore();
const authStore = useAuthStore();

async function submit() {
    const sucesso = await login.handleSubmit(login.usuarioLogin);
    if (sucesso) {
        login.router.push("/usuario")
    }
}
const mostrarSenha = ref(false)


defineProps<{
    rota: boolean
}>();
</script>



<template>

    <div class="flex flex-col gap-6 justify-center items-center bg-whiteSmoke w-full h-full">
        <form class="flex flex-col gap-4 w-[80%] lg:w-[60%] h-full justify-center lg:p-4 " @submit.prevent="submit">

            <div class="flex flex-col mb-6 gap-1">
                <h1 class="text-[26px] font-bold text-nights">Bem-vindo de volta</h1>
                <p class="text-[15px] font-normal text-grayBlue">Acesse sua conta para gerenciar a operação.</p>
            </div>

            <div class="flex flex-col gap-6">

                <div class="flex flex-col gap-2">

                    <p class="text-nights font-medium text-[13px] ">E-mail </p>

                    <div class="inpt inpt-primary">

                        <p class="text-center">
                            <Mail class="text-grayBlue/40 size-5" />
                        </p>
                        <input type="email" placeholder="email@teste" class="w-full outline-none"
                            v-model="login.usuarioLogin.email">
                    </div>
                </div>
                <div class="flex flex-col gap-2">
                    <p class="text-nights font-medium text-[13px]">Senha</p>

                    <div class="inpt inpt-primary">
                        <Lock class="text-grayBlue/40 size-5" />

                        <input :type="mostrarSenha ? 'text' : 'password'" placeholder="***********"
                            class="outline-none w-full" v-model="login.usuarioLogin.senha">

                        <button type="button" @click="mostrarSenha = !mostrarSenha" class="pr-2 cursor-pointer">
                            <Eye v-if="!mostrarSenha"
                                class="text-grayBlue/40 size-5 hover:text-pumpink transition-colors" />

                            <EyeOff v-else class="text-pumpink size-5 hover:text-pumpink/80 transition-colors" />
                        </button>
                    </div>
                </div>
            </div>

            <div class="flex justify-between">
                <div class="flex items-center gap-2">
                    <input type="checkbox" class="
                        w-5 h-5
                        accent-pumpink
                        cursor-pointer
                        transition-all
                        duration-200
                        text-white
                    ">
                    <span class="text-nights font-medium select-none">
                        Manter Conectado
                    </span>
                </div>
                <div>
                    <RouterLink to="/cadastro">
                        <p class="text-pumpink/90 font-medium">Esqueci minha senha</p>
                    </RouterLink>
                </div>
            </div>


            <div class="w-full flex justify-center ">
                <Submit :isLoading="login.isLoading" />
            </div>
            <div v-if="rota" class="w-full flex justify-center">
                <p class="text-grayBlue/60 font-medium">
                    Não tem uma conta?
                    <RouterLink to="/cadastro" class="text-pumpink font-medium">
                        Solicitar acesso
                    </RouterLink>
                </p>
            </div>
            <div v-else="rota" class="w-full flex justify-center">
                <p class="text-grayBlue/60 font-medium">
                    Não tem uma conta?
                    <span @click="login.limparForm(), authStore.isCadastro = !authStore.isCadastro" class="text-pumpink font-medium cursor-pointer">
                        Solicitar acesso
                    </span>
                </p>
            </div>
            <div v-if="login.error" class="flex justify-center  text-crimson">
                {{ login.error }}
            </div>
        </form>


    </div>
</template>