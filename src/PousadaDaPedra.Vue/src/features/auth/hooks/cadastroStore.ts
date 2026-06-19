import { defineStore } from "pinia";
import { ref } from "vue";
import { Cargo, type CadastroUsuario } from "../types/cadastroUser";
import { CadastrarUsuario } from "../services/cadastroService";
import { useRouter } from "vue-router";

export const CadastroUser = defineStore(('cadastorFuncionario'), () => {
    const router = useRouter();
    const error = ref('');
    const isLoading = ref(false);

    const cadastroUsuarioRequest = ref<CadastroUsuario>({
        email: '',
        nome: '',
        senha: '',
        confirmSenha: '',
        cargo: Cargo.funcionario,
    });

    async function handleSubmit(requestCadastro: CadastroUsuario) {
        if(
        requestCadastro.email.trim() === "" || 
        requestCadastro.nome.trim() === "" ||
        requestCadastro.senha.trim() === "" ||
        requestCadastro.confirmSenha.trim() === ""
        ){
            error.value = "Campos em branco";
            return false;
        }

        if(requestCadastro.senha != requestCadastro.confirmSenha){
            error.value = "Senhas incompativeis";
            return false;
        }

        if(requestCadastro.senha.length < 8 || requestCadastro.confirmSenha.length < 8){
            error.value = "senha muito curta";
            return false;
        }

        try{
            isLoading.value = true;
            const response = await CadastrarUsuario(requestCadastro);

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
            cargo: Cargo.funcionario
        }
    }

    return {
        router,
        isLoading,
        error,
        cadastroUsuarioRequest,
        handleSubmit,
    };
})