export interface TarefaResponse {
    id: number,
    titulo: string,
    descricao: string,
    prioridade: Prioridade,
    dificuldade: Dificuldade,
    status: Status,
    responsaveis: number[],
    dataInicio: string,
    prazo: string,
    dataTermino: string    
}


export enum Prioridade {
    Baixa,
    Media,
    Alta
}

export enum Dificuldade
{
    Facil,
    Medio,
    Dificil,
}

export enum Status
{
    Pendente,
    EmAndamento,
    Finalizada,
}