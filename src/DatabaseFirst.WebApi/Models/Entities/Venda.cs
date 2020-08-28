using System;
using System.Collections.Generic;

namespace DatabaseFirst.WebApi.Models.Entities
{
    public class Venda
    {
        public Venda(int vendaId, decimal valor, DateTime dataHora)
        {
            VendaId = vendaId;
            Valor = valor;
            DataHora = dataHora;
        }

        public int VendaId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataHora { get; private set; }

        public virtual ICollection<VendaItem> VendaItens { get; set; }

        public void ObterDataHoraAtual()
        {
            DataHora = DateTime.Now;
        }
    }
}