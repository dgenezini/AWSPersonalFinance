using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PutContasInteractor
    {
        private readonly ContaRepository _ContaRepository;

        public PutContasInteractor(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<bool> UpdateContaAsync(Conta conta)
        {
            //var ContaDB = await _ContaRepository
            //    .GetByIdAsync(conta.ContaId);

            //Mapper.Map(conta, ContaDB);

            return await _ContaRepository.UpdateAsync(conta);
        }
    }
}
