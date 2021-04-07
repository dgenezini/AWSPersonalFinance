using System;

namespace PersonalFinance.Domain.Entities
{
    public class Categoria
    {
        public Guid CategoriaId { get; set; }
        public string Descricao { get; set; }
        public bool IsTransferencia { get; set; }

        public string PK
        {
            get
            {
                return $"{TipoEntidade}#{CategoriaId}";
            }
        }

        public string SK
        {
            get
            {
                return $"{TipoEntidade}#{CategoriaId}";
            }
        }

        public string TipoEntidade
        {
            get
            {
                return $"Categoria";
            }
        }

        //public virtual List<Transacao> Transacoes { get; set; }

        //public double GetValorMensal(IQueryable<Transacao> transactions, DateTime Periodo, bool UsaDataPagamento)
        //{
        //    DateTime DataFinal = Periodo.AddMonths(1).AddDays(-1);

        //    var TransacoesPeriodoQ = transactions;

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Periodo)
        //                                    .Where(a => a.DataPagamento <= DataFinal);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Periodo)
        //                                    .Where(a => a.Data <= DataFinal);
        //    }

        //    var TransacoesPeriodo = TransacoesPeriodoQ
        //                                .Where(a => a.CategoriaId == CategoriaId)
        //                                .Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}

        //public double GetPrevistoMensal(IQueryable<Previsto> budgets, DateTime Periodo)
        //{
        //    var Mes = Periodo.Month;
        //    var Ano = Periodo.Year;

        //    var previstoPeriodo = budgets
        //                                .Where(a => a.CategoriaId == CategoriaId)
        //                                .Where(a => a.Mes == Mes)
        //                                .Where(a => a.Ano == Ano)
        //                                .SingleOrDefault()?.Valor;

        //    if (!previstoPeriodo.HasValue)
        //    {
        //        previstoPeriodo = budgets
        //                                .Where(a => a.CategoriaId == CategoriaId)
        //                                .Where(a => a.Mes == Mes)
        //                                .Where(a => a.Ano == null)
        //                                .SingleOrDefault()?.Valor;
        //    }

        //    if (!previstoPeriodo.HasValue)
        //    {
        //        previstoPeriodo = budgets
        //                                .Where(a => a.CategoriaId == CategoriaId)
        //                                .Where(a => a.Mes == null)
        //                                .Where(a => a.Ano == Ano)
        //                                .SingleOrDefault()?.Valor;
        //    }

        //    if (!previstoPeriodo.HasValue)
        //    {
        //        previstoPeriodo = budgets
        //                                .Where(a => a.CategoriaId == CategoriaId)
        //                                .Where(a => a.Mes == null)
        //                                .Where(a => a.Ano == null)
        //                                .SingleOrDefault()?.Valor;
        //    }

        //    if (!previstoPeriodo.HasValue)
        //    {
        //        previstoPeriodo = 0;
        //    }

        //    return Math.Round(previstoPeriodo.Value, 2);
        //}

        //public static double GetValorMensalSemCategoria(IQueryable<Transacao> transactions, DateTime Periodo, bool UsaDataPagamento)
        //{
        //    DateTime DataFinal = Periodo.AddMonths(1).AddDays(-1);

        //    var TransacoesPeriodoQ = transactions;

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Periodo)
        //                                    .Where(a => a.DataPagamento <= DataFinal);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Periodo)
        //                                    .Where(a => a.Data <= DataFinal);
        //    }

        //    var TransacoesPeriodo = TransacoesPeriodoQ
        //                                .Where(a => a.CategoriaId.HasValue == false)
        //                                .Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}

        //public static double GetTotalMensal(IQueryable<Transacao> transactions, DateTime Periodo, bool UsaDataPagamento)
        //{
        //    DateTime DataFinal = Periodo.AddMonths(1).AddDays(-1);

        //    var TransacoesPeriodoQ = transactions
        //        .Include(a => a.Categoria)
        //        .Where(a => a.Categoria.IsTransferencia == false);

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Periodo)
        //                                    .Where(a => a.DataPagamento <= DataFinal);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Periodo)
        //                                    .Where(a => a.Data <= DataFinal);
        //    }

        //    /*TransacoesPeriodoQ = TransacoesPeriodoQ
        //        .Where(a => a.Categoria.IsTransferencia == false);*/

        //    var TransacoesPeriodo = TransacoesPeriodoQ
        //                                .Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}

        //public static double GetTotalGeral(IQueryable<Transacao> transactions, DateTime Inicial, DateTime Final, bool UsaDataPagamento)
        //{
        //    DateTime DataFinal = Final.AddMonths(1).AddDays(-1);

        //    var TransacoesPeriodoQ = transactions
        //        .Where(a => a.Categoria.IsTransferencia == false);

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Inicial)
        //                                    .Where(a => a.DataPagamento <= Final);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Inicial)
        //                                    .Where(a => a.Data <= Final);
        //    }

        //    /*TransacoesPeriodoQ = TransacoesPeriodoQ
        //        .Where(a => a.Categoria.IsTransferencia == false);*/

        //    var TransacoesPeriodo = TransacoesPeriodoQ.Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}

        //public double GetTotal(DateTime Inicial, DateTime Final, bool UsaDataPagamento)
        //{
        //    var TransacoesPeriodoQ = Transacoes.Where(a => a.Categoria.IsTransferencia == false);

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Inicial)
        //                                    .Where(a => a.DataPagamento <= Final);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Inicial)
        //                                    .Where(a => a.Data <= Final);
        //    }

        //    var TransacoesPeriodo = TransacoesPeriodoQ
        //                                .Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}

        //public static double GetTotalSemCategoria(IQueryable<Transacao> transactions, DateTime Inicial, DateTime Final, bool UsaDataPagamento)
        //{
        //    var TransacoesPeriodoQ = transactions
        //        .Where(a => a.CategoriaId.HasValue == false)
        //        .AsQueryable();

        //    if (UsaDataPagamento)
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.DataPagamento >= Inicial)
        //                                    .Where(a => a.DataPagamento <= Final);
        //    }
        //    else
        //    {
        //        TransacoesPeriodoQ = TransacoesPeriodoQ
        //                                    .Where(a => a.Data >= Inicial)
        //                                    .Where(a => a.Data <= Final);
        //    }

        //    var TransacoesPeriodo = TransacoesPeriodoQ
        //                                .Sum(a => a.ValorLocal);

        //    return Math.Round(TransacoesPeriodo, 2);
        //}
    }
}
