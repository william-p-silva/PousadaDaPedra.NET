
export interface CadastroUsuario {
    nome: string,
    email: string,
    senha: string,
    cargo: Cargo
}

export enum Cargo {
    funcionario,
    gerente,
}