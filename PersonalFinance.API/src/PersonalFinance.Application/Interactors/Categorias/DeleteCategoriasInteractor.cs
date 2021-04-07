using PersonalFinance.Application.Repositories;
using System;
using System.Threading.Tasks;

namespace PersonalFinance.Application.Interactors
{
    public class DeleteCategoriasInteractor
    {
        private readonly CategoriaRepository _CategoriaRepository;

        public DeleteCategoriasInteractor(CategoriaRepository categoriaRepository)
        {
            _CategoriaRepository = categoriaRepository;
        }

        public async Task<bool> DeleteCategoriaAsync(Guid categoriaId)
        {
            return await _CategoriaRepository.DeleteAsync(categoriaId.ToString());
        }
    }
}
