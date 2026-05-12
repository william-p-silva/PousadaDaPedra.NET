using Microsoft.EntityFrameworkCore;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Infrastructure.Data.Context;

namespace PousadaDaPedra.Infrastructure.Data.Repositories;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Tarefa>> ListarTarefas()
    {
        return await _context.Tarefas.AsNoTracking().ToListAsync();
    }

    public async Task<Tarefa?> BuscarPorId(int id)
    {
        var tarefaId = await _context.Tarefas
            .Include(r => r.Responsaveis)
            .FirstOrDefaultAsync(i => i.Id == id);
        
        return tarefaId;
    }

    public void RemoverPorId(Tarefa tarefa)
    {
        _context.Tarefas.Remove(tarefa);
    }

    public async Task Salvar(Tarefa tarefa)
    {
        await _context.Tarefas.AddAsync(tarefa);
    }
}