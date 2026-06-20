import type { SuccessResponseApi } from "@/shared/types/response";
import type { CadastroUsuario } from "../types/usuarioType";



export async function CadastrarUsuario(CadastrarUsuarioRequest: any) {
    const response = await fetch("http://localhost:5171/api/Usuario/criar", {
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(CadastrarUsuarioRequest)
    });

    if(!response.ok) return null;

    return response.json();
}