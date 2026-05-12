using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class CriarTarefa
{
    private readonly ITarefaRepository _repositoryTarefa;
    private readonly IUsuarioRepository _repositoryUser;
    private readonly IUnitOfWork _iUnitOfWork;

    public CriarTarefa(ITarefaRepository repositoryTarefa,
        IUsuarioRepository repositoryUser, IUnitOfWork iUnitOfWork)
    {
        _repositoryTarefa = repositoryTarefa;
        _repositoryUser = repositoryUser;
        _iUnitOfWork = iUnitOfWork;
    }

    
    
    public async Task<CriarResponseDTO> Execute(CriarRequestDTO dto)
    {
        if (dto.Responsaveis == null || dto.Responsaveis.Count == 0)
            throw new ArgumentException("Erro é necessario pelo menos um Responsavel");
       
        var users = await _repositoryUser
            .BuscarPorListaIds(dto.Responsaveis, Cargo.Gerente);
        
        if (users.Count == 0 || users.Count != dto.Responsaveis.Count)
            throw new ArgumentException("Erro um dos Responsaveis era invalido");
        
        
        
        var tarefa = new Tarefa(dto.Titulo, dto.Descricao,
            dto.Prioridade, dto.Dificuldade,
            users);
        
        await _repositoryTarefa.Salvar(tarefa);
        await _iUnitOfWork.Commit();
        
        return new CriarResponseDTO()
        {
            Id = tarefa.Id,
            Responsaveis = tarefa.Responsaveis.Select(i => i.Id).ToList(),
            Dificuldade = tarefa.Dificuldade,
            Titulo = tarefa.Titulo,
            Descricao = tarefa.Descricao,
            Prioridade = tarefa.Prioridade,
        };
    }
}