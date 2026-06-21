import { Dificuldade, Prioridade, Status } from "../types/tarefaType";


export const tarefaStyleHook = () => {
    function getStyleDificuldade(dificuldade: Dificuldade){
        switch (dificuldade){
            case Dificuldade.Facil:
                return "bg-green-300 text-green-950 ";
            case Dificuldade.Medio:
                return "bg-yellow-300 text-yellow-950 ";
            case Dificuldade.Dificil:
                return "bg-red-300 text-red-950";
        }
    }

    function getStylePrioridade(prioridade: Prioridade){
        switch (prioridade){
            case Prioridade.Baixa:
                return "text-green-600";
            case Prioridade.Media:
                return "text-yellow-700";
            case Prioridade.Alta:
                return "text-red-600";
        }
    }

    function getStyleStatus(status: Status){
        switch (status){
            case Status.Finalizada:
                return "bg-green-300/60 text-green-900 ";
            case Status.EmAndamento:
                return "bg-yellow-300/60 text-yellow-800 ";
            case Status.Pendente:
                return "bg-red-300/60 text-red-800 ";
        }
    }

    return {
        getStyleDificuldade,
        getStylePrioridade,
        getStyleStatus,
    }
}