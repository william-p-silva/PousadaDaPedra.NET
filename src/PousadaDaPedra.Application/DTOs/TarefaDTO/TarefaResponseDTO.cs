using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO;

public class TarefaResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade Prioridade { get; set; }
    public Status Status { get; set; }
    public Dificuldade Dificuldade { get; set; }
    public List<int>? Responsaveis { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
    public DateTime? Prazo { get; set; }
    
}