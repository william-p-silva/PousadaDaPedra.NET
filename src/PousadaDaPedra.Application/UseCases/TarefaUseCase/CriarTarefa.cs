using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.UseCases.TarefaUseCase;

public class CriarTarefa
{
    protected readonly ITarefaRepository _repositoryTarefa;
    protected readonly IUsuarioRepository _repositoryUser;

    public CriarTarefa(ITarefaRepository repository, IUsuarioRepository repositoryUser)
    {
        _repositoryTarefa = repository;
        _repositoryUser = repositoryUser;
    }
    
    
    public async Task<CriarResponseDTO> Execute(CriarRequestDTO dto)
    {
        if (dto.Responsaveis.Count == 0)
            throw new ArgumentException("Erro é necessario pelo menos um Responsavel");
        
        List<Usuario> UsuariosResponsaveis = new();
       
        var users = await _repositoryUser.BuscarPorListaIds(dto.Responsaveis);
        UsuariosResponsaveis.AddRange(users);
        
        if (UsuariosResponsaveis.Count == 0)
            throw new ArgumentException("Erro é necessario pelo menos um Responsavel");
        
        var tarefa = new Tarefa(dto.Titulo, dto.Descricao, dto.Prioridade, dto.Dificuldade, UsuariosResponsaveis);
        
        await _repositoryTarefa.Salvar(tarefa);
        
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