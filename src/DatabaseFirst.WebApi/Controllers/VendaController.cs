using System.Net;
using System.Linq;
using DatabaseFirst.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using DatabaseFirst.WebApi.Infra.Data.LocalDb.Repositories;
using System.Collections.Generic;

namespace DatabaseFirst.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _vendaRepository;

        public VendaController(IVendaRepository vendaRepository)
        {
            _vendaRepository = vendaRepository;
        }

        [HttpPost("Adicionar")]
        public ObjectResult Adicionar([FromBody] Venda venda)
        {
            if (!ValidarAdicionar(venda))
            {
                return new ObjectResult("Venda inválida")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            };

            venda.ObterDataHoraAtual();
            _vendaRepository.Adicionar(venda);

            return new ObjectResult(venda);
        }

        [HttpGet("ObterTodos")]
        public ICollection<Venda> ObterTodos()
        {
            return _vendaRepository.ObterTodos();
        }

        [HttpGet("ObterPorId/{vendaId}")]
        public Venda ObterPorId(int vendaId)
        {
            return _vendaRepository.ObterPorId(vendaId);
        }

        [HttpGet("ObterPorProdutoId/{produtoId}")]
        public ICollection<Venda> ObterPorProdutoId(int produtoId)
        {
            return _vendaRepository.ObterPorProdutoId(produtoId);
        }

        [HttpGet("ObterPorProdutoIdProcedure/{produtoId}")]
        public ICollection<Venda> ObterPorProdutoIdProcedure(int produtoId)
        {
            return _vendaRepository
                .ObterPorProdutoIdProcedure(produtoId);
        }

        private bool ValidarAdicionar(Venda venda)
        {
            return venda.Valor > 0
                && venda.VendaItens != null
                && venda.VendaItens.Any()
                && venda.VendaItens.All(vi => vi.ProdutoId > 0)
                && venda.VendaItens.All(vi => vi.Quantidade > 0)
                && venda.VendaItens.All(vi => vi.ValorTotal > 0);
        }
    }
}