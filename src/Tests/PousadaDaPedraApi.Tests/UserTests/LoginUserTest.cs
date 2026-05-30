using Moq;
using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.UsuerUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedraApi.Tests.UserTests;

public class LoginUserTest
{
    private readonly Mock<IUsuarioRepository> _userMock = new();
    private readonly Mock<IPasswordHasher> _hashMock = new();
    private readonly Mock<ITokenService> _tokenMock = new();


    [Fact]
    public async Task DeveRetornarUsuario_QuandoAsCredenciasForemValidas()
    {
        var userEmail = "teste@gmail.com";
        var senha = "teste$123";
        var senhaHash = "TesteHash";
        var tokenGerado = "Token_JWT_Exemplo";
        var cargoEsperado = Cargo.Funcionario;

        var userFake = new Usuario("teste", "teste", senhaHash, Cargo.Funcionario);

        _userMock.Setup(x => 
            x.BuscarPorEmail(userEmail))
            .ReturnsAsync(userFake);

        _hashMock.Setup(x => x.VerificarSenha(senha, senhaHash)).Returns(true);

        _tokenMock.Setup(x => x.GerarToken(userFake)).Returns(tokenGerado);

        var dto = new LoginRequestDTO()
        {
            Email = userEmail,
            Senha = senha
        };

        var useCase = new LoginUserUseCase(
            _userMock.Object,
            _hashMock.Object,
            _tokenMock.Object
            );

        var userResponse = await useCase.Execute(dto);

        Assert.NotNull(userResponse);
        Assert.Equal(tokenGerado, userResponse.Token);
        Assert.Equal(cargoEsperado.ToString().ToLower(), userResponse.Cargo);

        _tokenMock.Verify(x => x.GerarToken(It.IsAny<Usuario>()), Times.Once);
    }

    [Fact]
    public async Task DeveRetornarExcecao_QuandoNaoExistirUsuario()
    {
        var userEmail = "teste@gmail.com";
        var senha = "teste$123";
        
        _userMock.Setup(x => x.BuscarPorEmail(userEmail)).ReturnsAsync((Usuario?) null);
        
        var dto = new LoginRequestDTO()
        {
            Email = userEmail,
            Senha = senha
        };

        var useCase = new LoginUserUseCase(
            _userMock.Object,
            _hashMock.Object,
            _tokenMock.Object
        );
        
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Usuario inexistente", exception.Message);
        _userMock.Verify(x => x.BuscarPorEmail(userEmail), Times.Once);
        
        
    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoSenhaForVazia()
    {
        var userEmail = "teste@gmail.com";
        var senha = " ";
        var senhaHash = "TesteHash";
        
        var userFake = new Usuario("teste", "teste", senhaHash, Cargo.Funcionario);
        _userMock.Setup(x => x.BuscarPorEmail(userEmail)).ReturnsAsync(userFake);
        _hashMock.Setup(x => x.VerificarSenha(senha, userFake.SenhaHash)).Returns(false);
        
        var dto = new LoginRequestDTO()
        {
            Email = userEmail,
            Senha = senha
        };

        var useCase = new LoginUserUseCase(
            _userMock.Object,
            _hashMock.Object,
            _tokenMock.Object
        );

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("é necessario digitar a senha", exception.Message);
        _tokenMock.Verify(x => x.GerarToken(userFake), Times.Never);

    }
    
    [Fact]
    public async Task DeveLancarExcecao_QuandoSenhaForInvalida()
    {
        var userEmail = "teste@gmail.com";
        var senha = "Teste$123";
        var senhaHash = "TesteHash";
        
        var userFake = new Usuario("teste", "teste", senhaHash, Cargo.Funcionario);
        _userMock.Setup(x => x.BuscarPorEmail(userEmail)).ReturnsAsync(userFake);
        _hashMock.Setup(x => x.VerificarSenha(senha, userFake.SenhaHash)).Returns(false);
        
        var dto = new LoginRequestDTO()
        {
            Email = userEmail,
            Senha = senha
        };

        var useCase = new LoginUserUseCase(
            _userMock.Object,
            _hashMock.Object,
            _tokenMock.Object
        );

        var exception = await Assert.ThrowsAsync<ArgumentException>(() => useCase.Execute(dto));
        
        Assert.Equal("Senhas incompativeis", exception.Message);
        _tokenMock.Verify(x => x.GerarToken(userFake), Times.Never);

    }
    
}