using Moq;
using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;


namespace PousadaDaPedraApi.Tests.TarefasTests;

public class CriarTarefaTest
{
    private readonly Mock<ITarefaRepository> _mockTarefa = new();
    private readonly Mock<IUsuarioRepository> _mockUser = new();
    private readonly Mock<IUnitOfWork> _mockUnit = new();

    [Fact]
    public async Task DeveRetornarTarefa_QuandoOsDadosForemValidos()
    {
        var titulo = "teste";
        var descricao = "descricao teste";
        var prioridade = Prioridade.Baixa;
        var dificuldade = Dificuldade.Facil;
        var users = new List<int>(){1, 5, 9};
        var listaUsers = new List<Usuario>()
        {
            new Usuario(nome: "teste", email: "email1", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "silva", email: "email2", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "sandro", email: "email3", senha: "HASH", cargo: Cargo.Gerente)
        };
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync((listaUsers));
        
        var dto = new CriarRequestDTO()
        {
            Titulo = titulo,
            Descricao = descricao,
            Dificuldade = dificuldade,
            Prioridade = prioridade,
            Responsaveis = users
        };

        var useCase = new CriarTarefa(
            _mockTarefa.Object,
            _mockUser.Object,
            _mockUnit.Object
        );

        var tarefa = await useCase.Execute(dto);
        
        Assert.NotNull(tarefa);
        Assert.Equal(titulo, tarefa.Titulo);
        Assert.Equal(descricao, tarefa.Descricao);
        Assert.Equal(prioridade, tarefa.Prioridade);
        Assert.Equal(dificuldade, tarefa.Dificuldade);
        _mockUser.Verify(x => x.BuscarPorListaIds(users, Cargo.Gerente), Times.Once);
        _mockTarefa.Verify(x => x.Salvar(It.IsAny<Tarefa>()), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Once);
        
    }
    
    [Fact]
    public async Task DeveRetornarExcecao_QuandoUmOuMaisDosResponsaveisForInvalido()
    {
        var titulo = "teste";
        var descricao = "descricao teste";
        var prioridade = Prioridade.Baixa;
        var dificuldade = Dificuldade.Facil;
        var users = new List<int>(){1, 5, 9, 22, 12};
        var listaUsers = new List<Usuario>()
        {
            new Usuario(nome: "teste", email: "email1", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "silva", email: "email2", senha: "HASH", cargo: Cargo.Gerente),
            new Usuario(nome: "sandro", email: "email3", senha: "HASH", cargo: Cargo.Gerente)
        };
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync((listaUsers));
        
        var dto = new CriarRequestDTO()
        {
            Titulo = titulo,
            Descricao = descricao,
            Dificuldade = dificuldade,
            Prioridade = prioridade,
            Responsaveis = users
        };

        var useCase = new CriarTarefa(
            _mockTarefa.Object,
            _mockUser.Object,
            _mockUnit.Object
        );

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Erro um dos Responsaveis era invalido", exception.Message);
        _mockUser.Verify(x => x.BuscarPorListaIds(users, Cargo.Gerente), Times.Once);
        _mockTarefa.Verify(x => x.Salvar(It.IsAny<Tarefa>()), Times.Never);
        _mockUnit.Verify(x => x.Commit(), Times.Never);
        
    }

    [Fact]
    public async Task DeveRetornarExcecao_QuandoOsResponsaveisForemInvalidos()
    {
        var titulo = "teste";
        var descricao = "descricao teste";
        var prioridade = Prioridade.Baixa;
        var dificuldade = Dificuldade.Facil;
        var users = new List<int>(){1, 5, 9, 7};
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync((List<Usuario>?)null);

        var dto = new CriarRequestDTO()
        {
            Titulo = titulo,
            Descricao = descricao,
            Dificuldade = dificuldade,
            Prioridade = prioridade,
            Responsaveis = users
        };

        var useCase = new CriarTarefa(
            _mockTarefa.Object,
            _mockUser.Object,
            _mockUnit.Object
        );

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Erro um dos Responsaveis era invalido", exception.Message);
        _mockTarefa.Verify(x => x.Salvar(It.IsAny<Tarefa>()), Times.Never);
        _mockUnit.Verify(x => x.Commit(), Times.Never);
        _mockUser.Verify(x => x.BuscarPorListaIds(users, Cargo.Gerente), Times.Once);

    }

    [Fact]
    public async Task DeveRetornarExcecao_QuandoONumeroDeResponsaveisFomInvalidos()
    {
        var titulo = "teste";
        var descricao = "descricao teste";
        var prioridade = Prioridade.Baixa;
        var dificuldade = Dificuldade.Facil;
        var users = new List<int>(){};
        
        var dto = new CriarRequestDTO()
        {
            Titulo = titulo,
            Descricao = descricao,
            Dificuldade = dificuldade,
            Prioridade = prioridade,
            Responsaveis = users
        };

        var useCase = new CriarTarefa(
            _mockTarefa.Object,
            _mockUser.Object,
            _mockUnit.Object
        );

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Erro é necessario pelo menos um Responsavel", exception.Message);
        _mockTarefa.Verify(x => x.Salvar(It.IsAny<Tarefa>()), Times.Never);
        _mockUnit.Verify(x => x.Commit(), Times.Never);
        _mockUser.Verify(x => x.BuscarPorListaIds(users, Cargo.Gerente), Times.Never);
    }
}