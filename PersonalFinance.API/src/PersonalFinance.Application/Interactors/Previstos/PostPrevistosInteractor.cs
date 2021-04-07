using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class PostPrevistosInteractor
    {
        private readonly PrevistoRepository _PrevistoRepository;

        public PostPrevistosInteractor(PrevistoRepository previstoRepository)
        {
            _PrevistoRepository = previstoRepository;
        }

        public async Task<bool> AddContaAsync(Previsto previsto)
        {
            //var ContaDB = Mapper.Map<Conta>(Conta);

            return await _PrevistoRepository.AddAsync(previsto);
        }
    }
}
