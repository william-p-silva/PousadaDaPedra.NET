import { defineStore } from "pinia";
import { ref } from "vue";
import { type UsuarioTarefaResponse, type TarefaResponse } from "../types/tarefaType";
import { listarTarefasService, listarUsuariosResponsaveis } from "../services/tarefaServices";



export const useTarefaStore = defineStore(('tarefa'), () => {
    const isLoading = ref(false);
    const error = ref('');

    const tarefas = ref<TarefaResponse[] | null>(null);

    async function listarTarefas() {
        isLoading.value = true;

        try{
            const response = await listarTarefasService();
            if(response === null){
                error.value = "Tarefas não encontradas";
                return false;
            }
            tarefas.value = response;
        }catch (err){
            error.value = "Erro interno por favor tente mais tarde";
        }finally{
            isLoading.value = false;
        }
    }

    return {
        tarefas,
        isLoading,
        listarTarefas,
    }
})