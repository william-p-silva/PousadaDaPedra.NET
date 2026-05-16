using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class FinalizarTarefa
{
    private readonly ITarefaRepository _repository;
    private readonly IUnitOfWork _iUnitOfWork;

    public FinalizarTarefa(ITarefaRepository repository, IUnitOfWork iUnitOfWork)
    {
        _repository = repository;
        _iUnitOfWork = iUnitOfWork;
    }

    public async Task<FinalizarResponseDTO> Execute(int id)
    {
        Tarefa? tarefa = await _repository.BuscarPorId(id, true);
        if (tarefa == null)
            throw new ArgumentException("Tarefa Inexistente");

        tarefa.Finalizar();
        await _iUnitOfWork.Commit();

        return new FinalizarResponseDTO()
        {
            DataInicio = tarefa.DataInicio,
            DataTermino = tarefa.DataTermino,
            Descricao = tarefa.Descricao,
            Status = tarefa.Status,
            Id = tarefa.Id,
            Titulo = tarefa.Titulo
        };
    }
}