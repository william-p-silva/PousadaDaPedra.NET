using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase
{
    public class ListarTarefasUseCase
    {
        private readonly ITarefaRepository _tarefaRepository;
        public ListarTarefasUseCase(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public async Task<List<TarefaResponseDTO>> Execute(bool responsavel)
        {
            var tarefas = await _tarefaRepository.ListarTarefas(responsavel);
            return tarefas.Select(t => new TarefaResponseDTO
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                Prioridade = t.Prioridade,
                Responsaveis = t.Responsaveis?.Select(r => r.Id).ToList(),
                Dificuldade = t.Dificuldade,
                DataInicio = t.DataInicio,
                DataTermino = t.DataTermino,
                Prazo = t.Prazo
            }).ToList();
        }

        public async Task<TarefaResponseDTO> ExecuteBuscaId(int id, bool responsavel)
        {
            var tarefa = await _tarefaRepository.BuscarPorId(id, responsavel);
            if (tarefa == null)
                throw new Exception("Tarefa não encontrada");
            return new TarefaResponseDTO
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Prioridade = tarefa.Prioridade,
                Responsaveis = tarefa.Responsaveis?.Select(r => r.Id).ToList(),
                Dificuldade = tarefa.Dificuldade,
                DataInicio = tarefa.DataInicio,
                DataTermino = tarefa.DataTermino,
                Prazo = tarefa.Prazo
            };
        }
    }
}
