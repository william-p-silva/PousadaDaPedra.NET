import {number, z} from "zod"
import type { CadastroUserSchema, RequestCadastroUserSchema } from "./cadastroSchemas";
import type { loginUsuarioSchema, responseLoginUsuarioSchema } from "./loginSchema";


export enum Cargo {
    funcionario = "Funcionario",
    gerente = "Gerente",
}

export enum CargoCadastro {
    funcionario,
    gerente,
}

export type CadastroUsuario = z.infer<typeof CadastroUserSchema>;


export type loginUsuarioType = z.infer<typeof loginUsuarioSchema>;


export type UsuarioLoginResponse = z.infer<typeof responseLoginUsuarioSchema>;


export type RequestCadastroUserType = z.infer<typeof RequestCadastroUserSchema>;