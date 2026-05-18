// src/shared/infrastructure/http/api.ts

export async function apiFetch<T>( 
    endpoint: string, options?: RequestInit, isPublic: boolean = false ): Promise<T> {

    const path = endpoint.startsWith("/") ? endpoint.slice(1) : endpoint;
    
    const response = await fetch(`/api/proxy/${path}`, {
        ...options,
        headers: {
            "Content-Type": "application/json",
            "x-public-route": isPublic ? "true" : "false",
            ...options?.headers,
        },
    })

    // Token expirado ou ausente — redireciona para login de forma centralizada.
    // Nenhum service ou componente precisa se preocupar com isso.
    if (response.status === 401) {
        window.location.href = "/login";
        throw new Error("Sessão expirada. Redirecionando para o login...");
    }

    if (!response.ok) {
        const errorData = await response.json().catch(() => ({}));
        throw new Error(errorData.message || `Erro: ${response.status}`);
    }

    return response.json();
   
}

export const api =  { 
    get: <T>(endpoint: string, options?: RequestInit, isPublic?: boolean) =>
        apiFetch(`${endpoint}`, {method: "GET", ...options}, isPublic),
    
    post: <T>(endpoint: string, body: unknown, options?: RequestInit, isPublic?: boolean) =>
        apiFetch(`${endpoint}`, {method: "POST", body: JSON.stringify(body), ...options}, isPublic),
    
    put: <T>(endpoint: string, body: unknown, options?: RequestInit, isPublic?: boolean) => 
        apiFetch(`${endpoint}`, {method: "PUT", body: JSON.stringify(body), ...options}, isPublic),

    patch: <T>(endpoint: string, body: unknown, options?: RequestInit) =>
        apiFetch<T>(endpoint, { method: "PATCH", body: JSON.stringify(body), ...options }),

    delete: <T>(endpoint: string, options?: RequestInit) =>
        apiFetch<T>(endpoint, { method: "DELETE", ...options }),
    
};

