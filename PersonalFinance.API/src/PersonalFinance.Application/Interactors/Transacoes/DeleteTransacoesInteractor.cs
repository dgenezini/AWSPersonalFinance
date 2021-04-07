using PersonalFinance.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class DeleteTransacoesInteractor
    {
        private readonly TransacaoRepository _TransacaoRepository;

        public DeleteTransacoesInteractor(TransacaoRepository transacaoRepository)
        {
            _TransacaoRepository = transacaoRepository;
        }

        public async Task<bool> DeleteTransacaoAsync(Guid transacaoId, string sortKey)
        {
            return await _TransacaoRepository.DeleteAsync(transacaoId.ToString(), sortKey);
        }
    }
}
