using PersonalFinance.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class DeletePrevistosInteractor
    {
        private readonly PrevistoRepository _PrevistoRepository;

        public DeletePrevistosInteractor(PrevistoRepository previstoRepository)
        {
            _PrevistoRepository = previstoRepository;
        }

        public async Task<bool> DeleteContaAsync(Guid previstoId)
        {
            var Previsto = await _PrevistoRepository.GetByIdAsync(previstoId);

            return await _PrevistoRepository.DeleteAsync(Previsto);
        }
    }
}
