using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class ReabrirUseCase
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUnitOfWork _ofWork;

    public ReabrirUseCase(
        ITarefaRepository tarefaRepository
        ,IUnitOfWork ofWork)
    {
        _tarefaRepository = tarefaRepository;
        _ofWork = ofWork;
    }

    public async Task<TarefaResponseDTO> Execute(DateTime? newPrazo, int id)
    {
        if (newPrazo == null || newPrazo <= DateTime.UtcNow)
            throw new ArgumentException("O prazo é obrigatorio");
        var tarefa = await _tarefaRepository.BuscarPorId(id, true);
        if (tarefa == null)
            throw new ArgumentException("Tarefa Inexistente");
        
        
        tarefa.Reabrir(newPrazo);
        await _ofWork.Commit();

        return new TarefaResponseDTO()
        {
            Descricao = tarefa.Descricao,
            DataTermino = tarefa.DataTermino,
            Dificuldade = tarefa.Dificuldade,
            Id = tarefa.Id,
            Prazo = tarefa.Prazo,
            Status = tarefa.Status,
            Prioridade = tarefa.Prioridade,
            DataInicio = tarefa.DataInicio,
            Responsaveis = tarefa.Responsaveis.Select(r => r.Id).ToList(),
            Titulo = tarefa.Titulo,
        };
    }
    
}