using Moq;
using PousadaDaPedra.Application.DTOs.UserDTO;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Application.UseCases.UsuerUseCase;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedraApi.Tests.UserTests;

public class CriarUserTest
{
    private readonly Mock<IUsuarioRepository> _userMock = new();
    private readonly Mock<IPasswordHasher> _hashMock = new();
    private readonly Mock<IUnitOfWork> _unitMock = new();

    [Fact]
    public async Task DeveSalvarUsuario_QuandoDadosValidos()
    {
        //Arranges
        var userName = "Teste da Silva";
        var userEmail = "teste@gmail.com";
        var passoword = "Teste$123";
        var userCargo = Cargo.Gerente;
        
        _hashMock.Setup(x => 
                x.SenhaHash(It.IsAny<string>()))
            .Returns("Hash_Fake");

        _userMock.Setup(x => x.BuscarPorEmail(userEmail)).ReturnsAsync((Usuario?) null);

        var dto = new CriarUserRequest()
        {
            Nome = userName,
            Email = userEmail,
            Senha = passoword,
            Cargo = userCargo
        };

        var userUseCase = new CriarUserUseCase(
            _userMock.Object,
            _unitMock.Object,
            _hashMock.Object
        );

        //Act
        await userUseCase.Execute(dto);
        
        //Assert
        _userMock.Verify(x => 
                x.Salvar(It.IsAny<Usuario>()),
                Times.Once);
        
        _unitMock.Verify(x => x.Commit(),
            Times.Once);

    }

    [Fact]
    public async Task DeveLancarExcecao_QuandoUsuarioExistir()
    {
        //Arranges
        var userName = "Teste da Silva";
        var userEmail = "teste@gmail.com";
        var passoword = "Teste$123";
        var userCargo = Cargo.Gerente;

        _userMock.Setup(x => x.BuscarPorEmail(userEmail))
            .ReturnsAsync(new Usuario(userName, userEmail, passoword, userCargo));

        _hashMock.Setup(x => x.SenhaHash(It.IsAny<string>())).Returns("HASH");
        
        var dto = new CriarUserRequest()
        {
            Nome = userName,
            Email = userEmail,
            Senha = passoword,
            Cargo = userCargo
        };

        var userUseCase = new CriarUserUseCase(
            _userMock.Object,
            _unitMock.Object,
            _hashMock.Object
        );

        //Act
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => userUseCase.Execute(dto));
        
        //Asserts
        Assert.Equal("Usuario existente", exception.Message);
        _userMock.Verify(x => x.Salvar(It.IsAny<Usuario>()), Times.Never);
        _unitMock.Verify(x => x.Commit(), Times.Never);
    }

}