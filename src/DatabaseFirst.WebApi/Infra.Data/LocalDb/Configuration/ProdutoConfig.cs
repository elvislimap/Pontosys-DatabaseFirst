using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Configuration
{
    public class ProdutoConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.ProdutoId);
            
            builder.Property(p => p.ProdutoId).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasColumnName("Nome").HasColumnType("varchar(100)").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Valor).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(p => p.Observacao).HasColumnType("varchar(200)").HasMaxLength(200);
            builder.Property(p => p.Apagado).HasColumnType("bit").HasDefaultValue(0).IsRequired();

            builder.ToTable("Produto");
        }
    }
}