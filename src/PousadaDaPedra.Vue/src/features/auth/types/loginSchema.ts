import z, { email } from "zod";
import { Cargo } from "./usuarioType";


export const loginUsuarioSchema = z.object({
    email: z.email("Email Inválido").max(150, "Email muito longo"),
    senha: z.string(),
})


export const responseLoginUsuarioSchema = z.object({
    cargo: z.enum(Cargo),
    email: z.email("Email inválido"),
    nome: z.string(),
})