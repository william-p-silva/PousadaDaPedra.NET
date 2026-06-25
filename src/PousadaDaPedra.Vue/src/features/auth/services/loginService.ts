import { loginUsuarioSchema, responseLoginUsuarioSchema } from "../types/loginSchema";
import type { loginUsuarioType } from "../types/usuarioType";



export async function LoginUsuarioService(requestLogin: loginUsuarioType) {
    const response = await fetch("http://localhost:5171/api/Usuario/login", {
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(requestLogin)
    });

    if(!response.ok) return null;

    const json = await response.json();

    if (!json || !json.success) {
        return null;
    }

    const result = responseLoginUsuarioSchema.safeParse(json.data);
    if(!result.success){
        return null;
    }

    return result.data
}

export async function verificarAuthService() {
    const response = await fetch("http://localhost:5171/api/Usuario/me", {
        credentials: "include",
        method: "GET",
    })

    if (!response.ok) return null;

    const json = await response.json();

    if (!json.success) return null;

    const result = responseLoginUsuarioSchema.safeParse(json.data);
    
    if (!result.success) return null;

    return result.data;
}


export async function logoutService() {
    const response = await fetch("http://localhost:5171/api/Usuario/logout", {
        credentials: "include",
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({}),
    })

    if (!response.ok) return null;

    const json = await response.json();

    if (!json.success) return null;


    return json;
}