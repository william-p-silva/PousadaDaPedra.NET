using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.UsuerUseCase;

public class ListarUserUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ListarUserUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UserResponseDTO> Execute(int id)
    {
        var user = await _usuarioRepository.BuscarPorId(id);
        if (user == null)
            throw new ArgumentException("Usuario não encontrado");
        
        return new UserResponseDTO()
        {
            Nome = user.Nome,
            Cargo = user.Cargo.ToString(),
            Email = user.Email,
            Id = user.Id
        };
    }
}