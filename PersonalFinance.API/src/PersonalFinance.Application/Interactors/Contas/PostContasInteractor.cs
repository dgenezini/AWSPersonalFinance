using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PostContasInteractor
    {
        private readonly ContaRepository _ContaRepository;

        public PostContasInteractor(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<bool> AddContaAsync(Conta conta)
        {
            //var ContaDB = Mapper.Map<Conta>(Conta);

            return await _ContaRepository.AddAsync(conta);
        }
    }
}
