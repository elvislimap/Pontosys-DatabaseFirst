using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using DatabaseFirst.WebApi.Infra.Data.LocalDb.Repositories;
using DatabaseFirst.WebApi.Models.Entities;

namespace DatabaseFirst.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [HttpGet("ObterTodos")]
        public ICollection<Produto> ObterTodos()
        {
            return _produtoRepository.ObterTodos();
        }

        [HttpPost("Adicionar")]
        public Produto Adicionar([FromBody] Produto produto)
        {
            _produtoRepository.Adicionar(produto);

            return produto;
        }

        [HttpPut("AtualizarValor/{produtoId}")]
        public bool AtualizarValor(int produtoId, [FromBody] decimal valor)
        {
            var produto = _produtoRepository.ObterPorId(produtoId);
            produto.ModificarValor(valor);

            _produtoRepository.Atualizar(produto);

            return true;
        }

        [HttpDelete("Remover/{produtoId}")]
        public ObjectResult Remover(int produtoId)
        {
            var produto = _produtoRepository.ObterPorId(produtoId);
            if (produto == null)
            {
                return new ObjectResult("Produto não encontrado")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            _produtoRepository.Remover(produtoId);

            return new ObjectResult("Ok");
        }
    }
}