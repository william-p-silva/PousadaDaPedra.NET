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
    public UsuarioController(CriarUserUseCase criarUserUseCase, LoginUserUseCase loginUserUseCase)
    {
        _criarUserUseCase = criarUserUseCase;
        _loginUserUseCase = loginUserUseCase;
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
        return Ok(new SuccessApiDTO<LoginResponseDTO>()
        {
            Data = user,
            Success = true,
        });
    }
}