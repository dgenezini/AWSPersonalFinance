using System;

namespace PersonalFinance.Domain.Entities
{
    public class Conta
    {
        public Guid ContaId { get; set; }
        public string Descricao { get; set; }
        public Guid? ContaPaiId { get; set; }
        public string FinalCartao { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaPagamento { get; set; }
        public double SaldoAtual { get; set; }

        public string PK
        {
            get
            {
                return ContaId.ToString();
            }
        }

        public string SK
        {
            get
            {
                return FinalCartao;
            }
        }

        //public virtual List<Transacao> TransacoesOrigem { get; set; }
        //public virtual List<Transacao> TransacoesDestino { get; set; }
    }
}
