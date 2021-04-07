using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PostTransacoesInteractor
    {
        private readonly TransacaoRepository _TransacaoRepository;
        private readonly TransacaoLocalRepository _TransacaoLocalRepository;

        public PostTransacoesInteractor(TransacaoRepository transacaoRepository,
            TransacaoLocalRepository transacaoLocalRepository)
        {
            _TransacaoRepository = transacaoRepository;
            _TransacaoLocalRepository = transacaoLocalRepository;
        }

        public async Task<bool> AddTransacaoAsync(Transacao transacao)
        {
            var success = await _TransacaoRepository.AddAsync(transacao);

            if (success)
            {
                success = await _TransacaoLocalRepository.UpdateAsync(new TransacaoLocal()
                {
                    LocalPagto = transacao.LocalPagto,
                    Descricao = transacao.Descricao,
                    CategoriaId = transacao.ContaDestinoId,
                    DataHora = transacao.DataHora
                });

                return success;
            }

            return false;
        }
    }
}
