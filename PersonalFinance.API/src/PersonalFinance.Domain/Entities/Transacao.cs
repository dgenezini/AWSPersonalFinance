using System;

namespace PersonalFinance.Domain.Entities
{
    public class Transacao
    {
        public Guid TransacaoId { get; set; }
        public Guid? ContaOrigemId { get; set; }
        public Guid? ContaDestinoId { get; set; }
        public DateTime DataHora { get; set; }
        public float Valor { get; set; }
        public float ValorOriginal { get; set; }
        public string Descricao { get; set; }
        public string LocalPagto { get; set; }
        public Guid? CategoriaId { get; set; }
        public string Moeda { get; set; }
        public float Cotacao { get; set; }
        public string Observacao { get; set; }
        public float ValorLocal { get; set; }
        public DateTime DataPagamento { get; set; }

        public string PK
        {
            get
            {
                return $"{TipoEntidade}#{TransacaoId}";
            }
        }

        public string SK
        {
            get
            {
                return $"{DataHora:yyyy-MM}";
            }
        }

        public string TipoEntidade
        {
            get
            {
                return $"Transacao";
            }
        }

        public virtual Categoria Categoria { get; set; }
        public virtual Conta ContaOrigem { get; set; }
        public virtual Conta ContaDestino { get; set; }

        private void SetDataPagamento()
        {
            if (ContaOrigemId.HasValue)
            {
                if (ContaOrigem.DiaFechamento.HasValue && ContaOrigem.DiaPagamento.HasValue)
                {
                    var DataPagto = new DateTime(DataHora.Year, DataHora.Month, ContaOrigem.DiaPagamento.Value);

                    if (DataHora.Day >= ContaOrigem.DiaFechamento.Value)
                    {
                        DataPagamento = DataPagto.AddMonths(2);
                    }
                    else
                    {
                        DataPagamento = DataPagto.AddMonths(1);
                    }
                }
                else
                {
                    DataPagamento = DataHora;
                }
            }
            else
            {
                DataPagamento = DataHora;
            }
        }

        private void SetValorLocal()
        {
            if (Moeda == "R$")
            {
                ValorLocal = Valor;
            }
            else
            {
                ValorLocal = Valor * Cotacao;
            }
        }

        public void BeforeUpdate()
        {
            if (ValorOriginal == 0)
            {
                ValorOriginal = Valor;
            }

            SetValorLocal();
            //SetDataPagamento();
        }
    }
}
