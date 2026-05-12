using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Application.Interfaces;

public interface ITarefaRepository
{
    Task<List<Tarefa>> ListarTarefas(bool responsavel);
    Task<Tarefa?> BuscarPorId(int id, bool responsavel);
    void RemoverPorId(Tarefa tarefa);
    Task Salvar(Tarefa tarefa);
}