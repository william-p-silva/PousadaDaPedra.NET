
export interface TarefaResponse {
    id: number,
    titulo: string,
    descricao: string,
    responsaveis: Responsaveis[],
    dataInicio: string,
    dataTermino: string,
    prazo: string,
    prioridade: Prioridade,
    dificuldade: Dificuldade,
    status: Status,
}

export enum Prioridade {
    Baixa,
    Media,
    Alta,
}

export enum Status {
    Pendente,
    EmAndamento,
    Finalizada
}

export enum Dificuldade {
    Facil,
    Medio,
    Dificil,
}


export interface UsuarioTarefaResponse {
    id: number,
    nome: string,
    email: string,
    cargo: string,
}

export interface Responsaveis {
    id: number,
    nome: string,
    email: string,
}