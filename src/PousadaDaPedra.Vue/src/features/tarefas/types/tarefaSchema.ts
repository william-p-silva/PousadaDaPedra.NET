import z from "zod";
import { Dificuldade, Prioridade } from "./tarefaType";



export const criarTarefaSchema = z.object({
    titulo: z.string().min(3, "Titulo muito curto").max(100, "Titulo muito longo"),

    descricao: z.string().min(3, "Descrição muito curto").max(150, "Descrição muito longo"),

    prioridade: z.enum(Prioridade),

    dificuldade: z.enum(Dificuldade),

    responsaveis: z.number(),

})