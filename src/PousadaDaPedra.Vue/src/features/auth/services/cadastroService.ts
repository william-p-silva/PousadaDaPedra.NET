import type { CadastroUsuario } from "../types/cadastroUser";



export async function CadastrarUsuario(CadastrarUsuarioRequest: CadastroUsuario) {
    const response = await fetch("http://localhost:5171/api/Usuario/criar", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(CadastrarUsuarioRequest)
    });

    return response.json();
}