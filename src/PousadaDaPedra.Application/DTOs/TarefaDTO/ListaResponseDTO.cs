using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO;

public class ListaResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade Prioridade { get; set; }
    public Dificuldade Dificuldade { get; set; }
    public List<int> Responsaveis { get; set; }
    public DateTime? DataInicio { get; private set; }
    public DateTime? DataTermino { get; private set; }
    public DateTime? Prazo { get; private set; }
}