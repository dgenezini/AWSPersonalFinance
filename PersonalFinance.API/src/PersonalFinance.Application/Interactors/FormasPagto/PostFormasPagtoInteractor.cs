using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PostFormasPagtoInteractor
    {
        private readonly FormaPagamentoRepository _FormaPagamentoRepository;

        public PostFormasPagtoInteractor(FormaPagamentoRepository formaPagamentoRepository)
        {
            _FormaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<bool> AddContaAsync(FormaPagamento formaPagamento)
        {
            //var ContaDB = Mapper.Map<Conta>(Conta);

            return await _FormaPagamentoRepository.AddAsync(formaPagamento);
        }
    }
}
