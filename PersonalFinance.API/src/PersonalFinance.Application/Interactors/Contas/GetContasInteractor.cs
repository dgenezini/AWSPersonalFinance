using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetContasInteractor
    {
        private readonly ContaRepository _ContaRepository;

        public GetContasInteractor(ContaRepository contaRepository)
        {
            _ContaRepository = contaRepository;
        }

        public async Task<IEnumerable<Conta>> GetContasAsync()
        {
            return (await _ContaRepository
                .GetAllAsync())
                .OrderBy(a => a.Descricao)
                .ToList();
        }

        public async Task<Conta> GetContaAsync(Guid id)
        {
            return await _ContaRepository.GetByIdAsync(id);
        }
    }
}
