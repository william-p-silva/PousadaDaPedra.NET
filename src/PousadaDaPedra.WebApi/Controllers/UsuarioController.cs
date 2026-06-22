using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PousadaDaPedra.Application.DTOs.ResponseDTO;
using PousadaDaPedra.Application.DTOs.TarefaDTO;
using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.UseCases.UsuerUseCase;

namespace PousadaDaPedra.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly CriarUserUseCase _criarUserUseCase;
    private readonly LoginUserUseCase _loginUserUseCase;
    private readonly ListarUserUseCase _listarUserUseCase;
    private readonly ListarAllUsuariosUseCase _listarAllUsuariosUseCase;
    public UsuarioController(CriarUserUseCase criarUserUseCase, LoginUserUseCase loginUserUseCase, ListarUserUseCase listarUserUseCase, ListarAllUsuariosUseCase listarAllUsuariosUseCase)
    {
        _criarUserUseCase = criarUserUseCase;
        _loginUserUseCase = loginUserUseCase;
        _listarUserUseCase = listarUserUseCase;
        _listarAllUsuariosUseCase = listarAllUsuariosUseCase;
    }

    [HttpPost("criar")]
    public async Task<IActionResult> CriarUser(CriarUserRequest dto)
    {
        await _criarUserUseCase.Execute(dto);
        return Ok(new SuccessApiDTO<string>()
        {
            Success = true,
            Data = "Usuário cadastrado :)"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDTO dto)
    {
        var user = await _loginUserUseCase.Execute(dto);

        Response.Cookies.Append(
            "auth_token",
            user.Token,
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddHours(2)
            }
        );

        return Ok(new SuccessApiDTO<Object>
        {
            Success = true,
            Data = new
            {
                user.Email,
                user.Nome,
                user.Cargo
            },
        });
    }

    [HttpPost("login/next")]
    public async Task<IActionResult> LoginNext(LoginRequestDTO dto)
    {
        var user = await _loginUserUseCase.Execute(dto);

        return Ok(new SuccessApiDTO<LoginResponseDTO>
        {
            Success = true,
            Data = user
        });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("auth_token");
        return Ok(new SuccessApiDTO<string>
        {
            Success = true,
            Data = "Logout realizado com sucesso"
        });
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult Me()
    {
        var nome = User.FindFirst(ClaimTypes.Name)?.Value;
        var email = User.FindFirst(ClaimTypes.Email)?.Value;
        var cargo = User.FindFirst(ClaimTypes.Role)?.Value;

        return Ok(new SuccessApiDTO<object>
        {
            Success = true,
            Data = new
            {
                Nome = nome,
                Email = email,
                Cargo = cargo
            }
        });
    }


    [HttpGet("listar")]
    public async Task<IActionResult> ListarUsuarios([FromQuery] bool gerente = false)
    {
        var usuarios = await _listarAllUsuariosUseCase.Execute(gerente);

        return Ok(new SuccessApiDTO<List<UserResponseDTO>>()
        {
            Data = usuarios,
            Success = true,
        });
    }


    [HttpGet("listar/{id:int}")]
    public async Task<IActionResult> ListarPorIds(int id)
    {
        var user = await _listarUserUseCase.Execute(id);

        return Ok(new SuccessApiDTO<UserResponseDTO>()
        {
            Data = user,
            Success = true,
        });
    }
}