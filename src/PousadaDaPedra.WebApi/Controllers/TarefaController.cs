using Microsoft.AspNetCore.Authorization;
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
    private readonly AtualizarTarefaUseCase _atualizarTarefaUseCase;
    private readonly ReabrirUseCase _reabrirUseCase;
    public TarefaController(
        CriarTarefa criarTarefa
        ,FinalizarTarefa finalizarTarefa
        ,IniciarTarefa iniciarTarefa
        ,ListarTarefasUseCase listarTarefasUseCase
        ,AtualizarTarefaUseCase atualizarTarefaUseCase
        ,ReabrirUseCase reabrirUseCase
        )
    {
        _criarTarefa = criarTarefa;
        _finalizarTarefa = finalizarTarefa;
        _iniciarTarefa = iniciarTarefa;
        _listarTarefas = listarTarefasUseCase;
        _atualizarTarefaUseCase = atualizarTarefaUseCase;
        _reabrirUseCase = reabrirUseCase;
    }
    
    
    [HttpPost("criar")]
    public async Task<IActionResult> CriarTarefa([FromBody] CriarRequestDTO dto)
    {
        var tarefa = await _criarTarefa.Execute(dto);
        return Ok(tarefa);
    }
    
        //var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
    [HttpPut("finalizar")]
    public async Task<IActionResult> FinalizarTarefa([FromBody] FinalizarRequestDTO dto)
    {
        await _finalizarTarefa.Execute(dto.Id);
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


    [HttpPut("atualizar")]
    public async Task<IActionResult> Atualizar([FromBody] AtualizarRequestDTO dto)
    {
        var tarefa = await _atualizarTarefaUseCase.Execute(dto);
        return Ok(tarefa);
    }


    [HttpPut("reabrir")]
    public async Task<IActionResult> Reabrir([FromBody] ReabrirRequestDTO dto)
    {
        var tarefa = await _reabrirUseCase.Execute(dto.newPrazo, dto.Id);
        return Ok(tarefa);
    }

}