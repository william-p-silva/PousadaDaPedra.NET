import z from "zod";
import { Cargo, CargoCadastro } from "./usuarioType";

export const CadastroUserSchema = z.object({
    nome: z.string()
    .min(5, "Nome curto. Digite seu nome completo")
    .max(100, "Nome muito longo"),

    email: z.email("Email inválido").max(150, "Email muito longo"),

    senha: z.string().min(8, "A senha deve ter pelo menos 8 caracteres"),

    confirmSenha: z.string(),

}).refine(data => data.senha === data.confirmSenha, {
    message: "Senhas não coincidem",
    path: ["confirmSenha"],
});


export const RequestCadastroUserSchema = z.object({
    nome: z.string()
    .min(5, "Nome curto. Digite seu nome completo")
    .max(100, "Nome muito longo"),

    email: z.email("Email inválido").max(150, "Email muito longo"),

    senha: z.string().min(8, "A senha deve ter pelo menos 8 caracteres"),

    cargo: z.enum(CargoCadastro),
})