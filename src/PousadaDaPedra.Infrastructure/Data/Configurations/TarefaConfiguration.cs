using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Infrastructure.Data.Configurations;

public class TarefaConfiguration : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("Tarefas");

        builder.HasKey(i => i.Id);

        builder.Property(t => t.Titulo)
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(d => d.Descricao)
            .IsRequired()
            .HasColumnType("text");

        // Datas com Timezone (Padrão Postgres)
        builder.Property(d => d.DataInicio).HasColumnType("timestamp with time zone");
        builder.Property(d => d.DataTermino).HasColumnType("timestamp with time zone");
        builder.Property(d => d.Prazo).HasColumnType("timestamp with time zone");

        //Convertendo os Enums como Strings no banco
        builder.Property(e => e.Prioridade).HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Dificuldade).HasConversion<string>().HasMaxLength(30);
        builder.Property(e => e.Status).HasConversion<string>().HasMaxLength(30);
        
        
        // Relacionamento Muitos-para-Muitos
        builder.HasMany(r => r.Responsaveis)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TarefaResponsaveis",
                j => j.HasOne<Usuario>()
                    .WithMany()
                    .HasForeignKey("usuarioId"),
                j => j.HasOne<Tarefa>()
                    .WithMany()
                    .HasForeignKey("tarefaId")
            );
    }
}