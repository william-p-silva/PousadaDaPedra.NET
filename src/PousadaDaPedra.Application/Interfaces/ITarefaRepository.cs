using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.Interfaces;

public interface ITarefaRepository
{
    Task<List<Tarefa>> ListarTarefas();
    Task<Tarefa?> BuscarPorId(int id);
    void RemoverPorId(Tarefa tarefa);
    Task Salvar(Tarefa tarefa);
}