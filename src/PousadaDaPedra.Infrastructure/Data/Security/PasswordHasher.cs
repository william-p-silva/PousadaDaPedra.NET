using PousadaDaPedra.Application.Interfaces;
using BCrypt.Net;
namespace PousadaDaPedra.Infrastructure.Data.Security;

public class PasswordHasher : IPasswordHasher
{
    public string SenhaHash(string senha)
    {
        return BCrypt.Net.BCrypt.HashPassword(senha);
    }

    public bool VerificarSenha(string senha, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(senha, hash);
    }
}