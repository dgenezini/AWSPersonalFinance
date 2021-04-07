using System;

namespace PersonalFinance.Domain.Entities
{
    public class Investimento
    {
        public Guid InvestimentoId { get; set; }
        public string Banco { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInvestimento { get; set; }
        public DateTime DataVencimento { get; set; }
        public double ValorInvestido { get; set; }
        public double ValorBrutoAtual { get; set; }
        public string Categoria { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool Encerrado { get; set; }

        public string PK
        {
            get
            {
                return "Investimento#" + InvestimentoId;
            }
        }

        public string SK
        {
            get
            {
                return "Investimento";
            }
        }

        public double ValorImpostosAtual
        {
            get
            {
                return Math.Round((ValorBrutoAtual - ValorInvestido) * IRAtual / 100, 2);
            }
        }

        public double ValorLiquido
        {
            get
            {
                return ValorBrutoAtual - ValorImpostosAtual;
            }
        }

        public double Rendimento
        {
            get
            {
                return Math.Round((ValorLiquido / ValorInvestido - 1) * 100, 2);
            }
        }

        public int DiasCorridos
        {
            get
            {
                return Convert.ToInt32(Math.Truncate((DataAtualizacao - DataInvestimento).TotalDays));
            }
        }

        public double IRAtual
        {
            get
            {
                if (DiasCorridos > 720)
                {
                    return 15;
                }
                else if (DiasCorridos > 360)
                {
                    return 17.5;
                }
                else if (DiasCorridos > 180)
                {
                    return 20;
                }
                else
                {
                    return 22.5;
                }
            }
        }
    }
}
