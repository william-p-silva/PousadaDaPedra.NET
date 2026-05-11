using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PousadaDaPedra.Domain.Entity;

namespace PousadaDaPedra.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(i => i.Id);

        builder.Property(n => n.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(150);
        builder.HasIndex(e => e.Email)
            .IsUnique();

        builder.Property(s => s.Senha)
            .IsRequired()
            .HasMaxLength(255);
        
        // Configurando o Enum como String no banco
        builder.Property(c => c.Cargo)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);
    }
}