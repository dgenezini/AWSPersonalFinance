using System;

namespace PersonalFinance.Domain.Entities
{
    public class FormaPagamento
    {
        public Guid FormaPagamentoId { get; set; }
        public string Descricao { get; set; }
        public string FinalCartao { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaPagamento { get; set; }

        public string PK
        {
            get
            {
                return FormaPagamentoId.ToString();
            }
        }

        public string SK
        {
            get
            {
                return FinalCartao;
            }
        }
    }
}
