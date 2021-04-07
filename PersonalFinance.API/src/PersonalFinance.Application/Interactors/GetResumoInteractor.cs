using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using PersonalFinance.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetResumoInteractor
    {
        private readonly CategoriaRepository _CategoriaRepository;
        private readonly TransacaoRepository _TransacaoRepository;
        private readonly PrevistoRepository _PrevistoRepository;

        public GetResumoInteractor(CategoriaRepository categoriaRepository,
            TransacaoRepository transacaoRepository,
            PrevistoRepository previstoRepository)
        {
            _CategoriaRepository = categoriaRepository;
            _TransacaoRepository = transacaoRepository;
            _PrevistoRepository = previstoRepository;
        }

        public async Task<ResumoMensal> GetResumoAsync(
            DateTime? data = null, bool isDataPagamento = false)
        {
            var ResumoMensal = new ResumoMensal();

            if (data == null)
            {
                data = DateTime.Today;
            }

            var TodasCategorias = (await _CategoriaRepository
                .GetAllAsync())
                .OrderBy(a => a.Descricao)
                .ToList();

            ResumoMensal.Categorias = TodasCategorias
                .Where(a => !a.IsTransferencia)
                .OrderBy(a => a.Descricao)
                .ToList();

            var CategoriasTransferencia = TodasCategorias
                .Where(a => a.IsTransferencia)
                .ToList();

            ResumoMensal.Periodos = new List<DateTime>();
            DateTime MesCorrente = new DateTime(data.Value.Year, data.Value.Month, 1);

            for (int I = 10; I > 0; I--)
            {
                ResumoMensal.Periodos.Add(MesCorrente.AddMonths(I * -1));
            }

            for (int I = 0; I < 2; I++)
            {
                ResumoMensal.Periodos.Add(MesCorrente.AddMonths(I));
            }

            var Transacoes = (await _TransacaoRepository
                .GetByPeriodoAsync(ResumoMensal.Periodos.First(), ResumoMensal.Periodos.Last()))
                .Where(a => !CategoriasTransferencia.Any(c => c.CategoriaId == a.CategoriaId))
                .OrderBy(a => a.DataHora)
                .ToList();

            ResumoMensal.CategoriasPeriodos = Transacoes
                .GroupBy(a => new { a.CategoriaId, Periodo = a.DataHora.ToString("yyyy-MM") })
                .Select(a => new CategoriaPeriodo()
                {
                    CategoriaId = a.Key.CategoriaId,
                    Periodo = DateTime.Parse(a.Key.Periodo + "-01"),
                    Valor = a.Sum(s => s.Valor)
                })
                .OrderBy(a => a.Periodo)
                .ToList();

            ResumoMensal.Previstos = new List<Previsto>();

            foreach (var Periodo in ResumoMensal.Periodos)
            {
                ResumoMensal.Previstos.AddRange(await _PrevistoRepository
                    .GetByPeriodoAsync(Periodo.Year, Periodo.Month));
            }

            return ResumoMensal;
        }
    }
}
