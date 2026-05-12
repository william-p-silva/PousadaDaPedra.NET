using Microsoft.EntityFrameworkCore;
using PousadaDaPedra.Application.Interfaces;
using PousadaDaPedra.Domain.Entity;
using PousadaDaPedra.Domain.Enums;
using PousadaDaPedra.Infrastructure.Data.Context;

namespace PousadaDaPedra.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Usuario>> BuscarPorListaIds(List<int> ids, Cargo? cargo)
    {
        var query = _context.Usuarios.AsQueryable();
        if (cargo != null)
            query = query.Where(c => c.Cargo == cargo);
        
        return await query.Where(u => ids.Contains(u.Id))
            .ToListAsync();
    }

    public async Task<Usuario?> BuscarPorId(int id)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario> BuscarPorEmail(string email)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == email);
    }

    public void Remover(Usuario user)
    {
        _context.Usuarios.Remove(user);
    }

    public async Task<List<Usuario>> ListarUsuarios()
    {
        return await _context.Usuarios.AsNoTracking().ToListAsync();
    }

    public async Task Salvar(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }
}