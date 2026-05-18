//src/features/auth/services/authService.ts

import {UserFormCadastro, UserFormLogin, UserResponse} from "@/features/auth/types";
import {api} from "@/shared/infrastructure/http/api";
import {APIResponse} from "@/shared/types/apiResponse";

export async function cadastroUser(body: UserFormCadastro) {
    const response = await api.post("/Usuario/criar", body, undefined, true)  ; 
    return response;
}

export async function loginUser(body: UserFormLogin) : Promise<APIResponse<UserResponse>> {
    const response = await api.post("/Usuario/login", body, undefined, true) as APIResponse<UserResponse>;
    
    await fetch("/api/auth", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({token: response.data.token})
    })
    
    return response;    
}

export async function logoutUser() {
    await fetch("/api/auth", { method: "DELETE" });
    window.location.href = "/login";
}