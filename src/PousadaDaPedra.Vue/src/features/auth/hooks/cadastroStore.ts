import { defineStore } from "pinia";
import { ref } from "vue";
import { Cargo, type CadastroUsuario } from "../types/cadastroUser";
import { CadastrarUsuario } from "../services/cadastroService";

export const CadastroUser = defineStore(('cadastorFuncionario'), () => {
    const cadastroUsuarioRequest = ref<CadastroUsuario>({
        email: '',
        nome: '',
        senha: '',
        cargo: Cargo.funcionario,
    });

    async function handleSubmit(requestCadastro: CadastroUsuario) {
        console.log(requestCadastro)
        const response = await CadastrarUsuario(requestCadastro);
        console.log(response)
    }

    return {
        cadastroUsuarioRequest,
        handleSubmit,
    }
})