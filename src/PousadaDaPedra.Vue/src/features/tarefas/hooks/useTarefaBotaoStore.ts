import { defineStore } from "pinia";
import { Status } from "../types/tarefaType";
import { ref } from "vue";
import { finalizarTarefaService, iniciarTarefaService, reabrirTarefaService } from "../services/acaoTarefaServices";
import { useTarefaStore } from "./useTarefaStore";


export function acaoBotaoTarefaStore(
    status: Status
) {
    const tarefaStore = useTarefaStore()
    switch (status) {
        case Status.Pendente:

            async function iniciar(id: number, prazo: Date) {
                if (isNaN(prazo.getTime())) {
                    alert("Data inválida.");
                    return;
                }

                if (prazo.getTime() <= Date.now()) {
                    alert("A data deve ser maior que a data atual.");
                    return;
                }
                await iniciarTarefaService(id, prazo)
                await tarefaStore.listarTarefas();
            }

            return {
                text: "Iniciar",
                style: "bg-green-500 ",
                action: iniciar,
            };

        case Status.EmAndamento:
            async function finalizar(id: number, prazo: Date) {
                await finalizarTarefaService(id);
                await tarefaStore.listarTarefas();
            }

            return {
                text: "Finalizar",
                style: "bg-yellow-500 ",
                action: finalizar,
            };

        case Status.Finalizada:
            async function reabrir(id: number, prazo: Date) {

                if (isNaN(prazo.getTime())) {
                    alert("Data inválida.");
                    return;
                }

                if (prazo.getTime() <= Date.now()) {
                    alert("A data deve ser maior que a data atual.");
                    return;
                }
                await reabrirTarefaService(id, prazo)
                await tarefaStore.listarTarefas();
            }

            return {
                text: "Reabrir Tarefa",
                style: "bg-red-500 ",
                action: reabrir,
            }

    }
}