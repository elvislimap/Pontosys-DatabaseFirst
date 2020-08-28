using DatabaseFirst.WebApi.Infra.Data.LocalDb.Configuration;
using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb
{
    public class ContextEf : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaItem> VendaItens { get; set; }

        public ContextEf(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(60);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfig());
            modelBuilder.ApplyConfiguration(new VendaConfig());
            modelBuilder.ApplyConfiguration(new VendaItemConfig());
        }
    }
}