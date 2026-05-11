using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class FinalizarTarefa
{
    protected readonly ITarefaRepository _repository;

    public FinalizarTarefa(ITarefaRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<FinalizarResponseDTO> Execute(FinalizarRequestDTO dto)
    {
        try
        {
            var tarefa = await _repository.BuscarPorId(dto.Id);
            if (tarefa == null)
                throw new ArgumentException("Tarefa Inexistente");
            
            tarefa.Finalizar();
            await _repository.Salvar(tarefa);
            
            return new FinalizarResponseDTO()
            {
                DataInicio = tarefa?.DataInicio,
                DataTermino = tarefa?.DataTermino,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
            };
        }
        catch (Exception ex)
        {
            throw new ArgumentException("ERRO ao tentar finalizar", ex.Message);
        }
    }
}