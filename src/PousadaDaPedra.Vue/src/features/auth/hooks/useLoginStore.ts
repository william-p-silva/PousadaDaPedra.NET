import { defineStore } from "pinia";
import { ref } from "vue";
import { type UsuarioLoginResponse, type loginUsuarioType } from "../types/usuarioType";
import { useRouter } from "vue-router";
import { loginUsuarioSchema } from "../types/loginSchema";
import { LoginUsuarioService, logoutService, verificarAuthService } from "../services/loginService";



export const loginUsuarioStore = defineStore(('login'), () => {
    const router = useRouter();
    const isLoading = ref(false);
    const error = ref('');

    const usuarioLogin = ref<loginUsuarioType>({
        email: '',
        senha: '',
    });

    const usuarioLogado = ref<UsuarioLoginResponse | null>(null)
    const isAuthenticated = ref(false);

    async function verificarAuth() {
        try {
            const user = await verificarAuthService();
            if (user) {
                usuarioLogado.value = user;
                isAuthenticated.value = true;
                return true;

            } else {
                usuarioLogado.value = null;
                isAuthenticated.value = false;
                return false;
            }
        } catch (err) {
            console.log("depois catch ", isAuthenticated)
            usuarioLogado.value = null;
            isAuthenticated.value = false;
            return false;
        }

    }

    async function handleSubmit(requestLogin: loginUsuarioType) {
        error.value = '';

        const result = loginUsuarioSchema.safeParse(requestLogin);
        if (!result.success) {
            error.value = result.error.issues[0]!.message;
            return false;
        }

        try {
            isLoading.value = true;
            const response = await LoginUsuarioService(requestLogin);

            if (response === null){
                error.value = "Ocorreu um [Erro] por favor tente novamente mais tarde"
                return false;
            }

            limparForm();
            isAuthenticated.value = true;
            usuarioLogado.value = response;
            return true;
        } catch (err) {
            error.value = "Erro inesperado, tente novamente mais tarde";
            return false;
        } finally {
            isLoading.value = false;
        }
    }

    async function logout() {
        try {
            isLoading.value = true;
            const response = await logoutService();
            console.log(response)

            if (response === null){
                error.value = "Ocorreu um [Erro] por favor tente novamente mais tarde"
                return false;
            }

            limparForm();
            isAuthenticated.value = false;
            usuarioLogado.value = null;
            return true;
        } catch (err) {
            error.value = "Erro inesperado, tente novamente mais tarde";
            return false;
        } finally {
            isLoading.value = false;
        }
    }

    function limparForm() {
        usuarioLogin.value = {
            email: "",
            senha: "",
        };
        error.value = '';
    }

    return {
        usuarioLogin,
        isLoading,
        error,
        router,
        isAuthenticated,
        usuarioLogado,
        handleSubmit,
        verificarAuth,
        limparForm,
        logout,
    }
})