using Moq;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;
using PousadaDaPedraApi.Tests.Fixtures;

namespace PousadaDaPedraApi.Tests.TarefasTests;

public class AtualizarTarefaTest : IClassFixture<TarefaTestFixture>
{
    private readonly Mock<ITarefaRepository> _mockTarefa = new();
    private readonly Mock<IUnitOfWork> _mockUnit = new();
    private readonly TarefaTestFixture _fixture;

    public AtualizarTarefaTest(TarefaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData(25)]
    public async Task DeveRetornarTarefa_QuandoOsDadosForemValidosETarefaEmAndamento(int id)
    {
        var tarefaFake = _fixture.CriarTarefaValida();
        

        _mockTarefa.Setup(x =>
            x.BuscarPorId(id, true)).ReturnsAsync(tarefaFake);

        var dto = _fixture.AtualizarTarefaDTOValido(id);
        var useCase = new AtualizarTarefaUseCase(_mockTarefa.Object, _mockUnit.Object);

        var tarefa = await useCase.Execute(dto);
        
        Assert.Equal(Dificuldade.Facil, tarefa.Dificuldade);
        Assert.Equal(Prioridade.Baixa, tarefa.Prioridade);
        Assert.Equal("descricao nova teste", tarefa.Descricao);
        
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Once);

    }
    
    [Theory]
    [InlineData(5)]
    public async Task DeveRetornarTarefa_QuandoOsDadosForemValidosETarefaIniciada(int id)
    {
        var tarefaFake = _fixture.CriarTarefaValidaIniciada();
        

        _mockTarefa.Setup(x =>
            x.BuscarPorId(id, true)).ReturnsAsync(tarefaFake);

        var dto = _fixture.AtualizarTarefaDTOValido(id);
        var useCase = new AtualizarTarefaUseCase(_mockTarefa.Object, _mockUnit.Object);
        dto.Prioridade = Prioridade.Media;
        var tarefa = await useCase.Execute(dto);
        
        Assert.Equal(Dificuldade.Facil, tarefa.Dificuldade);
        Assert.Equal(Prioridade.Media, tarefa.Prioridade);
        Assert.Equal("descricao nova teste", tarefa.Descricao);
        
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Once);

    }
    
    [Theory]
    [InlineData(5)]
    public async Task LancaExcecao_QuandoTarefaForNula(int id)
    {
        _mockTarefa.Setup(x =>
            x.BuscarPorId(id, true)).ReturnsAsync((Tarefa?) null);

        var dto = _fixture.AtualizarTarefaDTOValido(id);
        var useCase = new AtualizarTarefaUseCase(_mockTarefa.Object, _mockUnit.Object);
        dto.Prioridade = Prioridade.Media;

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Tarefa inexistente", exception.Message);
        
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Never);
    }
}