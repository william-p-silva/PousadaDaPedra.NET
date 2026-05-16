using Moq;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedraApi.Tests.Fixtures;

namespace PousadaDaPedraApi.Tests.TarefasTests;

public class ReabrirTarefaTest : IClassFixture<TarefaTestFixture>
{
    private readonly Mock<ITarefaRepository> _mockTarefa = new();
    private readonly Mock<IUnitOfWork> _mockUnit = new();
    private readonly TarefaTestFixture _fixture;

    public ReabrirTarefaTest(TarefaTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    public async Task DeveReabrir_QuandoOsDadosForemValidos(int id)
    {
        var tarefaFake = _fixture.CriarTarefaValidaFinalizada();

        _mockTarefa.Setup(x => 
            x.BuscarPorId(id, true)).ReturnsAsync(tarefaFake);

        var useCase = new ReabrirUseCase(_mockTarefa.Object, _mockUnit.Object);

        var tarefa = await useCase.Execute(DateTime.UtcNow.AddDays(2), id);

        Assert.NotNull(tarefa);
        Assert.Equal(tarefaFake.DataTermino, null);
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _mockUnit.Verify(x => x.Commit(), Times.Once);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    public async Task LancaExcecao_QuandoTarefaEstiverAberta(int id)
    {
        var tarefaFake = _fixture.CriarTarefaValidaFinalizada();

        _mockTarefa.Setup(x => 
            x.BuscarPorId(id, true)).ReturnsAsync(tarefaFake);

        var useCase = new ReabrirUseCase(_mockTarefa.Object, _mockUnit.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(()
            => useCase.Execute(null, id));

        Assert.Equal("O prazo é obrigatorio", exception.Message);
        _mockTarefa.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Never);
        _mockUnit.Verify(x => x.Commit(), Times.Never);
    }
}