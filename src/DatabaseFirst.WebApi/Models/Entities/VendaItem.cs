namespace DatabaseFirst.WebApi.Models.Entities
{
    public class VendaItem
    {
        public VendaItem(int vendaId, int produtoId, int quantidade, decimal valorTotal)
        {
            VendaId = vendaId;
            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorTotal = valorTotal;
        }

        public int VendaId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal ValorTotal { get; private set; }

        public virtual Venda Venda { get; set; }
        public virtual Produto Produto { get; set; }
    }
}