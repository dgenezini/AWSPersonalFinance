using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PutPrevistosInteractor
    {
        private readonly PrevistoRepository _PrevistoRepository;

        public PutPrevistosInteractor(PrevistoRepository previstoRepository)
        {
            _PrevistoRepository = previstoRepository;
        }

        public async Task<bool> UpdateContaAsync(Previsto previsto)
        {
            //var ContaDB = await _ContaRepository
            //    .GetByIdAsync(conta.ContaId);

            //Mapper.Map(conta, ContaDB);

            return await _PrevistoRepository.UpdateAsync(previsto);
        }
    }
}
