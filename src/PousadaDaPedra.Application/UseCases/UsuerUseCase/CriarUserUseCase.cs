using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.UseCases.UsuerUseCase;

public class CriarUserUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUnitOfWork _iUnitOfWork;
    private readonly IPasswordHasher _hasher;

    public CriarUserUseCase(IUsuarioRepository usuarioRepository
        ,IUnitOfWork iUnitOfWork
        ,IPasswordHasher hasher)
    {
        _usuarioRepository = usuarioRepository;
        _iUnitOfWork = iUnitOfWork;
        _hasher = hasher;
    }

    public async Task Execute(CriarUserRequest dto)
    {
        if (dto.Senha == null || String.IsNullOrWhiteSpace(dto.Senha))
            throw new ArgumentException("A senha é obrigatorio");

        var senhaHash = _hasher.SenhaHash(dto.Senha);
        
        Usuario user = new Usuario(dto.Nome,
            dto.Email, senhaHash, dto.Cargo);

        await _usuarioRepository.Salvar(user);
        await _iUnitOfWork.Commit();
    }
}