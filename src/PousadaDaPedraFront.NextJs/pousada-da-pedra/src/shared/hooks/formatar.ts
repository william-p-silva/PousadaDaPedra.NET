import {Prioridade, Status} from "@/features/tarefa/types";

export function formatarData(data: string | null | undefined): string {
    // 1. Tratamento caso a data seja nula, vazia ou inválida
    if (!data) return "Prazo Indeterminado";

    // 2. Cria o objeto Date (Garante o split para evitar bugs de fuso horário em strings simples como "2026-05-19")
    const dataObjeto = new Date(data);

    // Verifica se a string passada gerou uma data válida
    if (isNaN(dataObjeto.getTime())) {
        return "Data inválida";
    }

    // 3. Formata usando a API nativa do navegador para o padrão brasileiro
    return new Intl.DateTimeFormat("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        timeZone: "UTC" // Evita que o fuso horário local altere o dia
    }).format(dataObjeto);
}

export function formatarStatus(status: Status) {
    return  status === 0 ? "Pendente" : status === 1 ? "Em Andamento" : "Finalizado"
}

export function formatarPrioridade(prioridade: Prioridade) {
    return prioridade === 0 ? "Baixa" : prioridade === 1 ? "Media" : "Alta"
}