using System.Collections.Generic;

namespace DatabaseFirst.WebApi.Models.Entities
{
    public class Produto
    {
        public Produto(int produtoId, string nome, decimal valor, string observacao, bool apagado)
        {
            ProdutoId = produtoId;
            Nome = nome;
            Valor = valor;
            Observacao = observacao;
            Apagado = apagado;
        }

        public int ProdutoId { get; private set; }
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public string Observacao { get; private set; }
        public bool Apagado { get; private set; }

        public virtual ICollection<VendaItem> VendaItens { get; set; }

        public void ModificarValor(decimal valor)
        {
            Valor = valor;
        }
    }
}