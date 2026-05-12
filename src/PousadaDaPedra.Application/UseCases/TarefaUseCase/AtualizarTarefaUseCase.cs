using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class AtualizarTarefaUseCase
{
    private readonly ITarefaRepository _tarefaRepository;
    private readonly IUnitOfWork _ofWork;
    public AtualizarTarefaUseCase(ITarefaRepository tarefaRepository, IUnitOfWork ofWork)
    {
        _tarefaRepository = tarefaRepository;
        _ofWork = ofWork;
    }

    public async Task<TarefaResponseDTO> Execute(AtualizarRequestDTO dto)
    {
        Tarefa? tarefa = await _tarefaRepository.BuscarPorId(dto.Id, true);
        if (tarefa == null)
            throw new ArgumentException("Tarefa inexistente");
        
        if(!String.IsNullOrWhiteSpace(dto.Descricao))
           tarefa.AlterarDescricao(dto.Descricao);
        if(dto.Prioridade != null)
            tarefa.AlterarPrioridade(dto.Prioridade.Value);
        if(dto.Dificuldade != null)
            tarefa.AlterarDificuldade(dto.Dificuldade.Value);
        if(dto.Prazo != null)
            tarefa.AlterarPrazo(dto.Prazo.Value);

        await _ofWork.Commit();

        return new TarefaResponseDTO()
        {
            Id = tarefa.Id,
            DataInicio = tarefa.DataInicio,
            DataTermino = tarefa.DataTermino,
            Descricao = tarefa.Descricao,
            Dificuldade = tarefa.Dificuldade,
            Prazo = tarefa.Prazo,
            Prioridade = tarefa.Prioridade,
            Responsaveis = tarefa.Responsaveis.Select(i => i.Id).ToList(),
            Titulo = tarefa.Titulo,
        };
    }
}