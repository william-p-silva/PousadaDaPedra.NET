import {useEffect, useState} from "react";
import {TarefaResponse} from "@/features/tarefa/types";
import {BuscarUserPorId, GetTarefas} from "@/features/tarefa/service/tarefaService";


export function useTarefa() {
    const [tarefa, setTarefa] = useState<TarefaResponse[] | null>(null);
    

    useEffect(() => {        
        async function listarTarefas(){
            const data = await GetTarefas();
            setTarefa(data)
        }
        listarTarefas();
    }, []);
    
    return {
        tarefa,
    }
    
}

export async function buscarUserId(id: number){
    return await BuscarUserPorId(id);
}