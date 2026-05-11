using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<List<Usuario>> BuscarPorListaIds(List<int> ids);
    Task<Usuario> BuscarPorId(int id);
    Task Remover(int id);
    Task<List<Usuario>> ListarUsuarios();
    Task Salvar(Usuario usuario);
}