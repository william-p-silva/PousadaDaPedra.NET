


export async function listarTarefasService() {
    const response = await fetch("http://localhost:5171/api/Tarefa/listar", {
        credentials: "include",
        method: "GET",
    })

    if(!response.ok) return null

    const json = await response.json();

    if(!json.success) return null

    return json.data;
    
}


export async function listarUsuariosResponsaveis(id: number) {
    const response = await fetch(`http://localhost:5171/api/Usuario/listar/${id}`, {
        credentials: "include",
        method: "GET",
    })
    if(!response.ok) return null;
    
    const json = await response.json();

    if(!json.success) return null

    return json.data;
}


export async function listarAllUsuarios(gerente: boolean = false) {
    const response = await fetch(`http://localhost:5171/api/Usuario/listar?gerente=${gerente}`, {
        credentials: "include",
        method: "GET",
    })
    if(!response.ok) return null;
    
    const json = await response.json();

    if(!json.success) return null

    return json.data;
}