using Moq;
using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;
using PousadaDaPedraApi.Tests.Fixtures;

namespace PousadaDaPedraApi.Tests.TarefasTests;

public class IniciarTarefaTests : IClassFixture<TarefaTestFixture>
{
    private readonly Mock<ITarefaRepository> _tarefaMock = new();
    private readonly Mock<IUnitOfWork> _commitMock = new();
    private readonly TarefaTestFixture _fixture;

    public IniciarTarefaTests(TarefaTestFixture fixture)
    {
        _fixture = fixture;
    }


    [Theory]
    [InlineData(1)]
    [InlineData(25)]
    public async Task DeveRetornarTarefaIniciada_QuandoOsDadosForemValidos(int id)
    {
        var prazo = DateTime.UtcNow.AddDays(2);
        var tarefaFake = _fixture.CriarTarefaValida();
        var dto = _fixture.CriarIniciarDtoRequest(id, prazo);
        _tarefaMock.Setup(x => 
            x.BuscarPorId(dto.Id, true)).ReturnsAsync(tarefaFake);

        var useCase = new IniciarTarefa(_tarefaMock.Object, _commitMock.Object);

        var tarefa = await useCase.Execute(dto);
        
        Assert.Equal(tarefaFake.Titulo, tarefa.Titulo);
        Assert.Equal(tarefaFake.Prazo, tarefa.Prazo);
        Assert.Equal(tarefaFake.Descricao, tarefa.Descricao);
        _tarefaMock.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _commitMock.Verify(x => x.Commit(), Times.Once);
        
    }
    
    [Theory]
    [InlineData(5)]
    [InlineData(285)]
    public async Task DeveRetornarExcecao_QuandoOsDadosForemInvalidos(int id)
    {
        var data = DateTime.UtcNow.AddDays(2);
        var listaUsers = _fixture.CriarListaDeUsersValidos();

        var dto = _fixture.CriarIniciarDtoRequest(id, data);

        var tarefaFake = _fixture.CriarTarefaValida();

        _tarefaMock.Setup(x => 
            x.BuscarPorId(dto.Id, true)).ReturnsAsync((Tarefa?) null);

        var useCase = new IniciarTarefa(_tarefaMock.Object, _commitMock.Object);

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Tarefa não encontrada", exception.Message);
        
        _tarefaMock.Verify(x => 
            x.BuscarPorId(It.IsAny<int>(), true), Times.Once);
        _commitMock.Verify(x => x.Commit(), Times.Never);
        
    }

}