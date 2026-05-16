using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedraApi.Tests.Fixtures;

public class TarefaTestFixture
{
    public List<Usuario> CriarListaDeUsersValidos()
    {
        return new List<Usuario>()
        {
            new Usuario(nome: "teste", email: "email1", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "silva", email: "email2", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "sandro", email: "email3", senha: "HASH", cargo: Cargo.Gerente)

        };
    }

    public IniciarTarefaRequestDTO CriarIniciarDtoRequest(int id, DateTime prazo)
    {
        return new IniciarTarefaRequestDTO()
        {
            Id = id,
            Prazo = prazo,
        };
    }
    
    public Tarefa CriarTarefaValida()
    {
        return new Tarefa(
            titulo: "teste",
            descricao: "teste de descricao",
            responsaveis: CriarListaDeUsersValidos(),
            prioridade: Prioridade.Alta,
            dificuldade: Dificuldade.Dificil
        );
    }
    
    public Tarefa CriarTarefaValidaFinalizada()
    {
        var tarefa = CriarTarefaValidaIniciada();
        tarefa.Finalizar();
        return tarefa;
    }
    
    public Tarefa CriarTarefaValidaIniciada()
    {
        var tarefa = new Tarefa(
            titulo: "teste",
            descricao: "teste de descricao",
            responsaveis: CriarListaDeUsersValidos(),
            prioridade: Prioridade.Baixa,
            dificuldade: Dificuldade.Dificil
        );
        
        tarefa.Iniciar(null);
        return tarefa;
    }
    
    public CriarRequestDTO CriarTarefaDTOValido(List<int> responsaveisIds)
    {
        return new CriarRequestDTO
        {
            Titulo = "teste",
            Descricao = "descricao teste",
            Prioridade = Prioridade.Baixa,
            Dificuldade = Dificuldade.Facil,
            Responsaveis = responsaveisIds
        };
    }
    
    public AtualizarRequestDTO AtualizarTarefaDTOValido(int id)
    {
        return new AtualizarRequestDTO()
        {
            Id = id,
            Descricao = "descricao nova teste",
            Prioridade = Prioridade.Baixa,
            Dificuldade = Dificuldade.Facil,
            Prazo = DateTime.UtcNow.AddDays(2),
        };
    }
    
    
    
}