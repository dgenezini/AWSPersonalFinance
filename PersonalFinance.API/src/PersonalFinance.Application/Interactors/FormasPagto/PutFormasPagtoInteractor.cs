using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PutFormasPagtoInteractor
    {
        private readonly FormaPagamentoRepository _FormaPagamentoRepository;

        public PutFormasPagtoInteractor(FormaPagamentoRepository formaPagamentoRepository)
        {
            _FormaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<bool> UpdateContaAsync(FormaPagamento formaPagamento)
        {
            //var ContaDB = await _ContaRepository
            //    .GetByIdAsync(conta.ContaId);

            //Mapper.Map(conta, ContaDB);

            return await _FormaPagamentoRepository.UpdateAsync(formaPagamento);
        }
    }
}
