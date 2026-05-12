using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<List<Usuario>> BuscarPorListaIds(List<int> ids, Cargo? cargo);
    Task<Usuario> BuscarPorId(int id);
    void Remover(Usuario user);
    Task<List<Usuario>> ListarUsuarios();
    Task Salvar(Usuario usuario);
}