using PersonalFinance.Application.Interactors;
using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Services
{
    public class ImportadorServices
    {
        private readonly TransacaoRepository _TransacaoRepository;
        private readonly ContaRepository _ContaRepository;
        private readonly TransacaoLocalRepository _TransacaoLocalRepository;
        private readonly PostTransacoesInteractor _PostTransacoesInteractor;

        public ImportadorServices(TransacaoRepository transacaoRepository,
            ContaRepository contaRepository,
            TransacaoLocalRepository transacaoLocalRepository,
            PostTransacoesInteractor postTransacoesInteractor)
        {
            _TransacaoRepository = transacaoRepository;
            _ContaRepository = contaRepository;
            _TransacaoLocalRepository = transacaoLocalRepository;
            _PostTransacoesInteractor = postTransacoesInteractor;
        }

        public async Task<int> ImportarTransacoesAsync(IEnumerable<Transacao> transacoes)
        {
            foreach (var transacao in transacoes)
            {
                //Remove Numbers
                transacao.LocalPagto = Regex.Replace(transacao.LocalPagto, @"[\d-]", string.Empty);

                var existe = (await _TransacaoRepository
                    .GetByPeriodoAsync(transacao.DataHora.Year, transacao.DataHora.Month))
                    .Any(a => a.DataHora == transacao.DataHora &&
                              a.LocalPagto == transacao.LocalPagto &&
                              a.ValorOriginal == transacao.Valor);

                if (!existe)
                {
                    var TransacaoLocal = await _TransacaoLocalRepository
                        .GetByKeyAsync(transacao.LocalPagto);

                    if (TransacaoLocal != null)
                    {
                        if (!transacao.CategoriaId.HasValue)
                        {
                            transacao.CategoriaId = TransacaoLocal.CategoriaId;
                        }

                        transacao.Descricao = TransacaoLocal.Descricao;
                    }

                    transacao.ValorOriginal = transacao.Valor;

                    if (transacao.ContaOrigemId.HasValue)
                    {
                        transacao.ContaOrigem = await _ContaRepository
                            .GetByIdAsync(transacao.ContaOrigemId.Value);
                    }

                    transacao.BeforeUpdate();

                    await _PostTransacoesInteractor.AddTransacaoAsync(transacao);
                }
            }

            return transacoes.Count();
        }
    }
}
