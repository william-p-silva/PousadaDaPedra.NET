using Microsoft.AspNetCore.Mvc;
using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.UseCases.TarefaUseCase;

namespace PousadaDaPedra.WebApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly CriarTarefa _criarTarefa;
    private readonly FinalizarTarefa _finalizarTarefa;
    private readonly IniciarTarefa _iniciarTarefa;
    private readonly ListarTarefasUseCase _listarTarefas;
    public TarefaController(CriarTarefa criarTarefa, FinalizarTarefa finalizarTarefa, IniciarTarefa iniciarTarefa, ListarTarefasUseCase listarTarefasUseCase)
    {
        _criarTarefa = criarTarefa;
        _finalizarTarefa = finalizarTarefa;
        _iniciarTarefa = iniciarTarefa;
        _listarTarefas = listarTarefasUseCase;
    }
    
    
    [HttpPost("criar")]
    public async Task<IActionResult> CriarTarefa([FromBody] CriarRequestDTO dto)
    {
        var tarefa = await _criarTarefa.Execute(dto);
        return Ok(tarefa);
    }

    [HttpPut("finalizar")]
    public async Task<IActionResult> FinalizarTarefa([FromBody] FinalizarRequestDTO dto)
    {
        await _finalizarTarefa.Execute(dto);
        return Ok();
    }

    [HttpPut("iniciar")]
    public async Task<IActionResult> IniciarTarefa(IniciarTarefaRequestDTO dto)
    {
        await _iniciarTarefa.Execute(dto);
        return Ok();
    }

    [HttpGet("listar")]
    public async Task<IActionResult> ListarTarefas()
    {
        var tarefas = await _listarTarefas.Execute(true);
        return Ok(tarefas);
    }

    [HttpGet("buscar/{id:int}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var tarefa = await _listarTarefas.ExecuteBuscaId(id, true);
        return Ok(tarefa);
    }

}