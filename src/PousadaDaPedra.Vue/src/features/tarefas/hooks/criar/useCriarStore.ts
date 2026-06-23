import { defineStore } from "pinia";
import { type CriarTarefaType, Dificuldade, Prioridade, type UsuarioTarefaResponse } from "../../types/tarefaType";
import { ref } from "vue";
import { CadastrarTarefaService, listarAllUsuarios } from "../../services/tarefaServices";
import { criarTarefaSchema } from "../../types/tarefaSchema";



export const useCriarTarefaStore = defineStore(('criarTarefa'), () => {

    const error = ref('');
    const isLoadind = ref(false)

    const usuarios = ref<UsuarioTarefaResponse[]>([]);

    const tarefa = ref<CriarTarefaType>({
        dificuldade: Dificuldade.Facil,
        prioridade: Prioridade.Baixa,
        responsaveis: [],
        descricao: "",
        titulo: "",
    });

    async function handleSubmite() {
        const result = criarTarefaSchema.safeParse(tarefa.value);

        if(!result.success){
            error.value = result.error.issues[0]!.message;
            return false;
        }

        try {
            isLoadind.value = true;
            const response = await CadastrarTarefaService(tarefa.value);
            if(!response){
                error.value = "Erro no cadastro";
                return false;
            }
            limparForm()
            return true;
        }catch (erro){
            console.error("Erro capturado:", erro);

            error.value = "Ocorreu um erro inesperado";
        
            return false;
        }finally{
            isLoadind.value = false;
        }
        
    }

    function limparForm(){
        tarefa.value = {
            dificuldade: Dificuldade.Facil,
            prioridade: Prioridade.Baixa,
            responsaveis: [],
            descricao: "",
            titulo: "",
        }
    }

    async function listarUsuarios() {
        const data = await listarAllUsuarios(true);

        if (data) {
            usuarios.value = data;
        }
    }

    const dificuldades = [
        {
            value: Dificuldade.Facil,
            label: 'Fácil'
        },
        {
            value: Dificuldade.Medio,
            label: 'Médio'
        },
        {
            value: Dificuldade.Dificil,
            label: 'Difícil'
        }
    ];

    const prioridades = [
        {
            value: Prioridade.Baixa,
            label: 'Baixa',
        },
        {
            value: Prioridade.Media,
            label: 'Média',
        },
        {
            value: Prioridade.Alta,
            label: 'Alta',
        }
    ];





    return {
        dificuldades,
        prioridades,
        usuarios,
        tarefa,
        isLoadind,
        error,
        listarUsuarios,
        handleSubmite,
    }
})