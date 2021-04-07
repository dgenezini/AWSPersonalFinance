using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetTransacoesInteractor
    {
        private readonly TransacaoRepository _TransacaoRepository;
        private readonly ContaRepository _ContaRepository;
        private readonly CategoriaRepository _CategoriaRepository;

        public GetTransacoesInteractor(TransacaoRepository transacaoRepository,
            ContaRepository contaRepository, CategoriaRepository categoriaRepository)
        {
            _TransacaoRepository = transacaoRepository;
            _ContaRepository = contaRepository;
            _CategoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Transacao>> GetContasAsync(
            Guid? categoriaId, DateTime? periodo, bool dataPagamento, bool isSemCategoria,
            string filtro)
        {
            if (!periodo.HasValue)
            {
                periodo = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            }

            var transacoesQ = await _TransacaoRepository
                .GetByPeriodoAsync(periodo.Value.Year, periodo.Value.Month);

            if (!string.IsNullOrEmpty(filtro))
            {
                string SearchValue = filtro.ToLower();

                transacoesQ = transacoesQ
                    .Where(a => a.Descricao.ToLower().Contains(SearchValue) ||
                                a.LocalPagto.ToLower().Contains(SearchValue) ||
                                //a.ValorLocal.ToString().Contains(SearchValue) ||
                                (a.CategoriaId != null && a.Categoria.Descricao.ToLower().Contains(SearchValue)) ||
                                (a.ContaOrigemId != null && a.ContaOrigem.Descricao.ToLower().Contains(SearchValue)) ||
                                (a.ContaDestinoId != null && a.ContaDestino.Descricao.ToLower().Contains(SearchValue)));
            }

            if (isSemCategoria)
            {
                transacoesQ = transacoesQ
                    .Where(a => !a.CategoriaId.HasValue);
            }
            else if (categoriaId.HasValue)
            {
                transacoesQ = transacoesQ
                    .Where(a => a.CategoriaId == categoriaId);
            }

            //if (Periodo.HasValue)
            //{
            //    DateTime DataFinal = parametros.Periodo.Value.AddMonths(1).AddDays(-1);

            //    if (!parametros.DataPagamento)
            //    {
            //        transacoesQ = transacoesQ
            //            .Where(a => a.Data >= parametros.Periodo.Value && a.Data <= DataFinal);
            //    }
            //    else
            //    {
            //        transacoesQ = transacoesQ
            //            .Where(a => a.DataPagamento >= parametros.Periodo.Value && a.DataPagamento <= DataFinal);
            //    }
            //}

            var Transacoes = transacoesQ
                    .OrderByDescending(a => a.DataHora)
                    .ThenBy(a => a.ValorLocal)
                    .ThenBy(a => a.LocalPagto)
                    //.Skip(parametros.Start)
                    //.Take(parametros.Length)
                    .ToList();

            var Contas = await _ContaRepository.GetAllAsync();
            var Categorias = await _CategoriaRepository.GetAllAsync();

            foreach (var Transacao in Transacoes)
            {
                Transacao.Categoria = Categorias
                    .SingleOrDefault(a => a.CategoriaId == Transacao.CategoriaId);
                Transacao.ContaOrigem = Contas
                    .SingleOrDefault(a => a.ContaId == Transacao.ContaOrigemId);
                Transacao.ContaDestino = Contas
                    .SingleOrDefault(a => a.ContaId == Transacao.ContaDestinoId);
            }

            //response.data = Mapper.Map<List<TransacaoViewModel>>(Transacoes);

            return Transacoes;
        }

        public async Task<Transacao> GetTransacaoAsync(Guid id)
        {
            return await _TransacaoRepository.GetByIdAsync(id);
        }
    }
}
