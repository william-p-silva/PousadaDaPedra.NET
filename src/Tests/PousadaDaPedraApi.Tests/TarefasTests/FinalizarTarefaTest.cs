using Moq;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;
using PousadaDaPedraApi.Tests.Fixtures;

namespace PousadaDaPedraApi.Tests.TarefasTests;

public class FinalizarTarefaTest : IClassFixture<TarefaTestFixture>
{
    private readonly Mock<ITarefaRepository> _mockTarefa = new();
    private readonly Mock<IUnitOfWork> _mockUnit = new();
    private readonly TarefaTestFixture _fixture;

    public FinalizarTarefaTest(TarefaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    public async Task DeveRetornarTarefa_QuandoOsDadosFormValidos(int id)
    {
        var tarefa = _fixture.CriarTarefaValidaIniciada();

        _mockTarefa.Setup(x => 
            x.BuscarPorId(id, true)).ReturnsAsync(tarefa);

        var useCse = new FinalizarTarefa(_mockTarefa.Object, _mockUnit.Object);

        var tarefaFinalizada = await useCse.Execute(id);
        
        Assert.Equal(Status.Finalizada, tarefaFinalizada.Status);
        Assert.Equal(tarefa.Titulo, tarefaFinalizada.Titulo);
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        
        _mockUnit.Verify(x => x.Commit(), Times.Once);
        
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    public async Task LancaExcecao_QuandoTarefaForInvalida(int id)
    {
        _mockTarefa.Setup(x => 
            x.BuscarPorId(id, true)).ReturnsAsync((Tarefa?)null);
        var useCase = new FinalizarTarefa(_mockTarefa.Object, _mockUnit.Object);
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(id));
        
        Assert.Equal("Tarefa Inexistente", exception.Message);
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Never);

    }
}