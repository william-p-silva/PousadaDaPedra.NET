using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.Interfaces;

public interface ITokenService
{
    string GerarToken(Usuario usuario);
}