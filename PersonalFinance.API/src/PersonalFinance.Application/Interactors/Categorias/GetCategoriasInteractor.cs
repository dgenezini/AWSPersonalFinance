using PersonalFinance.Application.Repositories;
using PersonalFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class GetCategoriasInteractor
    {
        private readonly CategoriaRepository _CategoriaRepository;

        public GetCategoriasInteractor(CategoriaRepository categoriaRepository)
        {
            _CategoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return (await _CategoriaRepository
                .GetAllAsync())
                .OrderBy(a => a.Descricao)
                .ToList();
        }

        public async Task<Categoria> GetCategoriaAsync(Guid id)
        {
            return await _CategoriaRepository.GetByIdAsync(id);
        }
    }
}
