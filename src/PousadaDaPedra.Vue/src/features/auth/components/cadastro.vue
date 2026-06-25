<script setup lang="ts">
import { Eye, EyeOff, Lock, Mail, User } from '@lucide/vue';
import { CadastroUser } from '../hooks/cadastroStore';
import Navegar from './navegar.vue';
import { ref } from 'vue';
import Submit from '@/shared/components/botoes/submit.vue';
import { useAuthStore } from '../hooks/authStore.ts';

const cadastro = CadastroUser();
const authStore = useAuthStore();


async function submit() {
    const sucesso = await cadastro.handleSubmit(cadastro.cadastroUsuarioRequest)
    if (sucesso) {
        alert("Usuario Cadastrato com sucesso");
        cadastro.router.push("/login");
    }

}

defineProps<{
    rota: boolean
}>();

const mostrarSenha = ref(false);
const mostrarConfirmSenha = ref(false);
</script>

<template>


    <div class="flex flex-col gap-4 justify-center items-center bg-whiteSmoke w-full h-full">
        <form class="flex flex-col gap-4 w-[80%] lg:w-[60%] h-full justify-center lg:p-4 " @submit.prevent="submit">

            <div class="flex flex-col mb-6 gap-1">
                <h1 class="text-[26px] font-bold text-nights">Comece agora</h1>
                <p class="text-[15px] font-normal text-grayBlue">Crie sua conta e tenha controle total das suas
                    atividades.</p>
            </div>

            <div class="flex flex-col gap-6">

                <div class="flex flex-col gap-2">

                    <p class="text-nights font-medium text-[13px] ">Nome </p>

                    <div class="inpt inpt-primary">
                        <p class="text-center">
                            <User class="text-grayBlue/40 size-5" />
                        </p>
                        <input type="text" placeholder="Teste da Silva" class="w-full outline-none"
                            v-model="cadastro.cadastroUsuarioRequest.nome">
                    </div>
                </div>

                <div class="flex flex-col gap-2">

                    <p class="text-nights font-medium text-[13px] ">E-mail </p>

                    <div class="inpt inpt-primary">
                        <p class="text-center">
                            <Mail class="text-grayBlue/40 size-5" />
                        </p>
                        <input type="email" placeholder="teste@pousadadapedra.com" class="w-full outline-none"
                            v-model="cadastro.cadastroUsuarioRequest.email">
                    </div>
                </div>

                <div class="flex flex-col gap-2">
                    <p class="text-nights font-medium text-[13px]">Senha</p>

                    <div class="inpt inpt-primary">
                        <Lock class="text-grayBlue/40 size-5" />

                        <input :type="mostrarSenha ? 'text' : 'password'" placeholder="***********"
                            class="outline-none w-full" v-model="cadastro.cadastroUsuarioRequest.senha">

                        <button type="button" @click="mostrarSenha = !mostrarSenha" class="pr-2 cursor-pointer">
                            <Eye v-if="!mostrarSenha"
                                class="text-grayBlue/40 size-5 hover:text-pumpink transition-colors" />

                            <EyeOff v-else class="text-pumpink size-5 hover:text-pumpink/80 transition-colors" />
                        </button>
                    </div>
                </div>

                <div class="flex flex-col gap-2">
                    <p class="text-nights font-medium text-[13px]">Confirmes sua Senha</p>

                    <div class="inpt inpt-primary">
                        <Lock class="text-grayBlue/40 size-5" />

                        <input :type="mostrarConfirmSenha ? 'text' : 'password'" placeholder="***********"
                            class="outline-none w-full" v-model="cadastro.cadastroUsuarioRequest.confirmSenha">

                        <button type="button" @click="mostrarConfirmSenha = !mostrarConfirmSenha"
                            class="pr-2 cursor-pointer">
                            <Eye v-if="!mostrarConfirmSenha"
                                class="text-grayBlue/40 size-5 hover:text-pumpink transition-colors" />

                            <EyeOff v-else class="text-pumpink size-5 hover:text-pumpink/80 transition-colors" />
                        </button>
                    </div>
                </div>

                <div class="w-full flex justify-center ">
                    <Submit :isLoading="cadastro.isLoading" />
                </div>
            </div>
            <div v-if="rota" class="w-full flex justify-center">
                <p class="text-grayBlue/60 font-medium">
                    Já tem uma conta?
                    <RouterLink to="/login" class="text-pumpink font-medium">
                        Faça login
                    </RouterLink>
                </p>
            </div>
            <div v-else="rota" class="w-full flex justify-center">
                <p class="text-grayBlue/60 font-medium">
                    Já tem uma conta?
                    <span @click="cadastro.limparForm(), authStore.isCadastro = !authStore.isCadastro" class="text-pumpink font-medium cursor-pointer">
                        Faça login
                    </span>
                </p>
            </div>
            <div v-if="cadastro.error" class="w-full justify-center items-center flex text-red-600/80">
                {{ cadastro.error }}
            </div>
        </form>

    </div>
</template>