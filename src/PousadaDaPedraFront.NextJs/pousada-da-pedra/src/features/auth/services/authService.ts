//src/features/auth/services/authService.ts

import {UserFormCadastro} from "@/features/auth/types";
import {api} from "@/shared/infrastructure/http/api";

export async function cadastroUser(body: UserFormCadastro) {
    const response = api.post("Usuario/criar", body)   
    return response;
}