using Moq;
using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;
using PousadaDaPedraApi.Tests.Fixtures;


namespace PousadaDaPedraApi.Tests.TarefasTests;

public class CriarTarefaTest : IClassFixture<TarefaTestFixture>
{
    private readonly Mock<ITarefaRepository> _mockTarefa = new();
    private readonly Mock<IUsuarioRepository> _mockUser = new();
    private readonly Mock<IUnitOfWork> _mockUnit = new();
    private readonly TarefaTestFixture _fixture;

    public CriarTarefaTest(TarefaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task DeveRetornarTarefa_QuandoOsDadosForemValidos()
    {
        var users = new List<int>() { 1, 2, 5 };
        var listaUsers = _fixture.CriarListaDeUsersValidos();
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync((listaUsers));

        var dto = _fixture.CriarTarefaDTOValido(users);

        var useCase = new CriarTarefa(
            _mockTarefa.Object,
            _mockUser.Object,
            _mockUnit.Object
        );

        var tarefa = await useCase.Execute(dto);
        
        Assert.NotNull(tarefa);
        Assert.Equal(dto.Titulo, tarefa.Titulo);
        Assert.Equal(dto.Descricao, tarefa.Descricao);
        Assert.Equal(dto.Prioridade, tarefa.Prioridade);
        Assert.Equal(dto.Dificuldade, tarefa.Dificuldade);
        
        _mockUser.Verify(x => x.BuscarPorListaIds(users, Cargo.Gerente), Times.Once);
        _mockTarefa.Verify(x => x.Salvar(It.IsAny<Tarefa>()), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Once);
        
    }
    
    [Fact]
    public async Task DeveRetornarExcecao_QuandoUmOuMaisDosResponsaveisForInvalido()
    {
        var users = new List<int>(){1, 5, 9, 22, 12};
        var dto = _fixture.CriarTarefaDTOValido(users);
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync(_fixture.CriarListaDeUsersValidos());
     
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
        var users = new List<int>(){1, 5, 9, 7};
        var dto = _fixture.CriarTarefaDTOValido(users);   
        
        _mockUser.Setup(x => x.BuscarPorListaIds(users, Cargo.Gerente))
            .ReturnsAsync(new List<Usuario>());

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
        var users = new List<int>(){};
        var dto = _fixture.CriarTarefaDTOValido(users);

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