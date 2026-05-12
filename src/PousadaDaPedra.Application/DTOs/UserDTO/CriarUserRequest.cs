using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.DTOs.UserDTO;

public class CriarUserRequest
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public Cargo? Cargo { get; set; }
}