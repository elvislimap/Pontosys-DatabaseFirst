using System;
using System.Collections.Generic;
using System.Linq;
using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ContextEf _context;

        public ProdutoRepository(ContextEf context)
        {
            _context = context;
        }

        public ICollection<Produto> ObterTodos()
        {
            return _context.Produtos.ToList();
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();
        }

        public void Remover(int produtoId)
        {
            _context.Produtos.Remove(new Produto(produtoId, null, 0, null, false));
            _context.SaveChanges();
        }

        public Produto ObterPorId(int produtoId)
        {
            return _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == produtoId);
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}