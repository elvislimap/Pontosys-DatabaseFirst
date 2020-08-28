using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Configuration
{
    public class VendaItemConfig : IEntityTypeConfiguration<VendaItem>
    {
        public void Configure(EntityTypeBuilder<VendaItem> builder)
        {
            builder.HasKey(vi => new { vi.VendaId, vi.ProdutoId });
            builder.HasOne(vi => vi.Venda).WithMany(v => v.VendaItens).HasForeignKey(vi => vi.VendaId);
            builder.HasOne(vi => vi.Produto).WithMany(p => p.VendaItens).HasForeignKey(vi => vi.ProdutoId);

            builder.Property(vi => vi.Quantidade).HasColumnType("int").IsRequired();
            builder.Property(vi => vi.ValorTotal).HasColumnType("decimal(10,2)").IsRequired();

            builder.ToTable(nameof(VendaItem));
        }
    }
}