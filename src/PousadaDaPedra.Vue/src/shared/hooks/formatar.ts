import { Dificuldade, Prioridade, Status } from "@/features/tarefas/types/tarefaType";

export function formatarData(data: string | null | undefined): string {
    // 1. Tratamento caso a data seja nula, vazia ou inválida
    if (!data) return "Sem Prazo";

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

export function formatarDificuldade(dificuldade: Dificuldade){
    switch (dificuldade){
        case Dificuldade.Facil:
            return "Fácil";
        case Dificuldade.Medio:
            return "Médio";
        case Dificuldade.Dificil:
            return "Díficil"
    }
}

export function formatarStatus(status: Status){
    switch (status){
        case Status.Pendente:
            return "Pendente";
        case Status.EmAndamento:
            return "Em Andamento";
        case Status.Finalizada:
            return "Finalizada";
    }
}

export function formatarPrioridade(prioridade: Prioridade){
    switch (prioridade){
        case Prioridade.Baixa:
            return "Baixa";
        case Prioridade.Media:
            return "Média";
        case Prioridade.Alta:
            return "Alta";
    }
}

