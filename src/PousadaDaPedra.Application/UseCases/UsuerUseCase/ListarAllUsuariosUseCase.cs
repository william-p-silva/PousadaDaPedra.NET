using System;
using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.UsuerUseCase;

public class ListarAllUsuariosUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    public ListarAllUsuariosUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    public async Task<List<UserResponseDTO>> Execute(bool gerente)
    {
        var usuarios = await _usuarioRepository.ListarUsuarios(gerente);

        return usuarios.Select(x => new UserResponseDTO
        {
            Nome = x.Nome,
            Email = x.Email,
            Id = x.Id,
            Cargo = x.Cargo.ToString()
        }).ToList();
    }
}
