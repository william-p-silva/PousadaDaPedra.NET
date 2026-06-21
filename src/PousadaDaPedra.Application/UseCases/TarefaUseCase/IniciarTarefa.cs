using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase
{
    public class IniciarTarefa
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IUnitOfWork _unitOfWork;
        public IniciarTarefa(ITarefaRepository tarefaRepository, IUnitOfWork unitOfWork)
        {
            _tarefaRepository = tarefaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TarefaResponseDTO> Execute(IniciarTarefaRequestDTO dto)
        {
            Tarefa tarefa = await _tarefaRepository.BuscarPorId(dto.Id, true);
            if(tarefa == null)            
                throw new ArgumentException("Tarefa não encontrada");

            tarefa.Iniciar(dto.Prazo);
            await _unitOfWork.Commit();
            
            return new TarefaResponseDTO()
            {
                Id = tarefa.Id,
                Prazo = tarefa.Prazo,
                Titulo = tarefa.Titulo,
                DataInicio = tarefa.DataInicio,
                DataTermino = tarefa.DataTermino,
                Descricao = tarefa.Descricao,
                Status = tarefa.Status,
                Dificuldade = tarefa.Dificuldade,
                Prioridade = tarefa.Prioridade,
                Responsaveis = tarefa.Responsaveis.Select( r => new TarefaResponsavelResponseDTO
                {
                    Nome = r.Nome,
                    Email = r.Email,
                    Id = r.Id
                }).ToList()
            };
        }
    }
}
