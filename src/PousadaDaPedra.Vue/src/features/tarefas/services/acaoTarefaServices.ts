

export async function iniciarTarefaService(id: number, prazo: Date) {
    const response = await fetch("http://localhost:5171/api/Tarefa/iniciar", {
        headers: { "Content-Type": "application/json" },
        method: "PUT",
        credentials: "include",
        body: JSON.stringify({id, prazo}),
    });

    if(!response.ok) return null;

    const json = await response.json();

    if(!json.success) return null;

    return json.data;
}


export async function finalizarTarefaService(id: number) {
    const response = await fetch("http://localhost:5171/api/Tarefa/finalizar", {
        headers: { "Content-Type": "application/json" },
        method: "PUT",
        credentials: "include",
        body: JSON.stringify({id}),
    });

    if(!response.ok) return null;

    const json = await response.json();

    if(!json.success) return null;

    return json.data;
}

export async function reabrirTarefaService(id: number, prazo: Date) {
    const response = await fetch("http://localhost:5171/api/Tarefa/reabrir", {
        headers: { "Content-Type": "application/json" },
        method: "PUT",
        credentials: "include",
        body: JSON.stringify({id, prazo}),
    });

    if(!response.ok) return null;

    const json = await response.json();

    if(!json.success) return null;

    return json.data;
}