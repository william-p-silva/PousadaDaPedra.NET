using Microsoft.EntityFrameworkCore;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Infrastructure.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Força o uso de UTC para todos os DateTimes (evita erro de Kind no Postgres)
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", false);
        
        // Esta linha faz a mágica: busca todas as classes que 
        // implementam IEntityTypeConfiguration neste assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}