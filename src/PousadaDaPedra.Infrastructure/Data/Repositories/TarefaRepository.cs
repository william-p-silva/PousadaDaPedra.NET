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
    
    public async Task<List<Tarefa>> ListarTarefas(bool responsavel)
    {
        var query = _context.Tarefas.AsNoTracking().AsQueryable();
        if (responsavel)
            query = query.Include(r => r.Responsaveis);
        
        return await query.ToListAsync();
    }

    public async Task<Tarefa?> BuscarPorId(int id, bool responsavel)
    {
        var query = _context.Tarefas.AsQueryable();
        if (responsavel)
            query = query.Include(r => r.Responsaveis);

        return await query.FirstOrDefaultAsync(t => t.Id == id);
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