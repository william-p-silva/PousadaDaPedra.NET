using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.Interfaces;

public interface ITarefaRepository
{
    Task<List<Tarefa>> ListarTarefas();
    Task<Tarefa> BuscarPorId(int id);
    Task RemoverPorId(int id);
    Task<bool> Salvar(Tarefa tarefa);
}