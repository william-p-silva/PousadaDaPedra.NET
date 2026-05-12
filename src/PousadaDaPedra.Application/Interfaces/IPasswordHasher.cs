namespace PousadaDaPedra.Application.Interfaces;

public interface IPasswordHasher
{
    string SenhaHash(string senha);
    bool VerificarSenha(string senha, string hash);
}