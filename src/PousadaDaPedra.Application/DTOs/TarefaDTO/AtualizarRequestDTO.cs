using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO;

public class AtualizarRequestDTO
{
    public int Id { get; set; }
    public string? Descricao { get; set; }
    public Prioridade? Prioridade { get; set; } = null;
    public Dificuldade? Dificuldade { get; set; } = null;
    public DateTime? Prazo { get; set; } = null;
}