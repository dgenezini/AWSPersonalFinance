using PersonalFinance.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class DeleteFormasPagtoInteractor
    {
        private readonly FormaPagamentoRepository _FormaPagamentoRepository;

        public DeleteFormasPagtoInteractor(FormaPagamentoRepository formaPagamentoRepository)
        {
            _FormaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<bool> DeleteContaAsync(Guid formaPagamentoId)
        {
            return await _FormaPagamentoRepository.DeleteAsync(formaPagamentoId.ToString());
        }
    }
}
