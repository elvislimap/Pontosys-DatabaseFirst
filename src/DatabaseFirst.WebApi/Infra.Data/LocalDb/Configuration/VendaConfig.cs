using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Configuration
{
    public class VendaConfig : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.HasKey(v => v.VendaId);

            builder.Property(v => v.VendaId).ValueGeneratedOnAdd();
            builder.Property(v => v.Valor).HasColumnType("decimal(10,2)").IsRequired();
            builder.Property(v => v.DataHora).HasColumnType("datetime").IsRequired();

            builder.ToTable(nameof(Venda));
        }
    }
}