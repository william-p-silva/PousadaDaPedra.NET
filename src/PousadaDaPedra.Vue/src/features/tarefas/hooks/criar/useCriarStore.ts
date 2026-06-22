import { defineStore } from "pinia";
import { type CriarTarefaType, Dificuldade, Prioridade, type UsuarioTarefaResponse } from "../../types/tarefaType";
import { ref } from "vue";
import { listarAllUsuarios } from "../../services/tarefaServices";



export const useCriarTarefaStore = defineStore(('criarTarefa'), () => {
    const usuarios = ref<UsuarioTarefaResponse[]>([]);

    const tarefa = ref<CriarTarefaType>({
        dificuldade: Dificuldade.Facil,
        prioridade: Prioridade.Baixa,
        responsaveis: 0,
        descricao: "",
        titulo: "",
    });

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
        listarUsuarios,
    }
})