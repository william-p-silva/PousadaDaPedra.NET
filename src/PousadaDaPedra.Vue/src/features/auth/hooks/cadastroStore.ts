import { defineStore } from "pinia";
import { ref } from "vue";
import { Cargo, CargoCadastro, type CadastroUsuario, type RequestCadastroUserType } from "../types/usuarioType";
import { CadastrarUsuario } from "../services/cadastroService";
import { useRouter } from "vue-router";
import { CadastroUserSchema } from "../types/cadastroSchemas";

export const CadastroUser = defineStore(('cadastroFuncionario'), () => {
    const router = useRouter();
    const error = ref('');
    const isLoading = ref(false);

    const cadastroUsuarioRequest = ref<CadastroUsuario>({
        email: '',
        nome: '',
        senha: '',
        confirmSenha: '',
    });

    async function handleSubmit(requestCadastro: CadastroUsuario) {

        const result = CadastroUserSchema.safeParse(requestCadastro)

        if(!result.success){
            error.value = result.error.issues[0]!.message
            return false;
        }

        try{
            isLoading.value = true;
            const request: RequestCadastroUserType = {
                nome: requestCadastro.nome,
                email: requestCadastro.email,
                senha: requestCadastro.senha,
                cargo: CargoCadastro.funcionario,
            }
            const response = await CadastrarUsuario(request);

            if(!response|| !response.success){
                error.value = response.message;
                return false;
            }

            limparForm();
            return true;

        }catch(err){
            error.value = "Ocorreu um erro inesperado";
            return false;
        }finally{
            isLoading.value = false;
        }
    }


    function limparForm(){
        cadastroUsuarioRequest.value = {
            email: '',
            senha: '',
            confirmSenha: '',
            nome: '',
        };
        error.value = '';
    }

    return {
        router,
        isLoading,
        error,
        cadastroUsuarioRequest,
        handleSubmit,
        limparForm,
    };
})

