using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.TarefaDTO;

public class FinalizarResponseDTO
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public Status Status { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataTermino { get; set; }
}