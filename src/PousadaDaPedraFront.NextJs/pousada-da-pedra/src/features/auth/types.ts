export interface UserFormCadastro {
    nome: string,
    email: string,
    senha: string,
    cargo: Cargo
}

export interface UserFormLogin {
    senha: string,
    email: string,
}

export interface UserResponse {
    nome: string,
    email: string,
    cargo: string,
    token: string,
}


export enum Cargo {
    funcionario,
    gerente,
}

