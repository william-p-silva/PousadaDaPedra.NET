using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;

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

    public async Task<FinalizarResponseDTO> Execute(FinalizarRequestDTO dto)
    {
        var tarefa = await _repository.BuscarPorId(dto.Id, false);
        if (tarefa == null)
            throw new ArgumentException("Tarefa Inexistente");

        tarefa.Finalizar();
        await _iUnitOfWork.Commit();

        return new FinalizarResponseDTO
        {
            DataInicio = tarefa?.DataInicio,
            DataTermino = tarefa?.DataTermino,
            Descricao = tarefa?.Descricao ?? "Sem descrição",
            Status = tarefa.Status,
            Id = tarefa.Id,
            Titulo = tarefa.Titulo
        };
    }
}