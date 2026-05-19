import {api} from "@/shared/infrastructure/http/api";
import {APIResponse} from "@/shared/types/apiResponse";
import {TarefaResponse} from "@/features/tarefa/types";
import {UserResponse} from "@/shared/types/userResponse";


export async function GetTarefas(): Promise<TarefaResponse[]>{
    const response = await api.get("Tarefa/listar", 
        undefined) as APIResponse<TarefaResponse[]>;
    
    return response.data;
}

export async function BuscarUserPorId(id: number): Promise<UserResponse>{
    const response = await api.get(`/Usuario/listar/${id}`, undefined, true) as APIResponse<UserResponse>;
    return response.data;
}