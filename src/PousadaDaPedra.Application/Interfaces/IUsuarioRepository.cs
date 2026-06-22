using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;

namespace PousadaDaPedra.Application.Interfaces;

public interface IUsuarioRepository
{
    Task<List<Usuario>> BuscarPorListaIds(List<int> ids, Cargo? cargo);
    Task<Usuario> BuscarPorId(int id);
    Task<Usuario> BuscarPorEmail(string email);
    void Remover(Usuario user);
    Task<List<Usuario>> ListarUsuarios(bool gerente = false);
    Task Salvar(Usuario usuario);
}