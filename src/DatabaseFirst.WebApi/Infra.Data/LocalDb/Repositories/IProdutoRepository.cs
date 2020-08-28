using System.Collections.Generic;
using System;
using DatabaseFirst.WebApi.Models.Entities;

namespace DatabaseFirst.WebApi.Infra.Data.LocalDb.Repositories
{
    public interface IProdutoRepository : IDisposable
    {
        ICollection<Produto> ObterTodos();
        void Adicionar(Produto produto);
        void Atualizar(Produto produto);
        void Remover(int produtoId);
        Produto ObterPorId(int produtoId);
    }
}