using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO;

public class CriarRequestDTO
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Prioridade? Prioridade { get; set; }
    public Dificuldade? Dificuldade { get; set; }
    public List<int> Responsaveis { get; set; }
}