using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetFormasPagtoInteractor
    {
        private readonly FormaPagamentoRepository _FormaPagamentoRepository;

        public GetFormasPagtoInteractor(FormaPagamentoRepository formaPagamentoRepository)
        {
            _FormaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<IEnumerable<FormaPagamento>> GetContasAsync()
        {
            return (await _FormaPagamentoRepository
                .GetAllAsync())
                .OrderBy(a => a.Descricao)
                .ToList();
        }

        public async Task<FormaPagamento> GetContaAsync(Guid id)
        {
            return await _FormaPagamentoRepository.GetByIdAsync(id);
        }
    }
}
