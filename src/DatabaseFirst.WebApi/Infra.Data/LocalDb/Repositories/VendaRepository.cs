using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseFirst.WebApi.Infra.Data.LocalDb.Extensions;
using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Repositories
{
    public class VendaRepository : IVendaRepository
    {
        private readonly ContextEf _context;

        public VendaRepository(ContextEf context)
        {
            _context = context;
        }

        public void Adicionar(Venda venda)
        {
            _context.Vendas.Add(venda);
            _context.SaveChanges();
        }

        public ICollection<Venda> ObterTodos()
        {
            return _context.Vendas
                .Include(v => v.VendaItens)
                .ToList();
        }

        public Venda ObterPorId(int vendaId)
        {
            return _context.Vendas
                .Include(v => v.VendaItens)
                .FirstOrDefault(v => v.VendaId == vendaId);
        }

        public ICollection<Venda> ObterPorProdutoId(int produtoId)
        {
            return _context.Vendas
                .Join(_context.VendaItens, v => v.VendaId, vi => vi.VendaId,
                    (v, vi) => new { Venda = v, VendaItem = vi })
                .Join(_context.Produtos, join => join.VendaItem.ProdutoId, p => p.ProdutoId,
                    (join, p) => new { Venda = join.Venda, VendaItem = join.VendaItem, Produto = p })
                .Where(join => join.Produto.ProdutoId == produtoId && !join.Produto.Apagado)
                .Select(join => new Venda(join.Venda.VendaId, join.Venda.Valor, join.Venda.DataHora)
                {
                    VendaItens = new List<VendaItem>
                    {
                        new VendaItem(join.VendaItem.VendaId, join.VendaItem.ProdutoId,
                            join.VendaItem.Quantidade, join.VendaItem.ValorTotal) {
                                Produto = new Produto(join.Produto.ProdutoId, join.Produto.Nome, join.Produto.Valor,
                                join.Produto.Observacao, join.Produto.Apagado)
                        }
                    }
                })
                .ToList();
        }

        public ICollection<Venda> ObterPorProdutoIdProcedure(int produtoId)
        {
            return _context.RawSqlQuery($"proc_ObterVendaPorProdutoId {produtoId}",
                null, q =>
                    new Venda(Convert.ToInt32(q["VendaId"]), Convert.ToDecimal(q["Valor"]),
                        Convert.ToDateTime(q["DataHora"]))
                    {
                        VendaItens = new List<VendaItem>
                        {
                            new VendaItem(Convert.ToInt32(q["VendaId"]), Convert.ToInt32(q["ProdutoId"]),
                                Convert.ToInt32(q["Quantidade"]), Convert.ToDecimal(q["ValorTotal"]))
                            {
                                Produto = new Produto(Convert.ToInt32(q["ProdutoId"]), q["Nome"].ToString(),
                                    Convert.ToDecimal(q["Valor"]), q["Observacao"].ToString(),
                                    Convert.ToBoolean(q["Apagado"]))
                            }
                        }
                    });
        }


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}