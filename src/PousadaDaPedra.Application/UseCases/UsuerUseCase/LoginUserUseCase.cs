using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;

namespace PousadaDaPedra.Application.UseCases.UsuerUseCase;

public class LoginUserUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService _tokenService;

    public LoginUserUseCase(IUsuarioRepository usuarioRepository
        ,IPasswordHasher hasher
        ,ITokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _hasher = hasher;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDTO> Execute(LoginRequestDTO dto)
    {
        if (String.IsNullOrWhiteSpace(dto.Senha))
            throw new ArgumentException("é necessario digitar a senha");
        if (String.IsNullOrWhiteSpace(dto.Email)) 
            throw new ArgumentException("é necessario digitar o email");

        var user = await _usuarioRepository.BuscarPorEmail(dto.Email);
        if (user == null)
            throw new ArgumentException("Usuario inexistente");
        
        bool senhaValida = _hasher.VerificarSenha(dto.Senha, user.SenhaHash);
        if (!senhaValida)
            throw new ArgumentException("Senhas incompativeis");
        
        var token = _tokenService.GerarToken(user);

        return new LoginResponseDTO()
        {
            Email = user.Email,
            Cargo = user.Cargo.ToString(),
            Nome = user.Nome,
            Token = token,
        };

    }
}