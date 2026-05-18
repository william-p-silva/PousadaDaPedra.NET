// src/shared/infrastructure/http/api.ts

import {cookieArmazenado} from "@/shared/infrastructure/storage/cookies";

const BASE_URL = process.env.NEXT_PUBLIC_API_URL;

export async function apiFetch<T>( endpoint: string, options?: RequestInit ): Promise<T> {
    
    const url = endpoint.startsWith("/") ? endpoint: `/${endpoint}`;
    
    const headers: HeadersInit ={
        "Content-Type": "application/json",
    };
    
    const token = cookieArmazenado.getToken();
    if(token){
        headers["Authorization"] = `Bearer ${token}`;
    }
    
    const config: RequestInit = {
        ...options,
            headers: {
            ...headers,
            ...options?.headers,
        },
    };
    
    const response = await fetch(`${BASE_URL}${url}`, config);

    if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || "Erro na requisição");
    }

    return response.json();
}

export const api =  { 
    get: <T>(endpoint: string, options?: RequestInit) =>
        apiFetch(`${endpoint}`, {method: "GET", ...options}),
    
    post: <T>(endpoint: string, body: any, options?: RequestInit) =>
        apiFetch(`${endpoint}`, {method: "POST", body: JSON.stringify(body), ...options})
}

