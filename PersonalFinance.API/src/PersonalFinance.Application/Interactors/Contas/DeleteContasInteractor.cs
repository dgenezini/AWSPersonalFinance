using PersonalFinance.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class DeleteContasInteractor
    {
        private readonly ContaRepository _ContaRepository;

        public DeleteContasInteractor(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<bool> DeleteContaAsync(Guid contaId)
        {
            return await _ContaRepository.DeleteAsync(contaId.ToString());
        }
    }
}
