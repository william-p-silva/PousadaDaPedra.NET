"use client"
import { useTarefa } from "@/features/tarefa/hooks/useTarefa";
import {formatarData, formatarPrioridade, formatarStatus} from "@/shared/hooks/formatar";
import {ResponsaveisTarefa} from "@/features/tarefa/components/responsaveis";

function Card_tarefa() {
    const { tarefa } = useTarefa();

    // Cores sutis para as bordas e badges de dificuldade
    const badgeDificuldade = {
        0: "bg-emerald-100 text-emerald-800 border-emerald-200", // Fácil
        1: "bg-amber-100 text-amber-800 border-amber-200",     // Médio
        2: "bg-rose-100 text-rose-800 border-rose-200"          // Difícil
    };

    const labelDificuldade = { 0: "Fácil", 1: "Médio", 2: "Difícil" };

    return (
        <>
            {tarefa?.map((t) => (
                <div
                    key={t.id}
                    className="flex flex-col justify-between w-72 min-h-[400px] bg-white rounded-xl shadow-md border border-slate-100 p-5 hover:shadow-lg transition-shadow duration-200 text-slate-700"
                >
                    {/* Topo do Card: Título e Dificuldade */}
                    <div>
                        <div className="flex items-start justify-between gap-2 mb-3">
                            <h2 className="text-lg font-bold text-slate-900 leading-snug capitalize">
                                {t.titulo}
                            </h2>
                            <span className={`text-xs font-semibold px-2.5 py-1 rounded-full border ${badgeDificuldade[t.dificuldade as 0 | 1 | 2] || badgeDificuldade[2]}`}>
                                {labelDificuldade[t.dificuldade as 0 | 1 | 2] || "Desconhecido"}
                            </span>
                        </div>

                        {/* Descrição */}
                        <p className="text-sm text-slate-500 line-clamp-3 mb-4 bg-slate-50 p-2 rounded-lg border border-slate-100">
                            {t.descricao || "Sem descrição informada."}
                        </p>

                        {/* Detalhes Meio do Card */}
                        <div className="space-y-2 text-xs">
                            <div className="flex justify-between items-center py-1 border-b border-slate-50">
                                <span className="font-medium text-slate-400 uppercase tracking-wider text-[10px]">Prioridade</span>
                                <span className="font-semibold text-slate-700">{formatarPrioridade(t.prioridade)}</span>
                            </div>

                            <div className="flex justify-between items-center py-1 border-b border-slate-50">
                                <span className="font-medium text-slate-400 uppercase tracking-wider text-[10px]">Status</span>
                                <span className="px-2 py-0.5 font-medium rounded bg-blue-50 text-blue-700 border border-blue-100">
                                    {formatarStatus(t.status)}
                                </span>
                            </div>

                            <div className="flex flex-col gap-1 py-1">
                                <span className="font-medium text-slate-400 uppercase tracking-wider text-[10px]">Responsáveis</span>
                                <span className="text-slate-600 font-medium bg-slate-50 px-2 py-1 rounded border border-slate-100">
                                    {t.responsaveis.map((r) => (
                                        
                                            <div key={r}>
                                                <ResponsaveisTarefa id={r} />
                                            </div>
                                        
                                    ))}
                                </span>
                            </div>

                            <div className="w-full pt-3 mt-1 flex justify-center">
                                {t.status === 0 ? (
                                    <button className="cursor-pointer w-full bg-blue-600 hover:bg-blue-700 text-white font-semibold py-2 px-4 rounded-lg shadow-sm hover:shadow transition-all duration-200 text-xs tracking-wide uppercase">
                                        Iniciar Tarefa
                                    </button>
                                ) : t.status === 1 ? (
                                    <button className="cursor-pointer w-full bg-emerald-600 hover:bg-emerald-700 text-white font-semibold py-2 px-4 rounded-lg shadow-sm hover:shadow transition-all duration-200 text-xs tracking-wide uppercase">
                                        Finalizar Tarefa
                                    </button>
                                ) : t.status === 2 ? (
                                    <button className="cursor-pointer w-full bg-white hover:bg-slate-50 text-slate-600 font-semibold py-2 px-4 rounded-lg border border-slate-200 shadow-sm hover:border-slate-300 transition-all duration-200 text-xs tracking-wide uppercase">
                                        Reabrir Tarefa
                                    </button>
                                ) : null}
                            </div>
                        </div>
                    </div>

                    {/* Rodapé do Card: Datas */}
                    <div className="mt-6 pt-3 border-t border-slate-100 grid grid-cols-2 gap-2 text-[11px] text-slate-500">
                        <div>
                            <span className="block font-medium text-slate-400">Início:</span>
                            <span className="font-medium">{t.dataInicio == null ? "Em breve" : formatarData(t.dataInicio)}</span>
                        </div>
                        <div className="text-right">
                            <span className="block font-medium text-slate-400">Prazo:</span>
                            <span className="font-medium text-rose-600">{formatarData(t.prazo)}</span>
                        </div>
                    </div>
                </div>
            ))}
        </>
    );
}

export default Card_tarefa